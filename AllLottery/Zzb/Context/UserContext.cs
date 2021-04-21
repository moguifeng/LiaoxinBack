using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Zzb.Context
{
    /// <summary>
    /// 应用上下文
    /// </summary>
    public class UserContext : ConcurrentDictionary<string, object>
    {
        private static readonly AsyncLocal<UserContext> ThreadLocal = new AsyncLocal<UserContext>();

        [JsonIgnore]
        public static UserContext Current
        {
            get
            {
                if (ThreadLocal.Value == null)
                    ThreadLocal.Value = new UserContext();
                return ThreadLocal.Value;
            }
        }

        [JsonIgnore]
        public Guid Id
        {
            get { return GetGuidProperty("Id"); }
            set { SetProperty("Id", value); }
        }

        /// <summary>
        /// 令牌
        /// </summary>
        [JsonIgnore]
        public string Token
        {
            get { return GetProperty<string>("Token"); }
            set { SetProperty("Token", value); }
        }

        ///<summary>
        ///用户名
        ///</summary>
        [JsonIgnore]
        public string Name
        {
            get { return GetProperty<string>("Name"); }
            set { SetProperty("Name", value); }
        }

        ///<summary>
        ///账号
        ///</summary>
        [JsonIgnore]
        public string AccountName
        {
            get { return GetProperty<string>("AccountName"); }
            set { SetProperty("AccountName", value); }
        }

        [JsonIgnore]
        public bool IsAuthenticated { get; set; }

        [JsonIgnore]
        public bool IsMobile { get; set; }

        public string UserAgent { get; set; }

        /// <summary>
        /// 最近访问时间
        /// </summary>
        public DateTime LastTime
        {
            get { return GetProperty<DateTime>("LastTime"); }
            set { SetProperty("LastTime", value); }
        }

        public T GetProperty<T>(string key)
        {
            object value;
            if (TryGetValue(key, out value))
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }

            var firstChar = key.Substring(0, 1).ToLower();
            if (TryGetValue(firstChar + key.Substring(1), out value))
                return (T)Convert.ChangeType(value, typeof(T));

            // 用小写名称获取值，以解决手机微信/QQ浏览器擅自篡改header的问题
            if (TryGetValue(key.ToLower(), out value))
                return (T)Convert.ChangeType(value, typeof(T));

            return default(T);
        }

        public void SetProperty(string key, object value)
        {
            AddOrUpdate(key, value, (s, o) => value);
        }

        private Guid GetGuidProperty(string key)
        {
            if (TryGetValue(key, out var value))
                return Guid.Parse(value.ToString());

            if (TryGetValue(key.ToLower(), out value))
                return Guid.Parse(value.ToString());

            return Guid.Empty;
        }
    }
}
