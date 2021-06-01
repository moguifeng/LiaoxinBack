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



        [HttpGet("SendGroupMsg")]
        public ServiceResult<bool> SendGroupMsg( )
        {

            return (ServiceResult<bool>)Json(() =>
            {

                HuanxinRobotRequest.RobotSendMsg("148389810470913", "mwxkn8ungcgxnmg", "mcghnwbxwiueghn");
                return ObjectResult(true);
            });
        }




        #region Group

        /// <summary>
        /// 创建群聊
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpPost("CreateGroup")]
        public ServiceResult<GroupResponse> CreateGroup(CreateGroupRequest requestObj)
        {

            return (ServiceResult<GroupResponse>)Json(() =>
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
            });
        }

        /// <summary>
        /// 更新群
        /// </summary>
        /// <param name="model">群信息</param>
        /// <returns></returns>
        [HttpPost("UpdateGroup")]
        public ServiceResult UpdateGroup(GroupResponse model)
        {

            return Json(() =>
            {
                groupService.IsCurrentGroup(model.GroupId);
                List<string> ingores = new List<string>();
                ingores.Add("UnqiueId");
                ingores.Add("HuanxinGroupId");
                ingores.Add("ClientId");
                ingores.Add("IsEnable");
                ingores.Add("CreateTime");

                IList<string> updateFieldList = GetPostBodyFiledKey(ingores);
                Group entity = ConvertHelper.ConvertToModel<GroupResponse, Group>(model, updateFieldList);
                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"修改了基本信息,群号是:{model.UnqiueId}"));
                var res = HuanxinGroupRequest.ModifyGroup(model.HuanxinGroupId, entity.Notice, entity.Name);
                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    entity.UpdateTime = DateTime.Now;
                    bool result = groupService.Update<Group>(entity, "GroupId", updateFieldList) > 0;
                    return ObjectResult(result);
                }
                else
                {
                    return res;
                }
            }, "更新失败");


        }


        /// <summary>
        /// 我的群聊列表
        /// </summary>
        /// <param name="model">群信息</param>
        /// <returns></returns>
        [HttpPost("MyGroups")]
        public ServiceResult<List<MyGroupResponse>> MyGroups()
        {
            return (ServiceResult<List<MyGroupResponse>>)Json(() => {
                var groupIds = Context.GroupClients.Where(g => g.ClientId == CurrentClientId).Select(s => s.GroupId).ToList();
                var lis = Context.Groups.Where(g => groupIds.Contains(g.GroupId)).AsNoTracking().ToList();
                return ListGenericityResult(ConvertHelper.ConvertToList<Group, MyGroupResponse>(lis).ToList());
            });
          

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
        [HttpGet("DissolveGroup")]
        public ServiceResult DissolveGroup(Guid groupId)
        {

            return Json(() =>
            {

                Group g = groupService.GetGroup(groupId);
                if (g == null)
                {
                    throw new ZzbException("无效群");
                }
                if (!groupService.IsCurrentGroup(groupId))
                {
                    throw new ZzbException("不在群中无法解散群");
 
                }
                Guid clientId = groupService.GetCurClientId();
                if (clientId != g.ClientId)
                {
                    throw new ZzbException("不是群主不能解散群");
                }
              var  result = groupService.DissolveGroup(groupId);
                return ObjectResult(result);


            });
          
        }

        /// <summary>
        /// 变更群主
        /// </summary>
        /// <param name="newMasterClientId">新群主id</param>
        /// <param name="originalMasterClientId">旧群主id</param>
        /// <param name="groupId">群id</param>
        /// <returns></returns>
        [HttpGet("TransferGroupMaster")]
        public ServiceResult TransferGroupMaster(Guid newMasterClientId, Guid originalMasterClientId, Guid groupId)
        {


            return Json(() => {

                Group g = groupService.GetGroup(groupId);
                if (g == null)
                {
                    throw new ZzbException("无效群");
                }
                if (!groupService.IsCurrentGroup(groupId))
                {
                    throw new ZzbException("不在群中无法变更群主");
                }
                Guid clientId = groupService.GetCurClientId();
                if (clientId != g.ClientId)
                {
                    throw new ZzbException("不是群主不能变更群主");
                }
               var  result = groupService.TransferGroupMaster(newMasterClientId, originalMasterClientId, groupId);
                return ObjectResult(result);

            });


        }


        #endregion

        #region GroupClient

        /// <summary>
        /// 获取当前群某一个成员
        /// </summary>
        /// <param name = "clientId" > 客户id </ param >
        /// < param name="groupId">群id</param>
        /// <returns></returns>
        [HttpGet("GetClientGroup")]
        public ServiceResult<GroupClientResponse> GetClientGroup(Guid clientId, Guid groupId)
        {
            return (ServiceResult<GroupClientResponse>)Json(() =>
            {
                return ObjectGenericityResult<GroupClientResponse>(ConvertHelper.ConvertToModel<GroupClient, GroupClientResponse>(groupService.GetGroupClient(groupId,clientId)));
            }, "获取群客户失败");
        }

        /// <summary>
        /// 获取选中群的群成员列表
        /// </summary>
        /// <param name="groupId">群id</param>
        /// <param name="isEnable">客户是否已通过入群</param>
        /// <returns></returns>
        [HttpGet("GetGroupClients")]
        public ServiceResult<IList<GroupClientResponse>> GetGroupClients(Guid groupId, bool isEnable)
        {
            return (ServiceResult<IList<GroupClientResponse>>)Json(() =>
            {
                return ObjectGenericityResult(ConvertHelper.ConvertToList<GroupClient, GroupClientResponse>(groupService.GetGroupClients(groupId, isEnable)));
            }, "获取群成员基本信息失败");
        }


        /// <summary>
        /// 获取选中群的群成员列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("GetClientsOfGroup")]
        public ServiceResult<List<GroupClientByGroupResponse>> GetClientsOfGroup(Guid groupId)
        {
            return (ServiceResult<List<GroupClientByGroupResponse>>)Json(() =>
            {
                return ListGenericityResult(groupService.GetClientsOfGroup(groupId));

            }, "获取群成员基本信息失败");
        }




        /// <summary>
        /// 更新我的某一个群的基本信息(我的群昵称/设置顶置/免打扰)等
        /// </summary>
        /// <param name="model"></param>
        [HttpPost("UpdateGroupClient")]
        public ServiceResult<bool> UpdateGroupClient(GroupClientResponse model)
        {


            List<string> ingores = new List<string>();
            ingores.Add("CreateTime");
            ingores.Add("IsEnable");
            ingores.Add("GroupId");
            ingores.Add("ClientId");
            ingores.Add("IsGroupManager");
            IList<string> updateFieldList = GetPostBodyFiledKey(ingores);

            GroupClient entity = ConvertHelper.ConvertToModel<GroupClientResponse, GroupClient>(model, updateFieldList);

            var uniqueId = Context.Groups.Where(g => g.GroupId == model.GroupId).Select(g => g.UnqiueId).FirstOrDefault();
            bool result = groupService.Update<GroupClient>(entity, "GroupClientId", updateFieldList) > 0;
            Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"修改了群成员的基本信息,群号是:{uniqueId}"));
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


            return Json(() =>
            {

                Guid groupId = requestObj.GroupId;

                List<Guid> clientIdList = requestObj.ClientIdList.Distinct().ToList();

                Group groupEntity = groupService.GetGroup(groupId,false);
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
                    Context.ClientOperateLogs.Add(new ClientOperateLog(clientId, $"加入了群聊,群号是:{groupEntity.UnqiueId}"));
                }
                var huanxinIds = Context.Clients.Where(c => clientIdList.Contains(c.ClientId)).AsNoTracking().Select(c => c.HuanXinId).ToList();
                var res = HuanxinGroupRequest.AddGroupMembers(groupEntity.HuanxinGroupId, huanxinIds.ToArray());

                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    //批量操作后再保存数据库
                    groupService.SaveChanges();

                    return ObjectResult(true);
                }

                return ObjectResult(false);
            });
 

        }

        /// <summary>
        /// 自己退群
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet("LeaveGroup")]
        public ServiceResult LeaveGroup(Guid groupId)
        {

            return Json(() =>
            {


                ServiceResult service = new ServiceResult();
                //bool result = true;
                //string msg = "操作成功";
                Guid clientId = groupService.GetCurClientId();

                if (groupService.IsCurrentGroup(groupId))
                {
                    Group g = groupService.GetGroup(groupId);
                    //群主不能退群
                    if (g != null && g.ClientId != clientId)
                    {
                        GroupClient entity = Context.GroupClients.FirstOrDefault(a => a.ClientId == clientId && a.GroupId == groupId);
                        if (entity != null)
                        {
                            var res = HuanxinGroupRequest.RemoveGroupMember(g.HuanxinGroupId, entity.Client.HuanXinId);
                            if (res.ReturnCode == ServiceResultCode.Success)
                            {
                                Context.GroupClients.Remove(entity);
                                Context.ClientOperateLogs.Add(new ClientOperateLog(clientId, $"退出了群聊,群号是:{g.UnqiueId}"));
                                Context.SaveChanges();
                            }
                            else
                            {


                                throw new ZzbException(res.Message);
                            }

                        }
                    }
                    else if (g == null)
                    {

                        throw new ZzbException("群无效,操作失败");

                    }
                    else if (g.ClientId == clientId)
                    {
                        throw new ZzbException("群主不能退群");

                    }
                }
                else
                {
                    throw new ZzbException("不在该群中,操作失败");

                }
                return JsonObjectResult(true);


            });




        }

        /// <summary>
        /// 设置别人退群
        /// </summary>
        /// <param name="requestObj"></param>
        /// <returns></returns>
        [HttpPost("SetLeaveGroup")]
        public ServiceResult SetLeaveGroup(SetLeaveGroupRequest requestObj)
        {

            return Json(() =>
            {


                Guid groupId = requestObj.GroupId;
                IList<Guid> clientIdList = requestObj.clientIdList;
                bool result = true;
                string msg = "操作成功";

                if (clientIdList == null || clientIdList.Count == 0)
                {
                    throw new ZzbException("无退群名单");
                }
                Group g = groupService.GetGroup(groupId);
                if (g == null)
                {
                    throw new ZzbException("无效群");
      
                }
                if (!groupService.IsCurrentGroup(groupId))
                {
                    throw new ZzbException("不在群中无法设置退群");
     
                }
                Guid clientId = groupService.GetCurClientId();
                if (clientIdList.Contains(clientId))
                {

                    throw new ZzbException("不能设置自己退群");
  
                }
                bool isMaster = g.ClientId == clientId;
                if (clientIdList.Contains(g.ClientId))
                {

                    throw new ZzbException("群主不能退群");
 
                }
                IList<GroupClient> gm = groupService.GetGroupManagerList(groupId);
                bool isManager = gm.FirstOrDefault(p => p.ClientId == clientId) != null;

                IList<GroupClient> leaveList = Context.GroupClients.Where(a => clientIdList.Contains(a.ClientId) && a.GroupId == groupId).ToList();
                if (!isManager && !isMaster)
                {

                    throw new ZzbException("无设置退群权限");

  
                }
                else if (isManager && !isMaster && leaveList.FirstOrDefault(p => p.IsGroupManager) != null)
                {
                    throw new ZzbException("只有群主可以设置群管理员退群"); 

                }

                var huanxinIds = string.Join(",", leaveList.Select(l => l.Client.HuanXinId).ToArray());
                var liaoxinNumbers = string.Join(",", leaveList.Select(l => l.Client.LiaoxinNumber).ToArray());
                var res = HuanxinGroupRequest.RemoveGroupMember(g.HuanxinGroupId, huanxinIds);
                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    Context.ClientOperateLogs.Add(new ClientOperateLog(clientId, $"剔除了群成员,成员列表是:{liaoxinNumbers}"));
                    Context.GroupClients.RemoveRange(leaveList);
                    Context.SaveChanges();
                    return JsonObjectResult(result, msg);
                }
                else
                {
                    return JsonObjectResult(false, res.Message);
                }


            });
           



        }

        #endregion










        ///// <summary>
        ///// 设置群管理员
        ///// </summary>
        //[HttpPost("SetGroupManager")]
        //public ServiceResult<bool> SetGroupManager(Guid groupClientId)
        //{

        //    GroupClient gc = new GroupClient();
        //    gc.GroupClientId = groupClientId;
        //    gc.IsGroupManager = true;

        //    IList<string> updateFieldList = new List<string>() { "IsGroupManager" };
        //    bool result = groupService.Update(gc, "GroupClientId", updateFieldList) > 0;

        //    return (ServiceResult<bool>)Json(() =>
        //    {
        //        return ObjectGenericityResult<bool>(result);
        //    }, "设置群管理员失败");
        //}


        ///// <summary>
        ///// 取消群管理员
        ///// </summary>
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
        //[HttpGet("GetGroups")]/api/GroupController/GetClientGroups
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