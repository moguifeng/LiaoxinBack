using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zzb.EF;

namespace AllLottery.Model
{
    public class Bet : BaseModel
    {
        public Bet()
        {
        }

        public Bet(string order, int playerId, int lotteryPlayDetailId, string lotteryIssuseNo, string betNo, decimal betMoney, int betCount, int betModeId, int times)
        {
            Order = order;
            PlayerId = playerId;
            LotteryPlayDetailId = lotteryPlayDetailId;
            LotteryIssuseNo = lotteryIssuseNo;
            BetNo = betNo;
            BetMoney = betMoney;
            BetCount = betCount;
            BetModeId = betModeId;
            Times = times;
        }

        public int BetId { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        [ZzbIndex]
        [MaxLength(100)]
        public string Order { get; set; }

        /// <summary>
        /// 玩家
        /// </summary>
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        public int LotteryPlayDetailId { get; set; }

        public virtual LotteryPlayDetail LotteryPlayDetail { get; set; }

        /// <summary>
        /// 投注期号
        /// </summary>
        [MaxLength(20)]
        [ZzbIndex]
        public string LotteryIssuseNo { get; set; }

        /// <summary>
        /// 投注号码
        /// </summary>
        public string BetNo { get; set; }

        /// <summary>
        /// 投注时间
        /// </summary>
        [ZzbIndex]
        public DateTime BetTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 投注金额
        /// </summary>
        public decimal BetMoney { get; set; } = 0;

        /// <summary>
        /// 注数
        /// </summary>
        public int BetCount { get; set; }

        /// <summary>
        /// 购买模式
        /// </summary>
        public int BetModeId { get; set; }

        /// <summary>
        /// 购买模式
        /// </summary>
        public virtual BetMode BetMode { get; set; }

        /// <summary>
        /// 购买倍数
        /// </summary>
        public int Times { get; set; }

        /// <summary>
        /// 开奖ID,可空,空代表没开奖
        /// </summary>
        public int? LotteryDataId { get; set; }

        public virtual LotteryData LotteryData { get; set; }

        /// <summary>
        /// 是否中奖
        /// </summary>
        [ZzbIndex]
        public BetStatusEnum Status { get; set; } = BetStatusEnum.Wait;

        /// <summary>
        /// 中奖金额
        /// </summary>
        [ZzbIndex]
        public decimal WinMoney { get; set; } = 0;

        /// <summary>
        /// 中奖注数
        /// </summary>
        public long WinBetCount { get; set; } = 0;

        /// <summary>
        /// 信用投注显示
        /// </summary>
        [ZzbIndex]
        [MaxLength(50)]
        public string CreditRemark { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        /// <summary>
        /// 计算赔率
        /// </summary>
        /// <param name="maxRebate"></param>
        /// <returns></returns>
        public decimal CalculateOdds(decimal maxRebate)
        {
            return LotteryPlayDetail.CalculateOdds(maxRebate, Player.Rebate);
        }
    }

    public enum BetStatusEnum
    {
        [Description("等待开奖")]
        Wait = 0,
        [Description("中奖")]
        Win = 1,
        [Description("未中奖")]
        Lose = 2,
        [Description("算法出错")]
        Error = 3,
        [Description("已撤单")]
        Revoke = 4
    }
}