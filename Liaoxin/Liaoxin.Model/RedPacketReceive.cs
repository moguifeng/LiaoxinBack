using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 红包接收
    /// </summary>
    public class RedPacketReceive : BaseModel
    {
        public RedPacketReceive()
        {
        }

        public Guid RedPacketReceiveId { get; set; } = Guid.NewGuid();


        public  Guid RedPacketId { get; set; }
        public   virtual RedPacket RedPacket { get; set; }


        /// <summary>
        /// 抢到的红包
        /// </summary>
        public decimal SnatchMoney { get; set; }

        /// <summary>
        /// 红包接收者
        /// </summary>
        public Guid ClientId { get; set; }


        /// <summary>
        /// 红包接收者
        /// </summary>
        public virtual Client Client { get; set; }


        /// <summary>
        /// 是否中奖
        /// </summary>
        public bool IsWin { get; set; } = false;
        
    }

 
}