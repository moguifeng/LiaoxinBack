using Liaoxin.IBusiness;
using Liaoxin.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Liaoxin.Business
{
    public class BaseService
    {
        public LiaoxinContext Context { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public IHostingEnvironment HostingEnvironment { get; set; }
    }
}