using System;
using Zzb.EF;

namespace AllLottery.Model
{
    public class DailyWage : BaseModel
    {
        public int DailyWageId { get; set; }

        /// <summary>
        /// 有效人数
        /// </summary>
        public int MenCount { get; set; }

        /// <summary>
        /// 总额
        /// </summary>
        public decimal BetMoney { get; set; }

        /// <summary>
        /// 发放比例
        /// </summary>
        public decimal Rate { get; set; }
    }
}