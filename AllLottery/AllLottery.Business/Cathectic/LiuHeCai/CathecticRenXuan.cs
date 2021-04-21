namespace AllLottery.Business.Cathectic.LiuHeCai
{
    public class CathecticRenXuan : BaseCathecticValid49
    {
        public override string[] SplitDatas(string datas)
        {
            return datas.Split(',');
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            var number = int.Parse(data);
            for (int i = 0; i < 6; i++)
            {
                if (number == openDatas[i])
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}