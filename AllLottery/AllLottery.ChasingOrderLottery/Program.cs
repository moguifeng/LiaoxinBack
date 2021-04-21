using AllLottery.Business.Cathectic;
using AllLottery.Model;
using System;
using System.Linq;
using System.Threading;
using Zzb.Common;

namespace AllLottery.ChasingOrderLottery
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
                        var orders = from o in context.ChasingOrders
                                     where o.IsEnable && (o.Status == ChasingStatus.Doing || o.Status == ChasingStatus.Wait)
                                     select o;
                        bool isChange = false;
                        if (orders.Any())
                        {
                            foreach (ChasingOrder chasingOrder in orders)
                            {
                                var sql = from d in context.ChasingOrderDetails
                                          where d.IsEnable && d.ChasingOrderId == chasingOrder.ChasingOrderId &&
                                                d.BetId != null && (d.Bet.Status == BetStatusEnum.Wait || d.Bet.Status == BetStatusEnum.Error)
                                          select d;
                                //不存在没开奖的
                                if (!sql.Any())
                                {
                                    isChange = true;
                                    var detail = (from d in context.ChasingOrderDetails
                                                  where d.IsEnable && d.ChasingOrderId == chasingOrder.ChasingOrderId &&
                                                        d.BetId == null
                                                  orderby d.Index
                                                  select d).FirstOrDefault();
                                    if (detail != null)
                                    {
                                        var cathectic = BaseCathectic.CreateCathectic(detail.ChasingOrder.LotteryPlayDetail.ReflectClass);

                                        var betCount = cathectic.CalculateBetCount(detail.ChasingOrder.BetNo);

                                        detail.ChasingOrder.Player.AddFMoney(-betCount * detail.ChasingOrder.BetMode.Money * detail.Times, CoinLogTypeEnum.ChasingBet, 0, out var log, $"追号订单[{detail.ChasingOrder.Order}]的自动投注");

                                        context.CoinLogs.Add(log);

                                        var bet = new Bet(RandomHelper.GetRandom("B"), detail.ChasingOrder.PlayerId, detail.ChasingOrder.LotteryPlayDetailId, detail.Number, detail.ChasingOrder.BetNo, betCount * detail.ChasingOrder.BetMode.Money * detail.Times, betCount, detail.ChasingOrder.BetModeId, detail.Times);

                                        detail.Bet = bet;

                                        detail.ChasingOrder.Status = ChasingStatus.Doing;
                                        detail.ChasingOrder.Update();


                                        context.Bets.Add(bet);
                                    }
                                    else
                                    {
                                        chasingOrder.Status = ChasingStatus.End;
                                    }

                                    chasingOrder.Update();
                                }
                            }
                            if (isChange)
                            {
                                context.SaveChanges();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("报错时间" + DateTime.Now.ToCommonString());
                }

                Thread.Sleep(5000);
            }
        }
    }
}
