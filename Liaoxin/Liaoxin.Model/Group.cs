using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.Common;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 客户组
    /// </summary>
    public class Group : BaseModel
    {
        public Group()
        {
        }
 
       
        public Guid GroupId { get; set; } = Guid.NewGuid();


        /// <summary>
        /// 群号码
        /// </summary>
        [MaxLength(15)]
        [ZzbIndex(IsUnique = true)]
        public string UnqiueId  { get; set; } =  SecurityCodeHelper.CreateRandomCode(15);

        
        /// <summary>
        /// 组名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 环信组id
        /// </summary>
        public  string HuanxinGroupId { get; set; } = SecurityCodeHelper.CreateRandomCode(32);


        /// <summary>
        /// 群公告
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// 群主
        /// </summary>
        public Guid ClientId { get; set; } 

        public virtual Client Client { get; set; }


        /// <summary>
        /// 全员禁言
        /// </summary>
        public bool AllBlock { get; set; } = false;


        public virtual List<GroupManager> GroupMangers { get; set; }

        public virtual List<GroupClient> GroupClients { get;set; }

        /// <summary>
        /// 确认群聊邀请
        /// </summary>
        public bool SureConfirmInvite { get; set; } = false;

        

    }

 
}