namespace AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticSingle
{
    /// <summary>
    /// 前二单式
    /// </summary>
    public class CathecticTwoFrontSingle : BaseCathecticSingle
    {
        protected override int Number => 5;
        protected override bool CheckTrue(string[] datas, int[] openDatas)
        {
            return openDatas[0] == int.Parse(datas[0]) && openDatas[1] == int.Parse(datas[1]);
        }
    }
}