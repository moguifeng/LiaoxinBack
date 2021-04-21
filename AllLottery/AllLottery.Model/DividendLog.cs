using System;
using Zzb.EF;

namespace AllLottery.Model
{
    public class DividendLog : BaseModel
    {
        public DividendLog()
        {
        }

        public DividendLog(decimal rate, decimal betMoney, decimal lostMoney, int betMen, decimal dividendMoney, DateTime calBeginDate, int dividendDateId, int playerId)
        {
            Rate = rate;
            BetMoney = betMoney;
            LostMoney = lostMoney;
            BetMen = betMen;
            DividendMoney = dividendMoney;
            CalBeginDate = calBeginDate;
            DividendDateId = dividendDateId;
            PlayerId = playerId;
        }

        public int DividendLogId { get; set; }

        public decimal Rate { get; set; }

        public decimal BetMoney { get; set; }

        public decimal LostMoney { get; set; }

        /// <summary>
        /// 总投注人数
        /// </summary>
        public int BetMen { get; set; }

        public decimal DividendMoney { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime CalBeginDate { get; set; }

        public int DividendDateId { get; set; }

        public virtual DividendDate DividendDate { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}