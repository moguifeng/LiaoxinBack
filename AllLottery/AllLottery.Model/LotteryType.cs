using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace AllLottery.Model
{
    public class LotteryType : BaseModel
    {
        [ZzbIndex]
        public int LotteryTypeId { get; set; }

        public string Name { get; set; }

        public string SpiderName { get; set; }

        public int LotteryClassifyId { get; set; }

        public virtual LotteryClassify LotteryClassify { get; set; }

        public int SortIndex { get; set; }

        public int NumberLength { get; set; } = 3;

        public string Description { get; set; }

        /// <summary>
        /// 是否维护
        /// </summary>
        public bool IsStop { get; set; } = false;

        /// <summary>
        /// 彩种风险时间
        /// </summary>
        public TimeSpan RiskTime { get; set; } = new TimeSpan(0, 2, 0);

        /// <summary>
        /// 时间格式
        /// </summary>
        public string DateFormat { get; set; } = "yyyyMMdd";

        /// <summary>
        /// 热门彩种
        /// </summary>
        public bool IsHot { get; set; } = false;

        /// <summary>
        /// 采种期号计算方式
        /// </summary>
        public LotteryCalNumberTypeEnum CalType { get; set; } = LotteryCalNumberTypeEnum.FromZeroEveryDay;

        /// <summary>
        /// 彩种图标
        /// </summary>
        public int IconId { get; set; }

        [ForeignKey("IconId")]
        public virtual Affix Icon { get; set; }

        public decimal BetMoney { get; set; }

        public decimal WinMoney { get; set; }

        [ZzbIndex]
        public DateTime ReportDate { get; set; }

        public decimal WinRate { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public void UpdateReportDate()
        {
            if (ReportDate < DateTime.Today)
            {
                BetMoney = 0;
                WinMoney = 0;
                ReportDate = DateTime.Today;
                UpdateTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 开奖时间
        /// </summary>
        public virtual List<LotteryOpenTime> LotteryOpenTimes { get; set; }

        public virtual List<LotteryPlayType> LotteryPlayTypes { get; set; }
    }

    public enum LotteryCalNumberTypeEnum
    {
        [Description("每天从零开始")]
        FromZeroEveryDay = 0,
        [Description("递增")]
        Increase = 1,
        [Description("自动生成")]
        Automatic = 2,
        [Description("根据开奖")]
        AccordingToOpenData = 3,
    }
}