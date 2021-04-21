using System.ComponentModel;
using Zzb.EF;

namespace AllLottery.Model
{
    public class CoinLog : BaseModel
    {
        public CoinLog()
        {
        }

        public CoinLog(int playerId, decimal flowCoin, decimal coin, CoinLogTypeEnum type, int aboutId, string remark)
        {
            PlayerId = playerId;
            FlowCoin = flowCoin;
            Coin = coin;
            Type = type;
            AboutId = aboutId;
            Remark = remark;
        }

        public CoinLog(int playerId, decimal flowCoin, decimal coin, decimal flowFCoin, decimal fCoin, CoinLogTypeEnum type, int aboutId, string remark)
        {
            PlayerId = playerId;
            FlowCoin = flowCoin;
            Coin = coin;
            Type = type;
            AboutId = aboutId;
            Remark = remark;
            FlowFCoin = flowFCoin;
            FCoin = fCoin;
        }

        [ZzbIndex]
        public int CoinLogId { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        /// <summary>
        /// 流动资金变化
        /// </summary>
        public decimal FlowCoin { get; set; } = 0;

        /// <summary>
        /// 当前资金
        /// </summary>
        public decimal Coin { get; set; }

        /// <summary>
        /// 流动冻结资金变化
        /// </summary>
        public decimal FlowFCoin { get; set; } = 0;

        /// <summary>
        /// 当冻结资金
        /// </summary>
        public decimal FCoin { get; set; }

        /// <summary>
        /// 变动类型
        /// </summary>
        [ZzbIndex]
        public CoinLogTypeEnum Type { get; set; } = CoinLogTypeEnum.Recharge;

        /// <summary>
        /// 相关ID
        /// </summary>
        [ZzbIndex]
        public int AboutId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }

    public enum CoinLogTypeEnum
    {
        [Description("充值")]
        Recharge = 0,

        [Description("投注")]
        Bet = 1,

        [Description("申请提现")]
        ApplyWithdraw = 2,

        [Description("中奖")]
        Win = 3,

        [Description("返点")]
        Rebate = 4,

        [Description("提现成功")]
        Withdraw = 5,

        [Description("提款失败")]
        CancelWithdraw = 6,

        [Description("赠送活动")]
        GiftMoney = 7,

        [Description("日工资")]
        ReceiveDaily = 8,

        [Description("发放日工资")]
        GiveDaily = 9,

        [Description("分红")]
        ReceiveDividend = 10,

        [Description("发放分红")]
        GiveDividend = 11,

        [Description("上下级转账")]
        Transfer = 12,

        [Description("投注撤单")]
        Revoke = 13,

        [Description("平台转账")]
        PlatformTransfer = 14,

        [Description("创建追号")]
        CreateChasingOrder = 15,

        [Description("追号投注")]
        ChasingBet = 16,

        [Description("取消追号")]
        ChasingBetCancle = 17,
    }
}