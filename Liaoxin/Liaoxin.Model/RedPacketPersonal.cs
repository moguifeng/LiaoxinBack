using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 红包(个人)
    /// </summary>
    public class RedPacketPersonal : BaseModel
    {
        public RedPacketPersonal()
        {
        }

        public Guid RedPacketPersonalId { get; set; } = Guid.NewGuid();
 
        /// <summary>
        /// 红包发送者
        /// </summary>
        public Guid FromClientId { get; set; }


        /// <summary>
        /// 红包发送者
        /// </summary>
        public virtual Client FromClient { get; set; }


        /// <summary>
        /// 红包接收者者
        /// </summary>
        public Guid ToClientId { get; set; }


        /// <summary>
        /// 红包发送者
        /// </summary>
        public virtual Client ToClient { get; set; }


        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }


        /// <summary>
        /// 红包类型
        /// </summary>
        public RedPacketTranferTypeEnum Type { get; set; }

 
   
        /// <summary>
        /// 红包金额
        /// </summary>
        public decimal Money { get; set; }
 

          
        /// <summary>
        /// 是否领取
        /// </summary>
        public   bool  IsReceive   { get; set; }


        /// <summary>
        /// 祝福语(尾数)
        /// </summary>
        public string Greeting { get; set; }


    }


    public enum RedPacketTranferTypeEnum
    {
        [Description("红包")]
        RedPacket = 0,
        [Description("转账")]
        Traner = 1,
    }



}