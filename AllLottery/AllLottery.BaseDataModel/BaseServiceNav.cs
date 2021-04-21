using AllLottery.Model;
using Zzb.BaseData.Model;

namespace AllLottery.BaseDataModel
{
    public abstract class BaseServiceNav : BaseNav
    {
        public LotteryContext Context { get; set; }
    }
}