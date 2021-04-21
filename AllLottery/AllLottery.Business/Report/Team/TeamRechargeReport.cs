using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.Team
{
    public class TeamRechargeReport : BaseTeamReport
    {
        public TeamRechargeReport(int playerId) : base(playerId)
        {
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            return from r in context.Recharges
                   where list.Contains(r.PlayerId) && r.UpdateTime >= begin && r.UpdateTime < end && r.IsEnable && r.State == RechargeStateEnum.Ok
                   select r.Money;
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            return from p in context.Players where p.ReportDate == DateTime.Today && list.Contains(p.PlayerId) select p.RechargeMoney;
        }
    }
}