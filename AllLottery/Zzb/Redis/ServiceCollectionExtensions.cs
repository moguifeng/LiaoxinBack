using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Zzb.ICacheManger;

namespace Zzb.Redis
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRedisCache(this IServiceCollection services, Action<RedisOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            services.Configure(options);
            services.AddSingleton<RedisHelper>();
            services.AddSingleton<ICacheManager, RedisCacheManager>();

            return services;
        }

        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfigurationSection configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (!string.IsNullOrEmpty(configuration.Value))
                services.ConfigureJsonValue<RedisOptions>(configuration);
            else
                services.Configure<RedisOptions>(configuration);
            services.AddSingleton<RedisHelper>();
            services.AddSingleton<ICacheManager, RedisCacheManager>();

            return services;
        }
    }
}
