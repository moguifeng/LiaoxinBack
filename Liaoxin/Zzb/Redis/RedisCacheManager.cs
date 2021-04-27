using Newtonsoft.Json;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.ICacheManger;

namespace Zzb.Redis
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly RedLockFactory _redLockFactory;
        private readonly RedisHelper _redisHelper;

        public RedisCacheManager( RedisHelper redisHelper)
        {
         
            _redisHelper = redisHelper;

            var multiplexers = new List<RedLockMultiplexer> { _redisHelper.Connection };
            _redLockFactory = RedLockFactory.Create(multiplexers);
        }

        public T Get<T>(string key)
        {
            var value = _redisHelper.Database.StringGet(key);
            try
            {
                if (value.HasValue)
                    return JsonConvert.DeserializeObject<T>(value);
                return default(T);
            }
            catch (Exception exception)
            {
               
                return default(T);
            }
        }

        public T Get<T>(string key, Func<T> func)
        {
            try
            {
                T value;
                var obj = _redisHelper.Database.StringGet(key);
                if (obj.IsNull)
                {
                    if (func != null)
                    {
                        value = func.Invoke();
                        _redisHelper.Database.StringSet(key, JsonConvert.SerializeObject(value));
                        return value;
                    }
                    return default(T);
                }
                value = JsonConvert.DeserializeObject<T>(obj);
                return value;
            }
            catch (Exception exception)
            {
              //  _logger.Error(exception.Message, exception);
                return default(T);
            }
        }

        public void Set(string key, object value, int minutes = 0)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            TimeSpan? timeSpan = null;
            if (minutes > 0)
            {
                timeSpan = TimeSpan.FromMinutes(minutes);
            }
            _redisHelper.Database.StringSet(key, serializedValue, timeSpan);
        }

        public bool IsSet(string key)
        {
            return _redisHelper.Database.KeyExists(key);
        }

        public List<T> GetByPattern<T>(string pattern)
        {
            var list = new List<T>();

            var endPoints = _redisHelper.Connection.GetEndPoints();
            if (endPoints.Length > 0)
            {

                var server = _redisHelper.Connection.GetServer(endPoints[0]);
                var keys = server.Keys(pattern: string.Concat('*', pattern, '*'), pageSize: 100);

                foreach (var redisKey in keys)
                {
                    var t = Get<T>(redisKey);
                    list.Add(t);
                }
            }

            return list;
        }

        public void Remove(string key)
        {
            if (key != null)
                _redisHelper.Database.KeyDelete(key);
        }

        public void RemoveByPattern(string pattern)
        {
            var endPoints = _redisHelper.Connection.GetEndPoints();
            if (endPoints.Length > 0)
            {
                var server = _redisHelper.Connection.GetServer(endPoints[0]);
                var keys = server.Keys(pattern: string.Concat('*', pattern, '*'), pageSize: 100);
                _redisHelper.Database.KeyDelete(keys.ToArray());
            }
        }

        public void HashSet(string key, string hashkey, object value)
        {
            var serializedValue = JsonConvert.SerializeObject(value);
            _redisHelper.Database.HashSet(key, hashkey, serializedValue);
        }

        public void HashSet<T>(string key, List<T> values) where T : Iid
        {
            var list = new List<HashEntry>();
            foreach (var value in values)
            {
                HashEntry e = new HashEntry(value.Id.ToString(), JsonConvert.SerializeObject(value));
                list.Add(e);
            }
            _redisHelper.Database.HashSet(key, list.ToArray());
        }

        public void HashSet<TId, T>(string key, IEnumerable<T> values) where T : Iid<TId>
        {
            var list = new List<HashEntry>();
            foreach (var value in values)
            {
                HashEntry e = new HashEntry(value.Id.ToString(), JsonConvert.SerializeObject(value));
                list.Add(e);
            }
            _redisHelper.Database.HashSet(key, list.ToArray());
        }

        public void HashRemove(string key, string hashkey)
        {
            _redisHelper.Database.HashDelete(key, hashkey);
        }

        public void HashRemove(string key, List<string> hashkeys)
        {
            var hashs = hashkeys.Select(s => (RedisValue)s).ToArray();
            _redisHelper.Database.HashDelete(key, hashs);
        }

        public T HashGet<T>(string key, string hashkey)
        {
            if (string.IsNullOrWhiteSpace(hashkey)) return default(T);

            var value = _redisHelper.Database.HashGet(key, hashkey);
            try
            {
                if (value.HasValue)
                    return JsonConvert.DeserializeObject<T>(value);
                return default(T);
            }
            catch (Exception exception)
            {
               // _logger.Debug(exception.Message, exception);
                return default(T);
            }
        }
        public List<T> HashGet<T>(string key, List<string> hashkeys)
        {
            var result = new List<T>();
            var values = _redisHelper.Database.HashGetAll(key);
            try
            {
                foreach (var hashEntry in values)
                {
                    if (hashEntry.Value.HasValue)
                        if (hashkeys.Contains(hashEntry.Name))
                            result.Add(JsonConvert.DeserializeObject<T>(hashEntry.Value));
                }
                return result;
            }
            catch (Exception exception)
            {
               // _logger.Debug(exception.Message, exception);
                return new List<T>();
            }
        }
        public List<T> HashGet<T>(string key)
        {
            var result = new List<T>();
            var values = _redisHelper.Database.HashGetAll(key);
            try
            {
                foreach (var hashEntry in values)
                {
                    if (hashEntry.Value.HasValue)
                        result.Add(JsonConvert.DeserializeObject<T>(hashEntry.Value));
                }
                return result;
            }
            catch (Exception exception)
            {
              //  _logger.Debug(exception.Message, exception);
                return new List<T>();
            }
        }

        public bool HashExists(string key, string hashkey)
        {
            return _redisHelper.Database.HashExists(key, hashkey);
        }

        public long Increment(string key, int value = 1)
        {
            return _redisHelper.Database.StringIncrement(key, value);
        }

        public void Lock(string resource, TimeSpan expiry, Action action)
        {
            var wait = TimeSpan.FromMilliseconds(50);
            var retry = TimeSpan.FromSeconds(2);
            using (var redLock = _redLockFactory.CreateLock(resource, expiry, wait, retry))
            {
                if (redLock.IsAcquired)
                {
                    action();
                }
            }
        }

        public string StringGet(string key)
        {
            var value = _redisHelper.Database.StringGet(key);
            return value;
        }

        public void StringSet(string key, string data, int minutes = 0)
        {
            TimeSpan? timeSpan = null;
            if (minutes > 0)
            {
                timeSpan = TimeSpan.FromMinutes(minutes);
            }
            _redisHelper.Database.StringSet(key, data, timeSpan);
        }
    }
}
