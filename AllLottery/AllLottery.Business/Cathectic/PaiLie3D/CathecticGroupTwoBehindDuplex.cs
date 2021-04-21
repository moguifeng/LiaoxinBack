namespace AllLottery.Business.Cathectic.PaiLie3D
{
    public class CathecticGroupTwoBehindDuplex : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 2, 10) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 1));
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 2);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {

            if (openDatas[1] == openDatas[2])
            {
                return 0;
            }
            int dataOne = int.Parse(data[0].ToString());
            int dataTwo = int.Parse(data[1].ToString());

            if ((dataOne == openDatas[1] && dataTwo == openDatas[2]) ||
                (dataTwo == openDatas[1] && dataOne == openDatas[2]))
            {
                return 1;
            }
            return 0;
        }
    }
}