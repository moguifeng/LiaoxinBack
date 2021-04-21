using AllLottery.Business;
using AllLottery.Business.Generate;
using AllLottery.Model;
using AllLottery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Zzb.Common;

namespace AllLottery.AutomaticGenerate
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = LotteryContext.CreateContext())
            {
                var lotteryTypes = (from t in context.LotteryTypes where t.CalType == LotteryCalNumberTypeEnum.Automatic select t).ToArray();
                if (lotteryTypes.Any())
                {
                    foreach (LotteryType type in lotteryTypes)
                    {
                        Generate(type.LotteryTypeId);
                    }
                }
            }
            while (true)
            {
                Console.ReadKey();
            }
        }

        static void Generate(int typeId)
        {
            string typeName = string.Empty;
            using (var context = LotteryContext.CreateContext())
            {
                var type = (from t in context.LotteryTypes
                            where t.LotteryTypeId == typeId
                            select t).FirstOrDefault();
                if (type == null)
                {
                    return;
                }
                if (!type.LotteryOpenTimes.Any())
                {
                    return;
                }
                typeName = type.Name;

                var firstData =
                (from d in context.LotteryDatas
                 where d.LotteryTypeId == type.LotteryTypeId
                 orderby d.Time descending
                 select d).FirstOrDefault();
                //type.LotteryDatas.OrderByDescending(l => l.Time).FirstOrDefault();

                if (firstData == null)
                {
                    //获取生成日期
                    DateTime dt = DateTime.Today;

                    //计算第一期开奖
                    firstData = new LotteryData()
                    {
                        Time = dt + type.LotteryOpenTimes.OrderBy(c => c.OpenTime).First().OpenTime,
                        Number = RandomHelper.Next(1, 1000000).ToString().PadLeft(7, '0'),
                        LotteryTypeId = type.LotteryTypeId
                    };
                    firstData.Data = BaseGenerate.CreateGenerate(type.LotteryClassify.Type).RandomGenerate();
                    //插入数据库
                    context.LotteryDatas.Add(firstData);
                    context.SaveChanges();
                }
            }

            new Task(() =>
            {
                Console.WriteLine($"采种[{typeName}]自动生成线程开始");
                int i = 0;
                LotteryOpenTimeServer lotteryOpenTimeServer = new LotteryOpenTimeServer();
                while (true)
                {
                    string name = null;

                    try
                    {
                        using (var context = LotteryContext.CreateContext())
                        {
                            lotteryOpenTimeServer.Context = context;
                            var exist = (from t in context.LotteryTypes
                                         where t.LotteryTypeId == typeId
                                         select t).FirstOrDefault();
                            if (exist == null)
                            {
                                return;
                            }
                            name = exist.Name;
                            if (exist.IsEnable == false)
                            {
                                Thread.Sleep(5000);
                                continue;
                            }

                            var data = (from d in context.LotteryDatas
                                        where d.LotteryTypeId == exist.LotteryTypeId && d.IsEnable
                                        orderby d.Time descending
                                        select d)
                                .FirstOrDefault(); //exist.LotteryDatas.OrderByDescending(c => c.Time).FirstOrDefault();

                            if (data == null)
                            {
                                return;
                            }
                            Dictionary<string, Dictionary<string, LotteryNumberBetTimeViewMode>> cache;
                            if (data.Time.AddDays(1) < DateTime.Now)
                            {
                                cache = lotteryOpenTimeServer.GetBetCacheDictionary(data);
                            }
                            else
                            {
                                cache = lotteryOpenTimeServer.GetBetCacheDictionary(typeId);
                            }
                            string number = data.Number;
                            List<LotteryData> list = new List<LotteryData>();
                            DateTime openTime = DateTime.MaxValue;

                            //判断数据库是否改变
                            bool isChange = false;

                            foreach (var c in cache)
                            {
                                while (c.Value.ContainsKey(GetNextNumber(number)))
                                {
                                    openTime = c.Value[GetNextNumber(number)].OpenTime;
                                    if (openTime < DateTime.Now)
                                    {
                                        var nextNumber = GetNextNumber(number);
                                        var existData = (from d in context.LotteryDatas
                                                         where d.LotteryTypeId == exist.LotteryTypeId && d.Number == nextNumber
                                                         select d).FirstOrDefault();
                                        if (existData == null)
                                        {
                                            var temp = new LotteryData()
                                            {
                                                Time = openTime,
                                                Data = CalculateLotteryOpenData.GetOpenData(exist.LotteryTypeId,
                                                    nextNumber),
                                                Number = nextNumber,
                                                LotteryTypeId = typeId
                                            };
                                            list.Add(temp);
                                        }
                                        else
                                        {
                                            existData.IsEnable = true;
                                            existData.UpdateTime = DateTime.Now;
                                            isChange = true;
                                        }
                                        number = nextNumber;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }

                            if (isChange)
                            {
                                context.SaveChanges();
                            }

                            //Console.WriteLine($"彩种[{name}]待添加的开奖数据为[{list.Count}]条，现在开始添加");
                            while (list.Count > 0)
                            {
                                int remove = 100;
                                if (list.Count > remove)
                                {
                                    list.GetRange(0, remove).ForEach(l => context.LotteryDatas.Add(l));
                                    context.SaveChanges();
                                    //Console.WriteLine($"彩种[{name}]添加数据成功");
                                    list.RemoveRange(0, remove);
                                }
                                else
                                {
                                    list.ForEach(l => context.LotteryDatas.Add(l));
                                    context.SaveChanges();
                                    //Console.WriteLine($"彩种[{name}]添加数据成功");
                                    break;
                                }
                            }

                            if (openTime == DateTime.MaxValue)
                            {
                                Console.WriteLine($"自动开奖获取opentime错误，采种为[{typeId}]");
                            }
                            var span = openTime - DateTime.Now;
                            if (span > new TimeSpan(0))
                            {
                                Thread.Sleep(span > new TimeSpan(0, 1, 0) ? new TimeSpan(0, 1, 0) : span);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"彩种[{name}]添加错误[{e}]");
                        i++;
                        if (i > 10)
                        {
                            Console.WriteLine($"自动开奖已经出现10次错误，情况严重请排查,采种为[{typeId}][{e}]");
                        }
                        Thread.Sleep(1000 * 10);
                    }
                    Thread.Sleep(5000);


                }

            }).Start();
        }

        private static string GetNextNumber(string number)
        {
            return (int.Parse(number) + 1).ToString().PadLeft(7, '0');
        }
    }
}
