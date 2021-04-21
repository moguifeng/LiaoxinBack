namespace AllLottery.Business.Cathectic.ShiYiXuanWu
{
    /// <summary>
    /// 前一直选
    /// </summary>
    public class CathecticFrontOne : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckRepeatData(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 1, 11) && CheckEachChoiceCount(datas, 2));
        }

        public override string[] SplitDatas(string datas)
        {
            return this.SplitByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (openDatas[0] == int.Parse(data))
            {
                return 1;
            }
            return 0;
        }
    }
}