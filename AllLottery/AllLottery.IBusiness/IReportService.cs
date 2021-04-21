using AllLottery.Model;
using System;

namespace AllLottery.IBusiness
{
    public interface IReportService
    {
        decimal GetGiftCoin(int playerId, DateTime? begin, DateTime? end);

        decimal GetRebateCoin(int playerId, DateTime? begin, DateTime? end);

        decimal GetWithdrawCoin(int playerId, DateTime? begin, DateTime? end);

        decimal GetRechargeCoin(int playerId, DateTime? begin, DateTime? end);

        decimal GetWinMoney(int playerId, DateTime? begin, DateTime? end);

        decimal GetBetMoney(int playerId, DateTime? begin, DateTime? end);

        decimal GetTeamCoin(int playerId);

        decimal GetTeamFCoin(int playerId);

        int GetAllMenCount(PlayerTypeEnum? type = null);

        decimal GetAllCoin();

        /// <summary>
        /// 全部彩种
        /// </summary>
        /// <returns></returns>
        LotteryType[] GetAllLotteryTypes();
    }
}