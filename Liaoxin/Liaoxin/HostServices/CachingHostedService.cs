using Liaoxin.IBusiness;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zzb.ICacheManger;

namespace Liaoxin.HostServices
{
    public class CachingHostedService : IHostedService
    {

        public  AreaCacheManager _AreaCache { get; set; }
        public IHttpContextAccessor _HttpContextAccessor { get; set; }



        public Task StartAsync(CancellationToken cancellationToken)
        {
          //  _AreaCache = _HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(AreaCacheManager)) as AreaCacheManager;
            _AreaCache.Load();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}
