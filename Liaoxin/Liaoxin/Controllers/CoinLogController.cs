using Liaoxin.Cache;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using LIaoxin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Zzb;
using Zzb.Common;
using Zzb.ICacheManger;
using Zzb.Mvc;
using Zzb.Utility;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CoinLogController : LiaoxinBaseController
    {
        public ICoinLogService CoinLogService { get; set; }

        /// <summary>
        /// 获取张单类型
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCoinLogTypes")]
        public ServiceResult<IList<CoinLogTypeModel>> GetCoinLogTypes()
        {
            return (ServiceResult<IList<CoinLogTypeModel>>)Json(() =>
            {
                IList<CoinLogTypeModel> list = new List<CoinLogTypeModel>();
                //Dictionary<int, string> dicCoinLogType = new Dictionary<int, string>();

                Type type = typeof(CoinLogTypeEnum);
                foreach (CoinLogTypeEnum item in Enum.GetValues(typeof(CoinLogTypeEnum)))
                {
                    if ((int)item != 3)
                    {
                        var field = type.GetField(item.ToString());
                        //dicCoinLogType.Add((int)item, item.ToDescriptionString());
                        list.Add(new CoinLogTypeModel() { Type = (int)item, Name = item.ToDescriptionString() });
                    }
                }

                return new ServiceResult<IList<CoinLogTypeModel>>(ServiceResultCode.Success, "OK", list);
            });
        }

        /// <summary>
        /// 获取个人张单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("GetPersonCoinLogs")]
        public ServiceResult<IList<CoinLogResponse>> GetPersonCoinLogs(PersonCoinLogQuery model)
        {
            int year = model.Year;
            int month = model.Month;
            Guid clientId = model.ClientId;
            int coinLogType = model.CoinLogType;

            return (ServiceResult<IList<CoinLogResponse>>)Json(() =>
            {
                List<CoinLogTypeEnum> allTypes = new List<CoinLogTypeEnum>() { };
                if (!model.IsGetAllType)
                {
                    allTypes.Add((CoinLogTypeEnum)coinLogType);

                }
                else
                {
                    foreach (CoinLogTypeEnum item in Enum.GetValues(typeof(CoinLogTypeEnum)))
                    {
                        allTypes.Add(item);
                    }
                }
                IList<CoinLogResponse> coinLogs = (from c in Context.CoinLogs.AsNoTracking().Where(p => p.CreateTime.Year == year && p.CreateTime.Month == month && p.Type != CoinLogTypeEnum.CancelWithdraw && allTypes.Contains(p.Type) && clientId == p.ClientId)
                                                   select new CoinLogResponse() { Money = c.FlowCoin, LogTime=c.CreateTime,Remark = c.Remark, CoinType = c.Type.ToDescriptionString() }).ToList();

                return ObjectGenericityResult(true, coinLogs, "");
            });

        }


        //[HttpPost("GetCoinLogs")]
        //public ServiceResult GetCoinLogs(CoinLogQuery model)
        //{
        //    return Json(() =>
        //    {
        //        List<CoinLogTypeEnum> list = new List<CoinLogTypeEnum>();
        //        if (!string.IsNullOrEmpty(model.Types))
        //        {
        //            var types = model.Types.Split(',');
        //            foreach (string type in types)
        //            {
        //                list.Add(Enum.Parse<CoinLogTypeEnum>(type));
        //            }
        //        }

        //        var logs = CoinLogService.GetCoinLogs(UserId, model.Index, model.Size, out var total, model.Begin, model.End,
        //            list.ToArray());
        //        return ObjectResult(new
        //        {
        //            total,
        //            data =
        //            from i in logs
        //            select new
        //            {
        //                i.Coin,
        //                i.FCoin,
        //                i.FlowCoin,
        //                i.FlowFCoin,
        //                i.Remark,
        //                Type = i.Type.ToDescriptionString(),
        //                CreateTime = i.CreateTime.ToCommonString()
        //            }
        //        });
        //    }, "获取账变记录失败");
        //}




        //[HttpPost("GetTeamCoinLogs")]
        //public ServiceResult GetTeamCoinLogs(CoinLogQuery model)
        //{
        //    return Json(() =>
        //    {
        //        List<CoinLogTypeEnum> list = new List<CoinLogTypeEnum>();
        //        if (!string.IsNullOrEmpty(model.Types))
        //        {
        //            var types = model.Types.Split(',');
        //            foreach (string type in types)
        //            {
        //                list.Add(Enum.Parse<CoinLogTypeEnum>(type));
        //            }
        //        }

        //        var logs = CoinLogService.GetTeamCoinLogs(UserId, model.Index, model.Size, out var total, model.Begin, model.End,
        //            list.ToArray(), model.Name);
        //        return ObjectResult(new
        //        {
        //            total,
        //            data =
        //            from i in logs
        //            select new
        //            {
        //                i.Coin,
        //                i.FCoin,
        //                i.FlowCoin,
        //                i.FlowFCoin,
        //                i.Remark,
        //                Type = i.Type.ToDescriptionString(),
        //                i.CreateTime,
        //                i.Player.Name
        //            }
        //        });
        //    }, "获取团队账变记录失败");
        //}

    }

    public class CoinLogQuery
    {
        public string Types { get; set; }

        public string Name { get; set; }
    }

    public class PersonCoinLogQuery
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public Guid ClientId { get; set; }

        public int CoinLogType { get; set; }

        public bool IsGetAllType { get; set; }

    }

    public class CoinLogTypeModel
    {
        public int Type { get; set; }

        public string Name { get; set; }
    }

    public class CoinLogResponse
    {
        public decimal Money { get; set; }

        public string CoinType { get; set; }

        public string Remark { get; set; }

        public DateTime LogTime { get; set; }

    }

}