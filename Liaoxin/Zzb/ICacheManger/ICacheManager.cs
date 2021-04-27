using System;
using System.Collections.Generic;
using System.Text;

namespace Zzb.ICacheManger
{
    /// <summary>
    /// Cache manager interface
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        T Get<T>(string key);

        string StringGet(string key);

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Get<T>(string key, Func<T> func);

        List<T> GetByPattern<T>(string pattern);
        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        void Set(string key, object data, int cacheTime = 0);

        void StringSet(string key, string data, int minutes = 0);

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        bool IsSet(string key);

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        void Remove(string key);

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        void RemoveByPattern(string pattern);

        void HashSet(string key, string hashkey, object value);

        [Obsolete("使用强类型方法HashSet<TId, T>代替")]
        void HashSet<T>(string key, List<T> values) where T : Iid;

        void HashSet<TId, T>(string key, IEnumerable<T> values) where T : Iid<TId>;

        void HashRemove(string key, string hashkey);

        void HashRemove(string key, List<string> hashkeys);

        T HashGet<T>(string key, string hashkey);

        List<T> HashGet<T>(string key, List<string> hashkeys);

        List<T> HashGet<T>(string key);

        bool HashExists(string key, string hashkey);

        long Increment(string key, int value = 1);

        void Lock(string resource, TimeSpan expiry, Action action);
    }
    public interface Iid
    {
        object Id { get; set; }
    }

    public interface Iid<T>
    {
        T Id { get; set; }
    }
}
