using AllLottery.Model;
using Zzb.BaseData.Attribute.Field;

namespace AllLottery.BaseDataModel
{
    public abstract class TreeServiceFieldAttribute : TreeFieldAttribute
    {
        public LotteryContext Context => HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(LotteryContext)) as LotteryContext;
    }
}