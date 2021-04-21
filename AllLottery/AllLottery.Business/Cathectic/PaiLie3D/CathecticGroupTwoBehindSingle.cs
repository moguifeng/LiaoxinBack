namespace AllLottery.Business.Cathectic.PaiLie3D
{
    public class CathecticGroupTwoBehindSingle : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return AnyStartSingleValid(datas, 2);
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas, "_");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (openDatas[1] == openDatas[2])
            {
                return 0;
            }

            var datas = data.Split(',');
            int dataOne = int.Parse(datas[0].ToString());
            int dataTwo = int.Parse(datas[1].ToString());

            if ((dataOne == openDatas[1] && dataTwo == openDatas[2]) ||
                (dataTwo == openDatas[1] && dataOne == openDatas[2]))
            {
                return 1;
            }
            return 0;
        }
    }
}