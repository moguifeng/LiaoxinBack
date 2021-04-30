using System.ComponentModel;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class CoinLog : BaseModel
    {
        public CoinLog()
        {
        }

        public CoinLog(int clientId, decimal flowCoin, decimal coin, CoinLogTypeEnum type, int aboutId, string remark)
        {
            ClientId = clientId;
            FlowCoin = flowCoin;
            Coin = coin;
            Type = type;
            AboutId = aboutId;
            Remark = remark;
        }
 

        [ZzbIndex]
        public int CoinLogId { get; set; }

        public int ClientId { get; set; }

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

      
        [Description("申请提现")]
        ApplyWithdraw = 2,

  

        [Description("提现成功")]
        Withdraw = 5,

        [Description("提款失败")]
        CancelWithdraw = 6,

        [Description("赠送")]
        GiftMoney = 7,

      
    }
}