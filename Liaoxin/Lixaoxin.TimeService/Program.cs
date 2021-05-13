using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Topshelf;

namespace Lixaoxin.TimeService
{
    class Program
    {
        static ServiceRunner QuartzServiceRunner = null;

        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                
                x.Service<ServiceRunner>(s =>
                {
                    s.ConstructUsing(name => new ServiceRunner());
                    s.WhenStarted((tc, hc) => tc.Start(hc));
                    s.WhenStopped((tc, hc) => tc.Stop(hc));
                    s.WhenContinued((tc, hc) => tc.Continue(hc));
                    s.WhenPaused((tc, hc) => tc.Pause(hc));
                    
                });

                x.RunAsLocalService();
                x.StartAutomaticallyDelayed();
                x.RunAsLocalSystem();

                x.SetDescription("测试后台服务");
                x.SetDisplayName("TestServer");
                x.SetServiceName("TestServer");
                x.EnablePauseAndContinue();
            });
          
            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }




    }
}
