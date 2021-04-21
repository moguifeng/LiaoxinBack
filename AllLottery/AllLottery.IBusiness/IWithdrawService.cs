using System;
using AllLottery.Model;

namespace AllLottery.IBusiness
{
    public interface IWithdrawService
    {
        void AddWithdraw(decimal money, int playerBankId, int playerId, string password);

        Withdraw[] GetWithdraws(int id, int index, int size, out int total);

        Withdraw[] GetTeamWithdraws(int id, string name, DateTime? begin, DateTime? end, int index, int size,
            out int total);
    }
}