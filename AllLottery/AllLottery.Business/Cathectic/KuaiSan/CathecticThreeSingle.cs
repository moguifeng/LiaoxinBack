using System.Linq;

namespace AllLottery.Business.Cathectic.KuaiSan
{
    /// <summary>
    /// 三同号单选
    /// </summary>
    public class CathecticThreeSingle : BaseCathectic
    {
        protected string[] SummaryData = { "111", "222", "333", "444", "555", "666" };

        public override bool Valid(string datas)
        {
            return ThreeValid(datas);
        }

        public override string[] SplitDatas(string datas)
        {
            return SplitByDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            string openDataStr = string.Join(string.Empty, openDatas);
            if (data == openDataStr)
            {
                return 1;
            }
            return 0;
        }

        protected bool ThreeValid(string datas)
        {
            string[] dataNums = SplitByDatas(datas);

            foreach (var dataNum in dataNums)
            {
                if (!SummaryData.Contains(dataNum))
                {
                    return false;
                }
            }
            return (
                CheckAllNumber(datas) &&
                CheckChoiceOfMaxMinRange(datas, 1, 6) &&
                CheckEachChoiceCount(datas, 3)) ? true : false;
        }
    }
}