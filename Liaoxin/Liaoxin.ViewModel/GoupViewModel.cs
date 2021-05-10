using System;
using System.Collections.Generic;
using System.Text;

namespace LIaoxin.ViewModel
{
    public class CreateGroupRequest
    {
        /// <summary>
        /// 群聊名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 群主ClientId
        /// </summary>
        //public Guid MasterClientId { get; set; }
        /// <summary>
        /// 群成员ClientId清单
        /// </summary>
        public IList<Guid> ClientIdList { get; set; }
    }

    public class AddGroupRequest
    {
        /// <summary>
        /// 群主ClientId
        /// </summary>
        public Guid GroupId { get; set; }
        /// <summary>
        /// 群成员ClientId清单
        /// </summary>
        public IList<Guid> ClientIdList { get; set; }
    }

    public class GroupResponse
    {


        public Guid GroupId { get; set; } = Guid.NewGuid();



        public string UnqiueId { get; set; }


        /// <summary>
        /// 组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 环信组id
        /// </summary>
        public string HuanxinGroupId { get; set; }


        /// <summary>
        /// 群公告
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// 群主
        /// </summary>
        public Guid ClientId { get; set; }




        /// <summary>
        /// 全员禁言
        /// </summary>
        public bool AllBlock { get; set; } = false;


        //public virtual List<GroupManager> GroupMangers { get; set; }

        /// <summary>
        /// 确认群聊邀请
        /// </summary>
        public bool SureConfirmInvite { get; set; } = false;


    }

    public class GroupClientResponse
    {
        public Guid GroupClientId { get; set; } = Guid.NewGuid();
        public Guid GroupId { get; set; }
        public Guid ClientId { get; set; }
        /// <summary>
        /// 我的群昵称
        /// </summary>
        public string MyNickName { get; set; }
        /// <summary>
        /// 显示其他成员的群昵称
        /// </summary>
        public bool ShowOtherNickName { get; set; }
        /// <summary>
        /// 保存到我的群聊
        /// </summary>
        public bool SaveMyGroup { get; set; }
        /// <summary>
        /// 保存到顶置
        /// </summary>
        public bool SetTop { get; set; }
        /// <summary>
        /// 设置为免打扰
        /// </summary>
        public bool SetNoDisturb { get; set; }
        /// <summary>
        /// 是否禁言
        /// </summary>
        public bool IsBlock { get; set; } = false;
        /// <summary>
        /// 聊天背景
        /// </summary>
        public int? BackgroundImg { get; set; }
        /// <summary>
        /// 是否群管理员
        /// </summary>
        public bool IsGroupManager { get; set; }
    }

    public class GroupManagerResponse
    {
        public Guid GroupManagerId { get; set; } = Guid.NewGuid();

        public Guid GroupId { get; set; }


        public Guid ClientId { get; set; }




    }
}
