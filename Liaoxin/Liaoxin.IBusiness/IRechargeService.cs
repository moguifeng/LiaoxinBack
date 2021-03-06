using Liaoxin.Model;
using Liaoxin.ViewModel;
using System;

namespace Liaoxin.IBusiness
{
    public interface IRechargeService
    {
        void AddRecharge(int bankId, int playerId, decimal money);

        bool IsExistRecharges(int playerid);

        Recharge[] GetRecharges(int id, int size, int index, out int total);

        Recharge[] GetTeamRecharges(int id, string name, DateTime? begin, DateTime? end, int size, int index,
            out int total);

        ThirdPayModel AddThirdPayRecharge(int bankId, int playerId, decimal money, string url);
    }
}