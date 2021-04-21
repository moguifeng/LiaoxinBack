using System;
using Zzb.EF;

namespace AllLottery.Model
{
    public class DailyWageLog:BaseModel
    {
        public DailyWageLog()
        {
        }

        public DailyWageLog(decimal rate, decimal betMoney, int betMen, decimal dailyMoney, DateTime calDate, int playerId)
        {
            Rate = rate;
            BetMoney = betMoney;
            BetMen = betMen;
            DailyMoney = dailyMoney;
            CalDate = calDate;
            PlayerId = playerId;
        }

        public int DailyWageLogId { get; set; }

        public decimal Rate { get; set; }

        public decimal BetMoney { get; set; }

        /// <summary>
        /// 总投注人数
        /// </summary>
        public int BetMen { get; set; }

        /// <summary>
        /// 日工资总额
        /// </summary>
        public decimal DailyMoney { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime CalDate { get; set; }
        
        /// <summary>
        /// 用户
        /// </summary>
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}