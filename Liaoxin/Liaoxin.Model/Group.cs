using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
 
       
        public int GroupId { get; set; }


        /// <summary>
        /// 组名称
        /// </summary>
       public string Name { get; set; }

        /// <summary>
        /// 环信组id
        /// </summary>
        public  string HuanxinGroupId { get; set; }


        /// <summary>
        /// 群公告
        /// </summary>
        public string Notice { get; set; }

        /// <summary>
        /// 群主
        /// </summary>
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }


        /// <summary>
        /// 全员禁言
        /// </summary>
        public bool AllBlock { get; set; }


        /// <summary>
        /// 确认群聊邀请
        /// </summary>
        public bool SureConfirmInvite { get; set; }

        

    }

 
}