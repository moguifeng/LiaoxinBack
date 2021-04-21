using System;
using AllLottery.Model;
using System.Linq;
using Zzb.ZzbLog;

namespace AllLottery.Spider
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = LotteryContext.CreateContext())
            {
                var lotteryTypes = from t in context.LotteryTypes where !string.IsNullOrEmpty(t.SpiderName) select t;
                if (lotteryTypes.Any())
                {
                    foreach (LotteryType type in lotteryTypes)
                    {
                        BaseSpiderThread.Start(type);
                    }
                }
            }
            while (true)
            {
                Console.ReadKey();
            }
        }
    }
}
