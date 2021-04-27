namespace Zzb.BaseData.StringHandle
{
    public class BoolHandle : BaseStringHandle
    {
        protected override object GetValue(string value)
        {
            if (value == "1" || value == "0")
            {
                return value == "1";
            }
            return bool.Parse(value);
        }

        protected override string Handle(object obj)
        {
            return bool.Parse(obj.ToString()) ? "1" : "0";
        }
    }
}