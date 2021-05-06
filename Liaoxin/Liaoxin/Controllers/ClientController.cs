
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
using static Liaoxin.ViewModel.ClientViewModel;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : BaseApiController
    {
        public IClientService clientService { get; set; }
        public LiaoxinContext Context { get; set; }


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

                var clientAddId = Context.ClientAdds.Where(cd => cd.ClientId == CurrentClientId).Select(cd => cd.ClientAddId).FirstOrDefault();
                var entities = Context.ClientAddDetails.Where(c => c.ClientAddId == clientAddId && c.CreateTime > DateTime.Now.AddMonths(-1)).ToList();
                List<ClientAddDetailResponse> lis = new List<ClientAddDetailResponse>();
                entities.ForEach(e =>
                {
                    lis.Add(new ClientAddDetailResponse()
                    {
                        AddRemark = e.AddRemark,
                        Cover = e.Client.Cover,
                        CreateTime = e.CreateTime,
                        HuanxinId = e.Client.HuanXinId,
                        LiaoxinNumber = e.Client.LiaoxinNumber,
                        NickName = e.Client.NickName,
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
                var clientAddEntity = Context.ClientAdds.Where(c => c.ClientId == CurrentClientId).FirstOrDefault();
                if (clientAddEntity == null)
                {
                    clientAddEntity = new ClientAdd();
                    clientAddEntity.ClientId = CurrentClientId;
                    Context.ClientAdds.Add(clientAddEntity);
                }
                ClientAddDetail detailEntity = new ClientAddDetail()
                {
                    ClientId = applyClientEntity.ClientId,
                    ClientAddId = clientAddEntity.ClientAddId,
                    Status = ClientAddDetail.ClientAddDetailTypeEnum.StandBy,
                    AddRemark = request.AddRemark
                };
                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"申请添加好友[{applyClientEntity.LiaoxinNumber}]"));
                Context.ClientAddDetails.Add(detailEntity);
                return ObjectResult(Context.SaveChanges() > 0);
            }, "添加好友申请失败");
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
                var clientRelationEntity = Context.ClientRelations.Where(c => c.ClientId == CurrentClientId && c.RelationType == ClientRelation.RelationTypeEnum.Friend).FirstOrDefault();
                if (clientRelationEntity == null)
                {
                    clientRelationEntity = new ClientRelation();
                    clientRelationEntity.ClientId = CurrentClientId;
                    clientRelationEntity.RelationType = ClientRelation.RelationTypeEnum.Friend;
                    Context.ClientRelations.Add(clientRelationEntity);
                }
                ClientRelationDetail detailEntity = new ClientRelationDetail()
                {
                    ClientId = applyClientEntity.ClientId,
                    ClientRelationId = clientRelationEntity.ClientRelationId,
                };
                //环信确认添加
                //

                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"确认添加好友[{applyClientEntity.LiaoxinNumber}]"));
                Context.ClientRelationDetails.Add(detailEntity);
                return ObjectResult(Context.SaveChanges() > 0);
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
                return ObjectResult(Context.SaveChanges() > 0);
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

                return ObjectResult(Context.SaveChanges() > 0);
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
                //环信设置好有备注

                return ObjectResult(Context.SaveChanges() > 0);
            }, "设置好友备注失败");
        }


        //基本设置

        //修改头像

        //修改昵称

        //修改地区

        //个性签名      
    }
}