using System;
using Zzb.Common;

namespace Zzb.BaseData.StringHandle
{
    public class DateTimeHandle : BaseStringHandle
    {
        protected override string Handle(object obj)
        {
            DateTime dt = (DateTime)obj;
            return dt.ToCommonString();
        }

        protected override object GetValue(string value)
        {
            return DateTime.Parse(value);
        }
    }
}