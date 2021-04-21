using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.Team
{
    public class TeamWinMoneyReport : BaseTeamReport
    {
        public TeamWinMoneyReport(int playerId) : base(playerId)
        {
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            return from b in context.Bets
                   where list.Contains(b.PlayerId) && b.UpdateTime >= begin && b.UpdateTime < end
                         && b.IsEnable
                   select b.WinMoney;
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            return from p in context.Players where p.ReportDate == DateTime.Today && list.Contains(p.PlayerId) select p.WinMoney;
        }
    }
}