using Liaoxin.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.ICacheManger;

namespace Liaoxin.Cache
{
    public class CacheManager
    {
        public ICacheManager _cache { get; set; }
        public LiaoxinContext _context { get; set; }


        public static ICacheManager singleCache = null;

        public CacheManager(ICacheManager Cache, LiaoxinContext Context)
        {
            _cache = Cache;
            CacheManager.singleCache = Cache;
            _context = Context;
        }
        public CacheManager() { }




    }
}
