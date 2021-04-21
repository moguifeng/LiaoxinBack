using Zzb.EF;

namespace AllLottery.Model
{
    public class DividendSetting : BaseModel
    {
        public DividendSetting()
        {
        }

        public DividendSetting(int menCount, decimal betMoney, decimal lostMoney, decimal rate)
        {
            MenCount = menCount;
            BetMoney = betMoney;
            LostMoney = lostMoney;
            Rate = rate;
        }

        public int DividendSettingId { get; set; }

        /// <summary>
        /// 有效人数
        /// </summary>
        public int MenCount { get; set; }

        /// <summary>
        /// 总额
        /// </summary>
        public decimal BetMoney { get; set; }

        /// <summary>
        /// 亏盈
        /// </summary>
        public decimal LostMoney { get; set; }

        /// <summary>
        /// 发放比例
        /// </summary>
        public decimal Rate { get; set; }
    }
}