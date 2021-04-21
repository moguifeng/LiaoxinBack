using AllLottery.Business.Report;
using AllLottery.Business.Report.All;
using AllLottery.Business.Socket;
using AllLottery.IBusiness;
using AllLottery.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Mvc;

namespace AllLottery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackReportController : BaseApiController
    {
        public IReportService ReportService { get; set; }

        [HttpPost("GetPlayerReport")]
        public ServiceResult GetPlayerReport()
        {
            return JsonObjectResult(new
            {
                AllPlayer = ReportService.GetAllMenCount(),
                ProxyPlayer = ReportService.GetAllMenCount(PlayerTypeEnum.Proxy),
                MemberPlayer = ReportService.GetAllMenCount(PlayerTypeEnum.Member),
                OnlinePlayer = new PlayerSocketMiddleware(null).Count,
                AllCoin = ReportService.GetAllCoin().ToDecimalString()
            }, "获取会员数据失败");
        }

        [HttpPost("GetAllReport")]
        public ServiceResult GetAllReport(BackReportGetAllReport model)
        {
            return Json(() =>
            {
                var begin = model.Begin ?? new DateTime(2019, 1, 1);
                var end = model.End?.AddDays(1) ?? DateTime.MaxValue;
                var allRechargeCoin = new AllRechargeReport().GetReportData(begin, end);
                var allWithdrawCoin = new AllWithdrawReport().GetReportData(begin, end);
                var allBetMoney = new AllBetMoneyReport().GetReportData(begin, end);
                var allWinMoney = new AllWinMoneyReport().GetReportData(begin, end);
                var allRebate = new AllRebateReport().GetReportData(begin, end);


                return ObjectResult(new
                {
                    AllRechargeCoin = allRechargeCoin.ToDecimalString(),
                    AllWithdrawCoin = allWithdrawCoin.ToDecimalString(),
                    AllBetMoney = allBetMoney.ToDecimalString(),
                    AllWinMoney = allWinMoney.ToDecimalString(),
                    AllRebate = allRebate.ToDecimalString(),
                    AllGiftMoney = new AllGiftReport().GetReportData().ToDecimalString(),
                    AllRechargeWinMoney = (allRechargeCoin - allWithdrawCoin).ToDecimalString(),
                    AllBetWinMoney = (allBetMoney - allWinMoney).ToDecimalString(),
                    LotteryTypes = from l in ReportService.GetAllLotteryTypes()
                                   select new
                                   {
                                       Id = l.LotteryTypeId,
                                       l.Name,
                                       BetMoney = new LotteryBetReport(l.LotteryTypeId).GetReportData(begin, end),
                                       WinMoney = new LotteryWinReport(l.LotteryTypeId).GetReportData(begin, end),
                                   }
                });
            });

        }
    }

    public class BackReportGetAllReport
    {
        public DateTime? Begin { get; set; }

        public DateTime? End { get; set; }
    }
}