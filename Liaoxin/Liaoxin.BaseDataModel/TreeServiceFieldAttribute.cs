using Liaoxin.Model;
using Zzb.BaseData.Attribute.Field;

namespace Liaoxin.BaseDataModel
{
    public abstract class TreeServiceFieldAttribute : TreeFieldAttribute
    {
        public LiaoxinContext Context => HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(LiaoxinContext)) as LiaoxinContext;
    }
}