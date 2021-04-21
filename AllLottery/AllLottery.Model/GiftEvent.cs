using System;
using System.ComponentModel;
using Zzb.EF;

namespace AllLottery.Model
{
    /// <summary>
    /// 赠送活动
    /// </summary>
    public class GiftEvent : BaseModel
    {
        public GiftEvent()
        {
        }

        public GiftEvent(DateTime beginTime, DateTime endTime, ReceivingTypeEnum receivingType, GiftRuleEnum rule, decimal returnMoney, decimal returnRate, decimal minMoney, decimal maxMoney)
        {
            BeginTime = beginTime;
            EndTime = endTime;
            ReceivingType = receivingType;
            Rule = rule;
            ReturnMoney = returnMoney;
            ReturnRate = returnRate;
            MinMoney = minMoney;
            MaxMoney = maxMoney;
        }

        public int GiftEventId { get; set; }
        public DateTime BeginTime { get; set; } = DateTime.Now;

        public DateTime EndTime { get; set; } = DateTime.Now;

        public ReceivingTypeEnum ReceivingType { get; set; } = ReceivingTypeEnum.All;

        public GiftRuleEnum Rule { get; set; } = GiftRuleEnum.EveryOne;

        /// <summary>
        /// 固定金额
        /// </summary>
        public decimal ReturnMoney { get; set; } = 0;

        public string TestRow { get; set; }

        /// <summary>
        /// 返还百分比
        /// </summary>
        public decimal ReturnRate { get; set; } = 0;

        /// <summary>
        /// 参与活动最低金额
        /// </summary>
        public decimal MinMoney { get; set; } = 0;

        /// <summary>
        /// 参与活动最高金额
        /// </summary>
        public decimal MaxMoney { get; set; } = int.MaxValue;
    }

    public enum ReceivingTypeEnum
    {
        [Description("新用户")]
        New = 0,
        [Description("老用户")]
        Old = 1,
        [Description("所有用户")]
        All = 2
    }

    public enum GiftRuleEnum
    {
        [Description("每日首充")]
        DailyFirst,
        [Description("每次都送")]
        EveryOne
    }
}