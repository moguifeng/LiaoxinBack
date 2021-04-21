using System;

namespace Zzb.BaseData.Attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class NavFieldAttribute : System.Attribute
    {
        public NavFieldAttribute(string name)
        {
            Name = name;
        }

        public NavFieldAttribute(string name, bool isDisplay)
        {
            Name = name;
            IsDisplay = isDisplay;
        }

        public NavFieldAttribute(string name, int width) : this(name)
        {
            Width = width;
        }

        public string Name { get; set; }

        public bool IsDisplay { get; set; } = true;

        public bool IsKey { get; set; } = false;

        public bool IsRequired { get; set; } = false;

        public int Width { get; set; } = 100;

        public string ActionType { get; set; }
    }
}