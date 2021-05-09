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
using System.Text.RegularExpressions;
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

        private readonly ICacheManager _cacheManager = CacheManager.singleCache;

        /// <summary>
        /// 发群红包
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateGroupRedPacket")]
        public ServiceResult<GroupRedPacketResponse> CreateGroupRedPacket(CreateGroupRedPacketsRequest request)
        {
            GroupRedPacketResponse returnObject = null;
            bool result = true;
            string errMsg = "";
            if (request.Money < request.Count * (decimal)0.01)
            {
                errMsg = "红包金额不足,要确保每人能领0.01";
                result = false;
                return ObjectGenericityResult<GroupRedPacketResponse>(result, returnObject, errMsg);
            }
            else if (request.Money > 1800)
            {
                errMsg = "红包最大金额为1800";
                result = false;
                return ObjectGenericityResult<GroupRedPacketResponse>(result, returnObject, errMsg);
            }
            Client sender = Context.Clients.AsNoTracking().FirstOrDefault(p => p.ClientId == request.SenderClientId);
            if (sender != null && sender.IsEnable && sender.Coin >= request.Money)
            {
                using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
                {
                    try
                    {
                        RedPacket entity = new RedPacket();
                        entity.ClientId = request.SenderClientId;
                        entity.GroupId = request.GroupId;
                        entity.SendTime = DateTime.Now;
                        entity.Count = request.Count;
                        entity.Money = request.Money;
                        entity.Over = request.Money;
                        entity.Greeting = request.Greeting;
                        //默认1
                        //entity.LuckIndex = request.LuckIndex;
                        entity.Type = (RedPacketTypeEnum)request.ReadPacketsType;
                        returnObject = ConvertHelper.ConvertToModel<RedPacket, GroupRedPacketResponse>(entity);
                        Context.RedPackets.Add(entity);
                        Context.SaveChanges();
                        sender.Coin -= request.Money;
                        sender.UpdateTime = DateTime.Now;
                        Update<Client>(sender, "ClientId", new List<string>() { "Coin", "UpdateTime" });
                        Context.SaveChanges();
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        result = false;
                        returnObject = null;
                        transaction.Rollback();
                    }
                }
            }
            else
            {
                result = false;
                errMsg = "用户无效或余额不足";
            }
            return ObjectGenericityResult<GroupRedPacketResponse>(result, returnObject, errMsg);
        }

        /// <summary>
        /// 领取群红包
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpPost("ReceiveRedPacket")]
        public ServiceResult<decimal> ReceiveRedPacket(ReceiveGroupRedPacketsRequest requestObj)
        {
            bool result = true;
            string errMsg = "";
            Guid redPacketId = requestObj.RedPacketId;
            Guid clientId = requestObj.ClientId;
            string operKey = redPacketId.ToString();
            decimal receiveMoney = 0;
            try
            {
                //当前红包空闲
                while (_cacheManager.Get<object>(operKey) != null)
                {
                    Thread.Sleep(200);
                }
                _cacheManager.Set(operKey, clientId.ToString(), 2);//2分钟过期
                RedPacket entity = Context.RedPackets.FirstOrDefault(p => p.RedPacketId == redPacketId);
                //Client reveiver = Context.Clients.AsNoTracking().FirstOrDefault(p => p.ClientId == clientId);

                int luckIndex = entity.LuckIndex;
                List<string> luckNumbers = new List<string>();

                string greeting = entity.Greeting + "";
                Regex regex = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
                if (greeting.Length >= 1 && greeting.Length <= 3 && regex.IsMatch(greeting))
                {
                    if (greeting.Split().Distinct().Count() == greeting.Length)
                    {
                        greeting.Split().ToList().ForEach(c =>
                        {
                            luckNumbers.Add(c.ToString());
                        });
                    }

                }
                //greeting= Regex.Replace(greeting, @"[^\d,]*", "");


                //string[] greetingArr = greeting.Split(',');
                //foreach (string g in greetingArr)
                //{
                //    if (g.Trim().Length == 1)
                //    {
                //        luckNumbers.Add(g.Trim());
                //    }
                //}


                //if (reveiver == null)
                //{
                //    result = false;
                //    errMsg = "无效用户不能参与抽奖";
                //} else
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
                        result = false;
                        errMsg = "已领取,不能重复领取";
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
                                //receive.NickName = reveiver.NickName;
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
                                    receiveMoney = Math.Floor(curMoney * ((decimal)rd.Next(1, 1000000) / (decimal)1000000));
                                    //lucknumber
                                    decimal lucknumber = (decimal)rd.Next(1, 99) / 100;

                                    receiveMoney += lucknumber;

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
                                receiveMoney = Math.Round(receiveMoney, 2);
                                receive.SnatchMoney = receiveMoney;//领取金额
                                if (entity.Type == RedPacketTypeEnum.Lucky && luckIndex >= 1 && luckIndex <= 3 && luckNumbers != null)
                                {
                                    string temp = receiveMoney.ToString().Replace(".", "");
                                    string luckIndexChar = temp.Substring(temp.Length - luckIndex, 1);
                                    if (luckNumbers.Contains(luckIndexChar))
                                    {
                                        receive.IsLuck = true;
                                        receive.LuckNumber = luckIndexChar;
                                    }
                                }
                                entity.Over -= receiveMoney;//更新剩余金额
                                Context.RedPacketReceives.Add(receive);
                                int addCount = Context.SaveChanges();
                                int updateCount = Update<RedPacket>(entity, "RedPacketId", new List<string>() { "ReceiveCount", "Status", "Over", "UpdateTime" });
                                Context.SaveChanges();

                                if (entity.Type == RedPacketTypeEnum.Lucky && luckIndex >= 1 && luckIndex <= 3 && luckNumbers != null)
                                {
                                    //LuckNumber
                                    string strsql = $@"  UPDATE redpackets SET LuckNumbers=(SELECT GROUP_CONCAT(DISTINCT LuckNumber) FROM redpacketreceives WHERE RedPacketId='{entity.RedPacketId}' AND IsLuck )  WHERE RedPacketId='{entity.RedPacketId}' ";
                                    int exeCount = Context.Database.ExecuteSqlCommand(strsql);
                                }
                                if (entity.ReceiveCount >= entity.Count)
                                {
                                    //抢完红包,决定谁是手气王
                                    string strsql = $@" UPDATE redpacketreceives SET isWin=TRUE WHERE RedPacketId='{entity.RedPacketId}' 
 AND SnatchMoney = (SELECT MAX(SnatchMoney) FROM (SELECT SnatchMoney FROM redpacketreceives WHERE RedPacketId='{entity.RedPacketId}')v) ";
                                    int exeCount = Context.Database.ExecuteSqlCommand(strsql);
                                }
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

        /// <summary>
        /// 获取群红包信息
        /// </summary>
        /// <param name="redPacketId">redPacketId</param>
        /// <returns></returns>
        [HttpGet("GetGroupRedPacket")]
        public ServiceResult<GroupRedPacketResponse> GetGroupRedPacket(Guid redPacketId)
        {
            bool result = false;
            string msg = "";
            RedPacket entity = Context.RedPackets.AsNoTracking().FirstOrDefault(p => p.RedPacketId == redPacketId);
            GroupRedPacketResponse returnObject = null;
            if (entity != null)
            {
                result = true;
                returnObject = ConvertHelper.ConvertToModel<RedPacket, GroupRedPacketResponse>(entity);

                IList<RedPacketReceive> details = Context.RedPacketReceives.AsNoTracking().Where(p => p.RedPacketId == redPacketId).ToList();
                if (details != null && details.Count > 0)
                {
                    returnObject.RedPacketReceives = ConvertHelper.ConvertToList<RedPacketReceive, RedPacketReceiveResponse>(details);
                }
            }
            else
            {
                result = false;
                msg = "无法获取红包信息";
            }
            return ObjectGenericityResult<GroupRedPacketResponse>(result, returnObject, msg);
        }

        /// <summary>
        /// 获取Client的红包获取记录
        /// </summary>
        /// <param name="redPacketId">RedPacketId</param>
        /// <param name="clientId">ClientId</param>
        /// <returns></returns>
        [HttpGet("GetGroupRedPacketClientReceive")]
        public ServiceResult<RedPacketReceiveResponse> GetGroupRedPacketClientReceive(Guid redPacketId, Guid clientId)
        {
            bool result = false;
            string msg = "";
            RedPacketReceive entity = Context.RedPacketReceives.AsNoTracking().FirstOrDefault(p => p.RedPacketId == redPacketId && p.ClientId == clientId);
            RedPacketReceiveResponse returnObject = null;
            if (entity != null)
            {
                result = true;
                returnObject = ConvertHelper.ConvertToModel<RedPacketReceive, RedPacketReceiveResponse>(entity);

            }
            else
            {
                result = false;
                msg = "无接收红包信息";
            }
            return ObjectGenericityResult<RedPacketReceiveResponse>(result, returnObject, msg);

        }



        /// <summary>
        /// 发个人红包
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("CreateClientRedPacket")]
        public ServiceResult<ClientRedPacketResponse> CreateClientRedPacket(CreateClientRedPacketsRequest request)
        {
            ClientRedPacketResponse returnObject = null;
            bool result = true;
            string errMsg = "";
            if (request.Money < (decimal)0.01)
            {
                errMsg = "红包最小金额为0.01";
                result = false;
                return ObjectGenericityResult<ClientRedPacketResponse>(result, returnObject, errMsg);
            }
            else if (request.Money > 1800)
            {
                errMsg = "红包最大金额为1800";
                result = false;
                return ObjectGenericityResult<ClientRedPacketResponse>(result, returnObject, errMsg);
            }
            Client sender = Context.Clients.AsNoTracking().FirstOrDefault(p => p.ClientId == request.SenderClientId);
            if (sender != null && sender.IsEnable && sender.Coin >= request.Money)
            {
                using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
                {
                    try
                    {
                        RedPacket entity = new RedPacket();
                        entity.ClientId = request.SenderClientId;
                        entity.GroupId = Guid.Empty;
                        entity.SendTime = DateTime.Now;
                        entity.Count = 1;
                        entity.Money = request.Money;
                        entity.Over = request.Money;
                        entity.Greeting = request.Greeting;
                        //默认1
                        //entity.LuckIndex = request.LuckIndex;
                        entity.Type = RedPacketTypeEnum.Normal;
                        returnObject = ConvertHelper.ConvertToModel<RedPacket, ClientRedPacketResponse>(entity);
                        Context.RedPackets.Add(entity);
                        Context.SaveChanges();
                        sender.Coin -= request.Money;
                        sender.UpdateTime = DateTime.Now;
                        Update<Client>(sender, "ClientId", new List<string>() { "Coin", "UpdateTime" });
                        Context.SaveChanges();
                        transaction.Commit();

                    }
                    catch (Exception ex)
                    {
                        result = false;
                        returnObject = null;
                        transaction.Rollback();
                    }
                }
            }
            else
            {
                result = false;
                errMsg = "用户无效或余额不足";
            }
            return ObjectGenericityResult<ClientRedPacketResponse>(result, returnObject, errMsg);
        }




        /// <summary>
        /// 获取Client红包信息
        /// </summary>
        /// <param name="redPacketId">redPacketId</param>
        /// <returns></returns>
        [HttpGet("GetClientRedPacket")]
        public ServiceResult<ClientRedPacketResponse> GetClientRedPacket(Guid redPacketId)
        {
            bool result = false;
            string msg = "";
            RedPacket entity = Context.RedPackets.AsNoTracking().FirstOrDefault(p => p.RedPacketId == redPacketId);
            ClientRedPacketResponse returnObject = null;
            if (entity != null)
            {
                result = true;
                returnObject = ConvertHelper.ConvertToModel<RedPacket, ClientRedPacketResponse>(entity);
                RedPacketReceive detail = Context.RedPacketReceives.AsNoTracking().FirstOrDefault(p => p.RedPacketId == redPacketId);
                if (detail != null )
                {
                    returnObject.RedPacketReceive = ConvertHelper.ConvertToModel<RedPacketReceive, RedPacketReceiveResponse>(detail);
                }
            }
            else
            {
                result = false;
                msg = "无法获取红包信息";
            }
            return ObjectGenericityResult<ClientRedPacketResponse>(result, returnObject, msg);
        }




    }


}