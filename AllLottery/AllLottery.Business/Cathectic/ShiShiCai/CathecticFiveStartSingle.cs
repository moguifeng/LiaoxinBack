namespace AllLottery.Business.Cathectic.ShiShiCai
{
    public class CathecticFiveStartSingle : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return AnyStartSingleValid(datas, 5);
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas, "_");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            data = data.Replace(",", string.Empty);
            if (string.Join(string.Empty, openDatas) == data)
            {
                return 1;
            }
            return 0;
        }
    }
}