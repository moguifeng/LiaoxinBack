using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.Team
{
    public class TeamGiftMoneyReport : BaseTeamReport
    {
        public TeamGiftMoneyReport(int playerId) : base(playerId)
        {
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            return from g in context.GiftReceives
                   where g.IsEnable && list.Contains(g.Player.PlayerId) && g.CreateTime >= begin &&
                         g.CreateTime < end
                   select g.GiftMoney;
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            return from p in context.Players where p.ReportDate == DateTime.Today && list.Contains(p.PlayerId) select p.GiftMoney;
        }
    }
}