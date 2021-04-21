namespace AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeSingle
{
    public abstract class BaseCathecticThreeSingle : BaseCathectic
    {
        protected abstract int Number { get; }

        public override bool Valid(string datas)
        {
            return AnyStartSingleValid(datas, 3);
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas, "_");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            data = data.Replace(",", string.Empty);
            string openDataStr = string.Join(string.Empty, openDatas);
            for (int i = 0; i < 3; i++)
            {
                if (openDataStr[i + Number] != data[i])
                {
                    return 0;
                }
            }
            return 1;
        }
    }
}