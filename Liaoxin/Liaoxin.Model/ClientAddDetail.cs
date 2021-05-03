using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 客户添加(默认3天)
    /// </summary>
    public class ClientAddDetail : BaseModel
    {
        public ClientAddDetail()
        {
        }

        public Guid ClientAddDetailId { get; set; } = Guid.NewGuid();

        public Guid ClientAddId { get; set; }


        public virtual ClientAdd ClientAdd { get; set; }
 
        /// <summary>
        /// 当前登录客户的申请客户
        /// </summary>
        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        /// <summary>
        /// 添加备注
        /// </summary>
        public string AddRemark { get; set; }
       
        /// <summary>
        /// 添加状态
        /// </summary>
        public ClientAddDetailTypeEnum Status { get; set; }


        public enum ClientAddDetailTypeEnum {
            
            [Description("待确认")]
            StandBy = 0,
            [Description("拒绝")]
            Reject = 1,
            [Description("已添加")]
            Agree = 2,
        }
        

    }

 
}