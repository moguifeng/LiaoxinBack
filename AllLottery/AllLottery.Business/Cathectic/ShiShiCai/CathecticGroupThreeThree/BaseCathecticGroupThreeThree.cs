using Zzb.Common;

namespace AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeThree
{
    public abstract class BaseCathecticGroupThreeThree : BaseCathectic
    {
        public override int Cathectic(string datas)
        {
            return GetPermutationByDimension(datas, 2).Count;
        }

        protected abstract int Number { get; }

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
            return GroupThreeIsEachWin(Number, data, openDatas) ? 1 : 0;
        }

        public override string CreateRandomNumber()
        {
            return string.Join(",", CreateSscBetIntList(RandomHelper.Next(9, 11)));
        }
    }
}