using AllLottery.Model;
using AllLottery.ViewModel;
using System.Collections.Generic;

namespace AllLottery.IBusiness
{
    public interface ILotteryOpenTimeServer
    {
        Dictionary<string, Dictionary<string, LotteryNumberBetTimeViewMode>> GetBetCacheDictionary(int id);
        Dictionary<string, Dictionary<string, LotteryNumberBetTimeViewMode>> GetBetCacheDictionary(LotteryData data);
        void ClearCache(int id);
        BetTimeViewModel GetBetTime(int lotteryTypeId, string no);
    }
}