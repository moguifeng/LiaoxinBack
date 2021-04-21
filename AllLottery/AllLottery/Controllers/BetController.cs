using AllLottery.IBusiness;
using AllLottery.Model;
using AllLottery.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Mvc;

namespace AllLottery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ZzbAuthorize]
    public class BetController : BaseApiController
    {
        public IBetService BetService { get; set; }

        public IChasingOrderService ChasingOrderService { get; set; }

        [HttpPost("GetBetModes")]
        public ServiceResult GetBetModes()
        {
            return JsonObjectResult(from b in BetService.GetBetModes() select new { b.Index, b.BetModeId, b.Name, b.Money });
        }

        [HttpPost("GetCreditBetMode")]
        public ServiceResult GetCreditBetMode()
        {
            return JsonObjectResult(BetService.GetCreditBetMode()?.BetModeId);
        }

        [HttpPost("AddBetting")]
        public ServiceResult AddBetting(AddBettingViewModel[] model)
        {
            return Json(() =>
            {
                BetService.AddBettings(model, UserId);
                return new ServiceResult(ServiceResultCode.Success, "投注成功");
            }, "投注失败");
        }

        [HttpPost("GetBets")]
        public ServiceResult GetBets(BetGetBets model)
        {
            return Json(() =>
            {
                List<BetStatusEnum> list = new List<BetStatusEnum>();
                if (!string.IsNullOrEmpty(model.Types))
                {
                    var types = model.Types.Split(',');
                    foreach (string type in types)
                    {
                        list.Add(Enum.Parse<BetStatusEnum>(type));
                    }
                }
                var bets = BetService.GetBets(UserId, model.Index, model.Size, out var total, model.IsCredit, model.LotteryTypeId,
                    list, model.Order);
                return ObjectResult(new
                {
                    total,
                    data = from b in bets
                           select new
                           {
                               b.BetCount,
                               b.BetId,
                               BetMode = b.BetMode.Name,
                               b.BetMoney,
                               b.BetNo,
                               BetTime = b.BetTime.ToCommonString(),
                               b.LotteryIssuseNo,
                               PlayType = b.LotteryPlayDetail.LotteryPlayType.Name + "-" + b.LotteryPlayDetail.Name,
                               b.Order,
                               Status = b.Status.ToDescriptionString(),
                               b.WinBetCount,
                               b.WinMoney,
                               b.LotteryData?.Data,
                               LotteryTypeName = b.LotteryPlayDetail.LotteryPlayType.LotteryType.Name,
                               IsChasing = ChasingOrderService.IsChasingBet(b.BetId)
                           }
                });
            }, "获取投注记录");
        }

        [HttpPost("GetBet")]
        public ServiceResult GetBet(BetCancleBet model)
        {
            return Json(() =>
            {
                var b = BetService.GetBet(model.Id, UserId);
                return ObjectResult(new
                {
                    b.BetCount,
                    b.BetId,
                    BetMode = b.BetMode.Name,
                    b.BetMoney,
                    b.BetNo,
                    BetTime = b.BetTime.ToCommonString(),
                    b.LotteryIssuseNo,
                    PlayType = b.LotteryPlayDetail.LotteryPlayType.Name + "-" + b.LotteryPlayDetail.Name,
                    b.Order,
                    Status = b.Status.ToDescriptionString(),
                    b.WinBetCount,
                    b.WinMoney,
                    b.LotteryData?.Data,
                    LotteryTypeName = b.LotteryPlayDetail.LotteryPlayType.LotteryType.Name,
                    IsChasing = ChasingOrderService.IsChasingBet(b.BetId)
                });
            }, "获取投注订单失败");
        }

        [HttpPost("GetTeamBets")]
        public ServiceResult GetTeamBets(RechargeGetTeamRecharges model)
        {
            return Json(() =>
            {
                var bets = BetService.GetTeamBets(UserId, model.Name, model.Begin, model.End, model.Index, model.Size, out var total);
                return ObjectResult(new
                {
                    total,
                    data = from b in bets
                           select new
                           {
                               b.BetCount,
                               b.BetId,
                               BetMode = b.BetMode.Name,
                               b.BetMoney,
                               b.BetNo,
                               BetTime = b.BetTime.ToCommonString(),
                               b.LotteryIssuseNo,
                               PlayType = b.LotteryPlayDetail.LotteryPlayType.Name + "-" + b.LotteryPlayDetail.Name,
                               b.Order,
                               Status = b.Status.ToDescriptionString(),
                               b.WinBetCount,
                               b.WinMoney,
                               b.LotteryData?.Data,
                               LotteryTypeName = b.LotteryPlayDetail.LotteryPlayType.LotteryType.Name,
                               b.Player.Name
                           }
                });
            }, "获取投注记录");
        }

        [HttpGet("GetBetStatus")]
        public ServiceResult GetBetStatus()
        {
            return new ServiceResult<object>(ServiceResultCode.Success, "OK", BetStatusEnum.Win.GetDropListModels());
        }

        [HttpPost("CancleBet")]
        public ServiceResult CancleBet(BetCancleBet model)
        {
            return Json(() =>
            {
                BetService.CancleBet(model.Id, UserId);
                return new ServiceResult();
            }, "撤单失败");
        }
    }

    public class BetCancleBet
    {
        public int Id { get; set; }
    }

    public class BetGetBets : PageViewModel
    {
        public int? LotteryTypeId { get; set; }

        public string Types { get; set; }

        public string Order { get; set; }

        public bool? IsCredit { get; set; }
    }
}