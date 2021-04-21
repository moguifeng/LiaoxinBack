using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.All
{
    public class AllWithdrawReport : BaseAllReport
    {
        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
            return from w in context.Withdraws where w.IsEnable && w.UpdateTime >= begin && w.UpdateTime < end && w.Status == WithdrawStatusEnum.Ok && !ids.Contains(w.PlayerBank.PlayerId) select w.Money;
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
            return from p in context.Players where p.ReportDate == DateTime.Today && !ids.Contains(p.PlayerId) select p.WithdrawMoney;
        }
    }
}