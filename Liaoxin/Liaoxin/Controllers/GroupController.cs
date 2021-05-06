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
using Liaoxin.Model;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupControllerController : BaseApiController
    {
        public IClientService clientService { get; set; }
        public LiaoxinContext Context { get; set; }

        public IGroupService groupService { get; set; }
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
        /// 获取群客户
        /// </summary>
        /// <param name="clientId">客户id</param>
        /// <param name="groupId">群id</param>
        /// <returns></returns>
        [HttpGet("GetClientGroup")]
        public ServiceResult<GroupClientResponse> GetClientGroup(Guid clientId, Guid groupId)
        {
            return (ServiceResult<GroupClientResponse>)Json(() =>
            {
                return ObjectGenericityResult<GroupClientResponse>(ConvertHelper.ConvertToModel<GroupClient, GroupClientResponse>(groupService.GetClientGroup(clientId, groupId)));
            }, "获取群客户失败");
        }




        /// <summary>
        /// 获取客户所有群基本信息
        /// </summary>
        /// <param name="clientid">客户端群id</param>
        /// <param name="isEnable">群是已审核通过</param>
        /// <returns></returns>
        [HttpGet("GetClientGroups")]
        public ServiceResult<IList<GroupResponse>> GetClientGroups(Guid clientid, bool isEnable)
        {
            return (ServiceResult<IList<GroupResponse>>)Json(() =>
            {
                return ObjectGenericityResult<IList<GroupResponse>>(ConvertHelper.ConvertToModel<IList<Group>, IList<GroupResponse>>(groupService.GetClientGroups(clientid, isEnable)));
            }, "获取客户所有群基本信息失败");
        }


        /// <summary>
        /// 解散群
        /// </summary>
        /// <param name="groupId">群id</param>
        /// <returns></returns>
        [HttpPost("DissolveGroup")]
        public ServiceResult<bool> DissolveGroup(Guid groupId)
        {
            return (ServiceResult<bool>)Json(() =>
            {
                return ObjectGenericityResult<bool>(groupService.DissolveGroup(groupId));
            }, "解散群失败");
        }

        /// <summary>
        /// 设置群管理员
        /// </summary>
        /// <param name="clientId">客户id</param>
        /// <param name="groupId">群id</param>
        /// <returns></returns>
        [HttpPost("SetGroupManager")]
        public ServiceResult<bool> SetGroupManager(Guid clientId, Guid groupId)
        {
            return (ServiceResult<bool>)Json(() =>
            {
                groupService.SetGroupManager(clientId, groupId);
                return ObjectGenericityResult<bool>(true);
            }, "设置群管理员失败");
        }


        /// <summary>
        /// 取消群管理员
        /// </summary>
        /// <param name="clientId">客户id</param>
        /// <param name="groupId">群id</param>
        /// <returns></returns>
        [HttpPost("CancelGroupManager")]
        public ServiceResult<bool> CancelGroupManager(Guid clientId, Guid groupId)
        {
            return (ServiceResult<bool>)Json(() =>
            {
                groupService.CancelGroupManager(clientId, groupId);
                return ObjectGenericityResult<bool>(true);
            }, "取消群管理员失败");
        }

        /// <summary>
        /// 变更群主
        /// </summary>
        /// <param name="newMasterClientId">新群主id</param>
        /// <param name="originalMasterClientId">旧群主id</param>
        /// <param name="groupId">群id</param>
        /// <returns></returns>
        [HttpPost("TransferGroupMaster")]
        public ServiceResult<bool> TransferGroupMaster(Guid newMasterClientId, Guid originalMasterClientId, Guid groupId)
        {
            return (ServiceResult<bool>)Json(() =>
            {
                groupService.TransferGroupMaster(newMasterClientId, originalMasterClientId, groupId);
                return ObjectGenericityResult<bool>(true);
            }, "变更群主失败");
        }


        /// <summary>
        /// 创建群
        /// </summary>
        /// <param name="model">群信息</param>
        /// <returns></returns>
        [HttpPost("CreateGroup")]
        public ServiceResult<bool> CreateGroup(GroupResponse model)
        {
            return (ServiceResult<bool>)Json(() =>
            {
                Group entity = ConvertHelper.ConvertToModel<GroupResponse, Group>(model);
                return ObjectGenericityResult<bool>(groupService.CreateGroup(entity));
            }, "创建群失败");
        }

        /// <summary>
        /// 更新群
        /// </summary>
        /// <param name="model">群信息</param>
        /// <returns></returns>
        [HttpPost("UpdateGroup")]
        public ServiceResult<bool> UpdateGroup(GroupResponse model)
        {
            return (ServiceResult<bool>)Json(() =>
            {
                Group entity = ConvertHelper.ConvertToModel<GroupResponse, Group>(model);
                entity.UpdateTime = DateTime.Now;
                return ObjectGenericityResult<bool>(groupService.UpdateGroup(entity));
            }, "更新群信息失败");
        }

        /// <summary>
        /// 获取群成员基本信息
        /// </summary>
        /// <param name="groupId">群id</param>
        /// <param name="isEnable">客户是否已通过入群</param>
        /// <returns></returns>
        [HttpGet("GetGroupClients")]
        public ServiceResult<IList<ClientBaseInfoResponse>> GetGroupClients(Guid groupId, bool isEnable)
        {
            return (ServiceResult<IList<ClientBaseInfoResponse>>)Json(() =>
            {
                return ObjectGenericityResult<IList<ClientBaseInfoResponse>>(ConvertHelper.ConvertToModel<IList<Client>, IList<ClientBaseInfoResponse>>(groupService.GetGroupClients(groupId, isEnable)));
            }, "获取群成员基本信息失败");
        }



        /// <summary>
        /// 获取群
        /// </summary>
        /// <param name="groupId">群id</param>
        /// <returns></returns>
        [HttpGet("GetGroup")]
        public ServiceResult<GroupResponse> GetGroup(Guid groupId)
        {
            return (ServiceResult<GroupResponse>)Json(() =>
            {
                return ObjectGenericityResult<GroupResponse>(ConvertHelper.ConvertToModel<Group, GroupResponse>(groupService.GetGroup(groupId)));
            }, "获取群基本信息失败");
        }

        /// <summary>
        /// 审核群是否通过
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="isEnable"></param>
        /// <returns></returns>
        [HttpPost("AuditGroup")]
        public ServiceResult<bool> AuditGroup(Guid groupId, bool isEnable)
        {
            return (ServiceResult<bool>)Json(() =>
            {
                groupService.AuditGroup(groupId, isEnable);
                return ObjectGenericityResult<bool>(true);
            }, "审核群是否通过失败");
        }


        /// <summary>
        /// 获取所有群基本信息
        /// </summary>
        /// <param name="clientid">客户端群id</param>
        /// <param name="isEnable">群是已审核通过</param>
        /// <returns></returns>
        [HttpGet("GetGroups")]
        public ServiceResult<IList<GroupResponse>> GetGroups(bool isEnable)
        {
            return (ServiceResult<IList<GroupResponse>>)Json(() =>
            {
                return ObjectGenericityResult<IList<GroupResponse>>(ConvertHelper.ConvertToModel<IList<Group>, IList<GroupResponse>>(groupService.GetGroups(isEnable, 0, 10000)));
            }, "获取所有群基本信息失败");
        }

        /// <summary>
        /// 加入群
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="clientIdList"></param>
        /// <returns></returns>
        [HttpPost("BatchAddGroupClient")]
        public ServiceResult<bool> BatchAddGroupClient(Guid groupId, List<Guid> clientIdList)
        {
            Group groupEntity = groupService.GetGroup(groupId);
            IList<GroupManager> gmList = groupService.GetGroupManagerList(groupId);
            if (gmList == null)
            {
                gmList = new List<GroupManager>();
            }
            bool isEnable = false;
            if (groupEntity != null)
            {
                Guid curClientId = groupService.GetCurClientId();
                isEnable = !groupEntity.SureConfirmInvite || curClientId == groupEntity.ClientId || gmList.FirstOrDefault(p => p.ClientId == curClientId) != null;
            }
            return (ServiceResult<bool>)Json(() =>
            {
                foreach (Guid clientId in clientIdList)
                {
                    groupService.AddGroupClient(clientId, groupId, isEnable, false, groupEntity);
                }
                //批量操作后再保存数据库
                groupService.SaveChanges();
                return ObjectGenericityResult<bool>(true);
            }, "审核群是否通过失败");
        }



        /// <summary>
        /// 批量更新群成员信息
        /// </summary>
        /// <param name="modelList"></param>
        [HttpPost("BatchUpdateGroupClient")]
        public ServiceResult<bool> BatchUpdateGroupClient(IList<GroupClientResponse> modelList)
        {
            IList<GroupClient> entityList = ConvertHelper.ConvertToModel<IList<GroupClientResponse>, IList<GroupClient>>(modelList);
            foreach (GroupClient entity in entityList)
            {
                groupService.UpdateGroupClient(entity);
            }
            groupService.SaveChanges();
            return ObjectGenericityResult<bool>(true);
        }






    }


}