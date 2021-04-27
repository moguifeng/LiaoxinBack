namespace Zzb.BaseData.StringHandle
{
    public class DecimalNullHandle : BaseStringHandle
    {
        protected override object GetValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return decimal.Parse(value);
        }
    }
}