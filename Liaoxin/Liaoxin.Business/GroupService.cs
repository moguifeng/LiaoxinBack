using Liaoxin.Business.Socket;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using LIaoxin.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zzb;
using Zzb.EF;
using Zzb.ZzbLog;

namespace Liaoxin.Business
{
    public class GroupService : BaseService, IGroupService
    {

        public IClientService clientService { get; set; }

        public bool IsCurrentGroup(Guid groupId,Guid? clientId= null)
        {
            if (clientId == null)
            {
                clientId = CurrentClientId;
            }
            var isexist = Context.GroupClients.AsNoTracking().FirstOrDefault(c => c.ClientId == clientId.Value && c.GroupId == groupId)!=null;
            if (isexist == false)
            {
                throw new ZzbException("你不是这个群的成员,无法操作/获取");
            }
            return true;
        }

        /// <summary>
        /// 获取当前群成员
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public GroupClient GetGroupClient(Guid groupId,Guid clientId)
        {
            this.IsCurrentGroup(groupId, clientId);
            return Context.GroupClients.AsNoTracking().FirstOrDefault(a => a.ClientId == clientId && a.GroupId == groupId);
        }

        #region Group
        /// <summary>
        /// 获取客户的所有群
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public IList<Group> GetClientGroups(Guid clientId, bool isEnable)
        {
            return (from a in Context.GroupClients.AsNoTracking() where a.ClientId == clientId && a.IsEnable == isEnable select a.Group).ToList();
        }

        /// <summary>
        /// 解散群
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public bool DissolveGroup(Guid groupId)
        {
            this.IsCurrentGroup(groupId);
            var entity = Context.Groups.Where(g => g.GroupId == groupId).Select(g => new  { HuanxinGroupId= g.HuanxinGroupId, UnqiueId = g.UnqiueId}).FirstOrDefault();
            var res = HuanxinGroupRequest.RemoveGroup(entity.HuanxinGroupId);

            if (res.ReturnCode == ServiceResultCode.Success)
            {
                Context.GroupClients.RemoveRange(Context.GroupClients.Where(p => p.GroupId == groupId));
                Context.Groups.RemoveRange(Context.Groups.Where(p => p.GroupId == groupId));
                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, "解散了群" +entity .UnqiueId));

