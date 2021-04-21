using System.ComponentModel;
using Zzb.EF;

namespace AllLottery.Model
{
    public class PlatformMoneyLog : BaseModel
    {
        public PlatformMoneyLog()
        {

        }

        public PlatformMoneyLog(int playerId, int platformId, decimal flowMoney)
        {
            PlayerId = playerId;
            PlatformId = platformId;
            FlowMoney = flowMoney;
        }

        public int PlatformMoneyLogId { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        public int PlatformId { get; set; }

        public virtual Platform Platform { get; set; }

        public decimal FlowMoney { get; set; }
    }
}