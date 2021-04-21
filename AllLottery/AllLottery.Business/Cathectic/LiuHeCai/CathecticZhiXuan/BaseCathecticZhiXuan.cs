using System;
using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.LiuHeCai.CathecticZhiXuan
{
    public abstract class BaseCathecticZhiXuan : BaseCathecticValid49
    {
        public abstract int Index { get; }

        public override string[] SplitDatas(string datas)
        {
            return datas.Split(',');
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (int.Parse(data) == openDatas[Index])
            {
                return 1;
            }
            return 0;
        }
    }
}