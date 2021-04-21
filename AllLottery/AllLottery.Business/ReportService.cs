using AllLottery.Business.Report;
using AllLottery.Business.Report.Self;
using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using System.Linq;

namespace AllLottery.Business
{
    public class ReportService : BaseService, IReportService
    {

        public LotteryType[] GetAllLotteryTypes()
        {
            return (from l in Context.LotteryTypes orderby l.SortIndex select l).ToArray();
        }
        public decimal GetAllCoin()
        {
            var ids = (from n in Context.NotReportPlayers select n.PlayerId).ToArray();
            var sql = from p in Context.Players where p.IsEnable && !ids.Contains(p.PlayerId) && p.Type != PlayerTypeEnum.TestPlay select p;
            if (sql.Any())
            {
                return sql.Sum(t => t.Coin) + sql.Sum(t => t.FCoin);
            }
            else
            {
                return 0;
            }
        }

        public decimal GetBetMoney(int playerId, DateTime? begin, DateTime? end)
        {
            DateTime tbegin = begin ?? new DateTime(2019, 1, 1);
            DateTime tend = end?.AddDays(1) ?? DateTime.Now;
            return new BetMoneyReport(playerId).GetReportData(tbegin, tend);
        }

        public decimal GetWinMoney(int playerId, DateTime? begin, DateTime? end)
        {
            DateTime tbegin = begin ?? new DateTime(2019, 1, 1);
            DateTime tend = end?.AddDays(1) ?? DateTime.Now;
            return new WinMoneyReport(playerId).GetReportData(tbegin, tend);
        }

        public decimal GetRechargeCoin(int playerId, DateTime? begin, DateTime? end)
        {
            DateTime tbegin = begin ?? new DateTime(2019, 1, 1);
            DateTime tend = end?.AddDays(1) ?? DateTime.Now;
            return new RechargeReport(playerId).GetReportData(tbegin, tend);
        }

        public decimal GetWithdrawCoin(int playerId, DateTime? begin, DateTime? end)
        {
            DateTime tbegin = begin ?? new DateTime(2019, 1, 1);
            DateTime tend = end?.AddDays(1) ?? DateTime.Now;
            return new WithdrawReport(playerId).GetReportData(tbegin, tend);
        }

        public decimal GetRebateCoin(int playerId, DateTime? begin, DateTime? end)
        {
            DateTime tbegin = begin ?? new DateTime(2019, 1, 1);
            DateTime tend = end?.AddDays(1) ?? DateTime.Now;
            return new RebateReport(playerId).GetReportData(tbegin, tend);
        }

        public decimal GetGiftCoin(int playerId, DateTime? begin, DateTime? end)
        {
            DateTime tbegin = begin ?? new DateTime(2019, 1, 1);
            DateTime tend = end?.AddDays(1) ?? DateTime.Now;
            return new GiftReport(playerId).GetReportData(tbegin, tend);
        }

        public decimal GetTeamCoin(int playerId)
        {
            var list = BaseReport.GetTeamPlayerIdsWhitoutSelf(playerId);
            list.Add(playerId);
            return (from p in Context.Players where list.Contains(p.PlayerId) && p.IsEnable select p.Coin).Sum();
        }

        public decimal GetTeamFCoin(int playerId)
        {
            var list = BaseReport.GetTeamPlayerIdsWhitoutSelf(playerId);
            list.Add(playerId);
            return (from p in Context.Players where list.Contains(p.PlayerId) && p.IsEnable select p.FCoin).Sum();
        }

        public int GetAllMenCount(PlayerTypeEnum? type = null)
        {
            var ids = (from n in Context.NotReportPlayers select n.PlayerId).ToArray();
            var sql = from p in Context.Players where p.IsEnable && !ids.Contains(p.PlayerId) && p.Type != PlayerTypeEnum.TestPlay select p;
            if (type != null)
            {
                sql = sql.Where(t => t.Type == type.Value);
            }
            if (sql.Any())
            {
                return sql.Count();
            }
            else
            {
                return 0;
            }
        }
    }
}