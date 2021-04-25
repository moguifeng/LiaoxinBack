using AllLottery.IBusiness;
using AllLottery.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Mvc;

namespace AllLottery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ZzbAuthorize]
    //public class CoinLogController : BaseApiController
    //{
    //    public ICoinLogService CoinLogService { get; set; }

    //    [HttpGet("GetCoinLogType")]
    //    public ServiceResult GetCoinLogType()
    //    {
    //        return new ServiceResult<object>(ServiceResultCode.Success, "OK", CoinLogTypeEnum.Win.GetDropListModels());
    //    }

    //    [HttpPost("GetCoinLogs")]
    //    public ServiceResult GetCoinLogs(CoinLogQuery model)
    //    {
    //        return Json(() =>
    //        {
    //            List<CoinLogTypeEnum> list = new List<CoinLogTypeEnum>();
    //            if (!string.IsNullOrEmpty(model.Types))
    //            {
    //                var types = model.Types.Split(',');
    //                foreach (string type in types)
    //                {
    //                    list.Add(Enum.Parse<CoinLogTypeEnum>(type));
    //                }
    //            }

    //            var logs = CoinLogService.GetCoinLogs(UserId, model.Index, model.Size, out var total, model.Begin, model.End,
    //                list.ToArray());
    //            return ObjectResult(new
    //            {
    //                total,
    //                data =
    //                from i in logs
    //                select new
    //                {
    //                    i.Coin,
    //                    i.FCoin,
    //                    i.FlowCoin,
    //                    i.FlowFCoin,
    //                    i.Remark,
    //                    Type = i.Type.ToDescriptionString(),
    //                    CreateTime = i.CreateTime.ToCommonString()
    //                }
    //            });
    //        }, "获取账变记录失败");
    //    }

    //    [HttpPost("GetTeamCoinLogs")]
    //    public ServiceResult GetTeamCoinLogs(CoinLogQuery model)
    //    {
    //        return Json(() =>
    //        {
    //            List<CoinLogTypeEnum> list = new List<CoinLogTypeEnum>();
    //            if (!string.IsNullOrEmpty(model.Types))
    //            {
    //                var types = model.Types.Split(',');
    //                foreach (string type in types)
    //                {
    //                    list.Add(Enum.Parse<CoinLogTypeEnum>(type));
    //                }
    //            }

    //            var logs = CoinLogService.GetTeamCoinLogs(UserId, model.Index, model.Size, out var total, model.Begin, model.End,
    //                list.ToArray(), model.Name);
    //            return ObjectResult(new
    //            {
    //                total,
    //                data =
    //                from i in logs
    //                select new
    //                {
    //                    i.Coin,
    //                    i.FCoin,
    //                    i.FlowCoin,
    //                    i.FlowFCoin,
    //                    i.Remark,
    //                    Type = i.Type.ToDescriptionString(),
    //                    i.CreateTime,
    //                    i.Player.Name
    //                }
    //            });
    //        }, "获取团队账变记录失败");
    //    }

    //}

    public class CoinLogQuery 
    {
        public string Types { get; set; }

        public string Name { get; set; }
    }
}