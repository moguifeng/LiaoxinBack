using AllLottery.Business.Config;
using AllLottery.Business.Report;
using AllLottery.Business.Report.Team;
using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Zzb.Common;

namespace AllLottery.GrantDailyWage
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
                        //计算最后一次的日工资计算时间
                        var calTime = BaseConfig.CreateInstance(SystemConfigEnum.DailyWageTime).DateTimeValue;
                        var begin = calTime;
                        var end = calTime.AddDays(1);
                        if (DateTime.Now > calTime.AddDays(1).AddHours(3))
                        {
                            var players = from p in context.Players where p.DailyWageRate != null select p;
                            if (players.Any())
                            {
                                var dailyWages = from d in context.DailyWages where d.IsEnable orderby d.Rate descending select d;
                                if (dailyWages.Any())
                                {
                                    List<DailyWageLog> logs = new List<DailyWageLog>();
                                    List<CoinLog> coinLogs = new List<CoinLog>();
                                    foreach (Player player in players)
                                    {
                                        //团队投注金额
                                        var betMoney = new TeamBetMoneyReport(player.PlayerId).GetReportData(begin, end);

                                        if (betMoney > 0)
                                        {
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
                                            //有效投注人数
                                            var betMen = ids.Count();

                                            //开始遍历日工资配置
                                            foreach (DailyWage dailyWage in dailyWages)
                                            {
                                                if (dailyWage.BetMoney <= betMoney && dailyWage.MenCount <= betMen)
                                                {
                                                    var rate = Math.Min(player.DailyWageRate.Value, dailyWage.Rate);
                                                    var dailyMoney = betMoney * rate;

                                                    // ReSharper disable once PossibleInvalidOperationException
                                                    DailyWageLog log = new DailyWageLog(rate, betMoney, betMen, dailyMoney, begin, player.PlayerId);
                                                    logs.Add(log);

                                                    player.AddMoney(dailyMoney, CoinLogTypeEnum.ReceiveDaily, 0, out var coinLog, $"[{begin.ToDateString()}]的日工资");

                                                    coinLogs.Add(coinLog);

                                                    if (player.ParentPlayer != null)
                                                    {
                                                        player.ParentPlayer.AddMoney(-dailyMoney, CoinLogTypeEnum.GiveDaily, 0, out var giveLog, $"发放给[{player.Name }]的[{begin.ToDateString()}]日工资");
                                                        coinLogs.Add(giveLog);
                                                    }

                                                    break;
                                                }
                                            }
                                        }
                                    }

                                    if (logs.Any())
                                    {
                                        context.DailyWageLogs.AddRange(logs);
                                        context.CoinLogs.AddRange(coinLogs);

                                    }
                                }
                            }
                            BaseConfig.CreateInstance(SystemConfigEnum.DailyWageTime).Save(end.ToDateString(), context);
                            context.SaveChanges();
                            Console.WriteLine($"计算完[{begin.ToDateString()}]的日工资");
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
