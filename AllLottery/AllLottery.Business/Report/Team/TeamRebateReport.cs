using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.Team
{
    public class TeamRebateReport : BaseTeamReport
    {
        public TeamRebateReport(int playerId) : base(playerId)
        {
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            return from p in context.Players where list.Contains(p.PlayerId) && p.ReportDate == DateTime.Today select p.RebateMoney;
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            return from r in context.RebateLogs
                   where r.IsEnable && list.Contains(r.Player.ParentPlayerId.Value) && r.CreateTime >= begin && r.CreateTime < end
                   select r.RebateMoney;
        }
    }
}