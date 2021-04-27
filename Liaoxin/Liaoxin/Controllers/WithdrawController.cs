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
    //public class WithdrawController : BaseApiController
    //{
    //    public IWithdrawService WithdrawService { get; set; }

    //    [HttpPost("AddWithdraw")]
    //    public ServiceResult AddWithdraw(WithdrawAddWithdrawViewModel model)
    //    {
    //        return Json(() =>
    //        {
    //            WithdrawService.AddWithdraw(model.Money, model.BankId, UserId, model.CoinPassword);
    //            return new ServiceResult(ServiceResultCode.Success, "申请提现成功");
    //        }, "申请提现失败");
    //    }

    //    [HttpPost("GetWithdraws")]
    //    public ServiceResult GetWithdraws(PageViewModel model)
    //    {
    //        return Json(() =>
    //        {
    //            var datas = WithdrawService.GetWithdraws(UserId, model.Index, model.Size,
    //                out var total);
    //            return ObjectResult(new
    //            {
    //                Total = total,
    //                Datas = from d in datas
    //                        select new
    //                        {
    //                            d.Money,
    //                            d.PlayerBank.CardNumber,
    //                            BankName = d.PlayerBank.SystemBank.Name,
    //                            d.OrderNo,
    //                            d.Remark,
    //                            Status = d.Status.ToDescriptionString(),
    //                            CreateTime = d.CreateTime.ToCommonString()
    //                        }
    //            });
    //        }, "获取提现记录失败");
    //    }

    //    [HttpPost("GetTeamWithdraws")]
    //    public ServiceResult GetTeamWithdraws(RechargeGetTeamRecharges model)
    //    {
    //        return Json(() =>
    //        {
    //            var datas = WithdrawService.GetTeamWithdraws(UserId, model.Name, model.Begin, model.End, model.Index, model.Size,
    //                out var total);
    //            return ObjectResult(new
    //            {
    //                Total = total,
    //                Datas = from d in datas
    //                        select new
    //                        {
    //                            d.Money,
    //                            d.PlayerBank.CardNumber,
    //                            BankName = d.PlayerBank.SystemBank.Name,
    //                            d.OrderNo,
    //                            d.Remark,
    //                            Status = d.Status.ToDescriptionString(),
    //                            CreateTime = d.CreateTime.ToCommonString(),
    //                            d.PlayerBank.Player.Name
    //                        }
    //            });
    //        }, "获取团队提现记录失败");
    //    }
    //}

    public class WithdrawAddWithdrawViewModel
    {
        public decimal Money { get; set; }

        public int BankId { get; set; }

        public string CoinPassword { get; set; }
    }
}