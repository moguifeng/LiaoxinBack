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

        public bool IsCurrentGroup(Guid groupId)
        {
            var isexist = Context.GroupClients.Where(c => c.ClientId == CurrentClientId && c.GroupId == groupId).Any();
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
        public GroupClient GetClientGroup(Guid clientId, Guid groupId)
        {
            this.IsCurrentGroup(groupId);
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
            var huanxinGroupId =     Context.Groups.Where(g => g.GroupId == groupId).Select(g => g.HuanxinGroupId).FirstOrDefault();
            var res = HuanxinGroupRequest.RemoveGroup(huanxinGroupId);

            if (res.ReturnCode == ServiceResultCode.Success)
            {
                Context.GroupClients.RemoveRange(Context.GroupClients.Where(p => p.GroupId == groupId));
                Context.Groups.RemoveRange(Context.Groups.Where(p => p.GroupId == groupId));
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
            Group g = Context.Groups.AsNoTracking().FirstOrDefault(p => p.GroupId == groupId);
            var newHuanxinId = Context.Clients.Where(c => c.ClientId == newMasterClientId).AsNoTracking().Select(c => c.HuanXinId).FirstOrDefault();
            if (g != null && g.ClientId == originalMasterClientId)
            {
                var res = HuanxinGroupRequest.TranferGroup(g.HuanxinGroupId, newHuanxinId);
                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    g.ClientId = newMasterClientId;
                    Context.Groups.Update(g);
                    CancelGroupManager(originalMasterClientId, groupId, false);
                    SetGroupManager(newMasterClientId, groupId, false);
                    return Context.SaveChanges() > 0;
                }
                else
                {
                    throw new ZzbException(res.Message);
                }
              
              
            }
            return false;
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
            AddGroupClient(entity.ClientId, entity.GroupId, true, false, entity);
            foreach (Guid clientId in clientIds)
            {
                AddGroupClient(clientId, entity.GroupId, true, false, entity);
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
        public Group GetGroup(Guid groupId)
        {
            this.IsCurrentGroup(groupId);
            return Context.Groups.AsNoTracking().FirstOrDefault(p => p.GroupId == groupId);
        }
        /// <summary>
        /// 审核群是否通过
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <param name="isEnable">true:通过;false:删除记录</param>
        public void AuditGroup(Guid groupId, bool isEnable)
        {
            this.IsCurrentGroup(groupId);
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
            this.IsCurrentGroup(groupId);
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
            var c = Context.Clients.AsNoTracking().Where(p => p.ClientId == clientId).
                Select(p => new { Clientid = p.ClientId, NickName = p.NickName }).FirstOrDefault();
            if (g != null && c != null)
            {
                GroupClient gc = new GroupClient();
                gc.GroupClientId = Guid.NewGuid();
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
            this.IsCurrentGroup(groupId);
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

        public List<GroupClientByGroupResponse> GetClientsOfGroup(Guid groupId)
        {
            this.IsCurrentGroup(groupId);

            //黑名单列表
            var blacks = Context.ClientRelationDetails.Where(crd => crd.ClientRelation.RelationType ==
            RelationTypeEnum.Black && crd.ClientRelation.ClientId == CurrentClientId).Select(crd => crd.ClientId).ToList();

            //好友列表
            var friends = Context.ClientRelationDetails.Where(crd => crd.ClientRelation.RelationType ==
       RelationTypeEnum.Friend && crd.ClientRelation.ClientId == CurrentClientId).Select(crd => crd.ClientId).ToList();

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
                entity.FriendShipType = blacks.Contains(item.ClientId) ? RelationTypeEnum.Black : 
                    friends.Contains(item.ClientId) ? 
                    RelationTypeEnum.Friend : RelationTypeEnum.Stranger;
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
