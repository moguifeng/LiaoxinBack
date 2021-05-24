using Liaoxin.Model;
using System;

namespace Liaoxin.IBusiness
{
    public interface IBankService
    {
        SystemBank[] GetSystemBanks();

        ClientBank[] GetClientBanks(Guid clientId);

        //void AddPlayerBank(int playerId, int systemBankId, string name, string cardNumber);

        //MerchantsBank[] GetMerchantsBanks(bool isApp);

        //MerchantsBank GetMerchantsBank(int id);
    }
}