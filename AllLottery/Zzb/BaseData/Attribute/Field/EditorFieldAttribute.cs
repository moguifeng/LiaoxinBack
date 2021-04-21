namespace Zzb.BaseData.Attribute.Field
{
    public class EditorFieldAttribute : BaseFieldAttribute
    {
        public EditorFieldAttribute()
        {
        }

        public EditorFieldAttribute(string name) : base(name)
        {
        }

        public EditorFieldAttribute(string id, string name) : base(id, name)
        {
        }
    }
}