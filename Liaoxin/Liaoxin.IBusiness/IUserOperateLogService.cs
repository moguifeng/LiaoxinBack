using Liaoxin.Model;

namespace Liaoxin.IBusiness
{
    public interface IUserOperateLogService
    {
        void Log(string message, LotteryContext context);
    }
}