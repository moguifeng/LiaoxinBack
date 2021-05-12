using Liaoxin.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.ICacheManger;

namespace Liaoxin.Cache
{
    public class RateOfClientCacheManager : CacheManager
    {

        private readonly string RedisRateOfClientKey = CacheRateOfClientEx.RedisRateOfClientKey;

        public RateOfClientCacheManager(ICacheManager Cache, LiaoxinContext Context) : base(Cache, Context) { }

        
        public void Load()
        {

            var list = _context.RateOfClients.Select(a => new CacheRateOfClient()
            {
                Id = a.RateOfClientId.ToString(),
                RateOfClientId = a.RateOfClientId,
                 ClientId = a.ClientId, IsEnable = a.IsEnable,
                  IsStop = a.IsStop,
                   Priority = a.Priority,
                    Rate = a.Rate
             
            }).ToList();

            _cache.Remove(RedisRateOfClientKey);
            _cache.HashSet<string, CacheRateOfClient>(RedisRateOfClientKey, list);
        }

        public void Remove(string value)
        {
            _cache.HashRemove(RedisRateOfClientKey, value.ToString());
        }

        public void Set(Guid id, CacheRateOfClient a)
        {

            _cache.HashSet(RedisRateOfClientKey, id.ToString(), new CacheRateOfClient
            {
                Id = a.RateOfClientId.ToString(),
                RateOfClientId = a.RateOfClientId,
                ClientId = a.ClientId,
                IsEnable = a.IsEnable,
                IsStop = a.IsStop,
                Priority = a.Priority,
                Rate = a.Rate
            });
        }
    }


    public class CacheRateOfClientEx
    {
        public const string RedisRateOfClientKey = "CacheRateOfClientEx";
 
        public static List<CacheRateOfClient> CacheRateOfClients
        {
            get
            {
                try
                {
                    var result = CacheManager.singleCache.HashGet<CacheRateOfClient>(RedisRateOfClientKey);
                    return result;
                }
                catch (Exception)
                {
                    return new List<CacheRateOfClient>();
                }
            }
        }

        public static CacheRateOfClient GetObj(string code)
        {
            if (!string.IsNullOrEmpty(code))
            {
                var cache = CacheManager.singleCache.HashGet<CacheRateOfClient>(RedisRateOfClientKey, code.ToString());
                return cache;
            }
            return null;
            
          
        }

    }

    public static partial class ExCache
    {
 

    }

    public class CacheRateOfClient : Iid<string>
    {
        public string Id { get; set; }

        public Guid RateOfClientId { get; set; }

        public bool IsEnable { get; set; }

        public Guid ClientId { get; set; }

        public int Rate { get; set; }
        public bool IsStop { get; set; }

        public int Priority { get; set; }
    }

}
