namespace Zzb.BaseData.StringHandle
{
    public class IntNullHandle : BaseStringHandle
    {
        protected override object GetValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return int.Parse(value);
        }
    }
}