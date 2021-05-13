using System;
using System.ComponentModel;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class CoinLog : BaseModel
    {
        public CoinLog()
        {
        }

        public CoinLog(Guid clientId, decimal flowCoin, decimal coin, CoinLogTypeEnum type, Guid aboutId, string remark)
        {
            ClientId = clientId;
            FlowCoin = flowCoin;
            Coin = coin;
            Type = type;
            AboutId = aboutId;
            Remark = remark;
        }
 

      
        public Guid CoinLogId { get; set; } = Guid.NewGuid();

        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        /// <summary>
        /// 流动资金变化
        /// </summary>
        public decimal FlowCoin { get; set; } = 0;

        /// <summary>
        /// 当前资金
        /// </summary>
        public decimal Coin { get; set; }

        ///// <summary>
        ///// 流动冻结资金变化
        ///// </summary>
        //public decimal FlowFCoin { get; set; } = 0;

        ///// <summary>
        ///// 当冻结资金
        ///// </summary>
        //public decimal FCoin { get; set; }

        /// <summary>
        /// 变动类型
        /// </summary>
       
        public CoinLogTypeEnum Type { get; set; } = CoinLogTypeEnum.Recharge;

        /// <summary>
        /// 相关ID
        /// </summary>
 
        public Guid AboutId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }

    public enum CoinLogTypeEnum
    {
        [Description("充值")]
        Recharge = 0,

        [Description("代充值")]
        InsteadRecharge = 1,

 
        [Description("提现成功")]
        Withdraw = 2,



        [Description("提款失败")]
        CancelWithdraw = 3,

        [Description("发红包")]
        SendRedPacket = 4,

        [Description("抢红包")]
        SnatRedPacket = 5,

        [Description("接收红包")]
        ReceiveRedPacket = 6,

        [Description("转账")]
        Transfer = 7,

        [Description("接收转账")]
        ReceiveTransfer = 8,

        [Description("红包退回")]
        RefundRedPacket = 9,



        [Description("提现手续费")]

        WithdrawRate = 10,


    }
}