namespace AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticSingle
{
    /// <summary>
    /// 后三单式
    /// </summary>
    public class CathecticThreeBehindSingle : BaseCathecticSingle
    {
        protected override int Number => 8;
        protected override bool CheckTrue(string[] datas, int[] openDatas)
        {
            return openDatas[openDatas.Length - 3] == int.Parse(datas[0]) &&
                   openDatas[openDatas.Length - 2] == int.Parse(datas[1]) &&
                   openDatas[openDatas.Length - 1] == int.Parse(datas[2]);
        }
    }
}