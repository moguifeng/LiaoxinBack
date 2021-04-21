using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.Self
{
    public class GiftReport : BaseReport
    {
        public GiftReport(int playerId) : base(playerId)
        {
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            return from g in context.GiftReceives where g.PlayerId == Id && g.IsEnable && g.CreateTime >= begin && g.CreateTime < end select g.GiftMoney;
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            return from p in context.Players where p.PlayerId == Id && p.ReportDate == DateTime.Today select p.GiftMoney;
        }
    }
}