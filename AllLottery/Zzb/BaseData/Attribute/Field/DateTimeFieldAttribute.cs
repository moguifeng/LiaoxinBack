using System;
using Zzb.Common;

namespace Zzb.BaseData.Attribute.Field
{
    public class DateTimeFieldAttribute : BaseFieldAttribute
    {
        public DateTimeFieldAttribute()
        {
        }

        public DateTimeFieldAttribute(string name) : base(name)
        {
        }

        public DateTimeFieldAttribute(string id, string name) : base(id, name)
        {
        }

        public override object GetField()
        {
            if (DateTime.TryParse(Value,out var date))
            {
                Value = date.ToDateString();
            }
            return base.GetField();

        }
    }
}