using System;
using System.IO;
using System.Reflection;
using Serilog;
using Serilog.Events;

namespace Zzb.ZzbLog
{
    public static class LogHelper
    {
        static LogHelper()
        {
            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            Serilog.Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()//最小的输出单位是Debug级别的
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)//将Microsoft前缀的日志的最小输出级别改成Information
                .Enrich.FromLogContext()
                .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(t => t.Level == LogEventLevel.Debug).WriteTo.File(Path.Combine(path, @"Logs\Debug-.txt"), rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 10240000, retainedFileCountLimit: 10))
                .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(t => t.Level == LogEventLevel.Information).WriteTo.File(Path.Combine(path, @"Logs\Info-.txt"), rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 10240000, retainedFileCountLimit: 10))
                .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(t => t.Level == LogEventLevel.Error).WriteTo.File(Path.Combine(path, @"Logs\Error-.txt"), rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 10240000, retainedFileCountLimit: 10))
                .WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(t => t.Level == LogEventLevel.Warning).WriteTo.File(Path.Combine(path, @"Logs\Warning-.txt"), rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 10240000, retainedFileCountLimit: 10))
                //.WriteTo.File(@"C:\logFiles\Debug-.txt", LogEventLevel.Debug)//将日志输出到目标路径，文件的生成方式为每天生成一个文件
                //.WriteTo.File(@"C:\logFiles\Information-.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day)//将日志输出到目标路径，文件的生成方式为每天生成一个文件
                .CreateLogger();
        }

        public static void Debug(string msg, Exception e = null)
        {
            if (e != null)
            {
                Serilog.Log.Debug(e, msg);
            }
            else
            {
                Serilog.Log.Debug(msg);
            }

        }

        public static void Information(string msg, Exception e = null)
        {
            if (e != null)
            {
                Serilog.Log.Information(e, msg);
            }
            else
            {
                Serilog.Log.Information(msg);
            }

        }

        public static void Error(string msg, Exception e = null)
        {
            if (e != null)
            {
                Serilog.Log.Error(e, msg);
            }
            else
            {
                Serilog.Log.Error(msg);
            }

        }

        public static void Warning(string msg, Exception e = null)
        {
            if (e != null)
            {
                Serilog.Log.Warning(e, msg);
            }
            else
            {
                Serilog.Log.Warning(msg);
            }

        }
    }
}