using AllLottery.IBusiness;
using AllLottery.Model;
using AllLottery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.ZzbLog;


//作废啦
namespace AllLottery.Business
{
    //public class LotteryOpenTimeServer : BaseService, ILotteryOpenTimeServer
    //{
    //    #region 接口

    //    /// <summary>
    //    /// 彩种ID
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    public Dictionary<string, Dictionary<string, LotteryNumberBetTimeViewMode>> GetBetCacheDictionary(int id)
    //    {
    //        CalculateBetTime(id);

    //        if (_betTimeDic.ContainsKey(id))
    //        {
    //            return _betTimeDic[id];
    //        }

    //        return null;
    //    }

    //    public Dictionary<string, Dictionary<string, LotteryNumberBetTimeViewMode>> GetBetCacheDictionary(LotteryData data)
    //    {
    //        CalculateIncrease(data.LotteryType, data);

    //        return _betTimeDic[data.LotteryTypeId];
    //    }

    //    public void ClearCache(int id)
    //    {
    //        lock (_betTimeDic)
    //        {
    //            if (_betTimeDic.ContainsKey(id))
    //            {
    //                _betTimeDic.Remove(id);
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 获取彩种当前待开的期号
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    public string GetCurrentBetIssuse(int id)
    //    {
    //        var dic = GetBetCacheDictionary(id);
    //        if (dic == null)
    //        {
    //            throw new ZzbException($"无法找到彩种[{id}]开奖时间");
    //        }

    //        var today = dic[DateTime.Now.ToDateString()];
    //        foreach (KeyValuePair<string, LotteryNumberBetTimeViewMode> temp in today)
    //        {
    //            if (temp.Value.OpenRiskTime > DateTime.Now)
    //            {
    //                return temp.Key;
    //            }
    //        }

    //        return null;
    //    }

    //    #endregion

    //    #region 计算彩种期号

    //    /// <summary>
    //    /// 1.彩种ID
    //    /// 2.时间
    //    /// 3.期号
    //    /// </summary>
    //    private static Dictionary<int, Dictionary<string, Dictionary<string, LotteryNumberBetTimeViewMode>>> _betTimeDic = new Dictionary<int, Dictionary<string, Dictionary<string, LotteryNumberBetTimeViewMode>>>();


    //    public BetTimeViewModel GetBetTime(int lotteryTypeId, string no)
    //    {
    //        lock (_betTimeDic)
    //        {
    //            CalculateBetTime(lotteryTypeId);

    //            if (_betTimeDic.ContainsKey(lotteryTypeId))
    //            {
    //                foreach (var dic in _betTimeDic[lotteryTypeId])
    //                {
    //                    if (dic.Value.ContainsKey(no))
    //                    {
    //                        var i = dic.Value.Keys.ToList().IndexOf(no);
    //                        if (i != 0)
    //                        {
    //                            return new BetTimeViewModel(dic.Value[dic.Value.Keys.ToList()[i - 1]].OpenTime, dic.Value[no].OpenTime, dic.Value[no].RiskTime);
    //                        }
    //                        else
    //                        {
    //                            return new BetTimeViewModel(
    //                                dic.Value[dic.Value.Keys.ToList()[dic.Value.Count - 1]].OpenTime.AddDays(-1),
    //                                dic.Value[no].OpenTime, dic.Value[no].RiskTime);
    //                        }
    //                        //return dic.Value[no];
    //                    }
    //                }
    //            }

    //            return null;
    //        }
    //    }


    //    /// <summary>
    //    /// 彩种ID对应每天的开奖日期
    //    /// </summary>
    //    private static Dictionary<int, List<TimeSpan>> _betTimeDayDic = new Dictionary<int, List<TimeSpan>>();

    //    private int _cacheDays = 7;//计算多少天缓存

    //    private LotteryData[] GetLastLotteryDatasByTypeId(int typeId, int num)
    //    {
    //        var datas = (from d in Context.LotteryDatas where d.LotteryTypeId == typeId && d.IsEnable == true orderby d.Time descending select d).Take(num).ToArray();
    //        return datas;
    //    }

    //    private LotteryType GetLotteryType(int id)
    //    {
    //        return
    //            (from t in Context.LotteryTypes where t.LotteryTypeId == id select t).FirstOrDefault();
    //    }

