namespace AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticSingle
{
    /// <summary>
    /// 后二单式
    /// </summary>
    public class CathecticTwoBehindSingle : BaseCathecticSingle
    {
        protected override int Number => 5;
        protected override bool CheckTrue(string[] datas, int[] openDatas)
        {
            return openDatas[openDatas.Length - 2] == int.Parse(datas[0]) &&
                   openDatas[openDatas.Length - 1] == int.Parse(datas[1]);
        }
    }
}