using Liaoxin.Model;
using System;
using System.Collections.Generic;

namespace Liaoxin.IBusiness
{
    public interface IGroupService
    {

        /// <summary>
        /// 获取当前群成员
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        GroupClient GetClientGroup(Guid clientId, Guid groupId);
        /// <summary>
        /// 获取客户的所有群
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        IList<Group> GetClientGroups(Guid clientid, bool isEnable);

        /// <summary>
        /// 解散群
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public bool DissolveGroup(Guid groupId);

        /// <summary>
        /// 设置为管理员
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public void SetGroupManager(Guid clientId, Guid groupId, bool isExeSave = true);

        /// <summary>
        /// 取消管理员
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="groupId"></param>
        public void CancelGroupManager(Guid clientId, Guid groupId, bool isExeSave = true);

        /// <summary>
        /// 获取群所有管理员
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        IList<GroupManager> GetGroupManagerList(Guid groupId);

        /// <summary>
        /// 转让群主
        /// </summary>
        /// <param name="newMasterClientId"></param>
        /// <param name="originalMasterClientId"></param>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public bool TransferGroupMaster(Guid newMasterClientId, Guid originalMasterClientId, Guid groupId);

        /// <summary>
        /// 创建群信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool CreateGroup(Group entity);

        /// <summary>
        /// 更新群信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool UpdateGroup(Group entity, IList<string> updateFieldList = null);
        /// <summary>
        /// 获取群成员
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        IList<Client> GetGroupClients(Guid groupId, bool isEnable);

        /// <summary>
        /// 获取群
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
         Group GetGroup(Guid groupId);
        /// <summary>
        /// 审核群是否通过
        /// </summary>
        /// <param name="groupId">groupId</param>
        /// <param name="isEnable">true:通过;false:删除记录</param>
        void AuditGroup(Guid groupId, bool isEnable);

        /// <summary>
        /// 获取群
        /// </summary>
        /// <param name="isEnable">是否已通过</param>
        /// <param name="skip">起始位置,默认0</param>
        /// <param name="pageSize">记录数,默认1000</param>
        /// <returns></returns>
        IList<Group> GetGroups(bool isEnable, int skip = 0, int pageSize = 1000);
        /// <summary>
        /// 增加群成员
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="groupId"></param>
        /// <param name="isEnable">是否通过</param>
        /// <param name="isExeSave">是否立刻执行数据库</param>
        void AddGroupClient(Guid clientId, Guid groupId, bool isEnable, bool isExeSave = true, Group g = null);
        /// <summary>
        /// 更新群成员信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isExeSave">是否立刻执行数据库</param>
        void UpdateGroupClient(GroupClient entity, bool isExeSave = true);


        void SaveChanges();

        Guid GetCurClientId();

    }
}