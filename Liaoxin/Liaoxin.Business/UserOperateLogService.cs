using Liaoxin.IBusiness;
using Liaoxin.Model;

namespace Liaoxin.Business
{
    public class UserOperateLogService : BaseService, IUserOperateLogService
    {
        public void Log(string message, LiaoxinContext context)
        {
            context.UserOperateLogs.Add(new UserOperateLog(message, UserId));
        }

        protected int UserId
        {
            get
            {
                if (HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    if (int.TryParse(HttpContextAccessor.HttpContext.User.Identity.Name, out var id))
                    {
                        return id;
                    }

                    return -1;
                }

                return -1;
            }
        }
    }
}