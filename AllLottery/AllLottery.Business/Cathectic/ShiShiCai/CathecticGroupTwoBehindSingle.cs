namespace AllLottery.Business.Cathectic.ShiShiCai
{
    /// <summary>
    /// 二星单式
    /// </summary>
    public class CathecticGroupTwoBehindSingle : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return AnyStartSingleValid(datas, 2);
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas, "_");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (openDatas[3] == openDatas[4])
            {
                return 0;
            }

            var datas = data.Split(',');
            int dataOne = int.Parse(datas[0].ToString());
            int dataTwo = int.Parse(datas[1].ToString());

            if ((dataOne == openDatas[3] && dataTwo == openDatas[4]) ||
                (dataTwo == openDatas[3] && dataOne == openDatas[4]))
            {
                return 1;
            }
            return 0;
        }
    }
}