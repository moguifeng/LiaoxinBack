using System;

namespace AllLottery.Tool
{
    public class Player
    {
        public Guid PlayerId { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Coin { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public string WeChat { get; set; }

        /// <summary>
        /// 流动资金
        /// </summary>
        public decimal FCoin { get; set; }

        public int Type { get; set; }

        public Guid? ParentPlayerId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { get; set; } = true;
    }
}