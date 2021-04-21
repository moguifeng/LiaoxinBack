using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.Team
{
    public class TeamDailyWageReport : BaseTeamReport
    {
        public TeamDailyWageReport(int playerId) : base(playerId)
        {
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            return from d in context.DailyWageLogs where d.CreateTime >= DateTime.Today && d.PlayerId == Id select d.DailyMoney;
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            return from d in context.DailyWageLogs where d.CreateTime >= begin && d.CreateTime < end && d.PlayerId == Id select d.DailyMoney;
        }
    }
}