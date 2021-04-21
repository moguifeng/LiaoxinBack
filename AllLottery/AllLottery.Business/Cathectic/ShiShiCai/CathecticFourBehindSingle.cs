namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticFourBehindSingle : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return AnyStartSingleValid(datas, 4);
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas, "_");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            data = data.Replace(",", string.Empty);
            string openDataStr = string.Join(string.Empty, openDatas);
            for (int i = 1; i < 5; i++)
            {
                if (openDataStr[i] != data[i - 1])
                {
                    return 0;
                }
            }
            return 1;
        }
    }
}