using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using LIaoxin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Mvc;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BankController :LiaoxinBaseController
    {
        public IBankService BankService { get; set; }



        private Client GetCurrentClient()
        {
            var entity = Context.Clients.Where(c => c.ClientId == CurrentClientId).FirstOrDefault();
            return entity;

        }

        /// <summary>
        /// 获取系统银行
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetSystemBanks")]
        public ServiceResult GetSystemBanks()
        {
            return JsonObjectResult(
                from b in BankService.GetSystemBanks() select new { b.AffixId, b.Name, b.SystemBankId }, "获取系统银行失败");
        }

        /// <summary>
        /// 获取当前登录客户的银行卡
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetClientBanks")]
        public ServiceResult<List<ClientBankResponse>> GetClientBanks()
        {
            var objs = (from b in BankService.GetClientBanks(CurrentClientId)
                        select new ClientBankResponse()
                        {
                            ClientBankId = b.ClientBankId,
                            CardName = b.SystemBank.Name,
                            FrontCardNumber =b.CardNumber.Substring(0,4),
                            BackCardNumber = b.CardNumber.Substring(b.CardNumber.Length - 4),                            
                            SystemBankId = b.SystemBankId,
                            AffixId = b.SystemBank.AffixId
                        }).ToList();

            return (ServiceResult<List<ClientBankResponse>>)Json(() =>
            {
                return ListGenericityResult(objs);
            });
        }

        /// <summary>
        /// 绑定银行卡
        /// </summary>        
        /// <returns></returns>
        [HttpPost("BindClientBank")]
        public ServiceResult BindClientBank(BindClientBankRequest request)
        {
            return Json(() =>
            {

                var entity = this.GetCurrentClient();
                if (string.IsNullOrEmpty(entity.RealName))
                {
                    throw new ZzbException("请先进行实名身份绑定");
                }
                if (string.IsNullOrEmpty(entity.CoinPassword))
                {
                    throw new ZzbException("请先设置支付密码");
                }
                if (request.CardNumber.Length <= 12)
                {
                    throw new ZzbException("请输入正确的银行卡号");
                }

                if (entity.CoinPassword != SecurityHelper.Encrypt(request.CoinPassword))
                {
                    throw new ZzbException("资金密码不正确");
                }

                var exist = from b in Context.SystemBanks where b.IsEnable && b.SystemBankId == request.SystemBankdId select b;
                if (!exist.Any())
                {
                    throw new ZzbException("参数错误，未找到系统银行");
                }

                var existBank = from b in Context.ClientBanks where b.CardNumber == request.CardNumber && b.ClientId == CurrentClientId select b;
                if (existBank.Any())
                {
                    throw new ZzbException("已经存在相同卡号，无法添加");
                }

                Context.ClientBanks.Add(new ClientBank(CurrentClientId, request.SystemBankdId, request.CardNumber));
                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"添加了银行卡[{exist.First().Name},卡号:{request.CardNumber}]"));
                return ObjectResult(Context.SaveChanges() > 0);
            }, "绑定银行卡失败");

        }

    }
}