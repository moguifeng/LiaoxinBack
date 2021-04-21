using AllLottery.Business.Report;
using AllLottery.Business.Report.Team;
using AllLottery.Business.Socket;
using AllLottery.IBusiness;
using AllLottery.Model;
using AllLottery.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using AllLottery.Business.Report.Self;
using Zzb;
using Zzb.Mvc;

namespace AllLottery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ZzbAuthorize]
    public class ReportController : BaseApiController
    {
        public IReportService ReportService { get; set; }

        public IPlayerService PlayerService { get; set; }


        [HttpPost("GetPersonReport")]
        public ServiceResult GetPersonReport(ReportQuery model)
        {
            return Json(() =>
            {
                var recharge = ReportService.GetRechargeCoin(UserId, model.Begin, model.End);
                var withdraw = ReportService.GetWithdrawCoin(UserId, model.Begin, model.End);
                var win = ReportService.GetWinMoney(UserId, model.Begin, model.End);
                var bet = ReportService.GetBetMoney(UserId, model.Begin, model.End);
                var rebate = ReportService.GetRebateCoin(UserId, model.Begin, model.End);
                var gift = ReportService.GetGiftCoin(UserId, model.Begin, model.End);

                return new ServiceResult<object>(ServiceResultCode.Success, "OK", new
                {
                    recharge,
                    withdraw,
                    win,
                    bet,
                    rebate,
                    gift,
                    allWin = win - bet
                });
            }, "获取个人报表失败");
        }

        [HttpPost("GetTeamReport")]
        public ServiceResult GetTeamReport(TeamReportQuery model)
        {
            return Json(() =>
            {
                var exist = PlayerService.GetPlayer(model.PlayerId);
                if (exist == null)
                {
                    return new ServiceResult(ServiceResultCode.Error, "不存在下级");
                }
                while (exist.PlayerId != UserId)
                {
                    if (exist.ParentPlayerId == null)
                    {
                        return new ServiceResult(ServiceResultCode.Error, "查询玩家不是当前登录玩家的下级");
                    }
                    exist = exist.ParentPlayer;
                }
                var begin = CreateBegin(model.Begin);
                var end = CreateEnd(model.End);
                var players = PlayerService.GetUnderPlayers(model.PlayerId, model.Name, null, model.Index, model.Size,
                    out var total, PlayerSortEnum.CreateTimeDesc, null, null, null, null);
                List<object> list = new List<object>();
                foreach (Player p in players)
                {
                    var teamWinMoney = new TeamWinMoneyReport(p.PlayerId).GetReportData(begin, end);
                    var teamBetMoney = new TeamBetMoneyReport(p.PlayerId).GetReportData(begin, end);
                    var rebate = new RebateReport(p.PlayerId).GetReportData(begin, end);
                    var teamGift = new TeamGiftMoneyReport(p.PlayerId).GetReportData(begin, end);
                    list.Add(new
                    {
                        p.PlayerId,
                        TeamCount = BaseReport.GetTeamPlayerIdsWhitoutSelf(p.PlayerId).Count + 1,
                        p.Players.Count,
                        p.Name,
                        TeamRecharge = new TeamRechargeReport(p.PlayerId).GetReportData(begin, end),
                        TeamWithdraw = new TeamWithdrawReport(p.PlayerId).GetReportData(begin, end),
                        teamBetMoney,
                        teamWinMoney,
                        rebate,
                        teamGift,
                        AllWinMoney = teamWinMoney - teamBetMoney,
                        teamCoin = ReportService.GetTeamCoin(p.PlayerId)
                    });
                }
                return ObjectResult(new
                {
                    total,
                    data = list
                });
            }, "获取团队报表失败");
        }

        [HttpPost("GetTeamAllReport")]
        public ServiceResult GetTeamAllReport(ReportQuery model)
        {
            return Json(() =>
            {
                var player = PlayerService.GetPlayer(UserId);
                var begin = CreateBegin(model.Begin);
                var end = CreateEnd(model.End);
                return ObjectResult(new
                {
                    TeamCount = BaseReport.GetTeamPlayerIdsWhitoutSelf(UserId).Count + 1,
                    Online = 1,
                    ProxyCount = player.Players.Count(t => t.IsEnable && t.Type == PlayerTypeEnum.Proxy),
                    MenberCount = player.Players.Count(t => t.IsEnable && t.Type == PlayerTypeEnum.Member),
                    TeamRecharge = new TeamRechargeReport(UserId).GetReportData(begin, end),
                    TeamWithdraw = new TeamWithdrawReport(UserId).GetReportData(begin, end),
                    TeamBetMoney = new TeamBetMoneyReport(UserId).GetReportData(begin, end),
                    Rebate = new RebateReport(UserId).GetReportData(begin, end),
                    TeamWinMoney = new TeamWinMoneyReport(UserId).GetReportData(begin, end),
                    TeamCoin = ReportService.GetTeamCoin(UserId),
                    OnlineCount = (from i in BaseReport.GetTeamPlayerIdsWhitoutSelf(UserId) where new PlayerSocketMiddleware(null).IsConnect(i) select i).Count()
                });
            }, "获取团队概况失败");
        }

        private DateTime CreateBegin(DateTime? begin)
        {
            if (begin == null)
            {
                return new DateTime(2000, 1, 1);
            }
            return begin.Value;
        }

        private DateTime CreateEnd(DateTime? end)
        {
            if (end == null)
            {
                return DateTime.Now;
            }
            return end.Value.AddDays(1);
        }
    }

    public class TeamReportQuery : ReportQuery
    {
        public int PlayerId { get; set; }

        public string Name { get; set; }
    }

    public class ReportQuery : PageViewModel
    {
        public DateTime? Begin { get; set; }

        public DateTime? End { get; set; }
    }

}