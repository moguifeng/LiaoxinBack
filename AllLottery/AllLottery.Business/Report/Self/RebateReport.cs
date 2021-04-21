using System;
using System.Collections.Generic;
using System.Linq;
using AllLottery.Model;

namespace AllLottery.Business.Report.Self
{
    public class RebateReport : BaseReport
    {
        public RebateReport(int playerId) : base(playerId)
        {
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            return from p in context.Players where p.PlayerId == Id && p.ReportDate == DateTime.Today select p.RebateMoney;
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            return from r in context.RebateLogs
                   where r.IsEnable && r.Player.ParentPlayerId == Id && r.CreateTime >= begin && r.CreateTime < end
                   select r.RebateMoney;
        }
    }
}