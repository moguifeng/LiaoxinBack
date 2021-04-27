using Liaoxin.IBusiness;
using Liaoxin.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Mvc;

namespace Liaoxin.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[ZzbAuthorize]
    //public class RechargeController : BaseApiController
    //{
    //    public IRechargeService RechargeService { get; set; }

    //    [HttpPost("AddRecharge")]
    //    public ServiceResult AddRecharge(RechargeAddRecharge model)
    //    {
    //        return Json(() =>
    //        {
    //            RechargeService.AddRecharge(model.BankId, UserId, model.Money);
    //            return new ServiceResult(ServiceResultCode.Success, "创建订单成功，请耐心等待管理员审批");
    //        }, "创建订单失败");
    //    }

    //    [HttpPost("IsExistRecharges")]
    //    public ServiceResult IsExistRecharges()
    //    {
    //        return JsonObjectResult(RechargeService.IsExistRecharges(UserId), "获取是否存在充值订单失败");
    //    }

    //    [HttpPost("GetRecharges")]
    //    public ServiceResult GetRecharges(PageViewModel model)
    //    {
    //        return Json(() =>
    //        {
    //            var datas = RechargeService.GetRecharges(UserId, model.Size, model.Index,
    //                out var total);
    //            return ObjectResult(new
    //            {
    //                Total = total,
    //                Datas = from d in datas
    //                        select new
    //                        {
    //                            d.Money,
    //                            MerchantsBankName = d.MerchantsBank == null ? "手工充值" : d.MerchantsBank.Name,
    //                            d.OrderNo,
    //                            d.Remark,
    //                            State = d.State.ToDescriptionString(),
    //                            CreateTime = d.CreateTime.ToCommonString(),
    //                        }
    //            });
    //        }, "获取充值记录失败");
    //    }

    //    [HttpPost("GetTeamRecharges")]
    //    public ServiceResult GetTeamRecharges(RechargeGetTeamRecharges model)
    //    {
    //        return Json(() =>
    //        {
    //            var datas = RechargeService.GetTeamRecharges(UserId, model.Name, model.Begin, model.End, model.Size, model.Index,
    //                out var total);
    //            return ObjectResult(new
    //            {
    //                Total = total,
    //                Datas = from d in datas
    //                        select new
    //                        {
    //                            d.Money,
    //                            MerchantsBankName = d.MerchantsBank == null ? "手工充值" : d.MerchantsBank.Name,
    //                            d.OrderNo,
    //                            d.Remark,
    //                            State = d.State.ToDescriptionString(),
    //                            CreateTime = d.CreateTime.ToCommonString(),
    //                            d.Player.Name
    //                        }
    //            });
    //        }, "获取团队充值记录失败");
    //    }

    //    [HttpPost("AddThirdPayRecharge")]
    //    public ServiceResult AddThirdPayRecharge(ApiRechargeAddThirdPayRechargeViewModel model)
    //    {
    //        return Json(() =>
    //        {
    //            var m = RechargeService.AddThirdPayRecharge(model.BankId, UserId, model.Money, model.Url);
    //            return new ServiceResult<object>(ServiceResultCode.Success, "创建订单成功", m);
    //        }, "创建订单失败");
    //    }
    //}

    public class RechargeGetTeamRecharges
    {
        public string Name { get; set; }
    }

    public class RechargeAddRecharge
    {
        public int BankId { get; set; }

        public decimal Money { get; set; }
    }

    public class ApiRechargeAddThirdPayRechargeViewModel : RechargeAddRecharge
    {
        public string Url { get; set; }
    }
}