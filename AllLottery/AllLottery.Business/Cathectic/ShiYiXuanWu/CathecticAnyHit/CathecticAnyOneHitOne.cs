namespace AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticAnyHit
{
    /// <summary>
    /// 任选一中一
    /// </summary>
    public class CathecticAnyOneHitOne : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 1, 11) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 2));
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            for (int i = 0; i < openDatas.Length; i++)
            {
                if (openDatas[i] == int.Parse(data))
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}