using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace Zzb.BaseData.StringHandle
{
    public class StringArrayHandle : BaseStringHandle
    {
        protected override object GetValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return value.Split(",");
        }

        protected override string Handle(object obj)
        {
            return (obj as string[]).Join(",");
        }
    }
}