using Liaoxin.Model;
using Zzb.BaseData.Model;

namespace Liaoxin.BaseDataModel
{
    public abstract class BaseServiceNav : BaseNav
    {
        public LotteryContext Context { get; set; }
    }
}