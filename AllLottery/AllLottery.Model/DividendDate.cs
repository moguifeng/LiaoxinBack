using System;
using System.ComponentModel.DataAnnotations;
using Zzb.EF;

namespace AllLottery.Model
{
    /// <summary>
    /// 分红期数表
    /// </summary>
    public class DividendDate : BaseModel
    {
        public DividendDate()
        {
        }

        public DividendDate(DateTime settleTime)
        {
            SettleTime = settleTime;
        }

        public int DividendDateId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [ZzbIndex(IsUnique = true)]
        public DateTime SettleTime { get; set; } = DateTime.Now;

        public bool IsCal { get; set; } = false;
        
        [Timestamp]
        public byte[] Version { get; set; }
    }
}