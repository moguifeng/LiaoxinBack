namespace AllLottery.Business.Cathectic.PaiLie3D
{
    public class CathecticFixedBileThreeStart : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return ValidFixBiles(datas, 3, 1, 10, 1);
        }

        public override string[] SplitDatas(string datas)
        {
            return CathecticFixBiles(datas).ToArray();
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            return CatheticFixBilesIsWin(data, openDatas) ? 1 : 0;
        }
    }
}