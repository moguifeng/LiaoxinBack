using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 群成员
    /// </summary>
    public class GroupClient : BaseModel
    {
        public GroupClient()
        {
        }

        public Guid GroupClientId { get; set; } = Guid.NewGuid(); 

        public Guid GroupId { get; set; }

        public virtual Group Group { get; set; }


        public Guid ClientId { get; set; }

        public virtual Client Client{get;set;}


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

 
}