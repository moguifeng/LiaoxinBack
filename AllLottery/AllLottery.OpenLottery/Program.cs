using AllLottery.Business.Cathectic;
using AllLottery.Business.Config;
using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Zzb.Common;
using Zzb.ZzbLog;

namespace AllLottery.OpenLottery
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = LotteryContext.CreateContext())
            {
                var bets = from b in context.Bets where b.Status == BetStatusEnum.Error select b;
                foreach (var bet in bets)
                {
                    bet.Status = BetStatusEnum.Wait;
                }

                context.SaveChanges();
            }

            while (true)
            {
                try
                {
                    using (var context = LotteryContext.CreateContext())
                    {
                        var bets = from b in context.Bets
                                   join d in context.LotteryDatas
                                on new
                                {
                                    b.LotteryPlayDetail.LotteryPlayType.LotteryTypeId,
                                    Number = b.LotteryIssuseNo

                                } equals new
                                {
                                    d.LotteryTypeId,
                                    d.Number
                                }
                                   where b.Status == BetStatusEnum.Wait && d != null && b.IsEnable && d.IsEnable
                                   select b;
                        if (bets.Any())
                        {
                            var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
                            var logList = new List<CoinLog>();
                            List<RebateLog> rebateLogs = new List<RebateLog>();
                            List<Message> messages = new List<Message>();
                            foreach (Bet bet in bets.Take(100).ToList())
                            {
                                var data = (from d in context.LotteryDatas
                                            where d.LotteryTypeId == bet.LotteryPlayDetail.LotteryPlayType.LotteryTypeId && d.Number == bet.LotteryIssuseNo
                                            select d).FirstOrDefault();
                                if (data == null)
                                {
                                    Console.WriteLine(
                                        $"投注还没开奖，出现不应该有的BUG，彩种[{bet.LotteryPlayDetail.LotteryPlayType.LotteryType.Name}],期号[{bet.LotteryIssuseNo}]，投注订单[{bet.BetId}]");
                                    continue;
                                }

                                int[] numbers = null;
                                try
                                {
                                    numbers = BaseCathectic.ConvertoNumbers(data.Data);
                                }
                                catch (Exception)
                                {
                                    throw new Exception($"开奖数据错误，采种[{data.LotteryTypeId}],期号[{data.Number}],数据[{data.Data}]");

                                }

                                string reflectClass = bet.LotteryPlayDetail.ReflectClass;
                                long winCount = 0;
                                try
                                {
                                    winCount = BaseCathectic.CreateCathectic(reflectClass)
                                        .IsWin(bet.BetNo, numbers);
                                }
                                catch (Exception e)
                                {
                                    bet.Status = BetStatusEnum.Error;
                                    LogHelper.Error($"开奖算法出错。反射玩法[{bet.LotteryPlayDetail.ReflectClass}]，IsWin出错，参数[{bet.BetNo}],[{numbers.ToLogString()}]", e);
                                    continue;
                                }

                                bet.LotteryDataId = data.LotteryDataId;
                                bet.UpdateTime = DateTime.Now;
                                bet.Status = winCount > 0
                                    ? BetStatusEnum.Win
                                    : BetStatusEnum.Lose;
                                bet.Player.UpdateLastBetMoney(-bet.BetMoney);
                                bet.Player.UpdateReportDate();
                                bet.Player.BetMoney += bet.BetMoney;
                                bet.LotteryPlayDetail.LotteryPlayType.LotteryType.UpdateTime = DateTime.Now;
                                if (!ids.Contains(bet.PlayerId) && bet.Player.Type != PlayerTypeEnum.TestPlay)
                                {
                                    bet.LotteryPlayDetail.LotteryPlayType.LotteryType.UpdateReportDate();
                                    bet.LotteryPlayDetail.LotteryPlayType.LotteryType.BetMoney += bet.BetMoney;
                                }

                                var maxRate = BaseConfig.CreateInstance(SystemConfigEnum.MaxRebate).DecimalValue;

                                if (winCount > 0)
                                {
                                    bet.WinBetCount = winCount;
                                    bet.WinMoney = BaseCathectic.CreateCathectic(reflectClass).CalculateWinMoney(bet, maxRate, numbers);
                                    bet.Player.Coin += bet.WinMoney;
                                    bet.Player.UpdateReportDate();
                                    bet.Player.WinMoney += bet.WinMoney;
                                    if (!ids.Contains(bet.PlayerId) && bet.Player.Type != PlayerTypeEnum.TestPlay)
                                    {
                                        bet.LotteryPlayDetail.LotteryPlayType.LotteryType.UpdateReportDate();
                                        bet.LotteryPlayDetail.LotteryPlayType.LotteryType.WinMoney += bet.WinMoney;
                                    }

                                    CoinLog log = new CoinLog();
                                    log.Type = CoinLogTypeEnum.Win;
                                    log.AboutId = bet.BetId;
                                    log.Coin = bet.Player.Coin;
                                    log.FlowCoin = bet.WinMoney;
                                    log.FCoin = bet.Player.FCoin;
                                    log.PlayerId = bet.PlayerId;
                                    log.Remark = $"投注[{bet.LotteryPlayDetail.LotteryPlayType.LotteryType.Name}][{bet.LotteryIssuseNo}]期中奖，订单号[{bet.Order}]";
                                    logList.Add(log);

                                    messages.Add(new Message(bet.PlayerId, MessageInfoTypeEnum.Player, MessageTypeEnum.Message, $"恭喜你,你投注的[{bet.Order}],期号[{bet.LotteryIssuseNo}],彩种[{bet.LotteryPlayDetail.LotteryPlayType.LotteryType.Name}]已中奖.奖金是:{bet.WinMoney:#0.0000}元") { Money = bet.WinMoney });

                                    var detail = (from d in context.ChasingOrderDetails where d.BetId == bet.BetId select d)
                                        .FirstOrDefault();

                                    if (detail != null && detail.ChasingOrder.IsWinStop)
                                    {
                                        if (detail.ChasingOrder.Status == ChasingStatus.Doing || detail.ChasingOrder.Status == ChasingStatus.Wait)
                                        {
                                            detail.ChasingOrder.Update();
                                            detail.ChasingOrder.Status = ChasingStatus.End;

                                            var sql = from c in context.ChasingOrderDetails
                                                      where c.ChasingOrderId == detail.ChasingOrderId && c.IsEnable &&
                                                            c.BetId == null
                                                      select c.BetMoney;
                                            if (sql.Any())
                                            {
                                                var betMoney = sql.Sum();
                                                bet.Player.Coin += betMoney;
                                                bet.Player.FCoin -= betMoney;
                                                logList.Add(new CoinLog(bet.PlayerId, betMoney, bet.Player.Coin, -betMoney, bet.Player.FCoin, CoinLogTypeEnum.ChasingBetCancle, detail.ChasingOrderId, $"追号[{detail.ChasingOrder.Order}]完成"));
                                            }
                                        }
                                    }
                                }

                                bet.Player.CalculateRebate(bet, logList, rebateLogs);
                            }

                            if (logList.Count > 0)
                            {
                                context.CoinLogs.AddRange(logList);
                            }
                            if (rebateLogs.Count > 0)
                            {
                                context.RebateLogs.AddRange(rebateLogs);
                            }
                            if (messages.Count > 0)
                            {
                                context.Messages.AddRange(messages);
                            }

                            context.SaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine($"出错时间为[{DateTime.Now.ToCommonString()}]");
                }


                Thread.Sleep(1000);
            }

        }
    }
}
