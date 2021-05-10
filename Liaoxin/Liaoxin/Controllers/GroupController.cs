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

using Zzb.Mvc;
using Zzb.Utility;
using static Liaoxin.ViewModel.ClientViewModel;


namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GroupControllerController : LiaoxinBaseController
    {
        public IClientService clientService { get; set; }

        public IGroupService groupService { get; set; }


        #region Group

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpPost("CreateGroup")]
        public ServiceResult<GroupResponse> CreateGroup(CreateGroupRequest requestObj)
        {
            string groupName = requestObj.GroupName;

            List<Guid> clientIdList = requestObj.ClientIdList.Distinct().ToList();
        
            Group entity = new Group();
            entity.IsEnable = true;
            entity.Name = groupName;
            entity.ClientId = CurrentClientId;
            bool result = groupService.CreateGroup(entity, clientIdList);
            GroupResponse returnObj = null;
            if (result)
            {
                returnObj = ConvertHelper.ConvertToModel<Group, GroupResponse>(entity);
            }
            return ObjectGenericityResult<GroupResponse>(result, returnObj);
        }

        /// <summary>
        /// 更新群
        /// </summary>
        /// <param name="model">群信息</param>
        /// <returns></returns>
        [HttpPost("UpdateGroup")]
        public ServiceResult UpdateGroup(GroupResponse model)
        {
            IList<string> updateFieldList = GetPostBodyFiledKey();
            Group entity = ConvertHelper.ConvertToModel<GroupResponse, Group>(model, updateFieldList);
            entity.UpdateTime = DateTime.Now;
            bool result = groupService.Update<Group>(entity, "GroupId", updateFieldList) > 0;
            return ObjectGenericityResult<object>(result, null);

        }


        /// <summary>
        /// 获取群基本信息
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
                return ObjectGenericityResult<IList<GroupResponse>>(ConvertHelper.ConvertToList<Group, GroupResponse>(groupService.GetClientGroups(clientid, isEnable)));
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


        #endregion

        #region GroupClient

        /// <summary>
        /// 获取GroupClient
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
        /// 获取GroupClient清单
        /// </summary>
        /// <param name="groupId">群id</param>
        /// <param name="isEnable">客户是否已通过入群</param>
        /// <returns></returns>
        [HttpGet("GetGroupClients")]
        public ServiceResult<IList<GroupClientResponse>> GetGroupClients(Guid groupId, bool isEnable)
        {
            return (ServiceResult<IList<GroupClientResponse>>)Json(() =>
            {
                return ObjectGenericityResult<IList<GroupClientResponse>>(ConvertHelper.ConvertToList<GroupClient, GroupClientResponse>(groupService.GetGroupClients(groupId, isEnable)));
            }, "获取群成员基本信息失败");
        }

        /// <summary>
        /// 更新GroupClient信息
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("UpdateGroupClient")]
        public ServiceResult<bool> UpdateGroupClient(GroupClientResponse model)
        {
            IList<string> updateFieldList = GetPostBodyFiledKey();

            GroupClient entity = ConvertHelper.ConvertToModel<GroupClientResponse, GroupClient>(model, updateFieldList);
            bool result = groupService.Update<GroupClient>(entity, "GroupClientId", updateFieldList) > 0;

            return (ServiceResult<bool>)Json(() =>
            {
                return ObjectGenericityResult<bool>(result);
            }, "更新群成员信息失败");

        }

        /// <summary>
        /// 加入群聊
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpPost("BatchAddGroupClient")]
        public ServiceResult BatchAddGroupClient(AddGroupRequest requestObj)
        {
            Guid groupId = requestObj.GroupId;

            List<Guid> clientIdList = requestObj.ClientIdList.Distinct().ToList();

            Group groupEntity = groupService.GetGroup(groupId);
            IList<GroupClient> clientList = groupService.GetGroupClients(groupId);
            IList<GroupClient> gmList = clientList.Where(p => p.IsGroupManager).ToList();
            //不能重复进群
            for (int i = 0; i < clientIdList.Count; i++)
            {
                if (clientList.FirstOrDefault(p => p.GroupId == clientIdList[i]) != null)
                {
                    clientIdList.RemoveAt(i--);
                }
            }
            if (clientIdList.Count == 0)
            {
                return JsonObjectResult(false, "无成员可入群");
            }
            if (gmList == null)
            {
                gmList = new List<GroupClient>();
            }
            bool isEnable = false;
            if (groupEntity != null)
            {
                Guid curUserClientId = groupService.GetCurClientId();
                isEnable = !groupEntity.SureConfirmInvite || curUserClientId == groupEntity.ClientId || gmList.FirstOrDefault(p => p.ClientId == curUserClientId) != null;
            }

            foreach (Guid clientId in clientIdList)
            {
                groupService.AddGroupClient(clientId, groupId, isEnable, false, groupEntity);
            }
            //批量操作后再保存数据库
            groupService.SaveChanges();
            return JsonObjectResult(false, "操作成功");

        }

        #endregion











        ///// <summary>
        ///// 设置群管理员
        ///// </summary>
        ///// <param name="clientId">客户id</param>
        ///// <param name="groupId">群id</param>
        ///// <returns></returns>
        //[HttpPost("SetGroupManager")]
        //public ServiceResult<bool> SetGroupManager(Guid groupClientId)
        //{
        //    GroupClient gc = new GroupClient();
        //    gc.GroupClientId = groupClientId;
        //    gc.IsGroupManager = true;

        //    IList<string> updateFieldList = new List<string>() { "IsGroupManager" };
        //    bool result= groupService.Update<GroupClient>(gc, "GroupClientId", updateFieldList)>0;

        //    return (ServiceResult<bool>)Json(() =>
        //    {
        //        return ObjectGenericityResult<bool>(result);
        //    }, "设置群管理员失败");
        //}


        ///// <summary>
        ///// 取消群管理员
        ///// </summary>
        ///// <param name="clientId">客户id</param>
        ///// <param name="groupId">群id</param>
        ///// <returns></returns>
        //[HttpPost("CancelGroupManager")]
        //public ServiceResult<bool> CancelGroupManager(Guid groupClientId)
        //{
        //    GroupClient gc = new GroupClient();
        //    gc.GroupClientId = groupClientId;
        //    gc.IsGroupManager = false;

        //    IList<string> updateFieldList = new List<string>() { "IsGroupManager" };
        //    bool result = groupService.Update<GroupClient>(gc, "GroupClientId", updateFieldList) > 0;

        //    return (ServiceResult<bool>)Json(() =>
        //    {
        //        return ObjectGenericityResult<bool>(result);
        //    }, "取消群管理员失败");
        //}


        ///// <summary>
        ///// 管理员审核群
        ///// </summary>
        ///// <param name="groupId"></param>
        ///// <param name="isEnable"></param>
        ///// <returns></returns>
        //[HttpPost("AuditGroup")]
        //public ServiceResult<bool> AuditGroup(Guid groupId, bool isEnable)
        //{
        //    return (ServiceResult<bool>)Json(() =>
        //    {
        //        groupService.AuditGroup(groupId, isEnable);
        //        return ObjectGenericityResult<bool>(true);
        //    }, "管理员审核群失败");
        //}


        ///// <summary>
        ///// 获取所有群基本信息
        ///// </summary>
        ///// <param name="clientid">客户端群id</param>
        ///// <param name="isEnable">群是已审核通过</param>
        ///// <returns></returns>
        //[HttpGet("GetGroups")]
        //public ServiceResult<IList<GroupResponse>> GetGroups(bool isEnable)
        //{
        //    return (ServiceResult<IList<GroupResponse>>)Json(() =>
        //    {
        //        return ObjectGenericityResult<IList<GroupResponse>>(ConvertHelper.ConvertToList<Group, GroupResponse>(groupService.GetGroups(isEnable, 0, 10000)));
        //    }, "获取所有群基本信息失败");
        //}





        ///// <summary>
        ///// 批量更新群成员信息
        ///// </summary>
        ///// <param name="modelList"></param>
        //[HttpPost("BatchUpdateGroupClient")]
        //public ServiceResult<bool> BatchUpdateGroupClient(IList<GroupClientResponse> modelList)
        //{
        //    IList<GroupClient> entityList = ConvertHelper.ConvertToList<GroupClientResponse, GroupClient>(modelList);
        //    foreach (GroupClient entity in entityList)
        //    {
        //        groupService.UpdateGroupClient(entity);
        //    }
        //    groupService.SaveChanges();
        //    return ObjectGenericityResult<bool>(true);
        //}






    }


}