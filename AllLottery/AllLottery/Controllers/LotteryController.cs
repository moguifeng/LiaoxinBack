using AllLottery.Business.Config;
using AllLottery.IBusiness;
using AllLottery.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Zzb;
using Zzb.Common;
using Zzb.Mvc;

namespace AllLottery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ZzbAuthorize]
    public class LotteryController : BaseApiController
    {
        public ILotteryService LotteryService { get; set; }

        public ILotteryOpenTimeServer LotteryOpenTimeServer { get; set; }

        public IPlayerService PlayerService { get; set; }

        public IPlayerLotteryTypeService PlayerLotteryTypeService { get; set; }

        [HttpPost("GetPlayerLotteryTypes")]
        public ServiceResult GetPlayerLotteryTypes()
        {
            return Json(() => ObjectResult(from l in PlayerLotteryTypeService.GetPlayerLotteryTypes(UserId)
                                           select new
                                           {
                                               l.LotteryType.LotteryTypeId,
                                               l.LotteryType.Name,
                                               Type = l.LotteryType.LotteryClassify.Type.ToDescriptionString(),
                                               l.LotteryType.IconId,
                                               l.LotteryType.Description
                                           }), "获取玩家彩种失败");
        }

        [HttpPost("AddPlayerLotteryType")]
        public ServiceResult AddPlayerLotteryType(ApiLotteryGetLotteryDetailViewModel model)
        {
            return Json(() =>
            {
                PlayerLotteryTypeService.AddPlayerLotteryType(UserId, model.Id);
                return new ServiceResult();
            }, "添加玩家彩种失败");
        }

        [HttpPost("DeletePlayerLotteryType")]
        public ServiceResult DeletePlayerLotteryType(ApiLotteryGetLotteryDetailViewModel model)
        {
            return Json(() =>
            {
                PlayerLotteryTypeService.DeletePlayerLotteryType(UserId, model.Id);
                return new ServiceResult();
            }, "删除添加玩家彩种失败");
        }

        [HttpPost("GetAllLottery")]
        public ServiceResult GetAllLottery()
        {
            return Json(() => new ServiceResult<object>(ServiceResultCode.Success, "OK", (from l in LotteryService.GetAllLotteryTypes()
                                                                                          select new
                                                                                          {
                                                                                              l.LotteryTypeId,
                                                                                              l.Name,
                                                                                              Type = l.LotteryClassify.Type.ToDescriptionString(),
                                                                                              l.IconId,
                                                                                              l.Description,
                                                                                              l.IsHot
                                                                                          }).ToArray())
                , "获取彩种失败");
        }

        [HttpPost("GetAllLotteryOneNewData")]
        public ServiceResult GetAllLotteryOneNewData()
        {

            return Json(() =>
            {
                var lotterys = LotteryService.GetAllLotteryTypes();
                List<object> list = new List<object>();
                foreach (LotteryType l in lotterys)
                {
                    var data = LotteryService.GetNewDatas(l.LotteryTypeId, 1).FirstOrDefault();
                    list.Add(new
                    {
                        l.LotteryTypeId,
                        l.Name,
                        Type = l.LotteryClassify.Type.ToDescriptionString(),
                        l.IconId,
                        l.Description,
                        data?.Data,
                        data?.Number
                    });
                }
                return ObjectResult(list);
            }, "获取所有彩种最新开奖信息失败");
        }

        [HttpPost("GetLotteryDetail")]
        public ServiceResult GetLotteryDetail([FromBody] ApiLotteryGetLotteryDetailViewModel model)
        {
            return Json(() =>
            {
                var lottery = LotteryService.GetLotteryType(model.Id);
                if (lottery == null)
                {
                    return new ServiceResult(ServiceResultCode.Error, "无法找到当前彩种");
                }
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var data = LotteryOpenTimeServer.GetBetCacheDictionary(model.Id);
                if (data == null)
                {
                    throw new ZzbException("无可用数据");
                }
                if (lottery.SpiderName != "10048")
                {
                    List<DetailTimeModel> list = new List<DetailTimeModel>();
                    //添加昨天的数据
                    var keys = data[DateTime.Today.AddDays(-1).ToDateString()].Keys.ToList();
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (i != 0)
                        {
                            list.Add(new DetailTimeModel() { Begin = data[DateTime.Today.AddDays(-1).ToDateString()][keys[i - 1]].OpenRiskTime, End = data[DateTime.Today.AddDays(-1).ToDateString()][keys[i]].OpenRiskTime, Key = keys[i] });
                            //dic.Add(keys[i], new { Begin = data[DateTime.Today.AddDays(-1).ToDateString()][keys[i - 1]].OpenRiskTime, End = data[DateTime.Today.AddDays(-1).ToDateString()][keys[i]].OpenRiskTime });
                        }
                    }
                    //添加今天的数据
                    keys = data[DateTime.Today.ToDateString()].Keys.ToList();
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (i == 0)
                        {
                            //dic.Add(keys[i], new { Begin = data[DateTime.Today.AddDays(-1).ToDateString()].Last().Value.OpenRiskTime, End = data[DateTime.Today.ToDateString()][keys[i]].OpenRiskTime });
                            list.Add(new DetailTimeModel() { Begin = data[DateTime.Today.AddDays(-1).ToDateString()].Last().Value.OpenRiskTime, End = data[DateTime.Today.ToDateString()][keys[i]].OpenRiskTime, Key = keys[i] });
                        }
                        else
                        {
                            //dic.Add(keys[i], new { Begin = data[DateTime.Today.ToDateString()][keys[i - 1]].OpenRiskTime, End = data[DateTime.Today.ToDateString()][keys[i]].OpenRiskTime });
                            list.Add(new DetailTimeModel() { Begin = data[DateTime.Today.ToDateString()][keys[i - 1]].OpenRiskTime, End = data[DateTime.Today.ToDateString()][keys[i]].OpenRiskTime, Key = keys[i] });
                        }
                    }
                    //dic.Add(data[DateTime.Today.AddDays(1).ToDateString()].First().Key, new { Begin = data[DateTime.Today.ToDateString()][keys[keys.Count - 1]].OpenRiskTime, End = data[DateTime.Today.AddDays(1).ToDateString()].First().Value.OpenRiskTime });
                    list.Add(new DetailTimeModel() { Begin = data[DateTime.Today.ToDateString()][keys[keys.Count - 1]].OpenRiskTime, End = data[DateTime.Today.AddDays(1).ToDateString()].First().Value.OpenRiskTime, Key = data[DateTime.Today.AddDays(1).ToDateString()].First().Key });
                    int index = 0;
                    for (int i = 1; i < list.Count; i++)
                    {
                        if (list[i].Begin < DateTime.Now && list[i].End > DateTime.Now)
                        {
                            index = i;
                            break;
                        }
                    }
                    if (index == 0)
                    {
                        for (int i = 0; (i < index + 30) && (i < list.Count); i++)
                        {
                            dic.Add(list[i].Key, new { list[i].Begin, list[i].End });
                        }
                    }
                    else
                    {
                        for (int i = index - 1; (i < index + 30) && (i < list.Count); i++)
                        {
                            dic.Add(list[i].Key, new { list[i].Begin, list[i].End });
                        }
                    }
                }
                else
                {
                    DateTime begin = DateTime.MinValue;
                    foreach (string s in data.Keys.OrderBy(t => t))
                    {
                        foreach (string key in data[s].Keys)
                        {
                            DateTime dt = DateTime.Now;
                            if (data[s][key].OpenRiskTime < dt)
                            {
                                dt = data[s][key].OpenRiskTime.Date;
                            }
                            if (begin != DateTime.MinValue)
                            {
                                dt = begin;
                            }
                            dic.Add(key, new { Begin = dt, End = data[s][key].OpenRiskTime });
                            begin = data[s][key].OpenRiskTime;
                        }
                    }
                }
                if (lottery.IsStop)
                {
                    return new ServiceResult(ServiceResultCode.Error, "当前彩种正在维护");
                }

                var maxRate = BaseConfig.CreateInstance(SystemConfigEnum.MaxRebate).DecimalValue;

                var player = PlayerService.GetPlayer(UserId);

                return new ServiceResult<object>(ServiceResultCode.Success, "OK", new
                {
                    lottery.LotteryTypeId,
                    lottery.IconId,
                    lottery.Name,
                    LotteryPlayTypes = from t in lottery.LotteryPlayTypes
                                       where t.IsEnable
                                       orderby t.SortIndex
                                       select new
                                       {
                                           t.LotteryPlayTypeId,
                                           t.Name,
                                           LotteryPlayDetails = from d in t.LotteryPlayDetails
                                                                where d.IsEnable
                                                                orderby d.SortIndex
                                                                select new
                                                                {
                                                                    d.LotteryPlayDetailId,
                                                                    d.Name,
                                                                    d.Description,
                                                                    Odds = d.CalculateOdds(maxRate, player.Rebate)
                                                                }
                                       },
                    OpenDatas = dic
                });
            }, "获取彩种失败");
        }

        [HttpPost("GetServiceTime")]
        [AllowAnonymous]
        public ServiceResult GetServiceTime()
        {
            return new ServiceResult<object>(ServiceResultCode.Success, "OK", DateTime.Now.ToCommonString());
        }

        [HttpPost("GetLotteryNewDatas")]
        public ServiceResult GetLotteryNewDatas(ApiLotteryGetLotteryNewDatasViewModel model)
        {
            return Json(() => ObjectResult(from d in LotteryService.GetNewDatas(model.Id, model.Size)
                                           select new
                                           {
                                               d.Data,
                                               Time = d.Time.ToCommonString(),
                                               d.Number
                                           }), "获取彩种最新开奖数据失败");
        }
    }

    public class ApiLotteryGetLotteryDetailViewModel
    {
        public int Id { get; set; }
    }

    public class ApiLotteryGetLotteryNewDatasViewModel : ApiLotteryGetLotteryDetailViewModel
    {
        public int Size { get; set; }
    }

    public class DetailTimeModel
    {
        public string Key { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }
    }
}