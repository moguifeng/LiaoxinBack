using System;
using System.ComponentModel.DataAnnotations;
using Zzb.EF;

namespace AllLottery.Model
{
    public class LotteryData : BaseModel
    {
        [ZzbIndex]
        public int LotteryDataId { get; set; }
        /// <summary>
        /// 玩法ID
        /// </summary>
        [ZzbIndex("WeiYi", 0, IsUnique = true)]
        public int LotteryTypeId { get; set; }

        public virtual LotteryType LotteryType { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        [ZzbIndex]
        public DateTime Time { get; set; }
        /// <summary>
        /// 开奖期号
        /// </summary>
        [ZzbIndex("WeiYi", 2, IsUnique = true)]
        [MaxLength(200)]
        public string Number { get; set; }
        /// <summary>
        /// 开奖数字
        /// </summary>
        [ZzbIndex]
        [MaxLength(200)]
        public string Data { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

    }
}