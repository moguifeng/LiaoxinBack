using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Report.Team
{
    public class TeamWithdrawReport : BaseTeamReport
    {
        public TeamWithdrawReport(int playerId) : base(playerId)
        {
        }

        protected override IQueryable<decimal> Sql(List<int> list, DateTime begin, DateTime end, LotteryContext context)
        {
            return from w in context.Withdraws
                   where list.Contains(w.PlayerBank.PlayerId) && w.UpdateTime >= begin && w.UpdateTime < end && w.IsEnable && w.Status == WithdrawStatusEnum.Ok
                   select w.Money;
        }

        protected override IQueryable<decimal> GetTodayData(List<int> list, LotteryContext context)
        {
            return from p in context.Players where p.ReportDate == DateTime.Today && list.Contains(p.PlayerId) select p.WithdrawMoney;
        }
    }
}