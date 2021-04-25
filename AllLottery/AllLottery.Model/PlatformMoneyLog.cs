using System.ComponentModel;
using Zzb.EF;

namespace AllLottery.Model
{
    public class PlatformMoneyLog : BaseModel
    {
        public PlatformMoneyLog()
        {

        }

        public PlatformMoneyLog(int playerId, decimal flowMoney)
        {
            PlayerId = playerId;            
            FlowMoney = flowMoney;
        }

        public int PlatformMoneyLogId { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
 
        

        public decimal FlowMoney { get; set; }
    }
}