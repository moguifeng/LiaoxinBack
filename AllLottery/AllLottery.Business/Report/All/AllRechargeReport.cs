using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.All
{
    public class AllRechargeReport : BaseAllReport
    {
        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
            return from r in context.Recharges
                   where r.IsEnable && r.UpdateTime >= begin && r.UpdateTime < end && r.State == RechargeStateEnum.Ok && !ids.Contains(r.PlayerId) && r.Player.Type != PlayerTypeEnum.TestPlay
                   select r.Money;
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
            return from p in context.Players where p.ReportDate == DateTime.Today && !ids.Contains(p.PlayerId) && p.Type != PlayerTypeEnum.TestPlay select p.RechargeMoney;
        }
    }
}