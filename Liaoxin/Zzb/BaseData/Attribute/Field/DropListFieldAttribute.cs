using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.Common;

namespace Zzb.BaseData.Attribute.Field
{
    public class DropListFieldAttribute : BaseFieldAttribute
    {
        public DropListFieldAttribute()
        {
        }

        public DropListFieldAttribute(string name) : base(name)
        {
        }

        public DropListFieldAttribute(string id, string name) : base(id, name)
        {
        }

        public DropListFieldAttribute(string id, string name, List<DropListModel> source) : base(id, name)
        {
            Source = source;
        }

        public override string Type => "DropListFieldAttribute";

        public virtual List<DropListModel> Source { get; set; }

        public override object GetField()
        {
            if (Source == null)
            {
                if (PropertyType == typeof(bool))
                {
                    Source = new List<DropListModel> { new DropListModel("0", "否"), new DropListModel("1", "是") };
                }

                if (PropertyType.BaseType == typeof(Enum))
                {
                    Source = new List<DropListModel>();
                    foreach (int value in Enum.GetValues(PropertyType))
                    {
                        string v = ((Enum)Enum.ToObject(PropertyType, value)).ToDescriptionString();
                        Source.Add(new DropListModel(value.ToString(), v));
                    }
                }
            }
            if ((string.IsNullOrEmpty(Value) || IsInit) && Source != null)
            {
                Value = Source.First().Key;
            }
            return new { Type, Title = Name, Id, Value, IsRequired, IsReadOnly, Placeholder, Source };
        }

        public bool IsInit { get; set; } = false;
    }

    public class DropListModel
    {
        public DropListModel(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}