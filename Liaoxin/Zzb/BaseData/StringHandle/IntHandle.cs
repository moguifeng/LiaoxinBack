namespace Zzb.BaseData.StringHandle
{
    public class IntHandle : BaseStringHandle
    {
        protected override object GetValue(string value)
        {
            return int.Parse(value);
        }
    }
}