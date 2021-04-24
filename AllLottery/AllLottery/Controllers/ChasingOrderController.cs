using AllLottery.IBusiness;
using AllLottery.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Mvc;

//作废啦
namespace AllLottery.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[ZzbAuthorize]
    //public class ChasingOrderController : BaseApiController
    //{
    //    public IChasingOrderService ChasingOrderService { get; set; }

    //    [HttpPost("AddOrder")]
    //    public ServiceResult AddOrder(ChasingOrderAddOrderViewModel model)
    //    {
    //        return Json(() =>
    //        {
    //            var i = ChasingOrderService.AddOrder(UserId, model);
    //            return ObjectResult(i);
    //        });
    //    }

    //    [HttpPost("CancleOrder")]
    //    public ServiceResult CancleOrder(ChasingOrderCancleOrder model)
    //    {
    //        return Json(() =>
    //        {
    //            ChasingOrderService.CancleChasing(model.OrderId);
    //            return new ServiceResult();
    //        }, "取消追号失败");
    //    }

    //    [HttpPost("GetOrders")]
    //    public ServiceResult GetOrders(PageViewModel model)
    //    {
    //        return Json(() =>
    //        {
    //            var order = ChasingOrderService.GetOrders(UserId, model.Index, model.Size, out var total);
    //            return ObjectResult(new
    //            {
    //                total,
    //                datas = from o in order
    //                        select new
    //                        {
    //                            o.ChasingOrderId,
    //                            o.Order,
    //                            o.LotteryPlayDetail.LotteryPlayType.LotteryType.Name,
    //                            AllCount = o.ChasingOrderDetails.Count,
    //                            DoneCount = o.ChasingOrderDetails.Count(t => t.BetId != null),
    //                            BeginNo = o.ChasingOrderDetails.OrderBy(t => t.Index).First().Number,
    //                            Status = o.Status.ToDescriptionString(),
    //                            AllBetMoney = o.ChasingOrderDetails.Sum(t => t.BetMoney),
    //                            CreateTime = o.CreateTime.ToCommonString()
    //                        }
    //            });
    //        }, "获取追号记录失败");
    //    }

    //    [HttpPost("GetOrder")]
    //    public ServiceResult GetOrder(ChasingOrderCancleOrder model)
    //    {
    //        return Json(() =>
    //        {
    //            var order = ChasingOrderService.GetOrder(model.OrderId);
    //            if (order == null)
    //            {
    //                return new ServiceResult(ServiceResultCode.Error, "未找到追号记录");
    //            }
    //            return ObjectResult(new
    //            {
    //                order.ChasingOrderId,
    //                BetModeName = order.BetMode.Name,
    //                order.BetNo,
    //                PlayType = order.LotteryPlayDetail.LotteryPlayType.Name + "-" + order.LotteryPlayDetail.Name,
    //                Status = order.Status.ToDescriptionString(),
    //                order.Order,
    //                Bets = from b in order.ChasingOrderDetails
    //                       where b.IsEnable
    //                       select new
    //                       {
    //                           Order = b.BetId == null ? "未投注" : b.Bet.Order,
    //                           b.Number,
    //                           b.BetMoney,
    //                           WinMoney = b.BetId == null ? 0M : b.Bet.WinMoney,
    //                           Status = b.BetId == null ? "未投注" : b.Bet.Status.ToDescriptionString(),
    //                           b.Times
    //                       }
    //            });
    //        }, "获取追号详细记录失败");
    //    }
    //}

    //public class ChasingOrderCancleOrder
    //{
    //    public int OrderId { get; set; }
    //}
}