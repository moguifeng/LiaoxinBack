using System;

namespace Zzb.BaseData.StringHandle
{
    public abstract class BaseStringHandle
    {
        protected Type Type { get; set; }

        private static BaseStringHandle CreateHanleInstance(Type type)
        {
            BaseStringHandle handle = null;
            if (type == typeof(DateTime))
            {
                handle = new DateTimeHandle();
            }

            if (type == typeof(int))
            {
                handle = new IntHandle();
            }

            if (type == typeof(string[]))
            {
                handle = new StringArrayHandle();
            }

            if (type == typeof(bool))
            {
                handle = new BoolHandle();
            }

            if (type == typeof(decimal))
            {
                handle = new DecimalHandle();
            }

            if (type.BaseType == typeof(Enum))
            {
                handle = new EnumHandle();
            }

            if (type == typeof(decimal?))
            {
                handle = new DecimalNullHandle();
            }

            if (type == typeof(int?))
            {
                handle = new IntNullHandle();
            }

            if (handle != null)
            {
                handle.Type = type;
            }
            return handle;
        }

        public static string Handle(Type type, object obj)
        {
            if (obj == null)
            {
                return null;
            }

            var ins = CreateHanleInstance(type);
            if (ins != null)
            {
                return ins.Handle(obj);
            }

            return obj.ToString();
        }

        public static object GetValue(Type type, string value)
        {
            if (value == null)
            {
                return null;
            }

            var ins = CreateHanleInstance(type);
            if (ins != null)
            {
                return ins.GetValue(value);
            }

            return value;
        }

        protected virtual string Handle(object obj)
        {
            return obj?.ToString();
        }

        protected virtual object GetValue(string value)
        {
            return value;
        }
    }
}