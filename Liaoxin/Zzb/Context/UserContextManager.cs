using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Zzb.ICacheManger;

namespace Zzb.Context
{
    public class UserContextManager
    {
        private readonly ICacheManager _cacheManager;
        private readonly int TTL = 1440;
        private readonly string _appId;

        public UserContextManager(IConfiguration configuration, ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            var ttlString = configuration["TTL"];
            if (!string.IsNullOrWhiteSpace(ttlString))
            {
                int ttl;
                if (int.TryParse(ttlString, out ttl))
                    TTL = ttl;
            }

            _appId = configuration["AppId"];
        }

        /// <summary>
        /// 获取用户数据，并缓存登陆信息
        /// </summary>
        /// <param name="userId">id</param>
        /// <param name="name">姓名</param>
        /// <param name="accountName">账号</param>
        /// <param name="rememberMe">记住我</param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public void SetUserContext(Guid userId, string name, string accountName, bool rememberMe = false, int? duration = null)
        {
            UserContext.Current.Id = userId;
            UserContext.Current.Name = name;
            UserContext.Current.AccountName = accountName;
            UserContext.Current.Token = HashEncryptHelper.MD5Encrypt(accountName + Guid.NewGuid());


            var tokenPri = (userId.ToString() + name + accountName);
          
            if (!string.IsNullOrEmpty(_cacheManager.Get<string>(tokenPri)))
            {
                var previousToken = _cacheManager.Get<string>(tokenPri);
                previousToken =   this.GetTokenCacheKey(previousToken);
                _cacheManager.Remove(previousToken);
            }

            var cacheKey = GetTokenCacheKey(UserContext.Current.Token);
            var ttl = TTL;
            if (rememberMe)
                ttl = 7 * 24 * 60;
            else if (duration.HasValue)
                ttl = duration.Value;

            _cacheManager.Set(tokenPri, cacheKey);
            _cacheManager.Set(cacheKey,
                new { Id = userId, Name = name, AccountName = accountName, UserContext.Current.Token, Time = DateTime.Now }, ttl);
            UserContext.Current.IsAuthenticated = true;
        }

        /// <summary>
        /// 加载用户上下文
        /// </summary> 
        /// <param name="token"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public bool LoadUserContext(string token, string appId = null)
        {
            var cacheKey = GetTokenCacheKey(token, appId);
            var userCache = _cacheManager.Get<UserContext>(cacheKey);

            if (userCache == null)
            {
                UserContext.Current.IsAuthenticated = false;
                return false;
            }

            foreach (var c in userCache)
            {
                UserContext.Current.SetProperty(c.Key, c.Value);
            }

            userCache.LastTime = DateTime.Now;
            _cacheManager.Set(cacheKey, userCache, TTL);


            //UserContext.Current.Id = userCache.Id;
            //UserContext.Current.Name = userCache.Name;
            //UserContext.Current.AccountName = userCache.AccountName;
            UserContext.Current.IsAuthenticated = true;
            return true;
        }

        /// <summary>
        /// 检测token的用户上线问
        /// </summary> 
        /// <param name="token"></param>
        /// <returns></returns>
        public ServiceResult CheckUserContext(string token)
        {
            var cacheKey = GetTokenCacheKey(token);
            var userCache = _cacheManager.Get<UserContext>(cacheKey);

            if (userCache == null)
            {
                UserContext.Current.IsAuthenticated = false;
                return new ServiceResult(ServiceResultCode.IllegalOperation, "用户未登陆");
            }

            UserContext.Current.IsAuthenticated = true;
            return new ServiceResult(ServiceResultCode.Success);
        }

        public void RemoveUserContext()
        {
            string cacheKey = GetTokenCacheKey(UserContext.Current.Token);
            _cacheManager.Remove(cacheKey);
        }

        private string GetTokenCacheKey(string token, string appId = null)
        {
            if (!string.IsNullOrEmpty(appId))
                return string.Format("{0}:{1}", appId, token);
            if (string.IsNullOrWhiteSpace(_appId))
                return token;
            return string.Format("{0}:{1}", _appId, token);
        }
    }
}
