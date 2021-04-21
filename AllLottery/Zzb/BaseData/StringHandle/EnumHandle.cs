using System;

namespace Zzb.BaseData.StringHandle
{
    public class EnumHandle : BaseStringHandle
    {
        protected override string Handle(object obj)
        {
            if (obj != null)
            {
                return ((int)obj).ToString();
            }
            else
            {
                return base.Handle(null);
            }
        }

        protected override object GetValue(string value)
        {
            return Enum.ToObject(Type, int.Parse(value));
        }
    }
}