namespace Zzb.BaseData.Attribute.Field
{
    public class TextFieldAttribute : BaseFieldAttribute
    {
        public TextFieldAttribute()
        {
        }

        public TextFieldAttribute(string name) : base(name)
        {
        }

        public TextFieldAttribute(string id, string name) : base(id, name)
        {
        }
    }
}