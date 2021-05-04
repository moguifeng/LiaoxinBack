using Liaoxin.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Zzb.ICacheManger;

namespace Liaoxin.Cache
{
    public class EnumCacheManager : CacheManager
    {

        private readonly string RedisEnumKey = CacheEnumEx.RedisEnumKey;

        public EnumCacheManager(ICacheManager Cache, LiaoxinContext Context) : base(Cache, Context) { }

        
        public void Load()
        {
            List<CacheEnum> lis = new List<CacheEnum>();

            foreach (Type type in Assembly.Load("Liaoxin.Model").GetTypes())
            {

                if (type.IsEnum)
                {
                    List<CacheDetail> details = new List<CacheDetail>();
    
                    foreach (var field in type.GetFields())
                    {
                        if (field.CustomAttributes.Count() > 0)
                        {
                            CacheDetail detail = new CacheDetail();

                            var attr = field.GetCustomAttribute<DescriptionAttribute>();
                                detail.Description = attr.Description;
                            detail.Text = field.Name;
                            detail.Value = (int)field.GetValue(null);
                            details.Add(detail);
                        }



                    }
                    lis.Add(new CacheEnum() { Id = type.Name.ToLower(), CacheDetails = details });
                }
            }

            _cache.Remove(RedisEnumKey);
            _cache.HashSet<string, CacheEnum>(RedisEnumKey, lis);

        }

 
    }


    public class CacheEnumEx
    {
        public const string RedisEnumKey = "CacheEnumEx";
 
        public static List<CacheEnum> Enums
        {
            get
            {
                try
                {
                    var result = CacheManager.singleCache.HashGet<CacheEnum>(RedisEnumKey);
                    return result;
                }
                catch (Exception)
                {
                    return new List<CacheEnum>();
                }
            }
        }

        public static CacheEnum GetObj(string enumText)
        {
            if (!string.IsNullOrEmpty(enumText.ToLower()))
            {
                var cache = CacheManager.singleCache.HashGet<CacheEnum>(RedisEnumKey, enumText.ToLower().ToString());
                return cache;
            }
            return null;
            
          
        }

    }

    public static partial class ExCache
    {
        public static List<CacheDetail> ToEnums(this string enumText)
        {
            var cache = CacheEnumEx.GetObj(enumText);
            
            return cache != null ? cache.CacheDetails : new List<CacheDetail>();

        }

 

    }

    public class CacheEnum : Iid<string>
    {
        public string Id { get; set; }
             
        public List<CacheDetail> CacheDetails { get; set; }
    }

    public class CacheDetail
    {
        public string Text { get; set; }
        public int Value { get; set; }

        public string Description { get; set; }
    }


}