                return Context.SaveChanges() > 0;
                
            }
            else
            {
                throw new ZzbException(res.Message);
            }
        }


        /// <summary>
        /// 转让群主
        /// </summary>
        /// <param name="newMasterClientId"></param>
        /// <param name="originalMasterClientId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public bool TransferGroupMaster(Guid newMasterClientId, Guid originalMasterClientId, Guid groupId)
        {
          

            this.IsCurrentGroup(groupId);
            Group g = Context.Groups.FirstOrDefault(p => p.GroupId == groupId);
            var entity = Context.Clients.Where(c => c.ClientId == newMasterClientId).AsNoTracking().Select(c => new { HuanXinId = c.HuanXinId, LiaoxinNumber = c.LiaoxinNumber }).FirstOrDefault();
            if (g != null && g.ClientId == originalMasterClientId)
            {
                var res = HuanxinGroupRequest.TranferGroup(g.HuanxinGroupId, entity.HuanXinId);
                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    g.ClientId = newMasterClientId;
                    Context.Groups.Update(g);
                    CancelGroupManager(originalMasterClientId, groupId, false);
                    SetGroupManager(newMasterClientId, groupId, false);

                    Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"转让了群主,新群主是:{entity.LiaoxinNumber},群号是:" + g.UnqiueId));

                    return Context.SaveChanges() > 0;
                }
                else
                {
                    throw new ZzbException(res.Message);
                }


            }
            throw new ZzbException("你已经是群主,无法转让给自己");
        }
        /// <summary>
        /// 创建群信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CreateGroup(Group entity, IList<Guid> clientIds)
        {
            bool result = false;
            //SetGroupManager(entity.ClientId, entity.GroupId, false);
            //AddGroupClient(entity.ClientId, entity.ClientId, entity.GroupId, true, false, entity);

            Context.ClientOperateLogs.Add(new ClientOperateLog(entity.ClientId, "创建了群"+entity.UnqiueId));
            if (!clientIds.Contains(entity.ClientId))
            {
                clientIds.Add(entity.ClientId);
            }
            foreach (Guid clientId in clientIds)
            {
                AddGroupClient(clientId, entity.ClientId, entity.GroupId, true, false, entity);
                Context.ClientOperateLogs.Add(new ClientOperateLog(clientId, "加入创建了群" + entity.UnqiueId));
            }
            var huanxinIds = Context.Clients.Where(c => clientIds.Contains(c.ClientId)).AsNoTracking().Select(c => c.HuanXinId).ToList();
            var res = HuanxinGroupRequest.CreateGroup(entity.Name, CurrentHuanxinId, huanxinIds.ToArray());
            if (res.ReturnCode == ServiceResultCode.Success)
            {
                entity.HuanxinGroupId = res.Data;
                Context.Groups.Add(entity);
                result = Context.SaveChanges() > 0;
            }


            return result;

        }

        /// <summary>
        /// 更新群信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateGroup(Group entity, IList<string> updateFieldList = null)
        {

            this.IsCurrentGroup(entity.GroupId);
            bool result = false;
            entity.UpdateTime = DateTime.Now;
            var entry = Context.Entry<Group>(entity);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            if (updateFieldList != null)
            {
                updateFieldList.Remove("GroupId");
                foreach (string updateField in updateFieldList)
                {
                    entry.Property(updateField).IsModified = true;
                }
                entry.Property("UpdateTime").IsModified = true;
            }
            Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"修改了群{ entity.UnqiueId}的基本信息"));
            if (updateFieldList == null)
            {
                Context.Groups.Update(entity);
           
                Context.SaveChanges();
            }
            else
            {            
                int rowCount = Context.SaveChanges();
                //Update(entity,"GroupId",updateFieldList);
            }
            result = true;
            return result;
        }

        /// <summary>
        /// 获取群
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public Group GetGroup(Guid groupId,bool checkIsGroupMember = true)
        {
            if (checkIsGroupMember)
            {
                this.IsCurrentGroup(groupId);
            }
       
            return Context.Groups.AsNoTracking().FirstOrDefault(p => p.GroupId == groupId);
        }
        /// <summary>
        /// 审核群是否通过
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <param name="isEnable">true:通过;false:删除记录</param>
        public void AuditGroup(Guid groupId, bool isEnable)
        {           
            Group g = GetGroup(groupId);
            if (g == null)
            {
                return;
            }
            Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"审核了群{ g.UnqiueId}"));
            if (isEnable)
            {
                g.IsEnable = true;
                Context.Groups.Update(g);
                Context.SaveChanges();
            }
            else
            {
                Context.Groups.Remove(g);
            }
        }

        /// <summary>
        /// 获取群
        /// </summary>
        /// <param name="isEnable">是否已通过</param>
        /// <param name="skip">起始位置,默认0</param>
        /// <param name="pageSize">记录数,默认1000</param>
        /// <returns></returns>
        public IList<Group> GetGroups(bool isEnable, int skip = 0, int pageSize = 1000)
        {
            return (from a in Context.GroupClients.AsNoTracking() where a.IsEnable == isEnable select a.Group).Skip(skip).Take(pageSize).ToList();
        }

        #endregion

        #region GroupClient
        /// <summary>
        /// 退群
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public bool ClientLeaveGroup(Guid clientId, Guid groupId)
        {
            this.IsCurrentGroup(groupId);
            bool result = false;
            Group g = GetGroup(groupId);
            //群主不能退群
            if (g != null && g.ClientId != clientId)
            {
                GroupClient entity = GetGroupClient(groupId,clientId);
                if (entity != null)
                {
                    Context.ClientOperateLogs.Add(new ClientOperateLog(clientId, $"退出了群{ g.UnqiueId}"));

                    Context.GroupClients.Remove(entity);
                    Context.SaveChanges();
                    //if (Context.GroupClients.FirstOrDefault(p => p.GroupId == groupId) == null)
                    //{
                    //    //群已经没人,自动解散
                    //    DissolveGroup(groupId);
                    //}
                    result = true;
                }
            }
            return result;
        }



        /// <summary>
        /// 获取群成员
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IList<GroupClient> GetGroupClients(Guid groupId, bool isEnable)
        {
            this.IsCurrentGroup(groupId);
            IList<GroupClient> clientList = Context.GroupClients.Where(p => p.GroupId == groupId && p.IsEnable == isEnable).ToList();
            return clientList;
        }
        /// <summary>
        /// 获取群成员
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IList<GroupClient> GetGroupClients(Guid groupId)
        {
            this.IsCurrentGroup(groupId);
            IList<GroupClient> clientList = Context.GroupClients.Where(p => p.GroupId == groupId).ToList();
            return clientList;
        }
        /// <summary>
        /// 增加群成员
        /// </summary>
        /// <param name="clientId">入群人ClientId</param>
        /// <param name="clientId">推荐人ClientId</param>
        /// <param name="groupId"></param>
        /// <param name="isEnable">是否通过</param>
        /// <param name="isExeSave">是否立刻执行数据库</param>
        public void AddGroupClient(Guid clientId,Guid? preClientId ,Guid groupId, bool isEnable, bool isExeSave = true, Group g = null)
        {
            if (g == null)
            {
                g = GetGroup(groupId,false);
            }
            var c = Context.Clients.AsNoTracking().Where(p => p.ClientId == clientId).
                Select(p => new { Clientid = p.ClientId, NickName = p.NickName }).FirstOrDefault();
            if (g != null && c != null)
            {
                GroupClient gc = new GroupClient();
                gc.GroupClientId = Guid.NewGuid();
                gc.ParentClientId = preClientId==null? CurrentClientId: preClientId.Value;
                gc.ClientId = clientId;
                gc.MyNickName = c.NickName;
                gc.ShowOtherNickName = true;
                gc.IsGroupManager = (g.ClientId == clientId);
                gc.GroupId = groupId;
                gc.IsBlock = false;
                gc.IsEnable = (isEnable || !g.SureConfirmInvite);
                Context.GroupClients.Add(gc);                
                if (isExeSave)
                {
                    Context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 更新群成员信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isExeSave"></param>
        public void UpdateGroupClient(GroupClient entity, bool isExeSave = true, IList<string> updateFieldList = null)
        {
            entity.UpdateTime = DateTime.Now;
            var entry = Context.Entry<GroupClient>(entity);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            if (updateFieldList != null)
            {
                updateFieldList.Remove("GroupClientId");
                foreach (string updateField in updateFieldList)
                {
                    entry.Property(updateField).IsModified = true;
                }
                entry.Property("UpdateTime").IsModified = true;
            }
            if (updateFieldList == null)
            {
                Context.GroupClients.Update(entity);
            }
            //Context.Entry(entity).Property("").IsModified = true;
            if (isExeSave)
            {
                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"更新了群成员{entity.ClientId}的基本信息,群号是:{entity.Group.UnqiueId}"));
                Context.SaveChanges();
            }
        }
        #endregion

        #region GroupManager
        /// <summary>
        /// 设置为管理员
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public void SetGroupManager(Guid clientId, Guid groupId, bool isExeSave = true)
        {
            this.IsCurrentGroup(groupId);
            GroupClient gm = Context.GroupClients.AsNoTracking().Include(g=>g.Group).Include(g=>g.Client).FirstOrDefault(p => p.GroupId == groupId && p.ClientId == clientId);


            if (gm != null)
            {
                gm.IsGroupManager = true;
                gm.UpdateTime = DateTime.Now;
                if (isExeSave)
                {
                    Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"将{gm.Client.LiaoxinNumber}设置成管理员,群号是:{gm.Group.UnqiueId}"));
                    Context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 取消管理员
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="groupId"></param>
        public void CancelGroupManager(Guid clientId, Guid groupId, bool isExeSave = true)
        {
            this.IsCurrentGroup(groupId);
            GroupClient gm = Context.GroupClients.AsNoTracking().Include(g => g.Group).Include(g => g.Client).FirstOrDefault(p => p.GroupId == groupId && p.ClientId == clientId);
            if (gm != null)
            {
                gm.IsGroupManager = false;
                gm.UpdateTime = DateTime.Now;
                if (isExeSave)
                {
                    Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"将{gm.Client.LiaoxinNumber}取消管理员,群号是:{gm.Group.UnqiueId}"));
                    Context.SaveChanges();
                }
            }
        }

        public List<GroupClientByGroupResponse> GetClientsOfGroup(Guid groupId)
        {
            this.IsCurrentGroup(groupId);

            var groupClients = Context.GroupClients.Where(g => g.GroupId == groupId).Select(s => new
            {
                GroupId = s.GroupId,
                GroupClientId = s.GroupClientId,
                Cover = s.Client.Cover,
                NickName = s.Client.NickName,
                ClientId = s.Client.ClientId,
                Sort = s.Group.ClientId == s.ClientId ? 1 : s.IsGroupManager ? 2 : 3,
                JoinTime = s.CreateTime,
            });
            groupClients = groupClients.OrderByDescending(g => g.Sort);

            var managers = groupClients.Where(s => s.Sort == 1 || s.Sort == 2).OrderByDescending(s => s.Sort);
            var clients = groupClients.Where(s => s.Sort == 3).OrderByDescending(s => s.JoinTime);
            List<GroupClientByGroupResponse> lis = new List<GroupClientByGroupResponse>();
            foreach (var item in managers)
            {
                GroupClientByGroupResponse entity = new GroupClientByGroupResponse();
                entity.GroupId = item.GroupId;
                entity.GroupClientId = item.GroupClientId;
                entity.Cover = item.Cover;
                entity.NickName = item.Sort == 1 ? "群主-" + item.NickName : "管理-" + item.NickName;
                entity.ClientId = item.ClientId;
                entity.FriendShipType = clientService.GetRelationThoughtClientId(item.ClientId);                 
                lis.Add(entity);
            }

            foreach (var item in clients)
            {
                GroupClientByGroupResponse entity = new GroupClientByGroupResponse();
                entity.GroupId = item.GroupId;
                entity.GroupClientId = item.GroupClientId;
                entity.Cover = item.Cover;
                entity.NickName = item.NickName;
                entity.ClientId = item.ClientId;

                lis.Add(entity);
            }
            return lis;
        }

        /// <summary>
        /// 获取群所有管理员
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IList<GroupClient> GetGroupManagerList(Guid groupId)
        {
            return Context.GroupClients.AsNoTracking().Where(p => p.GroupId == groupId && p.IsGroupManager).ToList();
        }
        #endregion



        public void SaveChanges()
        {
            Context.SaveChanges();
        }
        public Guid GetCurClientId()
        {
            return CurrentClientId;
        }
    }
}
