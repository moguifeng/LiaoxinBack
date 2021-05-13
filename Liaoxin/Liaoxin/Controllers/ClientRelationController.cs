
using Liaoxin.Business;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using LIaoxin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class ClientRelationController : LiaoxinBaseController
    {


        public IClientService clientService { get; set; }

        /// <summary>
        /// 全局搜索添加好友(聊信号/手机号码)
        /// </summary>       
        /// <returns></returns>
        [HttpPost("GlobalSearchFriend")]
        public ServiceResult<GlobalSearchCliengResponse> GlobalSearchFriend(string  searchText)
        {
            return (ServiceResult<GlobalSearchCliengResponse>)Json(() =>
            {

                var entity = Context.Clients.Where(c => c.Telephone == searchText || c.LiaoxinNumber == searchText).FirstOrDefault();
                if (entity == null)
                {
                    throw new ZzbException("找不到联系人");
                }

                GlobalSearchCliengResponse response = new GlobalSearchCliengResponse()
                {
                    ClientId = entity.ClientId,
                    Cover = entity.Cover,
                    HuanxinId = entity.HuanXinId,
                    LiaoxinNumber = entity.LiaoxinNumber,
                    NickName = entity.NickName,
                    FriendShipType = clientService.GetRelationThoughtClientId(entity.ClientId),
            };             
                return ObjectGenericityResult(response);
            }, "查找失败");

        }





        /// <summary>
        /// 客户(新的好友添加)列表
        /// </summary>       
        /// <returns></returns>
        [HttpPost("ApplyFriends")]
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
                var clientRelationEntity = Context.ClientRelations.Where(cd => cd.ClientId == CurrentClientId && cd.RelationType == RelationTypeEnum.Friend).FirstOrDefault();

                List<ClientFriendResponse> lis = new List<ClientFriendResponse>();
                clientRelationEntity.ClientRelationDetail.Where(c=>c.IsEnable).ToList().ForEach(e =>
                {
                    lis.Add(new ClientFriendResponse()
                    {
                         ClientId =e.Client.ClientId,
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
        /// 好友详细
        /// </summary>       
        /// <returns></returns>
        [HttpPost("FriendDetail")]
        public ServiceResult<ClientFriendDetailResponse> FriendDetail(ClientRelationShipRequest request)
        {
            return RelationShipDetail(request.ClientId, RelationTypeEnum.Friend);

        }


        /// <summary>
        /// 陌生人(有可能已经是好友/黑名单)详细
        /// </summary>       
        /// <returns></returns>
        [HttpPost("ClientStrangerDetail")]
        public ServiceResult<ClientFriendDetailResponse> ClientStrangerDetail(Guid clientId)
        {
            var entity = Context.Clients.Where(c => c.ClientId == clientId).AsNoTracking().Select(s => new ClientFriendDetailResponse()
            {
                ClientId = s.ClientId,
                CharacterSignature = s.CharacterSignature,
                Cover = s.Cover,
                HuanxinId = s.HuanXinId,
                LiaoxinNumber = s.LiaoxinNumber,
                NickName = s.NickName,
                FriendShipType = clientService.GetRelationThoughtClientId(s.ClientId),
                
            }).FirstOrDefault();

            if (entity.FriendShipType == (int)RelationTypeEnum.Friend)
            {
                var friendEntity = FriendDetail(new ClientRelationShipRequest() { ClientId = clientId });
                return friendEntity;
            }
            else if (entity.FriendShipType == (int)RelationTypeEnum.Black)
            {
                return BlackDetail(new ClientRelationShipRequest() { ClientId = clientId });
            }
            else
            {
                return ObjectGenericityResult(entity);
            }


        

        }


        /// <summary>
        /// 黑名单详细
        /// </summary>       
        /// <returns></returns>
        [HttpPost("BlackDetail")]
        public ServiceResult<ClientFriendDetailResponse> BlackDetail(ClientRelationShipRequest request)
        {
            return RelationShipDetail(request.ClientId, RelationTypeEnum.Black);

        }


        /// <summary>
        /// 客户黑名单列表
        /// </summary>       
        /// <returns></returns>
        [HttpPost("ClientBlacks")]
        public ServiceResult<List<ClientFriendResponse>> ClientBlacks()
        {
            return (ServiceResult<List<ClientFriendResponse>>)Json(() =>
            {
                var clientRelationEntity = Context.ClientRelations.Where(cd => cd.ClientId == CurrentClientId && cd.RelationType == RelationTypeEnum.Black).FirstOrDefault();

                List<ClientFriendResponse> lis = new List<ClientFriendResponse>();
                clientRelationEntity.ClientRelationDetail.Where(c => c.IsEnable).ToList().ForEach(e =>
                {
                    lis.Add(new ClientFriendResponse()
                    {
                        Cover = e.Client.Cover,
                        ClientId = e.Client.ClientId,
                        HuanxinId = e.Client.HuanXinId,
                        LiaoxinNumber = e.Client.LiaoxinNumber,
                        NickName = e.Client.NickName,
                        ClientRemark = e.ClientRemark,
                    });
                });
                return ListGenericityResult(lis);
            }, "获取黑名单列表失败");

        }




        /// <summary>
        /// 好友/黑名单详细
        /// </summary>       
        /// <returns></returns>

        private  ServiceResult<ClientFriendDetailResponse> RelationShipDetail(Guid clientId, RelationTypeEnum type )
        {
            return (ServiceResult<ClientFriendDetailResponse>)Json(() =>
            {

                var shipEntity = Context.ClientRelationDetails.Where
                 (crd => crd.ClientRelation.ClientId == CurrentClientId &&
                 crd.ClientRelation.RelationType == type &&
                 crd.ClientId == clientId).FirstOrDefault();
                if (shipEntity == null)
                {
                    throw new ZzbException("不存在关系");
                }
                int mutipleCount = 0;
                if (type == RelationTypeEnum.Friend)
                {
                      mutipleCount = Context.GroupClients.Where(g => g.ClientId == clientId || g.ClientId == CurrentClientId).
Select(g => new { g.ClientId, g.GroupId }).
GroupBy(g => new { g.GroupId }).
Select(g => new { Key = g.Key, Count = g.Count() }).Where(g => g.Count > 1).Count();
                }


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
            }, "获取关系详细失败");

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
                crd.ClientRelation.RelationType == RelationTypeEnum.Friend).Count() > 0;
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
        public ServiceResult RejectAddFriend(ClientRelationShipRequest request)
        {
            return Json(() =>
            {
                var applyClientEntity = Context.Clients.Where(c => c.ClientId == request.ClientId).FirstOrDefault();
                if (applyClientEntity == null)
                {
                    throw new ZzbException("找不到客户");
                }
                var clientRelationEntity = Context.ClientRelations.Where(c => c.ClientId == CurrentClientId && c.RelationType == RelationTypeEnum.Friend).FirstOrDefault();
                if (clientRelationEntity == null)
                {
                    clientRelationEntity = new ClientRelation();
                    clientRelationEntity.ClientId = CurrentClientId;
                    clientRelationEntity.RelationType = RelationTypeEnum.Friend;
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

                var clientRelationEntity = Context.ClientRelations.Where(c => c.ClientId == CurrentClientId && c.RelationType == RelationTypeEnum.Friend).FirstOrDefault();
                if (clientRelationEntity == null)
                {
                    clientRelationEntity = new ClientRelation();
                    clientRelationEntity.ClientId = CurrentClientId;
                    clientRelationEntity.RelationType = RelationTypeEnum.Friend;
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
        public ServiceResult DeleteFriend(ClientRelationShipRequest request)
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
                cd.ClientRelation.RelationType == RelationTypeEnum.Friend &&
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


        (ClientRelationDetail,Client) GetRelationDetailClient(Guid clientId, RelationTypeEnum relationType = RelationTypeEnum.Friend)
        {
            var friendClientEntity = Context.Clients.Where(c => c.ClientId == clientId).FirstOrDefault();
            if (friendClientEntity == null)
            {
                throw new ZzbException("找不到用户");
            }

            var clientRelationEntity = Context.ClientRelationDetails.Where(cd =>
            cd.ClientRelation.ClientId == CurrentClientId &&
            cd.ClientRelation.RelationType == relationType &&
            cd.ClientId == friendClientEntity.ClientId).FirstOrDefault();
            if (clientRelationEntity == null)
            {
                throw new ZzbException("这个用户与你没有任何关系,无法设置");
            }
            return (clientRelationEntity,friendClientEntity);
        }

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("BlackFriend")]
        public ServiceResult BlackFriend(ClientRelationShipRequest request)
        {
            return Json(() =>
            {
                 var  Combina  = GetRelationDetailClient(request.ClientId);
                ClientRelationDetail clientRelationDetailEntity = Combina.Item1;
                Client blackClientEntity = Combina.Item2;
                clientRelationDetailEntity.IsEnable = false; // 设置不可用
                Context.ClientRelationDetails.Update(clientRelationDetailEntity);

                var blackCR = Context.ClientRelations.Where(cr => cr.ClientId == CurrentClientId && cr.RelationType == RelationTypeEnum.Black).FirstOrDefault();
                if (blackCR == null)
                {
                    blackCR = new ClientRelation()
                    {
                        ClientId = CurrentClientId,
                        RelationType = RelationTypeEnum.Black,
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
        /// 移除黑名单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("RemoveBlackFriend")]
        public ServiceResult RemoveBlackFriend(ClientRelationShipRequest request)
        {
            return Json(() =>
            {
                var Combina = GetRelationDetailClient(request.ClientId, RelationTypeEnum.Black);
                ClientRelationDetail clientRelationDetailEntity = Combina.Item1;
                Client blackClientEntity = Combina.Item2;
                
                //判断这个黑名单是否是当前用户之前的好友.是就直接还原为好友. 
                var shipEntity =  Context.ClientRelationDetails.Where
                (c => c.ClientId == blackClientEntity.ClientId && c.ClientRelation.ClientId == CurrentClientId && c.ClientRelation.RelationType == RelationTypeEnum.Friend
                       ).FirstOrDefault();
                if (shipEntity != null)
                {
                    shipEntity.IsEnable = true;
                    Context.ClientRelationDetails.Update(shipEntity);
                }

                Context.ClientRelationDetails.Remove(clientRelationDetailEntity);       
                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"解除拉黑好友[{blackClientEntity.LiaoxinNumber}]"));    
                List<string> blackIds = new List<string>();
                blackIds.Add(blackClientEntity.HuanXinId);

                //环信解除拉黑
                var res = HuanxinClientRequest.DeleteBlockFriend(CurrentHuanxinId, blackClientEntity.HuanXinId);
                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    if (shipEntity != null)
                    {
                       HuanxinClientRequest.AddFriend(CurrentHuanxinId, shipEntity.Client.HuanXinId);
                    }
                    
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