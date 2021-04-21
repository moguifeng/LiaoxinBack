using AllLottery.Business.Cathectic;
using AllLottery.Business.Config;
using AllLottery.Business.Generate;
using AllLottery.Business.Report;
using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zzb;
using Zzb.Common;

namespace AllLottery.AutomaticGenerate
{
    public class CalculateLotteryOpenData
    {
        private static Dictionary<int, CalculateViewModel> _cache = new Dictionary<int, CalculateViewModel>();

        private static int _notReportPlayrCount = int.MinValue;

        public static string GetOpenData(int lotteryId, string number)
        {
            using (var context = LotteryContext.CreateContext())
            {
                lock (_cache)
                {
                    if (_notReportPlayrCount == int.MinValue)
                    {
                        _notReportPlayrCount = context.NotReportPlayers.Count();
                        CheckNotPlayerCount();
                    }
                    CheckLottery(lotteryId, context);
                }
                var lotteryType = (from l in context.LotteryTypes where l.LotteryTypeId == lotteryId select l)
                    .FirstOrDefault();
                if (lotteryType == null)
                {
                    throw new ZzbException("未找到彩种");
                }

                //盈利比例
                var rate = lotteryType.WinRate;

                if (rate == 0)
                {
                    return BaseGenerate.CreateGenerate(lotteryType.LotteryClassify.Type).RandomGenerate();
                }

                //查询当前彩种和期号的投注
                var bets = (from b in context.Bets
                            where b.IsEnable && b.Status == BetStatusEnum.Wait &&
                                  b.LotteryPlayDetail.LotteryPlayType.LotteryTypeId == lotteryId && b.LotteryIssuseNo == number
                            select b).ToList();

                //如果存在投注
                if (bets.Any())
                {
                    var ids = (from n in context.NotReportPlayers select n.PlayerId).ToArray();
                    //最大返点
                    var max = BaseConfig.CreateInstance(SystemConfigEnum.MaxRebate).DecimalValue;

                    //计时，避免死循环
                    DateTime dt = DateTime.Now;

                    Dictionary<string, decimal> cacheNumber = new Dictionary<string, decimal>();

                    while (true)
                    {
                        decimal betMoney = 0, winMoney = 0;



                        //开奖号码
                        var data = BaseGenerate.CreateGenerate(lotteryType.LotteryClassify.Type).RandomGenerate();
                        int count = 0;
                        //不开重复
                        while (cacheNumber.ContainsKey(data))
                        {
                            count++;
                            if (count > 10)
                            {
                                var kv = (from k in cacheNumber orderby k.Value select k).First();
                                _cache[lotteryId].WinMoney += kv.Value;
                                _cache[lotteryId].BetMoney += betMoney;
                                Console.WriteLine($"{lotteryType.Name}杀号失败，耗时{(DateTime.Now - dt).TotalSeconds}秒，尝试开奖号码{cacheNumber.Count},分别是{string.Join("|", cacheNumber.Keys.ToArray())}，最后返回{kv.Key}，盈利{betMoney - kv.Value}");
                                return kv.Key;
                            }
                            data = BaseGenerate.CreateGenerate(lotteryType.LotteryClassify.Type).RandomGenerate();
                        }

                        foreach (var bet in bets)
                        {
                            var numbers = BaseCathectic.ConvertoNumbers(data);
                            try
                            {
                                bet.WinBetCount = BaseCathectic.CreateCathectic(bet.LotteryPlayDetail.ReflectClass)
                                    .IsWin(bet.BetNo, numbers);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(
                                    $"开奖算法出错。反射玩法[{bet.LotteryPlayDetail.ReflectClass}]，IsWin出错，参数[{bet.BetNo}],[{numbers.ToLogString()}]",
                                    e);
                                continue;
                            }
                            if (!ids.Contains(bet.PlayerId) && bet.Player.Type != PlayerTypeEnum.TestPlay)
                            {
                                betMoney += bet.BetMoney;
                                if (bet.WinBetCount > 0)
                                {
                                    winMoney += BaseCathectic.CreateCathectic(bet.LotteryPlayDetail.ReflectClass)
                                        .CalculateWinMoney(bet, max, numbers);
                                }
                            }
                        }

                        lock (_cache)
                        {
                            //CheckLottery(lotteryId, context);
                            cacheNumber.Add(data, winMoney);
                            if (betMoney + _cache[lotteryId].BetMoney - winMoney - _cache[lotteryId].WinMoney > (betMoney + _cache[lotteryId].BetMoney) * rate)
                            {
                                _cache[lotteryId].WinMoney += winMoney;
                                _cache[lotteryId].BetMoney += betMoney;
                                Console.WriteLine($"{lotteryType.Name}杀号成功，耗时{(DateTime.Now - dt).TotalSeconds}秒，最终开奖{data}，盈利{betMoney - winMoney}，尝试开奖号码{cacheNumber.Count},分别是{string.Join("|", cacheNumber.Keys.ToArray())}");
                                return data;
                            }
                            //检查时间
                            var seconds = (DateTime.Now - dt).TotalSeconds;
                            Console.WriteLine($"[{lotteryType.Name}]" + seconds);
                            if (seconds > 5)
                            {
                                if (cacheNumber.Count > 0)
                                {
                                    var kv = (from k in cacheNumber orderby k.Value select k).First();
                                    _cache[lotteryId].WinMoney += kv.Value;
                                    _cache[lotteryId].BetMoney += betMoney;
                                    Console.WriteLine($"{lotteryType.Name}杀号失败，耗时{(DateTime.Now - dt).TotalSeconds}秒，尝试开奖号码{cacheNumber.Count},分别是{string.Join("|", cacheNumber.Keys.ToArray())}，最后返回{kv.Key}，盈利{betMoney - kv.Value}");
                                    return kv.Key;
                                }
                                else
                                {
                                    Console.WriteLine($"{lotteryType.Name}杀号错误,耗时{(DateTime.Now - dt).TotalSeconds}秒,请检查错误问题");
                                    return BaseGenerate.CreateGenerate(lotteryType.LotteryClassify.Type).RandomGenerate();
                                }
                            }
                        }
                    }
                }
                else
                {
                    return BaseGenerate.CreateGenerate(lotteryType.LotteryClassify.Type).RandomGenerate();
                }
            }
        }

        public static void CheckLottery(int lotteryId, LotteryContext context)
        {
            CalculateViewModel model = new CalculateViewModel();
            model.BetMoney = new LotteryBetReport(lotteryId).GetReportData();
            model.WinMoney = new LotteryWinReport(lotteryId).GetReportData();
            if (!_cache.ContainsKey(lotteryId))
            {
                _cache.Add(lotteryId, model);
            }
            else
            {
                _cache[lotteryId] = model;
            }
        }

        public static void CheckNotPlayerCount()
        {
            //新起线程
            new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        using (var cc = LotteryContext.CreateContext())
                        {
                            var count = cc.NotReportPlayers.Count();
                            if (count != _notReportPlayrCount)
                            {
                                lock (_cache)
                                {
                                    _cache = new Dictionary<int, CalculateViewModel>();
                                    _notReportPlayrCount = count;
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        Thread.Sleep(1000);
                    }
                }
            }).Start();
        }
    }

    public class CalculateViewModel
    {
        public decimal BetMoney { get; set; }

        public decimal WinMoney { get; set; }
    }
}