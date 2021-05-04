using Liaoxin.IBusiness;
using Liaoxin.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using Zzb.Context;

namespace Liaoxin.Business
{
    public class BaseService
    {
        public LiaoxinContext Context { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public IHostingEnvironment HostingEnvironment { get; set; }

        protected Guid ClientId
        {

            get
            {
                if (UserContext.Current.IsAuthenticated)
                {
                    return UserContext.Current.Id;
                }
                return Guid.Empty;


            }
        }

    }
}