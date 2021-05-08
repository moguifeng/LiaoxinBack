using Liaoxin.Business.Socket;
using Liaoxin.IBusiness;
using Liaoxin.Model;
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


        /// <summary>
        /// 获取当前群成员
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public GroupClient GetClientGroup(Guid clientId, Guid groupId)
        {
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
            bool result = false;
            //Context.GroupManagers.RemoveRange(Context.GroupManagers.Where(p => p.GroupId == groupId));
            Context.GroupClients.RemoveRange(Context.GroupClients.Where(p => p.GroupId == groupId));
            Context.Groups.RemoveRange(Context.Groups.Where(p => p.GroupId == groupId));
            Context.SaveChanges();
            result = true;
            return result;
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
            bool result = false;
            Group g = Context.Groups.AsNoTracking().FirstOrDefault(p => p.GroupId == groupId);
            if (g != null && g.ClientId == originalMasterClientId)
            {
                g.ClientId = newMasterClientId;
                Context.Groups.Update(g);
                CancelGroupManager(originalMasterClientId, groupId, false);
                SetGroupManager(newMasterClientId, groupId, false);
                Context.SaveChanges();
                result = true;
            }
            return false;
        }
        /// <summary>
        /// 创建群信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CreateGroup(Group entity,IList<Guid>clientIds)
        {
            bool result = false;
            entity.IsEnable = false;
            Context.Groups.Add(entity);
            //SetGroupManager(entity.ClientId, entity.GroupId, false);
            AddGroupClient(entity.ClientId, entity.GroupId, true, false, entity);
            foreach (Guid clientId in clientIds)
            {
                AddGroupClient(clientId, entity.GroupId, true, false, entity);
            }
            Context.SaveChanges();
            result = true;
            return result;

        }

        /// <summary>
        /// 更新群信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateGroup(Group entity, IList<string> updateFieldList = null)
        {
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
            if (updateFieldList == null)
            {
                Context.Groups.Update(entity);
                Context.SaveChanges();
            }
            else
            {
                int rowCount=Context.SaveChanges();
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
        public Group GetGroup(Guid groupId)
        {
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
            bool result = false;
            Group g = GetGroup(groupId);
            //群主不能退群
            if (g != null && g.ClientId != clientId)
            {
                GroupClient entity = GetClientGroup(clientId, groupId);
                if (entity != null)
                {
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
            IList<GroupClient> clientList =  Context.GroupClients.Where(p=> p.GroupId == groupId && p.IsEnable==isEnable ).ToList();
            return clientList;
        }
        /// <summary>
        /// 获取群成员
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IList<GroupClient> GetGroupClients(Guid groupId)
        {
            IList<GroupClient> clientList = Context.GroupClients.Where(p => p.GroupId == groupId).ToList();
            return clientList;
        }
        /// <summary>
        /// 增加群成员
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="groupId"></param>
        /// <param name="isEnable">是否通过</param>
        /// <param name="isExeSave">是否立刻执行数据库</param>
        public void AddGroupClient(Guid clientId, Guid groupId, bool isEnable, bool isExeSave = true, Group g = null)
        {
            if (g == null)
            {
                g = GetGroup(groupId);
            }
            Client c = Context.Clients.AsNoTracking().FirstOrDefault(p=>p.ClientId== clientId);
            if (g != null&&c!=null)
            {
                GroupClient gc = new GroupClient();
                gc.GroupClientId = Guid.NewGuid();
                gc.ClientId = clientId;
                gc.MyNickName = c.NickName;
                gc.ShowOtherNickName=true;
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
            GroupClient gm = Context.GroupClients.AsNoTracking().FirstOrDefault(p => p.GroupId == groupId && p.ClientId == clientId);

            
            if (gm != null)
            {
                gm.IsGroupManager = true;
                gm.UpdateTime = DateTime.Now;
                if (isExeSave)
                {
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
            GroupClient gm = Context.GroupClients.AsNoTracking().FirstOrDefault(p => p.GroupId == groupId && p.ClientId == clientId);
            if (gm != null)
            {
                gm.IsGroupManager = false;
                gm.UpdateTime = DateTime.Now;
                if (isExeSave)
                {
                    Context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// 获取群所有管理员
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IList<GroupClient> GetGroupManagerList(Guid groupId)
        {
            return Context.GroupClients.AsNoTracking().Where(p => p.GroupId == groupId&&p.IsGroupManager).ToList();
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
