using System;

namespace Liaoxin.ViewModel
{
    /// <summary>
    /// 期号的投注开始时间结束时间
    /// </summary>
    public class LotteryNumberBetTimeViewMode
    {
        public LotteryNumberBetTimeViewMode(DateTime open, TimeSpan risktime)
        {
            OpenTime = open;
            RiskTime = risktime;
        }

        public DateTime OpenTime { get; set; }

        public TimeSpan RiskTime { get; set; }

        public DateTime OpenRiskTime => OpenTime - RiskTime;
    }

    public class BetTimeViewModel : LotteryNumberBetTimeViewMode
    {

        public DateTime BeginTime { get; set; }

        public DateTime BeginRiskTime => BeginTime - RiskTime;

        public BetTimeViewModel(DateTime begin, DateTime open, TimeSpan risktime) : base(open, risktime)
        {
            BeginTime = begin;
        }
    }

    public class BetTimeEveryDayViewModel
    {
        public BetTimeEveryDayViewModel(TimeSpan begin, TimeSpan end)
        {
            BeginSpanTime = begin;
            EndSpanTime = end;
        }

        public TimeSpan BeginSpanTime { get; set; }

        public TimeSpan EndSpanTime { get; set; }
    }
}