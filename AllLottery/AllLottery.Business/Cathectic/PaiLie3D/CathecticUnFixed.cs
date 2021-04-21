namespace AllLottery.Business.Cathectic.PaiLie3D
{
    public class CathecticUnFixed : BaseCathectic
    {

        public override bool Valid(string datas)
        {
            return (CheckAllNumber(datas) &&
                    CheckChoiceOfMaxMinRange(datas, 1, 10) &&
                    CheckRepeatData(datas) && CheckEachChoiceCount(datas, 1)) ? true : false;
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            for (int i = 0; i < 3; i++)
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