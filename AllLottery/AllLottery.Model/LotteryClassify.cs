using System.Collections.Generic;
using System.ComponentModel;
using Zzb.EF;

namespace AllLottery.Model
{
    public class LotteryClassify : BaseModel
    {
        public int LotteryClassifyId { get; set; }

        [ZzbIndex(IsUnique = true)]
        public LotteryClassifyType Type { get; set; }

        public virtual List<LotteryType> LotteryTypes { get; set; }


    }

    public enum LotteryClassifyType
    {
        [Description("时时彩")]
        Ssc = 0,
        [Description("11选5")]
        Xuan5 = 1,
        [Description("快三")]
        Kuai3 = 2,
        [Description("赛车")]
        SaiChe = 3,
        [Description("排列3D")]
        PaiLie3D = 4,
        [Description("六合彩")]
        LiuHeCai = 5,
    }
}