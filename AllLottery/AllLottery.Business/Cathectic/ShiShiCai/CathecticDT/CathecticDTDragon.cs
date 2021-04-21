namespace AllLottery.Business.Cathectic.ShiShiCai.CathecticDT
{
    /// <summary>
    /// 龙
    /// </summary>
    public class CathecticDTDragon : BaseCathecticDT
    {
        protected override bool Check(int firstIndex, int secondIndex)
        {
            if (firstIndex > secondIndex)
            {
                return true;
            }
            return false;
        }
    }
}