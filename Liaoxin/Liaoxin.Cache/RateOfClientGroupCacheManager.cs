using Liaoxin.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.ICacheManger;

namespace Liaoxin.Cache
{
    public class RateOfClientGroupCacheManager : CacheManager
    {

        private readonly string RedisRateOfClientGroupKey = CacheRateOfClinetGroupEx.RedisRateOfClientGroupKey;

        public RateOfClientGroupCacheManager(ICacheManager Cache, LiaoxinContext Context) : base(Cache, Context) { }

        
        public void Load()
        {

            var list = _context.RateOfGroupClients.Select(a => new CacheRateOfClientGroup()
            {
                Id = a.RateOfGroupClientId.ToString(),
                RateOfGroupClientId = a.RateOfGroupClientId,
                 GroupId = a.GroupId, IsEnable = a.IsEnable,ClientId = a.ClientId,
                  IsStop = a.IsStop,
                   Priority = a.Priority,
                    Rate = a.Rate
             
            }).ToList();

            _cache.Remove(RedisRateOfClientGroupKey);
            _cache.HashSet<string, CacheRateOfClientGroup>(RedisRateOfClientGroupKey, list);
        }

        public void Remove(string value)
        {
            _cache.HashRemove(RedisRateOfClientGroupKey, value.ToString());
        }

        public void Set(Guid id, CacheRateOfClientGroup a)
        {

            _cache.HashSet(RedisRateOfClientGroupKey, id.ToString(), new CacheRateOfClientGroup
            {
                Id = a.RateOfGroupClientId.ToString(),
                RateOfGroupClientId = a.RateOfGroupClientId,
                GroupId = a.GroupId,
                IsEnable = a.IsEnable,
                ClientId = a.ClientId,
                IsStop = a.IsStop,
                Priority = a.Priority,
                Rate = a.Rate
            });
        }
    }


    public class CacheRateOfClinetGroupEx
    {
        public const string RedisRateOfClientGroupKey = "CacheRateOfClientGroupEx";
 
        public static List<CacheRateOfClientGroup> CacheRateOfClientGroups
        {
            get
            {
                try
                {
                    var result = CacheManager.singleCache.HashGet<CacheRateOfClientGroup>(RedisRateOfClientGroupKey);
                    return result;
                }
                catch (Exception)
                {
                    return new List<CacheRateOfClientGroup>();
                }
            }
        }

        public static CacheRateOfClientGroup GetObj(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var cache = CacheManager.singleCache.HashGet<CacheRateOfClientGroup>(RedisRateOfClientGroupKey, code.ToString());
                return cache;
            }
            return null;
            
          
        }

    }

    public static partial class ExCache
    {
 

    }

    public class CacheRateOfClientGroup : Iid<string>
    {
        public string Id { get; set; }

        public Guid RateOfGroupClientId { get; set; }

        public bool IsEnable { get; set; }

        public Guid GroupId { get; set; }
        
        public Guid ClientId { get; set; }

        public int Rate { get; set; }
        public bool IsStop { get; set; }

        public int Priority { get; set; }
    }

}
