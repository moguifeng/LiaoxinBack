using AllLottery.Model;

namespace AllLottery.IBusiness
{
    public interface IPlayerLotteryTypeService
    {
        void AddPlayerLotteryType(int playerId, int lotteryTypeId);

        void DeletePlayerLotteryType(int playerId, int lotteryTypeId);

        PlayerLotteryType[] GetPlayerLotteryTypes(int playerId);
    }
}