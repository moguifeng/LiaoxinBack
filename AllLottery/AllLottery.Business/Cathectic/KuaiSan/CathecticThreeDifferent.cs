namespace AllLottery.Business.Cathectic.KuaiSan
{
    /// <summary>
    /// 三不同号
    /// </summary>
    public class CathecticThreeDifferent : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckRepeatData(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 3, 6) && CheckEachChoiceCount(datas, 1));
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 3);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (string.Join(string.Empty, openDatas) == data)
            {
                return 1;
            }
            return 0;
        }
    }
}