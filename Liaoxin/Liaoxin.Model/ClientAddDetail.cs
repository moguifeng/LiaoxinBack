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


        /// <summary>
        /// 申请者
        /// </summary>
        public Guid FromClientId { get; set; }

        

        public virtual Client FromClient { get; set; }


        /// <summary>
        /// 向申请者
        /// </summary>
        public Guid ToClientId { get; set; }

        
        public virtual Client ToClient { get; set; }

        /// <summary>
        /// 添加备注
        /// </summary>
        public string AddRemark { get; set; }

        /// <summary>
        /// 添加状态
        /// </summary>
        public ClientAddDetailTypeEnum Status { get; set; } = ClientAddDetailTypeEnum.StandBy;


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