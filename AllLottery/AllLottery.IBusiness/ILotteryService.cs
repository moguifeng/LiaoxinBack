using AllLottery.Model;

namespace AllLottery.IBusiness
{
    public interface ILotteryService
    {
        LotteryType[] GetAllLotteryTypes();

        LotteryType GetLotteryType(int id);

        LotteryData[] GetNewDatas(int id, int size);
    }
}