    //    /// <summary>
    //    /// 计算彩种缓存，此方法主要判断异常情况，出现情况则关闭彩种
    //    /// </summary>
    //    /// <param name="lotteryTypeId"></param>
    //    private void CalculateBetTime(int lotteryTypeId)
    //    {
    //        lock (_betTimeDic)
    //        {
    //            if (!_betTimeDic.ContainsKey(lotteryTypeId))
    //            {
    //                _betTimeDic.Add(lotteryTypeId, new Dictionary<string, Dictionary<string, LotteryNumberBetTimeViewMode>>());
    //            }
    //            if (!_betTimeDic[lotteryTypeId].ContainsKey(DateTime.Now.AddDays(1).ToDateString()))//没找到第二天的开奖数据，则重新计算缓存
    //            {
    //                _betTimeDic[lotteryTypeId] = new Dictionary<string, Dictionary<string, LotteryNumberBetTimeViewMode>>();
    //                var list = GetLastLotteryDatasByTypeId(lotteryTypeId, 1);
    //                var type = GetLotteryType(lotteryTypeId);
    //                if (list == null || list.Length == 0)//没查到数据，不计算开奖时间
    //                {
    //                    if (type == null)
    //                    {
    //                        _betTimeDic.Remove(lotteryTypeId);
    //                    }
    //                    else
    //                    {
    //                        CloseLotteryType(type, "没查到数据，不计算开奖时间");
    //                    }
    //                }
    //                else
    //                {
    //                    var data = list[0];
    //                    //把延迟1改成7适配香港六合彩
    //                    if (data.Time < DateTime.Now.AddDays(-7).Date)//开奖时间太久没更新，计算开奖时间已经没意义
    //                    {
    //                        CloseLotteryType(type, "开奖时间太久没更新");
    //                    }
    //                    else
    //                    {
    //                        CalculateBetTime(type, data);
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 待优化
    //    /// </summary>
    //    /// <param name="type"></param>
    //    /// <param name="data"></param>
    //    private void CalculateBetTime(LotteryType type, LotteryData data)
    //    {
    //        switch (type.CalType)
    //        {
    //            case LotteryCalNumberTypeEnum.FromZeroEveryDay:
    //                CalculateFromZeroEveryDay(type);
    //                break;
    //            case LotteryCalNumberTypeEnum.Increase:
    //            case LotteryCalNumberTypeEnum.Automatic:
    //                CalculateIncrease(type, data);
    //                break;
    //            case LotteryCalNumberTypeEnum.AccordingToOpenData:
    //                CalculateAccordingToOpenData(data);
    //                break;
    //            default:
    //                LogHelper.Error($"无法找到对应的计算算法[{type.CalType}]");
    //                break;
    //        }

    //    }

    //    private void CalculateAccordingToOpenData(LotteryData data)
    //    {
    //        var exist = (from d in Context.LotteryDatas
    //                     where d.LotteryTypeId == data.LotteryTypeId && !d.IsEnable
    //                     orderby d.Time
    //                     select d).FirstOrDefault();
    //        if (exist == null)
    //        {
    //            return;
    //        }

    //        _betTimeDic[data.LotteryTypeId].Add(data.Time.ToDateString(), new Dictionary<string, LotteryNumberBetTimeViewMode>());
    //        _betTimeDic[data.LotteryTypeId][data.Time.ToDateString()].Add(data.Number, new LotteryNumberBetTimeViewMode(data.Time, data.LotteryType.RiskTime));
    //        _betTimeDic[data.LotteryTypeId].Add(exist.Time.ToDateString(), new Dictionary<string, LotteryNumberBetTimeViewMode>());
    //        _betTimeDic[data.LotteryTypeId][exist.Time.ToDateString()].Add(exist.Number, new LotteryNumberBetTimeViewMode(exist.Time, exist.LotteryType.RiskTime));
    //    }

