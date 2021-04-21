namespace AllLottery.Business.Cathectic.PaiLie3D
{
    public class CathecticGroupThreeThree : BaseCathectic
    {
        public override int Cathectic(string datas)
        {
            return GetPermutationByDimension(datas, 2).Count;
        }

        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 2, 10) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 1));
        }

        public override string[] SplitDatas(string datas)
        {
            return GroupThreePermutation(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            return GroupThreeIsEachWin(0, data, openDatas) ? 1 : 0;
        }
    }
}