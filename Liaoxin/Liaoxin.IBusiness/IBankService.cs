using Liaoxin.Model;

namespace Liaoxin.IBusiness
{
    public interface IBankService
    {
        SystemBank[] GetSystemBanks();

        PlayerBank[] GetPlayerBanks(int playerId);

        void AddPlayerBank(int playerId, int systemBankId, string name, string cardNumber);

        MerchantsBank[] GetMerchantsBanks(bool isApp);

        MerchantsBank GetMerchantsBank(int id);
    }
}