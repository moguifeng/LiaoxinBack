namespace AllLottery.Business.Cathectic.PkShi
{
    /// <summary>
    /// 猜冠军 
    /// </summary>
    public class CathecticGuessChampion : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 1, 10) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 2));
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas);
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