namespace Zzb.BaseData.Attribute.Field
{
    public class HiddenTextFieldAttribute : TextFieldAttribute
    {
        public HiddenTextFieldAttribute()
        {
        }

        public HiddenTextFieldAttribute(string name) : base(name)
        {
        }

        public HiddenTextFieldAttribute(string id, string name) : base(id, name)
        {
        }
    }
}