using AllLottery.Model;

namespace AllLottery.Spider
{
    public abstract class BaseSpiderThread
    {
        public abstract string Url { get; }

        public static void Start(LotteryType type)
        {
            CreateIns(type)?.StartRun(type);
        }

        private static BaseSpiderThread CreateIns(LotteryType type)
        {
            if (type.SpiderName == "10046")
            {
                return new PCDanDanSpiderThread();
            }
            if (type.SpiderName == "10014")
            {
                return new LuckySpiderThread();
            }
            if (type.SpiderName == "txffc")
            {
                return new TengXunFenFenCaiSpiderThread();
            }
            if (type.SpiderName == "viffc5")
            {
                return new CaiPiaoKongApiSpider();
            }
            switch (type.LotteryClassify.Type)
            {
                case LotteryClassifyType.Kuai3:
                    return new KuaiSanSpiderThread();
                case LotteryClassifyType.SaiChe:
                    return new SaiCheSpiderThread();
                case LotteryClassifyType.Ssc:
                    return new ShiShiCaiSpiderThread();
                case LotteryClassifyType.Xuan5:
                    return new Xuan5SpiderThread();
                case LotteryClassifyType.PaiLie3D:
                    return new Pailie3DSpiderThread();
                case LotteryClassifyType.LiuHeCai:
                    return new LiuHeCaiSpiderThread();
                default:
                    return null;
            }
        }

        protected abstract void StartRun(LotteryType type);
    }

    public class SpiderViewModel
    {
        public SpiderResultViewModel Result { get; set; }
    }

    public class SpiderResultViewModel
    {
        public SpiderResultDataViewModel Data { get; set; }
    }

    public class SpiderResultDataViewModel
    {
        public string PreDrawCode { get; set; }

        public string PreDrawIssue { get; set; }

        public string PreDrawTime { get; set; }

        public string DrawIssue { get; set; }

        public string DrawTime { get; set; }
    }
}