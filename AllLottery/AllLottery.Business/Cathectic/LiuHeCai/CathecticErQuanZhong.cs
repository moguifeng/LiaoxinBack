using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Cathectic.LiuHeCai
{
    public class CathecticErQuanZhong : BaseCathecticValid49
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
            var list = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                list.Add(openDatas[i]);
            }

            var datas = data.Split(",");
            int[] t = new int[2];
            for (int i = 0; i < 2; i++)
            {
                t[i] = int.Parse(datas[i]);
            }

            if (list.Contains(t[0]) && list.Contains(t[1]))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}