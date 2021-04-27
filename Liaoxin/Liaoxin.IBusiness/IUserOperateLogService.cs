using Liaoxin.Model;

namespace Liaoxin.IBusiness
{
    public interface IUserOperateLogService
    {
        void Log(string message, LiaoxinContext context);
    }
}