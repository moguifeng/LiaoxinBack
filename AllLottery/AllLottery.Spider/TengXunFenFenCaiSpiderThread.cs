using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zzb.Common;

namespace AllLottery.Spider
{
    public class TengXunFenFenCaiSpiderThread : BaseSpiderThread
    {
        public override string Url => "http://77tj.org/api/tencent/onlineim";

        Dictionary<string, bool> dic = new Dictionary<string, bool>();

        protected override void StartRun(LotteryType type)
        {
            new Task(() =>
            {
                while (true)
                {
                    try
                    {
                        var html = HttpHelper.GetPage(Url);
                        var json = JsonHelper.Json<TengXunFenFenCaiModel[]>(html);
                        foreach (TengXunFenFenCaiModel model in json)
                        {
                            var data = CreateData(model.OnlineNumber);
                            var issue = CreateNumber(model);
                            var time = model.OnlineTime;
                            if (!dic.ContainsKey(issue))
                            {
                                LotteryData d = new LotteryData()
                                {
                                    Data = data,
                                    LotteryTypeId = type.LotteryTypeId,
                                    Number = issue,
                                    Time = DateTime.Parse(time)
                                };
                                if (AddData(d))
                                {
                                    Log(type, d);
                                }
                                dic.Add(issue, true);
                            }
                        }

                        if (dic.Count > 10000)
                        {
                            dic = new Dictionary<string, bool>();
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    finally
                    {
                        Thread.Sleep(3000);
                    }
                }
            }).Start();
        }

        protected void Log(LotteryType type, LotteryData data)
        {
            Console.WriteLine($"采集彩种[{type.Name}]成功。期号：[{data.Number}]、开奖数据：[{data.Data}]、 开奖时间：[{data.Time.ToCommonString()}]");
        }

        //protected override SpiderViewModel GetNewSpider(string spiderName)
        //{
        //    var html = HttpHelper.GetPage(Url);
        //    var json = JsonHelper.Json<TengXunFenFenCaiModel[]>(html);
        //    return new SpiderViewModel() { Result = new SpiderResultViewModel() { Data = new SpiderResultDataViewModel() { PreDrawIssue = CreateNumber(json[0]), PreDrawCode = CreateData(json[0].OnlineNumber), PreDrawTime = json[0].OnlineTime } } };
        //}

        protected string CreateNumber(TengXunFenFenCaiModel json)
        {
            DateTime dt = DateTime.Parse(json.OnlineTime);
            int i = dt.Hour * 60 + dt.Minute;
            return dt.ToString("yyyyMMdd") + "-" + i.ToString().PadLeft(4, '0');
        }

        protected string CreateData(string onlineNumber)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    long sum = 0;
                    for (int j = 0; j < onlineNumber.Length; j++)
                    {
                        sum += int.Parse(onlineNumber[j].ToString());
                    }
                    sb.Append(sum % 10 + ",");
                }
                else
                {
                    sb.Append(onlineNumber[onlineNumber.Length - 5 + i] + ",");
                }
            }
            return sb.ToString().Trim(',');
        }

        protected bool AddData(LotteryData data)
        {
            using (var context = LotteryContext.CreateContext())
            {
                var exist = from d in context.LotteryDatas
                            where d.LotteryTypeId == data.LotteryTypeId && d.Number == data.Number
                            select d;
                if (exist.Any())
                {
                    return false;
                }
                else
                {
                    context.LotteryDatas.Add(data);
                    context.SaveChanges();
                    return true;
                }
            }
        }
    }

    public class TengXunFenFenCaiModel
    {
        public string OnlineTime { get; set; }

        public string OnlineNumber { get; set; }

        public string OnlineChange { get; set; }
    }
}