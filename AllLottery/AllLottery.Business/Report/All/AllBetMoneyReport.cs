using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.All
{
    public class AllBetMoneyReport : BaseAllReport
    {
        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
            return from p in context.Players where p.ReportDate == DateTime.Today && !ids.Contains(p.PlayerId) && p.Type != PlayerTypeEnum.TestPlay select p.BetMoney;
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
            return from b in context.Bets
                   where b.UpdateTime >= begin && b.UpdateTime < end && (b.Status == BetStatusEnum.Win || b.Status == BetStatusEnum.Lose)
                         && b.IsEnable && !ids.Contains(b.PlayerId) && b.Player.Type != PlayerTypeEnum.TestPlay
                   select b.BetMoney;
        }
    }
}