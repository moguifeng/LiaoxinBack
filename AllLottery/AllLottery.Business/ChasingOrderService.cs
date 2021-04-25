using AllLottery.Business.Cathectic;
using AllLottery.IBusiness;
using AllLottery.Model;
using AllLottery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;

//作废啦
namespace AllLottery.Business
{

    //public class ChasingOrderService : BaseService, IChasingOrderService
    //{
    //    public ILotteryOpenTimeServer LotteryOpenTimeServer { get; set; }

    //    public int AddOrder(int playerId, ChasingOrderAddOrderViewModel model)
    //    {
    //        var mode = (from m in Context.BetModes where m.BetModeId == model.BetModeId select m).FirstOrDefault();
    //        if (mode == null)
    //        {
    //            throw new ZzbException("未找到投注模式");
    //        }

    //        var detail =
    //            (from d in Context.LotteryPlayDetails where d.LotteryPlayDetailId == model.LotteryPlayDetailId select d)
    //            .FirstOrDefault();
    //        if (detail == null)
    //        {
    //            throw new ZzbException("未找到玩法");
    //        }

    //        if (!detail.IsEnable || !detail.LotteryPlayType.IsEnable)
    //        {
    //            throw new ZzbException($"玩法[{detail.Name}]维护中，无法追号");
    //        }

    //        if (!detail.LotteryPlayType.LotteryType.IsEnable)
    //        {
    //            throw new ZzbException($"彩种[{detail.LotteryPlayType.LotteryType.Name}]不存在，无法追号");
    //        }
    //        if (detail.LotteryPlayType.LotteryType.IsStop)
    //        {
    //            throw new ZzbException($"彩种[{detail.LotteryPlayType.LotteryType.Name}]正在维护，无法追号");
    //        }

    //        var cathectic = BaseCathectic.CreateCathectic(detail.ReflectClass);

    //        var betCount = cathectic.CalculateBetCount(model.BetNo);

    //        if (model.Orders == null || !model.Orders.Any())
    //        {
    //            throw new ZzbException("追号期数为空");
    //        }

    //        decimal betMoney = 0;

    //        List<ChasingOrderDetail> details = new List<ChasingOrderDetail>();

    //        foreach (AddOrderViewModel addOrderViewModel in model.Orders)
    //        {
    //            var lotteryData = (from d in Context.LotteryDatas
    //                               where d.LotteryTypeId == detail.LotteryPlayType.LotteryTypeId && d.IsEnable && d.Number == addOrderViewModel.Number
    //                               select d).FirstOrDefault();
    //            if (lotteryData != null)
    //            {
    //                throw new ZzbException($"[{addOrderViewModel.Number}]期已经开奖，无法投注");
    //            }

    //            var betTime =
    //                LotteryOpenTimeServer.GetBetTime(detail.LotteryPlayType.LotteryTypeId, addOrderViewModel.Number);

    //            if (betTime == null)
    //            {
    //                throw new ZzbException("期号不正确，请勿非法操作");
    //            }

    //            if (DateTime.Now > betTime.OpenRiskTime)
    //            {
    //                throw new ZzbException($"彩种[{detail.LotteryPlayType.Name}]的[{addOrderViewModel.Number}]期期已超过投注时间。");
    //            }

    //            if (addOrderViewModel.Times <= 0)
    //            {
    //                throw new ZzbException("倍数必须大于0");
    //            }

    //            if (detail.MaxBetMoney < mode.Money * addOrderViewModel.Times * betCount)
    //            {
    //                throw new ZzbException($"玩法[{detail.LotteryPlayType.Name}-{detail.Name}]最高投注[{detail.MaxBetMoney}]元，无法追号");
    //            }

    //            if (detail.MaxBetCount < betCount)
    //            {
    //                throw new ZzbException($"玩法[{detail.LotteryPlayType.Name}-{detail.Name}]最高投注注数为[{detail.MaxBetMoney}]，无法追号");
    //            }



    //            betMoney += mode.Money * addOrderViewModel.Times * betCount;

