using System.Collections.Generic;

namespace Zzb.BaseData.Attribute.Field
{
    public abstract class TreeFieldAttribute : BaseFieldAttribute
    {
        public abstract List<TreesModel> Source { get; }

        public override string Type => "TreeFieldAttribute";

        public override object GetField()
        {
            return new { Type, Title = Name, Id, Value, IsRequired, IsReadOnly, Placeholder, Source };
        }
    }

    public class TreesModel
    {
        public string Value { get; set; }

        public string Title { get; set; }

        public List<TreesModel> TreesModels { get; set; }
    }
}