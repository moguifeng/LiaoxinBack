using Liaoxin.Cache;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using LIaoxin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading;
using Zzb;
using Zzb.ICacheManger;
using Zzb.Mvc;
using Zzb.Utility;
using static Liaoxin.Model.RedPacket;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RedPacketsController : LiaoxinBaseController
    {
        public IClientService clientService { get; set; }

        private readonly ICacheManager _cacheManager= CacheManager.singleCache;

        /// <summary>
        /// 发群红包
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateGroupRedPackets")]
        public ServiceResult<GroupReadPacketsResponse> CreateGroupRedPackets(CreateGroupReadPacketsRequest request)
        {
            GroupReadPacketsResponse returnObject = null;
            bool result = true;
            string errMsg = "";
            if (request.Money < request.Count * (decimal)0.01)
            {
                errMsg = "红包金额不足,要确保每人能领0.01";
                result = false;
                return ObjectGenericityResult<GroupReadPacketsResponse>(result, returnObject);
            }

            entity.Database.SqlQuery<User>(strSQL);

            Client sender = Context.Clients.AsNoTracking().FirstOrDefault(p => p.ClientId == request.SenderClientId);
            if (sender != null && sender.IsEnable && sender.Coin >= request.Money)
            {
                RedPacket entity = new RedPacket();
                entity.ClientId = request.SenderClientId;
                entity.Count = request.Count;
                entity.Money = request.Money;
                entity.Over = request.Money;
                entity.Greeting = request.Greeting;
                entity.Type = (RedPacketTypeEnum)request.ReadPacketsType;
                returnObject = ConvertHelper.ConvertToModel<RedPacket, GroupReadPacketsResponse>(entity);
                Context.RedPackets.Add(entity);
                sender.Coin -= request.Money;
                sender.UpdateTime = DateTime.Now;
                Update<Client>(sender, "ClientId", new List<string>() { "Coin", "UpdateTime" });
                Context.SaveChanges();
            }
            else
            {
                result = false;
                errMsg = "用户无效或余额不足";
            }
            return ObjectGenericityResult<GroupReadPacketsResponse>(result, returnObject);
        }

        /// <summary>
        /// 领取群红包
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpPost("ReceiveGroupRedPackets")]
        public ServiceResult<decimal> ReceiveGroupRedPackets(ReceiveGroupRedPacketsRequest requestObj)
        {
            bool result = true;
            string errMsg = "";
            Guid redPacketId = requestObj.RedPacketId;
            Guid clientId = requestObj.ClientId;
            string operKey = redPacketId.ToString();
            decimal receiveMoney = 0;
            try
            {
                while (_cacheManager.Get<object>(operKey) != null)
                {
                    Thread.Sleep(200);
                }
                _cacheManager.Set(operKey, clientId.ToString());
                RedPacket entity = Context.RedPackets.FirstOrDefault(p => p.RedPacketId == redPacketId);

                if (entity == null)
                {
                    result = false;
                    errMsg = "红包已失效";
                }
                else if (entity.Status != RedPacketStatus.Send)
                {
                    result = false;
                    errMsg = "红包已失效";
                }
                else
                {
                    //检查是否已经领了,不能重复领取
                    RedPacketReceive receive = Context.RedPacketReceives.AsNoTracking().FirstOrDefault(p => p.RedPacketId == entity.RedPacketId && p.ClientId == clientId);
                    if (receive != null)
                    {
                        errMsg = "已领取";
                    }
                    else
                    {

                        using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
                        {
                            try
                            {
                                receive = new RedPacketReceive();
                                receive.ClientId = clientId;
                                receive.RedPacketId = entity.RedPacketId;
                                entity.ReceiveCount += 1;
                                if (entity.Count == entity.ReceiveCount)
                                {
                                    //最后一份
                                    receiveMoney = entity.Over;
                                    entity.Status = RedPacketStatus.End;
                                }
                                else if (entity.Type == RedPacketTypeEnum.Lucky)
                                {
                                    //拼手气
                                    decimal curMoney = entity.Over;

                                    Random rd = new Random();
                                    receiveMoney = Math.Round(curMoney * ((decimal)rd.Next(1, 10000) / (decimal)10000));

                                    //要确保剩余的钱足够剩余的人分
                                    while (curMoney - receiveMoney < (entity.Count - entity.ReceiveCount) * (decimal)0.01)
                                    {
                                        //钱不够分
                                        receiveMoney = receiveMoney / 2;
                                        if (receiveMoney <= (decimal)0.01)
                                        {
                                            receiveMoney = (decimal)0.01;
                                            break;
                                        }
                                        continue;
                                    }

                                }
                                else
                                {
                                    receiveMoney = entity.Money / entity.Count;
                                }
                                receive.SnatchMoney = receiveMoney;//领取金额
                                entity.Over -= receiveMoney;//更新剩余金额
                                Context.RedPacketReceives.Add(receive);
                                Update<RedPacket>(entity, "RedPacketId", new List<string>() { "ReceiveCount", "Status", "Over", "UpdateTime" });
                                Context.SaveChanges();
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                result = false;
                                errMsg = "保存数据异常";
                                transaction.Rollback();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                errMsg = "未知异常";
            }
            finally
            {
                _cacheManager.Remove(operKey);
            }
            if (!result)
            {
                receiveMoney = 0;
            }
            else
            {
                errMsg = "领取成功";
            }
            return ObjectGenericityResult<decimal>(result, receiveMoney, errMsg);
        }
    }

    public class GroupReadPacketsResponse
    {

        public Guid RedPacketId { get; set; }


        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

        /// <summary>
        /// 红包发送者
        /// </summary>
        public Guid ClientId { get; set; }

        /// <summary>
        /// 祝福语(尾数)
        /// </summary>
        public string Greeting { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }


        /// <summary>
        /// 红包类型
        /// </summary>
        public RedPacketTypeEnum Type { get; set; }


        /// <summary>
        /// 红包个数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 红包金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 剩余红包金额
        /// </summary>
        public decimal Over { get; set; }


        /// <summary>
        /// 红包状态
        /// </summary>
        public RedPacketStatus Status { get; set; }


        public enum RedPacketTypeEnum
        {
            [Description("拼手气红包")]
            Lucky = 0,
            [Description("普通红包")]
            Normal = 1,
        }

        public enum RedPacketStatus
        {
            [Description("发出")]
            Send = 0,
            [Description("已领完")]
            End = 1,
            [Description("已退款")]
            Refund = 2,
            [Description("未领完退款")]
            NotEndRefund = 3,
        }

    }

    public class CreateGroupReadPacketsRequest
    {
        /// <summary>
        /// 发红包者ClientId
        /// </summary>
        public Guid SenderClientId { get; set; }
        /// <summary>
        /// 群Id
        /// </summary>
        public Guid GroupId { get; set; }
        /// <summary>
        /// 红包总金额
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// 红包总个数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 红包类型 0:拼手气红包;1:普通红包
        /// </summary>
        public int ReadPacketsType { get; set; }

        /// <summary>
        /// 祝福语
        /// </summary>
        public string Greeting { get; set; }
    }

    public class ReceiveGroupRedPacketsRequest
    {
        public Guid RedPacketId { get; set; }

        public Guid ClientId { get; set; }
    }

}