using System.Linq;

namespace AllLottery.Business.Cathectic.LiuHeCai
{
    public class CathecticTeChuan : BaseCathecticValid49
    {
        protected override bool ExCheckError(string[] datas)
        {
            return datas.Length < 2;
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 2, ",");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            var datas = data.Split(",");
            int[] t = new int[2];
            for (int i = 0; i < 2; i++)
            {
                t[i] = int.Parse(datas[i]);
            }

            if (openDatas.Contains(t[0]) && openDatas.Contains(t[1]) && (openDatas[6] == t[0] || openDatas[6] == t[1]))
            {
                return 1;
            }
            return 0;
        }
    }
}