using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.All
{
    public class AllRebateReport : BaseAllReport
    {
        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
            return from p in context.Players where p.ReportDate == DateTime.Today && !ids.Contains(p.PlayerId) && p.Type != PlayerTypeEnum.TestPlay select p.RebateMoney;
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            return from r in context.RebateLogs
                   where r.IsEnable && r.CreateTime >= begin && r.CreateTime < end && r.Player.Type != PlayerTypeEnum.TestPlay
                   select r.RebateMoney;
        }
    }
}