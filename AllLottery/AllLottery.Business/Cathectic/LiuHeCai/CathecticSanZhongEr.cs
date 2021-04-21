using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Cathectic.LiuHeCai
{
    public class CathecticSanZhongEr : BaseCathecticValid49
    {
        protected override bool ExCheckError(string[] datas)
        {
            return datas.Length < 3;
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, 3, ",");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            var list = new List<int>();
            for (int i = 0; i < 6; i++)
            {
                list.Add(openDatas[i]);
            }
            var datas = data.Split(",");
            int[] t = new int[3];
            for (int i = 0; i < 3; i++)
            {
                t[i] = int.Parse(datas[i]);
            }

            int count = 0;
            if (list.Contains(t[0]))
            {
                count++;
            }
            if (list.Contains(t[1]))
            {
                count++;
            }
            if (list.Contains(t[2]))
            {
                count++;
            }
            if (count == 2)
            {
                return 8;
            }
            if (count == 3)
            {
                return 41;
            }
            return 0;
        }
    }
}