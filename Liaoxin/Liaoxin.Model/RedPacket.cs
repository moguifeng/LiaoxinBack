using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 红包
    /// </summary>
    public class RedPacket : BaseModel
    {
        public RedPacket()
        {
        }

        public Guid RedPacketId { get; set; } = Guid.NewGuid();
 

        public  Guid GroupId { get; set; }
        public virtual Group Group { get; set; }
 

        public virtual List<RedPacketReceive> RedPacketReceives { get; set; }

        /// <summary>
        /// 红包发送者
        /// </summary>
        public Guid ClientId { get; set; }


        /// <summary>
        /// 红包发送者
        /// </summary>
        public virtual Client Client { get; set; }


        /// <summary>
        /// 祝福语(尾数)
        /// </summary>
        public string Greeting { get; set; }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }


        /// <summary>
        /// 红包类型
        /// </summary>
        public RedPacketTypeEnum Type { get; set; }


        /// <summary>
        /// 红包个数
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 红包金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 剩余红包金额
        /// </summary>
        public decimal Over { get; set; }

          
        /// <summary>
        /// 红包状态
        /// </summary>
        public   RedPacketStatus Status { get; set; }


        public enum RedPacketTypeEnum
        {
            [Description("拼手气红包")]
           Lucky = 0,
            [Description("普通红包")]
            Normal = 1,
        }

        public enum RedPacketStatus
        {
            [Description("发出")]
            Send = 0,
            [Description("已领完")]
            End = 1,
            [Description("已退款")]
            Refund = 2,
            [Description("未领完退款")]
            NotEndRefund = 3,
        }
        
    }

 
}