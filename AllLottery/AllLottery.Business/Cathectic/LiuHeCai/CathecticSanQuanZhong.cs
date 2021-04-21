using System;
using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Cathectic.LiuHeCai
{
    public class CathecticSanQuanZhong : BaseCathecticValid49
    {
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

            if (list.Contains(t[0]) && list.Contains(t[1]) && list.Contains(t[2]))
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        protected override bool ExCheckError(string[] datas)
        {
            return datas.Length < 3;
        }
    }
}