namespace Zzb.BaseData.Attribute.Field
{
    public class PasswordFieldAttribute : TextFieldAttribute
    {
        public PasswordFieldAttribute()
        {
        }

        public PasswordFieldAttribute(string name) : base(name)
        {
        }

        public PasswordFieldAttribute(string id, string name) : base(id, name)
        {
        }
    }
}