using System;
using Zzb.EF;

namespace AllLottery.Model
{
    public class LotteryOpenTime : BaseModel
    {
        public int LotteryOpenTimeId { get; set; }
        /// <summary>
        /// 期数
        /// </summary>
        public int OpenNumber { get; set; }
        /// <summary>
        /// 开奖时间
        /// </summary>
        public TimeSpan OpenTime { get; set; }

        /// <summary>
        /// 是否明天
        /// </summary>
        public bool IsTomorrow { get; set; } = false;

        /// <summary>
        /// 彩种ID
        /// </summary>
        public int LotteryTypeId { get; set; }

        /// <summary>
        /// 彩种
        /// </summary>
        public virtual LotteryType LotteryType { get; set; }
    }
}