    //    /// <summary>
    //    /// 期号不停叠加，不会清0
    //    /// </summary>
    //    /// <param name="type"></param>
    //    /// <param name="data"></param>
    //    private void CalculateIncrease(LotteryType type, LotteryData data)
    //    {
    //        _betTimeDic[type.LotteryTypeId] = new Dictionary<string, Dictionary<string, LotteryNumberBetTimeViewMode>>();
    //        int index = -1;
    //        var list = CalculateOpenDate(type, (data.Time - DateTime.Now).Days - 1);
    //        var dt = DateTime.Now;
    //        long number = long.Parse(data.Number);
    //        //if (data.Time < list[0][0].OpenTime)
    //        //{
    //        //    CloseLotteryType(type, "开奖时间太久没更新");
    //        //    return;
    //        //}
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            for (int j = 0; j < list[i].Count; j++)
    //            {
    //                if (data.Time < list[i][j].OpenTime)
    //                {
    //                    index = i * list[0].Count + j - 1;
    //                    goto loop;
    //                }
    //            }
    //        }
    //        loop:
    //        if (index == -1)
    //        {
    //            CloseLotteryType(type, "开奖时间异常");
    //            return;
    //        }
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            _betTimeDic[type.LotteryTypeId].Add(dt.AddDays(i - 1).ToDateString(), new Dictionary<string, LotteryNumberBetTimeViewMode>());
    //            for (int j = 0; j < list[i].Count; j++)
    //            {
    //                _betTimeDic[type.LotteryTypeId][dt.AddDays(i - 1).ToDateString()].Add((number - index + i * list[0].Count + j).ToString().PadLeft(type.NumberLength, '0'), new LotteryNumberBetTimeViewMode(list[i][j].OpenTime, type.RiskTime));
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 期号每天清0的算法
    //    /// </summary>
    //    /// <param name="type"></param>
    //    /// <param name="data"></param>
    //    private void CalculateFromZeroEveryDay(LotteryType type)
    //    {
    //        var list = CalculateOpenDate(type);
    //        var dt = DateTime.Now;
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            _betTimeDic[type.LotteryTypeId].Add(dt.AddDays(i - 1).ToDateString(), new Dictionary<string, LotteryNumberBetTimeViewMode>());
    //            for (int j = 0; j < list[i].Count; j++)
    //            {
    //                int number = j + 1;
    //                if (type.SpiderName == "txffc")
    //                {
    //                    number = j;
    //                }
    //                _betTimeDic[type.LotteryTypeId][dt.AddDays(i - 1).ToDateString()].Add(dt.AddDays(i - 1).ToString(type.DateFormat) + number.ToString().PadLeft(type.NumberLength, '0'), new LotteryNumberBetTimeViewMode(list[i][j].OpenTime, type.RiskTime));
    //            }
    //        }
    //    }

    //    private List<List<LotteryNumberBetTimeViewMode>> CalculateOpenDate(LotteryType type, int cacheDays = -1)
    //    {
    //        lock (_betTimeDayDic)
    //        {
    //            if (!_betTimeDayDic.ContainsKey(type.LotteryTypeId))
    //            {
    //                CalculateBetTime(type);
    //            }
    //            List<List<LotteryNumberBetTimeViewMode>> list = new List<List<LotteryNumberBetTimeViewMode>>();
    //            DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
    //            for (int i = cacheDays; i <= _cacheDays; i++)
    //            {
    //                DateTime oneDt = dt.AddDays(i);
    //                List<LotteryNumberBetTimeViewMode> one = new List<LotteryNumberBetTimeViewMode>();
    //                for (int j = 0; j < _betTimeDayDic[type.LotteryTypeId].Count; j++)
    //                {
    //                    one.Add(new LotteryNumberBetTimeViewMode(oneDt + _betTimeDayDic[type.LotteryTypeId][j], type.RiskTime));
    //                }
    //                list.Add(one);
    //            }
    //            return list;
    //        }
    //    }

    //    /// <summary>
    //    /// 计算彩种一天的开奖信息，并放在_betTimeDayDic缓存
    //    /// </summary>
    //    /// <param name="type"></param>
    //    private void CalculateBetTime(LotteryType type)
    //    {
    //        lock (_betTimeDayDic)
    //        {
    //            if (!_betTimeDayDic.ContainsKey(type.LotteryTypeId))
    //            {
    //                _betTimeDayDic.Add(type.LotteryTypeId, (from t in type.LotteryOpenTimes where t.IsTomorrow == false orderby t.OpenTime select t.OpenTime).ToList());
    //                _betTimeDayDic[type.LotteryTypeId].AddRange((from t in type.LotteryOpenTimes where t.IsTomorrow orderby t.OpenTime select (t.OpenTime + new TimeSpan(1, 0, 0, 0))).ToList());
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// 关闭彩种
    //    /// </summary>
    //    /// <param name="type"></param>
    //    /// <param name="msg"></param>
    //    private void CloseLotteryType(LotteryType type, string msg)
    //    {
    //        _betTimeDic.Remove(type.LotteryTypeId);
    //        type.IsStop = true;
    //        Context.SaveChanges();
    //        LogHelper.Error($"彩种[{type.Name}]" + msg);
    //    }
    //    #endregion

    //}


}