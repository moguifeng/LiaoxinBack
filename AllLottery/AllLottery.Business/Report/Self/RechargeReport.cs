using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.Self
{
    public class RechargeReport : BaseReport
    {
        public RechargeReport(int playerId) : base(playerId)
        {
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            return from r in context.Recharges
                   where r.IsEnable && r.UpdateTime >= begin && r.UpdateTime < end && r.State == RechargeStateEnum.Ok && r.PlayerId == Id
                   select r.Money;
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            return from p in context.Players where p.PlayerId == Id && p.ReportDate == DateTime.Today select p.RechargeMoney;
        }
    }
}