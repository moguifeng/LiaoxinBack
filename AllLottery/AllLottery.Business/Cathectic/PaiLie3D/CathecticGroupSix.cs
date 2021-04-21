namespace AllLottery.Business.Cathectic.PaiLie3D
{
    public class CathecticGroupSix : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 3, 10) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 1));
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 3);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            return WinThreeGroupSix(0, openDatas, data) ? 1 : 0;
        }
    }
}