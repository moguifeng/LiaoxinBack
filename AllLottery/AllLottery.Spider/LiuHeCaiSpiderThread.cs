using AllLottery.Business.Generate;
using AllLottery.Model;
using System;
using System.Linq;

namespace AllLottery.Spider
{
    public class LiuHeCaiSpiderThread : Base168Spider
    {
        public override string Url => "https://1680660.com/smallSix/findSmallSixInfo.do?lotCode=";

        protected override void DoAnySomething(SpiderViewModel model, LotteryType type, LotteryContext context)
        {
            context.LotteryDatas.Add(new LotteryData()
            {
                Time = DateTime.Parse(model.Result.Data.DrawTime),
                LotteryTypeId = type.LotteryTypeId,
                Number = model.Result.Data.DrawIssue,
                IsEnable = false
            });
        }

        protected override string GetData(string data)
        {
            if (BaseGenerate.CreateGenerate(LotteryClassifyType.LiuHeCai).CheckDatas(data))
            {
                return data;
            }
            return String.Empty;
        }

        protected override void DoAlways(SpiderViewModel model, LotteryType type)
        {
            using (var context = LotteryContext.CreateContext())
            {
                var data = (from d in context.LotteryDatas
                            where d.Number == model.Result.Data.DrawIssue && !d.IsEnable && d.LotteryTypeId == type.LotteryTypeId
                            select d).FirstOrDefault();
                var time = DateTime.Parse(model.Result.Data.DrawTime);
                if (data != null && data.Time != time)
                {
                    data.Time = time;
                    context.SaveChanges();
                }
            }
        }
    }
}