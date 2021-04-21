using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AllLottery.Model;
using Zzb.Common;

namespace AllLottery.Spider
{
    public abstract class Base168Spider: BaseSpiderThread
    {
        protected override void StartRun(LotteryType type)
        {
            new Task(() =>
            {
                string issue = null;
                while (true)
                {
                    try
                    {
                        var model = GetNewSpider(type.SpiderName);
                        if (issue != model.Result.Data.PreDrawIssue)
                        {
                            using (var context = LotteryContext.CreateContext())
                            {
                                var exist = (from d in context.LotteryDatas
                                             where d.Number == model.Result.Data.PreDrawIssue &&
                                                   d.LotteryTypeId == type.LotteryTypeId
                                             select d).FirstOrDefault();
                                if (exist == null)
                                {
                                    context.LotteryDatas.Add(new LotteryData()
                                    {
                                        Time = DateTime.Parse(model.Result.Data.PreDrawTime),
                                        LotteryTypeId = type.LotteryTypeId,
                                        Data = GetData(model.Result.Data.PreDrawCode),
                                        Number = model.Result.Data.PreDrawIssue
                                    });
                                    Console.WriteLine($"采集彩种[{type.Name}]成功：期号为[{model.Result.Data.PreDrawIssue}]，开奖号码为[{GetData(model.Result.Data.PreDrawCode)}]");
                                    DoAnySomething(model, type, context);
                                    context.SaveChanges();
                                }
                                else if (!exist.IsEnable)
                                {
                                    if (string.IsNullOrEmpty(exist.Data))
                                    {
                                        //为解决六合彩采集空的问题
                                        if (string.IsNullOrEmpty(GetData(model.Result.Data.PreDrawCode)))
                                        {
                                            continue;
                                        }
                                        exist.Data = GetData(model.Result.Data.PreDrawCode);
                                        Console.WriteLine($"采集彩种[{type.Name}]成功：期号为[{model.Result.Data.PreDrawIssue}]，开奖号码为[{GetData(model.Result.Data.PreDrawCode)}]");
                                        DoAnySomething(model, type, context);
                                    }
                                    exist.IsEnable = true;
                                    context.SaveChanges();
                                }

                                issue = model.Result.Data.PreDrawIssue;
                            }
                        }
                        DoAlways(model, type);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"采集彩种[{type.Name}]错误:{e}");
                    }
                    finally
                    {
                        Thread.Sleep(3000);
                    }
                }
            }).Start();
        }

        protected virtual void DoAlways(SpiderViewModel model, LotteryType type)
        {

        }

        protected virtual void DoAnySomething(SpiderViewModel model, LotteryType type, LotteryContext context)
        {

        }

        protected virtual string GetData(string data)
        {
            return data;
        }

        protected virtual SpiderViewModel GetNewSpider(string spiderName)
        {
            string html = HttpHelper.GetPage(Url + spiderName);
            return JsonHelper.Json<SpiderViewModel>(html);
        }
    }
}