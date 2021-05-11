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
    public class WithdrawController : LiaoxinBaseController
    {
        //public IWithdrawService WithdrawService { get; set; }

        /// <summary>
        /// 提现
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpPost("AddWithdraw")]
        public ServiceResult<WithdrawResponse> AddWithdraw(AddWithdrawRequest requestObj)
        {

            return (ServiceResult<WithdrawResponse>)Json(() => {

            
                WithdrawResponse returnObject = null;
                if (requestObj.Money < (decimal)0.01)
                {
                    throw new ZzbException("提现金额金额最少为0.01");
                }


                Client clientEntity = Context.Clients.AsNoTracking().FirstOrDefault(p => p.ClientId == requestObj.ClientId);
                ClientBank clientBankEntity = Context.ClientBanks.AsNoTracking().FirstOrDefault(p => p.ClientBankId == requestObj.ClientBankId);
                if (clientEntity == null)
                {
                    throw new ZzbException("找不到提现银行");

                }
                else if (clientEntity.Coin < requestObj.Money)
                {
                    throw new ZzbException("余额不足");
                }
                else if (clientBankEntity == null)
                {
                    throw new ZzbException("账号银行卡无效");
                }
                //提现成功
                Withdraw entity = new Withdraw();
                entity.ClientId = requestObj.ClientId;
                entity.ClientBankId = requestObj.ClientBankId;
                entity.Money = requestObj.Money;
                entity.Remark = requestObj.Remark;
                entity.Status = WithdrawStatusEnum.Ok;
                using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
                {
                    try
                    {
                        CoinLog coinLogEntity = new CoinLog();
                        clientEntity.Coin -= entity.Money;
                        clientEntity.UpdateTime = DateTime.Now;
                        Update<Client>(clientEntity, "ClientId", new List<string>() { "Coin", "UpdateTime" });
                        coinLogEntity.ClientId = clientBankEntity.ClientId;
                        coinLogEntity.FlowCoin = -entity.Money;
                        coinLogEntity.Coin = clientEntity.Coin;
                        coinLogEntity.Type = CoinLogTypeEnum.Withdraw;
                        coinLogEntity.AboutId = entity.WithdrawId;
                        Context.CoinLogs.Add(coinLogEntity);

                        Context.Withdraws.Add(entity);
                        Context.SaveChanges();

                        returnObject = ConvertHelper.ConvertToModel<Withdraw, WithdrawResponse>(entity);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new ZzbException("提现失败,内部错误");
                    }
                }
             
                return ObjectGenericityResult(true, returnObject);

            });
          
        }

        /// <summary>
        /// 查询账号提现记录
        /// </summary>
        /// <param name="clientId">账号Id</param>
        /// <param name="year">年份</param>
        /// <returns></returns>
        [HttpGet("QueryWithdrawsByClient")]
        public ServiceResult<IList<WithdrawResponse>> QueryWithdrawsByClient(Guid clientId, int year)
        {

            return (ServiceResult<IList<WithdrawResponse>>)Json(() => {

                IList<Withdraw> list = Context.Withdraws.AsNoTracking().Where(p => p.ClientId == clientId && p.CreateTime.Year == year).ToList();
                IList<WithdrawResponse> returnList = ConvertHelper.ConvertToList<Withdraw, WithdrawResponse>(list);
                return ObjectGenericityResult<IList<WithdrawResponse>>(true, returnList, "");
            });
        
        }

        /// <summary>
        /// 查询银行卡提现记录
        /// </summary>
        /// <param name="clientBankId">账号银行卡Id</param>
        /// <param name="year">年份</param>
        /// <returns></returns>
        [HttpGet("QueryWithdrawsByClientBank")]
        public ServiceResult<IList<WithdrawResponse>> QueryWithdrawsByClientBank(Guid clientBankId, int year)
        {
            return (ServiceResult<IList<WithdrawResponse>>)Json(() =>
            {


                IList<Withdraw> list = Context.Withdraws.AsNoTracking().Where(p => p.ClientBankId == clientBankId && p.CreateTime.Year == year).ToList();
                IList<WithdrawResponse> returnList = ConvertHelper.ConvertToList<Withdraw, WithdrawResponse>(list);
                return ObjectGenericityResult<IList<WithdrawResponse>>(true, returnList, "");
            });
        }
    }

}