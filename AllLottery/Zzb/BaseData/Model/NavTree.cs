using System.Collections.Generic;

namespace Zzb.BaseData.Model
{
    public class NavTree
    {
        public string Name { get; set; }

        public string NavId { get; set; }

        public List<NavTree> Children { get; set; }
    }
}