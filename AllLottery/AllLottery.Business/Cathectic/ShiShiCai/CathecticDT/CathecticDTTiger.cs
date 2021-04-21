namespace AllLottery.Business.Cathectic.ShiShiCai.CathecticDT
{
    /// <summary>
    /// 虎
    /// </summary>
    public class CathecticDTTiger:BaseCathecticDT
    {
        protected override bool Check(int firstIndex, int secondIndex)
        {
            if (firstIndex < secondIndex)
            {
                return true;
            }
            return false;
        }
    }
}