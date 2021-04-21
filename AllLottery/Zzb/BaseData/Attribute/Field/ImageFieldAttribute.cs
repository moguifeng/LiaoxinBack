namespace Zzb.BaseData.Attribute.Field
{
    public class ImageFieldAttribute : BaseFieldAttribute
    {
        public ImageFieldAttribute()
        {
        }

        public ImageFieldAttribute(string name) : base(name)
        {
        }

        public ImageFieldAttribute(string id, string name) : base(id, name)
        {
        }

        public string BackColor { get; set; }

        public override object GetField()
        {
            return new { Type, Title = Name, Id, Value, IsRequired, IsReadOnly, Placeholder, BackColor };
        }
    }
}