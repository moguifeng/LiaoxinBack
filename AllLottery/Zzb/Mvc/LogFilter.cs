using Microsoft.AspNetCore.Mvc.Filters;
using System;
using Zzb.ZzbLog;

namespace Zzb.Mvc
{
    public class LogFilter : ActionFilterAttribute
    {
        private DateTime _date;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _date = DateTime.Now;
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var span = DateTime.Now - _date;
            string log = $"请求【{context.HttpContext.Request.Path}】，耗时【{span.TotalSeconds}】秒。登陆用户是[{context.HttpContext.User.Identity.Name}]";
            LogHelper.Debug(log);
            if (span.TotalSeconds > 5)
            {
                LogHelper.Error(log);
            }
            else if (span.TotalSeconds > 2)
            {
                LogHelper.Warning(log);
            }
            base.OnActionExecuted(context);
        }
    }
}