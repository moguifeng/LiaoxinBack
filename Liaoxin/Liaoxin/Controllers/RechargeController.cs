using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using LIaoxin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;

using Zzb.Mvc;
using Zzb.Utility;
namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RechargeController : LiaoxinBaseController
    {
        //public IRechargeService RechargeService { get; set; }

        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpPost("AddRecharge")]
        public ServiceResult<RechargeResponse> AddRecharge(AddRechargeRequest requestObj)
        {
            bool result = true;
            string msg = "";
            RechargeResponse returnObject = null;
            if (requestObj.Money < (decimal)0.01)
            {
                msg = "充值金额金额最少为0.01";
                result = false;
                return ObjectGenericityResult<RechargeResponse>(result, returnObject, msg);
            }


            Client clientEntity = Context.Clients.AsNoTracking().FirstOrDefault(p => p.ClientId == requestObj.ClientId);
            ClientBank clientBankEntity = Context.ClientBanks.AsNoTracking().FirstOrDefault(p => p.ClientBankId == requestObj.ClientBankId);
            if (clientEntity == null)
            {
                msg = "待充值账号无效";
                result = false;
                return ObjectGenericityResult<RechargeResponse>(result, returnObject, msg);
            }
            else if (clientBankEntity == null)
            {
                msg = "账号银行卡无效";
                result = false;
                return ObjectGenericityResult<RechargeResponse>(result, returnObject, msg);
            }
            //充值成功
            Recharge entity = new Recharge();
            entity.ClientId = requestObj.ClientId;
            entity.ClientBankId = requestObj.ClientBankId;
            entity.Money = requestObj.Money;
            entity.Remark = requestObj.Remark;
            entity.State = (clientBankEntity.ClientId == clientEntity.ClientId) ? RechargeStateEnum.Ok : RechargeStateEnum.AdminOk;
            using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
            {
                try
                {
                    CoinLog coinLogEntity = new CoinLog();
                    clientEntity.Coin += entity.Money;
                    clientEntity.UpdateTime = DateTime.Now;
                    Update<Client>(clientEntity, "ClientId", new List<string>() { "Coin", "UpdateTime" });
                    coinLogEntity.ClientId = entity.ClientId;
                    coinLogEntity.FlowCoin = entity.Money;
                    coinLogEntity.Coin = clientEntity.Coin;
                    coinLogEntity.Type = (clientBankEntity.ClientId == clientEntity.ClientId) ? CoinLogTypeEnum.Recharge : CoinLogTypeEnum.InsteadRecharge;
                    coinLogEntity.AboutId = entity.RechargeId;
                    Context.CoinLogs.Add(coinLogEntity);

                    Context.Recharges.Add(entity);
                    Context.SaveChanges();

                    returnObject = ConvertHelper.ConvertToModel<Recharge, RechargeResponse>(entity);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    transaction.Rollback();
                }
            }
            msg = "充值成功";
            return ObjectGenericityResult<RechargeResponse>(result, returnObject, msg);
        }

        /// <summary>
        /// 查询账号充值记录
        /// </summary>
        /// <param name="clientId">账号Id</param>
        /// <param name="year">年份</param>
        /// <returns></returns>
        [HttpGet("QueryRechargesByClient")]
        public ServiceResult<IList<RechargeResponse>> QueryRechargesByClient(Guid clientId, int year)
        {
            IList<Recharge> list = Context.Recharges.AsNoTracking().Where(p => p.ClientId == clientId && p.CreateTime.Year == year).ToList();
            IList<RechargeResponse> returnList = ConvertHelper.ConvertToList<Recharge, RechargeResponse>(list);
            return ObjectGenericityResult<IList<RechargeResponse>>(true, returnList, "");
        }

        /// <summary>
        /// 查询银行卡充值记录
        /// </summary>
        /// <param name="clientBankId">账号银行卡Id</param>
        /// <param name="year">年份</param>
        /// <returns></returns>
        [HttpGet("QueryRechargesByClientBank")]
        public ServiceResult<IList<RechargeResponse>> QueryRechargesByClientBank(Guid clientBankId, int year)
        {
            IList<Recharge> list = Context.Recharges.AsNoTracking().Where(p => p.ClientBankId == clientBankId && p.CreateTime.Year == year).ToList();
            IList<RechargeResponse> returnList = ConvertHelper.ConvertToList<Recharge, RechargeResponse>(list);
            return ObjectGenericityResult<IList<RechargeResponse>>(true, returnList, "");
        }
    }

}