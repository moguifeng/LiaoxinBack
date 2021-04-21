using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.Self
{
    public class WithdrawReport : BaseReport
    {
        public WithdrawReport(int playerId) : base(playerId)
        {
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            return from w in context.Withdraws where w.IsEnable && w.UpdateTime >= begin && w.UpdateTime < end && w.Status == WithdrawStatusEnum.Ok && w.PlayerBank.PlayerId == Id select w.Money;
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            return from p in context.Players where p.PlayerId == Id && p.ReportDate == DateTime.Today select p.WithdrawMoney;
        }
    }
}