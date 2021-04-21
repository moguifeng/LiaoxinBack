namespace AllLottery.Business.Cathectic.ShiYiXuanWu
{
    /// <summary>
    /// 趣味_猜中位
    /// </summary>
    public class CathecticFunnyGuessHit : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 1, 7) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 2));
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (openDatas[2] == int.Parse(data.ToString()))
            {
                return 1;
            }
            return 0;
        }
    }
}