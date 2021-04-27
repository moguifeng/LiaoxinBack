using System;

namespace Zzb.BaseData.Model
{
    public class NavModel
    {
        public NavModel()
        {
        }

        public NavModel(string navId, Type type)
        {
            NavId = navId;
            Type = type;
        }

        public string NavId { get; set; }

        public Type Type { get; set; }
    }
}