namespace AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticSingle
{
    /// <summary>
    /// 前三单式
    /// </summary>
    public class CathecticThreeFrontSingle : BaseCathecticSingle
    {
        protected override int Number => 8;
        protected override bool CheckTrue(string[] datas, int[] openDatas)
        {
            return openDatas[0] == int.Parse(datas[0]) &&
                   openDatas[1] == int.Parse(datas[1]) &&
                   openDatas[2] == int.Parse(datas[2]);
        }
    }
}