namespace AllLottery.Business.Cathectic.ShiShiCai.CathecticDT
{
    /// <summary>
    /// 和
    /// </summary>
    public class CathecticDTSum : BaseCathecticDT
    {
        protected override bool Check(int firstIndex, int secondIndex)
        {
            if (firstIndex == secondIndex)
            {
                return true;
            }
            return false;
        }
    }
}