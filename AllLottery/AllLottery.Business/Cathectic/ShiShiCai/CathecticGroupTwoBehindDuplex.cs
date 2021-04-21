namespace AllLottery.Business.Cathectic.ShiShiCai
{
    /// <summary>
    /// 二星组选
    /// </summary>
    public class CathecticGroupTwoBehindDuplex : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 2, 10) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 1));
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 2);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (openDatas[3] == openDatas[4])
            {
                return 0;
            }
            int dataOne = int.Parse(data[0].ToString());
            int dataTwo = int.Parse(data[1].ToString());

            if ((dataOne == openDatas[3] && dataTwo == openDatas[4]) ||
                (dataTwo == openDatas[3] && dataOne == openDatas[4]))
            {
                return 1;
            }
            return 0;
        }
    }
}