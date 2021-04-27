using Zzb.EF;

namespace Liaoxin.Model
{
    public class RebateLog : BaseModel
    {
        public RebateLog()
        {
        }

        public RebateLog(int playerId, decimal diffRate, decimal rebateMoney)
        {
            PlayerId = playerId;
         
            DiffRate = diffRate;
            RebateMoney = rebateMoney;
        }

        [ZzbIndex]
        public int RebateLogId { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

 

        /// <summary>
        /// 返还金额
        /// </summary>
        public decimal RebateMoney { get; set; }

        /// <summary>
        /// 比例差
        /// </summary>
        public decimal DiffRate { get; set; }
    }
}