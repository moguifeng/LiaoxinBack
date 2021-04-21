using Microsoft.AspNetCore.Http;
using System;

namespace Zzb.BaseData.Attribute.Field
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class BaseFieldAttribute : System.Attribute
    {
        protected BaseFieldAttribute()
        {
        }

        protected BaseFieldAttribute(string id, string name)
        {
            Id = id;
            Name = name;
        }

        protected BaseFieldAttribute(string name)
        {
            Name = name;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public virtual string Value { get; set; }

        public bool IsRequired { get; set; }

        public bool IsReadOnly { get; set; } = false;

        public string Placeholder { get; set; }

        public virtual string Type => GetType().Name;

        public Type PropertyType { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public virtual object GetField()
        {
            return new { Type, Title = Name, Id, Value, IsRequired, IsReadOnly, Placeholder };
        }
    }
}