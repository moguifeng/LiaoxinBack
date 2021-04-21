namespace AllLottery.Business.Cathectic.ShiYiXuanWu
{
    /// <summary>
    /// 不定位
    /// </summary>
    public class CathecticUnFixedBilePosition : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 1, 11) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 2));
        }

        public override string[] SplitDatas(string datas)
        {
            throw new System.NotImplementedException();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            throw new System.NotImplementedException();
        }
    }
}