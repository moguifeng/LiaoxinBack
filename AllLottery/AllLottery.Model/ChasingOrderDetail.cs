using System.ComponentModel.DataAnnotations;
using Zzb.EF;

namespace AllLottery.Model
{
    /// <summary>
    /// 追号详情
    /// </summary>
    public class ChasingOrderDetail : BaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ChasingOrderDetailId { get; set; }

        /// <summary>
        /// 追号订单
        /// </summary>
        public int ChasingOrderId { get; set; }

        public virtual ChasingOrder ChasingOrder { get; set; }

 

        /// <summary>
        /// 追号倍数
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 追号期号
        /// </summary>
        public string Number { get; set; }

        public int Index { get; set; } = 0;

        public decimal BetMoney { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

    }
}