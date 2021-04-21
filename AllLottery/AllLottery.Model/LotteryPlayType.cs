using System.Collections.Generic;
using Zzb.EF;

namespace AllLottery.Model
{
    public class LotteryPlayType : BaseModel
    {
        public int LotteryPlayTypeId { get; set; }

        public string Name { get; set; }

        public int LotteryTypeId { get; set; }

        public virtual LotteryType LotteryType { get; set; }

        public virtual List<LotteryPlayDetail> LotteryPlayDetails { get; set; }

        public int SortIndex { get; set; }
    }
}