using Liaoxin.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.ICacheManger;

namespace Zzb.ICacheManger
{
    public class AreaCacheManager
    {
        private readonly ICacheManager _cache;
        private static LiaoxinContext _context;
        private readonly string RedisAreaKey = CacheAreaEx.RedisAreaKey;
        public void Load()
        {

            var list = _context.Areas.Where(a => a.IsEnable).Select(a => new CacheArea()
            {
                Code = a.Code,
                Name = a.Name,
                FullName = a.FullName,
                Level = a.Level,
                LongCode = a.LongCode,
                ParentCode = a.ParentCode
            }).ToList();

            _cache.Remove(RedisAreaKey);
            _cache.HashSet<string, CacheArea>(RedisAreaKey, list);
        }

        //public void Remove(string code)
        //{
        //    _cache.HashRemove(RedisAreaKey, value.ToString());
        //}

        //public void Set(Guid id, CacheFarm farm)
        //{

        //    _cache.HashSet(RedisAreaKey, id.ToString(), new CacheFarm
        //    {
        //        Id = farm.Id,
        //        Name = farm.Name,
        //    });
        //}
    }


    public class CacheAreaEx
    {
        public const string RedisAreaKey = "CacheAreaEx";
        private static readonly ICacheManager _cache;

        public static List<CacheArea> Areas
        {
            get
            {
                try
                {
                    var result = _cache.HashGet<CacheArea>(RedisAreaKey);
                    return result;
                }
                catch (Exception)
                {
                    return new List<CacheArea>();
                }
            }
        }

        public static CacheArea GetObj(string code)
        {
            var cache = _cache.HashGet<CacheArea>(RedisAreaKey, code.ToString());
            return cache;
        }

    }

    public static partial class ExCache
    {
        public static string ToAreaFullName(this string Code)
        {
            var cache = CacheAreaEx.GetObj(Code);
            return cache != null ? cache.FullName : string.Empty;

        }

        public static string ToAreaName(this string Code)
        {
            var cache = CacheAreaEx.GetObj(Code);
            return cache != null ? cache.Name : string.Empty;

        }

    }

    public class CacheArea : Iid<string>
    {
        public string Id { get; set; }
        public string Code { get; set; }

        public string LongCode { get; set; }

        public string Name { get; set; }
        public string FullName { get; set; }

        public string ParentCode { get; set; }

        public int Level { get; set; }
    }

}
