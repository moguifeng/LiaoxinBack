namespace AllLottery.Business.Cathectic.ShiYiXuanWu
{
    /// <summary>
    /// 前二直选
    /// </summary>
    public class CathecticFrontTwoVertical : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            var res = (
                CheckRepeatData(datas, 2) &&
                CheckAllNumber(datas, 2)) ? true : false;
            if (res)
            {
                string[] groupDatas = GroupByDatas(datas);

                foreach (var data in groupDatas)
                {
                    if (!(CheckChoiceOfMaxMinRange(data, 1, 11) && CheckEachChoiceCount(data, 2)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            return TwoGroupCombination(datas).ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            string[] datas = SplitByDatas(data);
            if (int.Parse(datas[0]) == openDatas[0] && int.Parse(datas[1]) == openDatas[1])
            {
                return 1;
            }
            return 0;
        }
    }
}