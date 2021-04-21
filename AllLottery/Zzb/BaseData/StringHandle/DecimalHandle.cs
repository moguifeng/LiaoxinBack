namespace Zzb.BaseData.StringHandle
{
    public class DecimalHandle : BaseStringHandle
    {
        protected override object GetValue(string value)
        {
            if (value.Contains("E"))
            {
                return decimal.Parse(value, System.Globalization.NumberStyles.Float);
            }
            return decimal.Parse(value);
        }
    }
}