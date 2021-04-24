using AllLottery.Business.Generate;
using AllLottery.IBusiness;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Mvc;

//作废啦
namespace AllLottery.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class TrendController : BaseApiController
    //{
    //    public ILotteryService LotteryService { get; set; }

    //    [HttpPost("GetLotteryTrend")]
    //    public ServiceResult GetLotteryTrend(TrendGetLotteryTrend model)
    //    {
    //        return Json(() =>
    //        {
    //            if (model.Size > 5000)
    //            {
    //                return new ServiceResult(ServiceResultCode.Error, "最多获取100期");
    //            }
    //            var datas = LotteryService.GetNewDatas(model.LotteryId, model.Size);
    //            return ObjectResult(from d in datas
    //                                select new
    //                                {
    //                                    d.Number,
    //                                    Time = d.Time.ToCommonString(),
    //                                    d.Data,
    //                                    Trend = BaseGenerate.CreateGenerate(d.LotteryType.LotteryClassify.Type).CreatTrend(d.Data)
    //                                });
    //        }, "获取走势图失败");

    //    }
    //}

    //public class TrendGetLotteryTrend
    //{
    //    public int LotteryId { get; set; }

    //    public int Size { get; set; }
    //}
}