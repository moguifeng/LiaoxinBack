using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.All
{
    public class AllGiftReport : BaseAllReport
    {
        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
            return from g in context.GiftReceives where g.IsEnable && g.CreateTime >= begin && g.CreateTime < end && !ids.Contains(g.PlayerId) && g.Player.Type != PlayerTypeEnum.TestPlay select g.GiftMoney;
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
            return from p in context.Players where p.ReportDate == DateTime.Today && !ids.Contains(p.PlayerId) && p.Type != PlayerTypeEnum.TestPlay select p.GiftMoney;
        }
    }
}