using AllLottery.IBusiness;
using AllLottery.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AllLottery.Business
{
    public class BaseService
    {
        public LotteryContext Context { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public IHostingEnvironment HostingEnvironment { get; set; }
    }
}