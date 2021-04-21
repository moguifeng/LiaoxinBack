namespace AllLottery.Business.Cathectic.ShiYiXuanWu
{
    /// <summary>
    /// 趣味_定单双
    /// </summary>
    public class CathecticFunnyGuessSingBoth : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            int both = 0, single = 0;
            foreach (int openData in openDatas)
            {
                if (openData % 2 == 0)
                {
                    both++;
                }
                else
                {
                    single++;
                }
            }

            switch (data)
            {
                case "5单0双":
                    return (single == 5 && both == 0) ? 1 : 0;
                case "4单1双":
                    return (single == 4 && both == 1) ? 1 : 0;
                case "3单2双":
                    return (single == 3 && both == 2) ? 1 : 0;
                case "2单3双":
                    return (single == 2 && both == 3) ? 1 : 0;
                case "1单4双":
                    return (single == 1 && both == 4) ? 1 : 0;
                case "0单5双":
                    return (single == 0 && both == 5) ? 1 : 0;
                default:
                    return 0;

            }
        }
    }
}