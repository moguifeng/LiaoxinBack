namespace AllLottery.Business.Cathectic.LiuHeCai.CathecticShengXiao
{
    public class CathecticTeXiao : BaseCathecticShengXiao
    {
        public override long IsWinEach(string data, int[] openDatas)
        {
            if (GetAnimalNumber(data).Contains(openDatas[6]))
            {
                if (data == ToyearShuXiang())
                {
                    return 4;
                }
                else
                {
                    return 5;
                }
            }
            return 0;
        }
    }
}