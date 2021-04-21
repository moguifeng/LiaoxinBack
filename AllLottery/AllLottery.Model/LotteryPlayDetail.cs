using Zzb.EF;

namespace AllLottery.Model
{
    public class LotteryPlayDetail : BaseModel
    {
        public int LotteryPlayDetailId { get; set; }

        public string Name { get; set; }

        public int LotteryPlayTypeId { get; set; }

        public virtual LotteryPlayType LotteryPlayType { get; set; }

        public int SortIndex { get; set; }

        public string Description { get; set; }

        public decimal MaxBetMoney { get; set; } = 10000;

        public int? MaxBetCount { get; set; }

        /// <summary>
        /// 最高赔率
        /// </summary>
        public decimal MaxOdds { get; set; }

        /// <summary>
        /// 最低赔率
        /// </summary>
        public decimal MinOdds { get; set; }

        /// <summary>
        /// 反射方法名(用于扩展)
        /// </summary>
        public string ReflectClass { get; set; }

        /// <summary>
        /// 计算赔率
        /// </summary>
        /// <param name="maxRebate"></param>
        /// <param name="currentRebate"></param>
        /// <returns></returns>
        public decimal CalculateOdds(decimal maxRebate, decimal currentRebate)
        {
            return MinOdds + (MaxOdds - MinOdds) / maxRebate * currentRebate;
        }
    }
}