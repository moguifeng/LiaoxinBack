using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zzb.Common;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 追号订单
    /// </summary>
    public class ChasingOrder : BaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int ChasingOrderId { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        [ZzbIndex]
        [MaxLength(100)]
        public string Order { get; set; } = RandomHelper.GetRandom("Z");

        /// <summary>
        /// 中奖后是否停止
        /// </summary>
        public bool IsWinStop { get; set; } = true;

        /// <summary>
        /// 状态
        /// </summary>
        public ChasingStatus Status { get; set; } = ChasingStatus.Wait;

        /// <summary>
        /// 投注模式
        /// </summary>
        public int BetModeId { get; set; }

 
        /// <summary>
        /// 玩法
        /// </summary>
     //   public int LotteryPlayDetailId { get; set; }

   //     public virtual LotteryPlayDetail LotteryPlayDetail { get; set; }

        public virtual List<ChasingOrderDetail> ChasingOrderDetails { get; set; }

        /// <summary>
        /// 投注号
        /// </summary>
        public string BetNo { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

    }

    public enum ChasingStatus
    {
        [Description("未开始")]
        Wait = 0,
        [Description("追号中")]
        Doing = 1,
        [Description("已停止")]
        Stop = 2,
        [Description("已完成")]
        End = 3
    }
}