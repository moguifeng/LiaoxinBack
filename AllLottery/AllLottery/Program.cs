using AllLottery.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Zzb.Mvc;

namespace AllLottery
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).UseUrls("http://*:22001").Build();
          
            host.ZzbInitEf<LotteryContext>(Configuration.Seed);

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