    //            details.Add(new ChasingOrderDetail()
    //            {
    //                Times = addOrderViewModel.Times,
    //                Number = addOrderViewModel.Number,
    //                UpdateTime = betTime.BeginRiskTime,
    //                BetMoney = mode.Money * addOrderViewModel.Times * betCount
    //            });
    //        }

    //        int i = 0;

    //        foreach (ChasingOrderDetail chasingOrderDetail in details.OrderBy(t => t.UpdateTime))
    //        {
    //            chasingOrderDetail.Index = i++;
    //            chasingOrderDetail.UpdateTime = DateTime.Now;
    //        }

    //        var player = (from p in Context.Players where p.PlayerId == playerId select p).FirstOrDefault();
    //        if (player == null || !player.IsEnable || player.IsFreeze)
    //        {
    //            throw new ZzbException("当前用户不存在");
    //        }

    //        if (player.Coin < betMoney)
    //        {
    //            throw new ZzbException($"当前用户余额不足{betMoney}，无法追号");
    //        }

    //        player.Coin -= betMoney;
    //        player.FCoin += betMoney;


    //        ChasingOrder order = new ChasingOrder()
    //        {
    //            BetModeId = mode.BetModeId,
    //            ChasingOrderDetails = details,
    //            IsWinStop = model.IsWinStop,
    //            LotteryPlayDetailId = detail.LotteryPlayDetailId,
    //            PlayerId = playerId,
    //            BetNo = model.BetNo
    //        };
    //        var exist = Context.ChasingOrders.Add(order);

    //        CoinLog log = new CoinLog(playerId, -betMoney, player.Coin, betMoney, player.FCoin, CoinLogTypeEnum.CreateChasingOrder, 0, $"创建追号订单[{order.Order}]");
    //        Context.CoinLogs.Add(log);

    //        Context.SaveChanges();

    //        return exist.Entity.ChasingOrderId;
    //    }

    //    public bool IsChasingBet(int betId)
    //    {
    //        var chasing = (from c in Context.ChasingOrderDetails where c.BetId == betId select c).FirstOrDefault();
    //        return chasing != null;
    //    }

    //    public ChasingOrderDetail GetChasingOrderDetail(int betId)
    //    {
    //        return (from c in Context.ChasingOrderDetails where c.BetId == betId select c).FirstOrDefault();
    //    }

    //    public void CancleChasing(int orderId)
    //    {
    //        var order = (from c in Context.ChasingOrders where c.ChasingOrderId == orderId select c).FirstOrDefault();
    //        if (order == null)
    //        {
    //            throw new ZzbException($"未找到追号订单[{orderId}]");
    //        }
    //        if (order.Status == ChasingStatus.End || order.Status == ChasingStatus.Stop)
    //        {
    //            throw new ZzbException($"追号订单[{orderId}]已经被处理，无法取消");
    //        }
    //        order.Status = ChasingStatus.Stop;
    //        order.Update();
    //        var sql = from c in Context.ChasingOrderDetails
    //                  where c.ChasingOrderId == orderId && c.IsEnable &&
    //                        c.BetId == null
    //                  select c;
    //        if (sql.Any())
    //        {
    //            var player = order.Player;
    //            var betMoney = sql.Sum(t => t.BetMoney);
    //            player.Coin += betMoney;
    //            player.FCoin -= betMoney;
    //            Context.CoinLogs.Add(new CoinLog(player.PlayerId, betMoney, player.Coin, -betMoney, player.FCoin, CoinLogTypeEnum.ChasingBetCancle, orderId, $"追号[{order.Order}]完成"));
    //        }
    //        Context.SaveChanges();
    //    }

    //    public ChasingOrder[] GetOrders(int playerId, int index, int size, out int total)
    //    {
    //        var orders = from o in Context.ChasingOrders where o.IsEnable && o.PlayerId == playerId select o;
    //        total = orders.Count();
    //        return orders.OrderByDescending(t => t.CreateTime).Skip((index - 1) * size).Take(size).ToArray();
    //    }

    //    public ChasingOrder GetOrder(int orderId)
    //    {
    //        return (from c in Context.ChasingOrders where c.ChasingOrderId == orderId select c).FirstOrDefault();
    //    }
    //}
}