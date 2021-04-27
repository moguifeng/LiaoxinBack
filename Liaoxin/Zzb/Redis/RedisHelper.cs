using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Zzb.Redis
{
    public class RedisHelper
    {
        public readonly ConnectionMultiplexer Connection;
        public readonly IDatabase Database;
       

        public RedisHelper(IOptions<RedisOptions> optionsAccessor )
        {
            
            var options = optionsAccessor.Value;
            if (options.Endpoints == null || options.Endpoints.Length == 0)
                throw new ArgumentNullException(nameof(options.Endpoints));

            var configurationOptions = new ConfigurationOptions
            {
                AllowAdmin = options.AllowAdmin,
                AbortOnConnectFail = options.AbortOnConnectFail,
                ConnectTimeout = options.ConnectTimeout,
                SyncTimeout = options.SyncTimeout,
                DefaultDatabase = options.Db
            };
            foreach (var endpoint in options.Endpoints)
            {   
                configurationOptions.EndPoints.Add(endpoint);
            }

            Connection = ConnectionMultiplexer.Connect(configurationOptions);
            Connection.IncludeDetailInExceptions = true;
            Connection.ErrorMessage += ConnectionOnErrorMessage;
            Connection.InternalError += ConnectionOnInternalError;

            Database = Connection.GetDatabase(options.Db);
        }

        private void ConnectionOnInternalError(object sender, InternalErrorEventArgs e)
        {
          
        }

        private void ConnectionOnErrorMessage(object sender, RedisErrorEventArgs e)
        {
            
        }
    }
}
