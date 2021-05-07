﻿
using Liaoxin.Business;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using LIaoxin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Context;
using Zzb.Mvc;
using Zzb.Utility;
using static Liaoxin.ViewModel.ClientViewModel;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : LiaoxinBaseController
    {


        
        public IClientService clientService { get; set; }        


        /// <summary>
        /// 获取客户
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetClient")]
        public ServiceResult<ClientBaseInfoResponse> GetClient()
        {
            var entity = clientService.GetClient();
            return (ServiceResult<ClientBaseInfoResponse>)Json(() =>
            {
                return ObjectGenericityResult<ClientBaseInfoResponse>(entity);
            }, "获取客户失败");

        }

        /// <summary>
        /// 客户手机号码登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("LoginByCode")]
        public ServiceResult LoginByCode(ClientLoginByCodeRequest request)
        {


            return Json(() =>
            {
                var entity = clientService.LoginByCode(request);
                _UserContext.SetUserContext(entity.ClientId, entity.HuanXinId, entity.LiaoxinNumber);
                string token = UserContext.Current.Token;
                return ObjectResult(token);
            }, "登录失败");

        }


        /// <summary>
        /// 客户账号登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public ServiceResult Login(ClientLoginRequest request)
        {
            return Json(() =>
            {
                var entity = clientService.Login(request);
                _UserContext.SetUserContext(entity.ClientId, entity.HuanXinId, entity.LiaoxinNumber);
                string token = UserContext.Current.Token;
                return ObjectResult(token);
            }, "登录失败");

        }


        /// <summary>
        /// 修改客户密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        public ServiceResult ChangePassword(ClientChangePasswordRequest request)
        {

            return Json(() =>
            {
                var res = clientService.ChangePassword(request);
                return ObjectResult(res);
            }, "修改密码失败");

        }


        /// <summary>
        /// 修改昵称
        /// </summary>        
        /// <returns></returns>
        [HttpPost("ModifyNickName")]
        public ServiceResult ModifyNickName(SetClientNickNameRequest request)
        {
            return Json(() =>
            {

                var entity = Context.Clients.Where(c => c.ClientId == CurrentClientId).First();
                entity.NickName = request.nickName;
                Context.Clients.Update(entity);
                var res = HuanxinClientRequest.ModifyNickName(CurrentHuanxinId,request.nickName);
                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    return ObjectResult(Context.SaveChanges() > 0);
                }
                else
                {
                    return res;
                }

            }, "修改昵称失败");
        }


        /// <summary>
        /// 修改头像
        /// </summary>        
        /// <returns></returns>
        [HttpPost("ModifyCover")]
        public ServiceResult ModifyCover(int coverId)
        {
            return Json(() =>
            {
                var entity = Context.Clients.Where(c => c.ClientId == CurrentClientId).First();
                entity.Cover = coverId;
                Context.Clients.Update(entity);     
                return ObjectResult(Context.SaveChanges() > 0);             
            }, "修改头像失败");

        }
        

        /// <summary>
        /// 基本设置修改:个性签名/震动/提醒/字体大小等字段普通更改
        /// </summary>        
        /// <returns></returns>
        [HttpPost("ModifyBaseInfo")]
        public ServiceResult ModifyBaseInfo(ClientBaseInfoResponse request)
        {
            return Json(() =>
            {
                if (request.ClientId != CurrentClientId)
                {
                    throw new ZzbException("你想干嘛?");
                }
                List<string> ingores = new List<string>();
                ingores.Add("Cover");
                ingores.Add("HuanXinId");
                ingores.Add("RealName");
                ingores.Add("UniqueNo");
                ingores.Add("UniqueFrontImg");
                ingores.Add("UniqueBackImg");
                ingores.Add("Password");
                ingores.Add("CoinPassword");
                ingores.Add("LiaoxinNumber");
                ingores.Add("NickName");
                ingores.Add("Coin");
                ingores.Add("Telephone");
                IList<string> updateFieldList = GetPostBodyFiledKey(ingores);
                Client entity = ConvertHelper.ConvertToModel<ClientBaseInfoResponse, Client>(request, updateFieldList);
                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"进行了基本设置修改"));
                return ObjectResult(base.Update(entity, "ClientId", updateFieldList) > 0);
            }, "修改失败");

        }
        
        /// <summary>
        /// 修改客户资金密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("ChangeCoinPassword")]
        public ServiceResult ChangeCoinPassword(ClientChangeCoinPasswordRequest request)
        {
            return Json(() =>
            {
                var res = clientService.ChangeCoinPassword(request);
                return ObjectResult(res);
            }, "修改密码失败");

        }

        /// <summary>
        /// 客户(新的好友添加)列表
        /// </summary>       
        /// <returns></returns>
        [HttpPost("ApplFriends")]
        public ServiceResult<List<ClientAddDetailResponse>> ApplyFriends()
        {
            return (ServiceResult<List<ClientAddDetailResponse>>)Json(() =>
            {
                
                var entities = Context.ClientAddDetails.Where(c => c.FromClientId == CurrentClientId && c.CreateTime > DateTime.Now.AddMonths(-1)).ToList();
                List<ClientAddDetailResponse> lis = new List<ClientAddDetailResponse>();
                entities.ForEach(e =>
                {
                    lis.Add(new ClientAddDetailResponse()
                    {
                        AddRemark = e.AddRemark,
                        Cover = e.ToClient.Cover,
                        CreateTime = e.CreateTime,
                        HuanxinId = e.ToClient.HuanXinId,
                        LiaoxinNumber = e.ToClient.LiaoxinNumber,
                        NickName = e.ToClient.NickName,
                        Status = e.Status,
                        StatusName = e.Status.ToDescriptionString()
                    });
                });
                return ListGenericityResult(lis);
            }, "获取添加客户列表失败");

        }


        /// <summary>
        /// 客户好友列表
        /// </summary>       
        /// <returns></returns>
        [HttpPost("ClientFriends")]
        public ServiceResult<List<ClientFriendResponse>> ClientFriends()
        {
            return (ServiceResult<List<ClientFriendResponse>>)Json(() =>
            {
                var clientRelationEntity = Context.ClientRelations.Where(cd => cd.ClientId == CurrentClientId && cd.RelationType == ClientRelation.RelationTypeEnum.Friend).FirstOrDefault();

                List<ClientFriendResponse> lis = new List<ClientFriendResponse>();
                clientRelationEntity.ClientRelationDetail.ForEach(e =>
                {
                    lis.Add(new ClientFriendResponse()
                    {
                        Cover = e.Client.Cover,
                        HuanxinId = e.Client.HuanXinId,
                        LiaoxinNumber = e.Client.LiaoxinNumber,
                        NickName = e.Client.NickName,
                        ClientRemark = e.ClientRemark,
                    });
                });
                return ListGenericityResult(lis);
            }, "获取好友列表失败");

        }

        /// <summary>
        /// 客户好友详细
        /// </summary>       
        /// <returns></returns>
        [HttpPost("ClientFriendDetail")]
        public ServiceResult<ClientFriendDetailResponse> ClientFriendDetail(ApplyAddFriendRequest request)
        {
            return (ServiceResult<ClientFriendDetailResponse>)Json(() =>
            {

                var shipEntity = Context.ClientRelationDetails.Where
                 (crd => crd.ClientRelation.ClientId == CurrentClientId &&
                 crd.ClientRelation.RelationType == ClientRelation.RelationTypeEnum.Friend &&
                 crd.ClientId == request.ClientId).FirstOrDefault();
                if (shipEntity == null)
                {
                    throw new ZzbException("不存在这个好友");
                }
                var mutipleCount = Context.GroupClients.Where(g => g.ClientId == request.ClientId || g.ClientId == CurrentClientId).
                Select(g => new { g.ClientId, g.GroupId }).
                GroupBy(g => new { g.GroupId }).
                Select(g => new { Key = g.Key, Count = g.Count() }).Where(g => g.Count > 1).Count();

                ClientFriendDetailResponse response = new ClientFriendDetailResponse()
                {
                    CharacterSignature = shipEntity.Client.CharacterSignature,
                    ClientId = shipEntity.ClientId,
                    ClientRemark = shipEntity.ClientRemark,
                    Cover = shipEntity.Client.Cover,
                    HuanxinId = shipEntity.Client.HuanXinId,
                    LiaoxinNumber = shipEntity.Client.LiaoxinNumber,
                    NickName = shipEntity.Client.NickName,
                    Source = shipEntity.AddSource.ToDescriptionString(),
                    MutipleGroupCnt = mutipleCount,
                };


                return ObjectGenericityResult(response);
            }, "获取好友列表失败");

        }



        /// <summary>
        /// 添加好友申请
        /// </summary>       
        /// <returns></returns>
        [HttpPost("ApplyAddFriend")]
        public ServiceResult ApplyAddFriend(ApplyAddFriendRequest request)
        {
            return Json(() =>
            {
                var applyClientEntity = Context.Clients.Where(c => c.ClientId == request.ClientId).FirstOrDefault();
                if (applyClientEntity == null)
                {
                    throw new ZzbException("找不到申请客户");
                }
                var isApply = Context.ClientAddDetails.Where(c => c.FromClientId == CurrentClientId && c.ToClientId == applyClientEntity.ClientId
                && c.Status == ClientAddDetail.ClientAddDetailTypeEnum.StandBy).Count() > 0 ? true : false;
                if (isApply)
                {
                    throw new ZzbException("你已申请添加,需要再申请添加");
                }

                var isFriend =  Context.ClientRelationDetails.Where(crd => crd.ClientId == applyClientEntity.ClientId && crd.ClientRelation.ClientId == CurrentClientId && 
                crd.ClientRelation.RelationType == ClientRelation.RelationTypeEnum.Friend).Count() > 0;
                if (isFriend)
                {
                    throw new ZzbException("你已经添加了这个好友,无法申请添加");
                }

                ClientAddDetail detailEntity = new ClientAddDetail()
                {
                    ToClientId = applyClientEntity.ClientId,
                    FromClientId = CurrentClientId,
                    Status = ClientAddDetail.ClientAddDetailTypeEnum.StandBy,
                    AddRemark = request.AddRemark
                };
                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"申请添加好友[{applyClientEntity.LiaoxinNumber}]"));
                Context.ClientAddDetails.Add(detailEntity);
                return ObjectResult(Context.SaveChanges() > 0);
            }, "添加好友申请失败");
        }




        /// <summary>
        /// 拒绝添加好友
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("RejectAddFriend")]
        public ServiceResult RejectAddFriend(SureAddFriendRequest request)
        {
            return Json(() =>
            {
                var applyClientEntity = Context.Clients.Where(c => c.ClientId == request.ClientId).FirstOrDefault();
                if (applyClientEntity == null)
                {
                    throw new ZzbException("找不到客户");
                }
                var clientRelationEntity = Context.ClientRelations.Where(c => c.ClientId == CurrentClientId && c.RelationType == ClientRelation.RelationTypeEnum.Friend).FirstOrDefault();
                if (clientRelationEntity == null)
                {
                    clientRelationEntity = new ClientRelation();
                    clientRelationEntity.ClientId = CurrentClientId;
                    clientRelationEntity.RelationType = ClientRelation.RelationTypeEnum.Friend;
                    Context.ClientRelations.Add(clientRelationEntity);
                }
                var isExist = Context.ClientRelationDetails.Where(c => c.ClientRelationId == clientRelationEntity.ClientRelationId && c.ClientId == applyClientEntity.ClientId).Any();
                if (isExist)
                {
                    throw new ZzbException("你已添加这个好友,无法拒绝");
                }
                var addDetailEntity = Context.ClientAddDetails.Where(c => c.FromClientId == CurrentClientId && c.ToClientId == applyClientEntity.ClientId).FirstOrDefault();
                addDetailEntity.Status = ClientAddDetail.ClientAddDetailTypeEnum.Reject;
                return ObjectResult(Context.SaveChanges() > 0);
            }, "拒绝添加好友失败");

        }


        /// <summary>
        /// 确定添加好友
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        [HttpPost("SureAddFriend")]
        public ServiceResult SureAddFriend(SureAddFriendRequest request)
        {
            return Json(() =>
            {
                var applyClientEntity = Context.Clients.Where(c => c.ClientId == request.ClientId).FirstOrDefault();
                if (applyClientEntity == null)
                {
                    throw new ZzbException("找不到客户");
                }
                var canSure =  Context.ClientAddDetails.Where(cd => cd.FromClientId == CurrentClientId && cd.ToClientId == applyClientEntity.ClientId && cd.Status == ClientAddDetail.ClientAddDetailTypeEnum.StandBy).Any();
                if (!canSure)
                {
                    throw new ZzbException("请先申请,才能够添加好友");
                }

                var clientRelationEntity = Context.ClientRelations.Where(c => c.ClientId == CurrentClientId && c.RelationType == ClientRelation.RelationTypeEnum.Friend).FirstOrDefault();
                if (clientRelationEntity == null)
                {
                    clientRelationEntity = new ClientRelation();
                    clientRelationEntity.ClientId = CurrentClientId;
                    clientRelationEntity.RelationType = ClientRelation.RelationTypeEnum.Friend;
                    Context.ClientRelations.Add(clientRelationEntity);
                }
                var isExist =  Context.ClientRelationDetails.Where(c => c.ClientRelationId == clientRelationEntity.ClientRelationId && c.ClientId == applyClientEntity.ClientId).Any();
                if (isExist)
                {
                    throw new ZzbException("你已添加这个好友,无法添加");
                }

                ClientRelationDetail detailEntity = new ClientRelationDetail()
                {
                    ClientId = applyClientEntity.ClientId,
                    ClientRelationId = clientRelationEntity.ClientRelationId,
                    AddSource = request.AddSource
                };
              
         
                //环信确认添加              
                var res =  HuanxinClientRequest.AddFriend(CurrentHuanxinId, applyClientEntity.HuanXinId);
                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"确认添加好友[{applyClientEntity.LiaoxinNumber}]"));
                    Context.ClientRelationDetails.Add(detailEntity);
                   var addDetailEntity =    Context.ClientAddDetails.Where(c => c.FromClientId == CurrentClientId && c.ToClientId == applyClientEntity.ClientId && c.Status == ClientAddDetail.ClientAddDetailTypeEnum.StandBy).FirstOrDefault();
                    addDetailEntity.Status = ClientAddDetail.ClientAddDetailTypeEnum.Agree;
                    Context.ClientAddDetails.Update(addDetailEntity);
                    return ObjectResult(Context.SaveChanges() > 0);
                }
                else {
                    throw new ZzbException(res.Message);
                }
            
            }, "确认添加好友失败");

        }

        /// <summary>
        /// 删除好友
        /// </summary>        
        /// <returns></returns>

        [HttpPost("DeleteFriend")]
        public ServiceResult DeleteFriend(DeleteFriendRequest request)
        {
            return Json(() =>
            {
                var deleteClientEntity = Context.Clients.Where(c => c.ClientId == request.ClientId).FirstOrDefault();
                if (deleteClientEntity == null)
                {
                    throw new ZzbException("找不到要删除的用户");
                }
                var clientRelationEntity = Context.ClientRelationDetails.Where(cd =>
                cd.ClientRelation.ClientId == CurrentClientId &&
                cd.ClientRelation.RelationType == ClientRelation.RelationTypeEnum.Friend &&
                cd.ClientId == deleteClientEntity.ClientId).FirstOrDefault();
                if (clientRelationEntity == null)
                {
                    throw new ZzbException("这个用户不是你的好友,无法删除");
                }
                Context.ClientRelationDetails.Remove(clientRelationEntity);
                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"删除好友[{deleteClientEntity.LiaoxinNumber}]"));
                //环信确认删除                
                var res =  HuanxinClientRequest.DeleteFriend(clientRelationEntity.ClientRelation.Client.HuanXinId, deleteClientEntity.HuanXinId);
                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    return ObjectResult(Context.SaveChanges() > 0);
                }
                else
                {
                    throw new ZzbException(res.Message);
                }
              
            }, "删除好友好友失败");
        }


        (ClientRelationDetail,Client) GetRelationDetailClient(Guid clientId)
        {
            var friendClientEntity = Context.Clients.Where(c => c.ClientId == clientId).FirstOrDefault();
            if (friendClientEntity == null)
            {
                throw new ZzbException("找不到用户");
            }

            var clientRelationEntity = Context.ClientRelationDetails.Where(cd =>
            cd.ClientRelation.ClientId == CurrentClientId &&
            cd.ClientRelation.RelationType == ClientRelation.RelationTypeEnum.Friend &&
            cd.ClientId == friendClientEntity.ClientId).FirstOrDefault();
            if (clientRelationEntity == null)
            {
                throw new ZzbException("这个用户不是你的好友,无法设置");
            }
            return (clientRelationEntity,friendClientEntity);
        }

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("BlackFriend")]
        public ServiceResult BlackFriend(DeleteFriendRequest request)
        {
            return Json(() =>
            {
                 var  Combina  = GetRelationDetailClient(request.ClientId);
                ClientRelationDetail clientRelationDetailEntity = Combina.Item1;
                Client blackClientEntity = Combina.Item2;
                Context.ClientRelationDetails.Remove(clientRelationDetailEntity);

                var blackCR = Context.ClientRelations.Where(cr => cr.ClientId == CurrentClientId && cr.RelationType == ClientRelation.RelationTypeEnum.Black).FirstOrDefault();
                if (blackCR == null)
                {
                    blackCR = new ClientRelation()
                    {
                        ClientId = CurrentClientId,
                        RelationType = ClientRelation.RelationTypeEnum.Black,
                    };
                    Context.ClientRelations.Add(blackCR);
                }
                ClientRelationDetail detailEntity = new ClientRelationDetail()
                {
                    ClientId = blackClientEntity.ClientId,
                    ClientRelationId = blackCR.ClientRelationId,
                };
                Context.ClientRelationDetails.Add(detailEntity);
                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"拉黑好友[{blackClientEntity.LiaoxinNumber}]"));

                //环信确认拉黑
                List<string> blackIds = new List<string>();
                blackIds.Add(blackClientEntity.HuanXinId);

               var res =  HuanxinClientRequest.AddBlockFriend(CurrentHuanxinId, blackIds);
                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    return ObjectResult(Context.SaveChanges() > 0);
                }
                else
                {
                    return res;
                }

            
            }, "添加黑名单失败");
        }


        /// <summary>
        /// 设置好友备注
        /// </summary>        
        /// <returns></returns>
        [HttpPost("SetFriendNickName")]
        public ServiceResult SetFriendNickName(SetFriendRemarkRequest request)
        {
            return Json(() =>
            {

                var Combina = GetRelationDetailClient(request.ClientId);
                ClientRelationDetail clientRelationDetailEntity = Combina.Item1;
                Context.ClientRelationDetails.Update(clientRelationDetailEntity);
                clientRelationDetailEntity.ClientRemark = request.Remark; 
                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"设置好友备注[{Combina.Item2.LiaoxinNumber}]"));                 
                return ObjectResult(Context.SaveChanges() > 0);
            }, "设置好友备注失败");
        }


 

   
    }
}