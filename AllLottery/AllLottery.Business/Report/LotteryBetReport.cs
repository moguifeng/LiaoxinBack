using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report
{
    public class LotteryBetReport : BaseReport
    {
        public LotteryBetReport(int id) : base(id)
        {
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            return from p in context.LotteryTypes where p.LotteryTypeId == Id && p.ReportDate == DateTime.Today select p.BetMoney;
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
            return from b in context.Bets
                   where b.LotteryPlayDetail.LotteryPlayType.LotteryTypeId == Id && b.UpdateTime >= begin && b.UpdateTime < end && (b.Status == BetStatusEnum.Win || b.Status == BetStatusEnum.Lose)
                         && b.IsEnable && !ids.Contains(b.PlayerId) && b.Player.Type != PlayerTypeEnum.TestPlay
                   select b.BetMoney;
        }

        public override void Clear(LotteryContext context)
        {
            foreach (LotteryType type in context.LotteryTypes)
            {
                type.UpdateReportDate();
                type.BetMoney = GetMoney(from b in context.Bets
                                         where b.LotteryPlayDetail.LotteryPlayType.LotteryTypeId == type.LotteryTypeId &&
                                               b.UpdateTime > DateTime.Today &&
                                               (b.Status == BetStatusEnum.Win || b.Status == BetStatusEnum.Lose)
                                               && b.IsEnable
                                         select b.BetMoney);
            }
            base.Clear(context);
        }

        private decimal GetMoney(IQueryable<decimal> sql)
        {
            if (sql.Any())
            {
                return sql.Sum();
            }
            return 0;
        }
    }
}