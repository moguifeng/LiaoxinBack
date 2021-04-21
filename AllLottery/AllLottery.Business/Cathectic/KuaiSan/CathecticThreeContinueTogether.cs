namespace AllLottery.Business.Cathectic.KuaiSan
{
    public class CathecticThreeContinueTogether : BaseCathectic
    {
        public override int Cathectic(string datas)
        {
            return 1;
        }

        private readonly string[] _summaryNum = new string[] { "123", "234", "345", "456" };

        public override bool Valid(string datas)
        {
            return datas == "通选";
        }

        public override string[] SplitDatas(string datas)
        {
            return _summaryNum;
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (string.Join(string.Empty, openDatas) == data)
            {
                return 1;
            }
            return 0;
        }
    }
}