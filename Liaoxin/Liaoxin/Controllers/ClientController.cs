
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
        /// 客户登录
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
        /// 客户登录
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

                var clientAddId = Context.ClientAdds.Where(cd => cd.ClientId == ClientId).Select(cd => cd.ClientAddId).FirstOrDefault();
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
                var clientRelationEntity = Context.ClientRelations.Where(cd => cd.ClientId == ClientId && cd.RelationType == ClientRelation.RelationTypeEnum.Friend).FirstOrDefault();

                List<ClientFriendResponse> lis = new List<ClientFriendResponse>();
                clientRelationEntity.ClientRelationDetail.ForEach(e =>
                {
                    lis.Add(new ClientFriendResponse()
                    {
                        Cover = e.Client.Cover,
                        CreateTime = e.CreateTime,
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
        /// 添加好友申请
        /// </summary>       
        /// <returns></returns>
        [HttpPost("ApplyAddFriend")]
        public ServiceResult ApplyAddFriend(ApplyAddFriendRequest request)
        {
            return (ServiceResult<List<ClientFriendResponse>>)Json(() =>
            {
                var applyClientEntity = Context.Clients.Where(c => c.HuanXinId == request.HuanxinId).FirstOrDefault();
                if (applyClientEntity == null)
                {
                    throw new ZzbException("找不到申请客户");
                }
                var clientAddEntity = Context.ClientAdds.Where(c => c.ClientId == ClientId).FirstOrDefault();
                if (clientAddEntity == null)
                {
                    clientAddEntity = new ClientAdd();
                    clientAddEntity.ClientId = ClientId;
                    Context.ClientAdds.Add(clientAddEntity);
                }
                ClientAddDetail detailEntity = new ClientAddDetail()
                {
                    ClientId = applyClientEntity.ClientId,
                    ClientAddId = clientAddEntity.ClientAddId,
                    Status = ClientAddDetail.ClientAddDetailTypeEnum.StandBy,
                    AddRemark = request.AddRemark
                };
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
            return (ServiceResult<List<ClientFriendResponse>>)Json(() =>
            {
                var applyClientEntity = Context.Clients.Where(c => c.HuanXinId == request.HuanxinId).FirstOrDefault();
                if (applyClientEntity == null)
                {
                    throw new ZzbException("找不到客户");
                }
                var clientRelationEntity = Context.ClientRelations.Where(c => c.ClientId == ClientId && c.RelationType == ClientRelation.RelationTypeEnum.Friend).FirstOrDefault();
                if (clientRelationEntity == null)
                {
                    clientRelationEntity = new ClientRelation();
                    clientRelationEntity.ClientId = ClientId;
                    clientRelationEntity.RelationType = ClientRelation.RelationTypeEnum.Friend;
                    Context.ClientRelations.Add(clientRelationEntity);
                }
                ClientRelationDetail detailEntity = new ClientRelationDetail()
                {
                    ClientId = applyClientEntity.ClientId,
                    ClientRelationId = clientRelationEntity.ClientRelationId,
                };
                //环信确认添加
                Context.ClientRelationDetails.Add(detailEntity);
                return ObjectResult(Context.SaveChanges() > 0);
            }, "确认添加好友失败");

        }



        //删除好友

        //添加黑名单

        //设置好友备注

        //基本设置

        //修改头像

        //修改昵称

        //修改地区

        //个性签名


        //创建群

        //设置管理员

        //删除管理员




    }
}