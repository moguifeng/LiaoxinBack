using Liaoxin.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.ICacheManger;

namespace Liaoxin.Cache
{
    public class RateOfGroupCacheManager : CacheManager
    {

        private readonly string RedisRateOfGroupKey = CacheRateOfGroupEx.RedisRateOfGroupKey;

        public RateOfGroupCacheManager(ICacheManager Cache, LiaoxinContext Context) : base(Cache, Context) { }

        
        public void Load()
        {

            var list = _context.RateOfGroups.Select(a => new CacheRateOfGroup()
            {
                Id = a.RateOfGroupId.ToString(),
                RateOfGroupId = a.RateOfGroupId,
                 GroupId = a.GroupId, IsEnable = a.IsEnable,
                  IsStop = a.IsStop,
                   Priority = a.Priority,
                    Rate = a.Rate
             
            }).ToList();

            _cache.Remove(RedisRateOfGroupKey);
            _cache.HashSet<string, CacheRateOfGroup>(RedisRateOfGroupKey, list);
        }

        public void Remove(string value)
        {
            _cache.HashRemove(RedisRateOfGroupKey, value.ToString());
        }

        public void Set(Guid id, CacheRateOfGroup a)
        {

            _cache.HashSet(RedisRateOfGroupKey, id.ToString(), new CacheRateOfGroup
            {
                Id = a.RateOfGroupId.ToString(),
                RateOfGroupId = a.RateOfGroupId,
                GroupId = a.GroupId,
                IsEnable = a.IsEnable,
                IsStop = a.IsStop,
                Priority = a.Priority,
                Rate = a.Rate
            });
        }
    }


    public class CacheRateOfGroupEx
    {
        public const string RedisRateOfGroupKey = "CacheRateOfGroupEx";
 
        public static List<CacheRateOfGroup> CacheRateOfGroups
        {
            get
            {
                try
                {
                    var result = CacheManager.singleCache.HashGet<CacheRateOfGroup>(RedisRateOfGroupKey);
                    return result;
                }
                catch (Exception)
                {
                    return new List<CacheRateOfGroup>();
                }
            }
        }

        public static CacheRateOfGroup GetObj(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var cache = CacheManager.singleCache.HashGet<CacheRateOfGroup>(RedisRateOfGroupKey, code.ToString());
                return cache;
            }
            return null;
            
          
        }

    }

    public static partial class ExCache
    {
 

    }

    public class CacheRateOfGroup : Iid<string>
    {
        public string Id { get; set; }

        public Guid RateOfGroupId { get; set; }

        public bool IsEnable { get; set; }

        public Guid GroupId { get; set; }

        public int Rate { get; set; }
        public bool IsStop { get; set; }

        public int Priority { get; set; }
    }

}
