using AllLottery.Model;
using System;

namespace AllLottery.IBusiness
{
    public interface ICoinLogService
    {
        CoinLog[] GetCoinLogs(int playerId, int index, int size, out int total, DateTime? begin, DateTime? end,
            CoinLogTypeEnum[] types);

        CoinLog[] GetTeamCoinLogs(int playerId, int index, int size, out int total, DateTime? begin, DateTime? end, CoinLogTypeEnum[] types, string name);
    }
}