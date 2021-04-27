namespace Zzb.BaseData.Attribute.Field
{
    public class NumberFieldAttribute : BaseFieldAttribute
    {
        public NumberFieldAttribute()
        {
        }

        public NumberFieldAttribute(string name) : base(name)
        {
        }

        public NumberFieldAttribute(string name, int min, int max) : base(name)
        {
            Min = min;
            Max = max;
        }

        public int Min { get; set; }

        public int Max { get; set; }

        public override string Value { get; set; } = "0";
    }
}