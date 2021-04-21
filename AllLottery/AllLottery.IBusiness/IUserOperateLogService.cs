using AllLottery.Model;

namespace AllLottery.IBusiness
{
    public interface IUserOperateLogService
    {
        void Log(string message, LotteryContext context);
    }
}