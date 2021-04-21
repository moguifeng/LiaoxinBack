using System;

namespace AllLottery.Business.Cathectic.KuaiSan
{
    /// <summary>
    /// 三同号通选
    /// </summary>
    public class CathecticThreeTogether : BaseCathectic
    {
        public override bool Valid(string datas)
        {
            return datas == "通选";
        }

        public override string[] SplitDatas(string datas)
        {
            return new[] { datas };
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (openDatas[0] == openDatas[1] && openDatas[0] == openDatas[2])
            {
                return 1;
            }

            return 0;
        }
    }
}