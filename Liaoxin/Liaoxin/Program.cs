using Liaoxin.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Zzb.Mvc;

namespace Liaoxin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).UseUrls("http://*:22001").Build();
          
            host.ZzbInitEf<LiaoxinContext>(Configuration.Seed);

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
