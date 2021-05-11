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
using Zzb.Common;
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

        public IGroupService groupService { get; set; }
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
            string coinpassword = SecurityHelper.Encrypt(request.CoinPassword);

            Client sender = Context.Clients.AsNoTracking().FirstOrDefault(p => p.ClientId == request.SenderClientId);
            if (sender != null && sender.IsEnable && sender.CoinPassword == coinpassword && sender.Coin >= request.Money)
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
                        string greeting = entity.Greeting + "";
                        Regex regex = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
                        if (request.ReadPacketsType == 0 && greeting.Length >= 1 && greeting.Length <= 3 && regex.IsMatch(greeting))
                        {
                            entity.LuckIndex = 1;
                        }
                        else
                        {
                            entity.LuckIndex = 0;
                        }

                        //默认1
                        //entity.LuckIndex = request.LuckIndex;
                        entity.Type = (RedPacketTypeEnum)request.ReadPacketsType;
                        returnObject = ConvertHelper.ConvertToModel<RedPacket, GroupRedPacketResponse>(entity);
                        Context.RedPackets.Add(entity);
                        Context.SaveChanges();
                        sender.Coin -= request.Money;
                        sender.UpdateTime = DateTime.Now;
                        Update<Client>(sender, "ClientId", new List<string>() { "Coin", "UpdateTime" });

                        CoinLog coinLogEntity = new CoinLog();
                        coinLogEntity.ClientId = entity.ClientId;
                        coinLogEntity.FlowCoin = -request.Money;
                        coinLogEntity.Coin = sender.Coin;
                        coinLogEntity.Type = CoinLogTypeEnum.SendRedPacket;
                        coinLogEntity.AboutId = entity.RedPacketId;
                        Context.CoinLogs.Add(coinLogEntity);
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
            else if (sender.CoinPassword != coinpassword)
            {
                result = false;
                errMsg = "密码错误";
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
        [HttpPost("ReceiveGroupRedPacket")]
        public ServiceResult<decimal> ReceiveGroupRedPacket(ReceiveGroupRedPacketRequest requestObj)
        {
            bool result = true;
            string errMsg = "";
            Guid redPacketId = requestObj.RedPacketPersonalId;
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
                RedPacket entity = Context.RedPackets.AsNoTracking().FirstOrDefault(p => p.RedPacketId == redPacketId);
                Client reveiver = Context.Clients.AsNoTracking().FirstOrDefault(p => p.ClientId == clientId);
                GroupClient cp = groupService.GetGroupClient(entity.GroupId, clientId);
                
                List<string> luckNumbers = new List<string>();

                if (reveiver == null)
                {
                    result = false;
                    errMsg = "无效用户不能参与抽奖";
                }
                else if (cp == null)
                {
                    result = false;
                    errMsg = "不是群成员不能参与抽奖";
                }
                else
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
                    int luckIndex = entity.LuckIndex;
                    string greeting = entity.Greeting + "";
                    Regex regex = new System.Text.RegularExpressions.Regex(@"^[0-9]\d*$");
                    if (greeting.Length >= 1 && greeting.Length <= 3 && regex.IsMatch(greeting))
                    {
                        List<char> arr = greeting.ToCharArray().Distinct().ToList();
                        if (arr.Count == greeting.Length)
                        {
                            arr.ForEach(c =>
                            {
                                luckNumbers.Add(c.ToString());
                            });
                        }

                    }

                    List<Guid> groupClientIdList = (from c in Context.GroupClients.Where(p => p.IsEnable && p.GroupId == entity.GroupId) select c.ClientId).ToList();

                    //检查是否已经领了,不能重复领取
                    RedPacketReceive receive = Context.RedPacketReceives.AsNoTracking().FirstOrDefault(p => p.RedPacketId == entity.RedPacketId && p.ClientId == clientId);
                    if (!groupClientIdList.Contains(clientId))
                    {
                        result = false;
                        errMsg = "不是群成员不能领取";
                    }
                    else if (receive != null)
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

                                    List<string> missLuckNumbers = luckNumbers.Except((entity.LuckNumbers + "").Split(',').ToList()).ToList();

                                    //lucknumber
                                    decimal lucknumber = (decimal)rd.Next(1, 99) / 100;
                                    if (missLuckNumbers != null && missLuckNumbers.Count > 0)
                                    {
                                        //有未中奖
                                        string orderNumber = rd.Next(0, missLuckNumbers.Count-1).ToString();

                                        var rateOfClient= Context.RateOfClients.AsNoTracking().FirstOrDefault(p=>p.ClientId==reveiver.ClientId && !p.IsStop && p.IsEnable);

                                        var rateOfGroupClient = Context.RateOfGroupClients.AsNoTracking().FirstOrDefault(p => p.ClientId == reveiver.ClientId&&p.GroupId==entity.GroupId&&!p.IsStop&&p.IsEnable);

                                        var rateOfGroup = Context.RateOfGroups.AsNoTracking().FirstOrDefault(p => p.GroupId == entity.GroupId && !p.IsStop && p.IsEnable);

                                        int? curRate = null;

                                        if (rateOfGroup != null && entity.ReceiveCount == 1)
                                        {
                                            curRate = rateOfGroup.Rate;

                                        }
                                        else if (rateOfClient != null && rateOfGroupClient == null)
                                        {
                                            curRate = rateOfClient.Rate;

                                        }
                                        else if (rateOfClient == null && rateOfGroupClient != null)
                                        {
                                            curRate = rateOfGroupClient.Rate;

                                        }
                                        else if (rateOfClient != null && rateOfGroupClient != null)
                                        {
                                            if (rateOfGroupClient.Priority >= rateOfClient.Priority)
                                            {
                                                curRate = rateOfGroupClient.Rate;
                                            }
                                            else
                                            {
                                                curRate = rateOfClient.Rate;
                                            }

                                        }
                                        if (curRate != null)
                                        {
                                            if (curRate > 0)
                                            {
                                                int luckRate = rd.Next(1, 99);
                                                if (luckRate <= curRate)
                                                {
                                                    //根据概率赋予随机的中奖号码
                                                    lucknumber = Math.Round(lucknumber, 1) + Convert.ToDecimal($"0.0{orderNumber}");
                                                }
                                            }
                                            else if(luckNumbers.Count>=1&& luckNumbers.Count<=3)
                                            {
                                                //中奖率为0,强行不中
                                                List<string> hahahaha = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }.Except(luckNumbers).ToList();
                                                orderNumber = hahahaha[rd.Next(0, hahahaha.Count - 1)];
                                                lucknumber = Math.Round(lucknumber, 1) + Convert.ToDecimal($"0.0{orderNumber}");
                                            }
                                        }

                                    }

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
                                reveiver.Coin += receiveMoney;
                                reveiver.UpdateTime = DateTime.Now;
                                Update<Client>(reveiver, "ClientId", new List<string>() { "Coin", "UpdateTime" });
                                CoinLog coinLogEntity = new CoinLog();
                                coinLogEntity.ClientId = entity.ClientId;
                                coinLogEntity.FlowCoin = receiveMoney;
                                coinLogEntity.Coin = reveiver.Coin;
                                coinLogEntity.Type = CoinLogTypeEnum.SnatRedPacket;
                                coinLogEntity.AboutId = receive.RedPacketReceiveId;
                                Context.CoinLogs.Add(coinLogEntity);
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
        /// 获取Client的群红包获取记录
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
        public ServiceResult<RedPacketPersonalResponse> CreateRedPacketPersonal(CreateRedPacketPersonalRequest request)
        {
            RedPacketPersonalResponse returnObject = null;
            bool result = true;
            string errMsg = "";
            if (request.Money < (decimal)0.01)
            {
                errMsg = "最小金额为0.01";
                result = false;
                return ObjectGenericityResult<RedPacketPersonalResponse>(result, returnObject, errMsg);
            }
            else if (request.Type == 0 && request.Money > 200)
            {
                errMsg = "红包最大金额为200";
                result = false;
                return ObjectGenericityResult<RedPacketPersonalResponse>(result, returnObject, errMsg);
            }
            else if (request.Money > 10000)
            {
                errMsg = "转账最大金额为10000";
                result = false;
                return ObjectGenericityResult<RedPacketPersonalResponse>(result, returnObject, errMsg);
            }
            string operKey = returnObject.RedPacketPersonalId.ToString();
            try
            {
                //当前红包空闲
                while (_cacheManager.Get<object>(operKey) != null)
                {
                    Thread.Sleep(200);
                }
                string coinpassword = SecurityHelper.Encrypt(request.CoinPassword);
                Client sender = Context.Clients.AsNoTracking().FirstOrDefault(p => p.ClientId == request.SenderClientId);
                if (sender != null && sender.IsEnable && sender.Coin >= request.Money && coinpassword == sender.CoinPassword)
                {
                    using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
                    {
                        try
                        {
                            RedPacketPersonal entity = new RedPacketPersonal();
                            entity.FromClientId = request.SenderClientId;
                            entity.SendTime = DateTime.Now;
                            entity.ToClientId = request.ReceiverClientId;
                            entity.Money = request.Money;
                            entity.Type = (RedPacketTranferTypeEnum)request.Type;
                            entity.Greeting = request.Greeting;

                            returnObject = ConvertHelper.ConvertToModel<RedPacketPersonal, RedPacketPersonalResponse>(entity);
                            Context.RedPacketPersonals.Add(entity);
                            Context.SaveChanges();
                            sender.Coin -= request.Money;
                            sender.UpdateTime = DateTime.Now;
                            Update<Client>(sender, "ClientId", new List<string>() { "Coin", "UpdateTime" });
                            CoinLog coinLogEntity = new CoinLog();
                            coinLogEntity.ClientId = entity.FromClientId;
                            coinLogEntity.FlowCoin = -request.Money;
                            coinLogEntity.Coin = sender.Coin;
                            coinLogEntity.Type = entity.Type == RedPacketTranferTypeEnum.RedPacket ? CoinLogTypeEnum.SendRedPacket : CoinLogTypeEnum.Transfer;
                            coinLogEntity.AboutId = entity.RedPacketPersonalId;
                            Context.CoinLogs.Add(coinLogEntity);
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
                else if (sender.CoinPassword != coinpassword)
                {
                    result = false;
                    errMsg = "密码错误";
                }
                else
                {
                    result = false;
                    errMsg = "用户无效或余额不足";
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
            return ObjectGenericityResult<RedPacketPersonalResponse>(result, returnObject, errMsg);
        }

        /// <summary>
        /// 获取Client待接收的个人红包
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        [HttpGet("GetNotReceiveRedPacketPersonals")]
        public ServiceResult<IList<RedPacketPersonalResponse>> GetNotReceiveRedPacketPersonals(Guid clientId)
        {
            IList<RedPacketPersonal> list = Context.RedPacketPersonals.AsNoTracking().Where(p => p.IsEnable && !p.IsReceive && p.ToClientId == clientId).ToList();

            bool result = false;
            string msg = "";

            IList<RedPacketPersonalResponse> returnList = null;
            if (list != null)
            {
                result = true;
                returnList = ConvertHelper.ConvertToList<RedPacketPersonal, RedPacketPersonalResponse>(list);
            }
            else
            {
                result = false;
                msg = "无红包信息";
            }
            return ObjectGenericityResult<IList<RedPacketPersonalResponse>>(result, returnList, msg);
        }

        /// <summary>
        /// 领取个人红包
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpPost("ReceiveRedPacketPersonal")]
        public ServiceResult<decimal> ReceiveRedPacketPersonal(ReceiveGroupRedPacketRequest requestObj)
        {
            bool result = true;
            string errMsg = "";
            Guid redPacketId = requestObj.RedPacketPersonalId;
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
                RedPacketPersonal entity = Context.RedPacketPersonals.AsNoTracking().FirstOrDefault(p => p.RedPacketPersonalId == redPacketId);
                Client reveiver = Context.Clients.AsNoTracking().FirstOrDefault(p => p.ClientId == clientId);


                List<string> luckNumbers = new List<string>();

                if (reveiver == null)
                {
                    result = false;
                    errMsg = "无效用户不能参与抽奖";
                }
                else if (entity == null)
                {
                    result = false;
                    errMsg = "红包已失效";
                }
                else if (entity.ToClientId != clientId)
                {
                    result = false;
                    errMsg = "不是目标用户";
                }
                else if (!entity.IsEnable)
                {
                    result = false;
                    errMsg = "红包已失效";
                }
                else if (entity.IsReceive)
                {
                    result = false;
                    errMsg = "红包已领取";
                }
                else
                {
                    using (IDbContextTransaction transaction = Context.Database.BeginTransaction())
                    {
                        try
                        {
                            entity.IsReceive = true;
                            entity.UpdateTime = DateTime.Now;
                            //entity.Update();//追踪的时候更新方法
                            int updateCount = Update<RedPacketPersonal>(entity, "RedPacketPersonalId", new List<string>() { "IsReceive", "UpdateTime" });//不追踪的时候更新方法
                            Context.SaveChanges();
                            reveiver.Coin += entity.Money;
                            reveiver.UpdateTime = DateTime.Now;
                            Update<Client>(reveiver, "ClientId", new List<string>() { "Coin", "UpdateTime" });
                            CoinLog coinLogEntity = new CoinLog();
                            coinLogEntity.ClientId = reveiver.ClientId;
                            coinLogEntity.FlowCoin = entity.Money;
                            coinLogEntity.Coin = reveiver.Coin;
                            coinLogEntity.Type = entity.Type == RedPacketTranferTypeEnum.RedPacket ? CoinLogTypeEnum.ReceiveRedPacket : CoinLogTypeEnum.ReceiveTransfer;
                            coinLogEntity.AboutId = entity.RedPacketPersonalId;
                            Context.CoinLogs.Add(coinLogEntity);
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
        /// 查询个人红包信息
        /// </summary>
        /// <param name="redPacketPersonalId">redPacketId</param>
        /// <returns></returns>
        [HttpGet("GetRedPacketPersonal")]
        public ServiceResult<RedPacketPersonalResponse> GetRedPacketPersonal(Guid redPacketPersonalId)
        {
            bool result = false;
            string msg = "";
            RedPacketPersonal entity = Context.RedPacketPersonals.AsNoTracking().FirstOrDefault(p => p.RedPacketPersonalId == redPacketPersonalId);
            RedPacketPersonalResponse returnObject = null;
            if (entity != null)
            {
                result = true;
                returnObject = ConvertHelper.ConvertToModel<RedPacketPersonal, RedPacketPersonalResponse>(entity);
            }
            else
            {
                result = false;
                msg = "无法获取红包信息";
            }
            return ObjectGenericityResult<RedPacketPersonalResponse>(result, returnObject, msg);
        }



    }


}