using Liaoxin.IBusiness;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Zzb;
using Zzb.Mvc;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ZzbAuthorize]
    public class BankController : BaseApiController
    {
        public IBankService BankService { get; set; }

        [HttpPost("GetSystemBanks")]
        public ServiceResult GetSystemBanks()
        {
            return JsonObjectResult(
                from b in BankService.GetSystemBanks() select new { b.AffixId, b.Name, b.SystemBankId }, "获取系统银行失败");
        }

        [HttpPost("GetPlayerBanks")]
        public ServiceResult GetPlayerBanks()
        {
            return JsonObjectResult(from b in BankService.GetClientBanks(UserId)
                                    select new
                                    {
                                        b.ClientBankId,
                                        CardNumber = "****" + b.CardNumber.Substring(b.CardNumber.Length - 4),
                                        PayeeName = "*" + b.Client.RealName.Substring(1),
                                        b.SystemBankId,
                                        b.SystemBank.AffixId
                                    });
        }

        [HttpPost("AddPlayerBank")]
        public ServiceResult AddPlayerBank(BankAddPlayerBank model)
        {
            return Json(() =>
            {
                BankService.AddPlayerBank(UserId, model.SystemBankId, model.PayeeName, model.CardNumber);
                return new ServiceResult(ServiceResultCode.Success);
            }, "添加银行卡失败");
        }

        [HttpPost("GetMerchantsBanks")]
        public ServiceResult GetMerchantsBanks(BankGetMerchantsBanks model)
        {
            return Json(
                () => new ServiceResult<object>(ServiceResultCode.Success, "OK",
                    from b in BankService.GetMerchantsBanks(model.IsApp) select new { b.MerchantsBankId, b.Name, b.BannerAffixId, b.Type, b.Min, b.Max, ThirdPayMerchantsType = b.ThirdPayMerchantsType.ToString() }),
                "获取收款银行失败");
        }

        [HttpPost("GetMerchantsBank")]
        public ServiceResult GetMerchantsBank(BankGetMerchantsBank model)
        {
            return Json(() =>
            {
                var bank = BankService.GetMerchantsBank(model.Id);
                return ObjectResult(new
                {
                    bank.MerchantsBankId,
                    bank.BannerAffixId,
                    bank.ScanAffixId,
                    bank.Type,
                    bank.Description,
                    bank.Name,
                    bank.Account,
                    bank.BankUserName
                });
            }, "获取银行详细信息失败");
        }
    }

    public class BankAddPlayerBank
    {
        public int SystemBankId { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// 收款人
        /// </summary>
        public string PayeeName { get; set; }
    }

    public class BankGetMerchantsBanks
    {
        public bool IsApp { get; set; } = false;
    }

    public class BankGetMerchantsBank
    {
        public int Id { get; set; }
    }
}