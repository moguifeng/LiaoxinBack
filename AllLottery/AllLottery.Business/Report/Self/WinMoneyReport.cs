using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.Self
{
    public class WinMoneyReport : BaseReport
    {
        public WinMoneyReport(int playerId) : base(playerId)
        {
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            return from b in context.Bets
                   where b.PlayerId == Id && b.UpdateTime >= begin && b.UpdateTime < end
                         && b.IsEnable
                   select b.WinMoney;
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            return from p in context.Players where p.PlayerId == Id && p.ReportDate == DateTime.Today select p.WinMoney;
        }
    }
}