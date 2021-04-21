using AllLottery.Business.Report;
using AllLottery.Business.Report.Team;
using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Zzb.Common;

namespace AllLottery.GrantDividend
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                try
                {
                    using (var context = LotteryContext.CreateContext())
                    {
                        var dividendDate =
                            (from d in context.DividendDates where d.IsEnable && !d.IsCal orderby d.SettleTime select d)
                            .FirstOrDefault();
                        if (dividendDate != null && dividendDate.SettleTime.AddDays(1).AddHours(3) < DateTime.Now)
                        {
                            //分红结束时间
                            DateTime end = dividendDate.SettleTime.AddDays(1);

                            //分红开始时间
                            DateTime begin = new DateTime(2019, 3, 1);
                            var beginDate = (from d in context.DividendDates where d.IsEnable && d.IsCal orderby d.SettleTime descending select d).FirstOrDefault();
                            if (beginDate != null)
                            {
                                begin = beginDate.SettleTime.AddDays(1);
                            }

                            //查找有分配分红比率的玩家
                            var players = from p in context.Players where p.DividendRate != null select p;
                            if (players.Any())
                            {
                                //查找系统分红设置
                                var dividendSettings = from d in context.DividendSettings
                                                       where d.IsEnable
                                                       orderby d.Rate descending
                                                       select d;
                                if (dividendSettings.Any())
                                {
                                    List<DividendLog> logs = new List<DividendLog>();
                                    List<CoinLog> coinLogs = new List<CoinLog>();
                                    //开始计算分红
                                    foreach (Player player in players)
                                    {
                                        //团队投注金额
                                        var betMoney = new TeamBetMoneyReport(player.PlayerId).GetReportData(begin, end);

                                        if (betMoney > 0)
                                        {
                                            //计算团队亏损金额
                                            var lostMoney =
                                                betMoney - new TeamWinMoneyReport(player.PlayerId).GetReportData(begin, end) - new TeamRebateReport(player.PlayerId).GetReportData(begin, end) - new TeamGiftMoneyReport(player.PlayerId).GetReportData(begin, end) - new TeamDailyWageReport(player.PlayerId).GetReportData(begin, end);

                                            //计算有效投注人数
                                            var list = BaseReport.GetTeamPlayerIdsWhitoutSelf(player.PlayerId);
                                            if (list == null)
                                            {
                                                list = new List<int>();
                                            }
                                            list.Add(player.PlayerId);
                                            var ids = context.Bets.Where(t =>
                                                    list.Contains(t.PlayerId) && t.BetTime >= begin && t.BetTime < end &&
                                                    (t.Status == BetStatusEnum.Win || t.Status == BetStatusEnum.Lose))
                                                .GroupBy(t => t.PlayerId);
                                            var betMen = ids.Count();

                                            //开始遍历分红配置
                                            foreach (var setting in dividendSettings)
                                            {
                                                //满足日工资条件
                                                if (setting.BetMoney <= betMoney && setting.MenCount <= betMen && setting.LostMoney <= lostMoney && lostMoney > 0)
                                                {
                                                    var rate = Math.Min(setting.Rate, player.DividendRate.Value);
                                                    var dividendMoney = rate * lostMoney;

                                                    DividendLog dividendLog = new DividendLog(rate, betMoney, lostMoney, betMen, dividendMoney, begin, dividendDate.DividendDateId, player.PlayerId);
                                                    logs.Add(dividendLog);

                                                    player.AddMoney(dividendMoney, CoinLogTypeEnum.ReceiveDividend, 0, out var coinLog, $"[{begin.ToDateString()}]到[{dividendDate.SettleTime.ToDateString()}]的分红");
                                                    coinLogs.Add(coinLog);

                                                    if (player.ParentPlayer != null)
                                                    {
                                                        player.ParentPlayer.AddMoney(-dividendMoney, CoinLogTypeEnum.GiveDividend, 0, out var giveLog, $"发放给[{player.Name }]的[{begin.ToDateString()}]到[{dividendDate.SettleTime.ToDateString()}]分红");
                                                        coinLogs.Add(giveLog);
                                                    }

                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    if (logs.Any())
                                    {
                                        context.DividendLogs.AddRange(logs);
                                        context.CoinLogs.AddRange(coinLogs);
                                    }
                                }
                            }

                            //保存数据库
                            dividendDate.IsCal = true;
                            context.SaveChanges();
                            Console.WriteLine($"计算完[{begin.ToDateString()}]到[{dividendDate.SettleTime.ToDateString()}]的分红");
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                Thread.Sleep(5000);
            }

        }
    }
}
