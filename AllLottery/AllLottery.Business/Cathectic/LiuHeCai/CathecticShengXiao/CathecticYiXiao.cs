namespace AllLottery.Business.Cathectic.LiuHeCai.CathecticShengXiao
{
    public class CathecticYiXiao : BaseCathecticShengXiao
    {
        public override long IsWinEach(string data, int[] openDatas)
        {
            var list = GetAnimalNumber(data);
            foreach (int i in openDatas)
            {
                if (list.Contains(i))
                {
                    if (data == ToyearShuXiang())
                    {
                        return 1805;
                    }
                    else
                    {
                        return 2119;
                    }
                }
            }
            return 0;
        }
    }
}