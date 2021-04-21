using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.LiuHeCai.CathecticWeiShu
{
    public abstract class BaseCathecticWeiShu : BaseCathectic
    {
        protected abstract int Index { get; }

        protected abstract int Bao0 { get; }

        protected abstract int BuBao0 { get; }

        public override bool Valid(string data)
        {
            var t = data.Split(",");
            List<string> list = new List<string>();
            foreach (string datas in t)
            {
                if (list.Contains(datas))
                {
                    return false;
                }
                if (datas[1] == '尾')
                {
                    if (!int.TryParse(datas[0].ToString(), out var i))
                    {
                        return false;
                    }

                    if (i < 0 || i > 9)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
                list.Add(datas);
            }
            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            return HandlerCombinationStringArray(datas, Index, ",");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            var datas = data.Split(",");
            List<int> numbers = new List<int>();
            foreach (string s in datas)
            {
                numbers.Add(int.Parse(s[0].ToString()));
            }
            List<bool> list = new List<bool>();
            for (int j = 0; j < numbers.Count; j++)
            {
                bool baohan = false;
                for (int i = 0; i < openDatas.Length; i++)
                {
                    baohan = baohan || openDatas[i] % 10 == numbers[j];
                }
                list.Add(baohan);
            }
            bool t = true;
            foreach (bool b in list)
            {
                t = t && b;
            }
            if (t)
            {
                if (numbers.Contains(0))
                {
                    return Bao0;
                }
                else
                {
                    return BuBao0;
                }
            }
            return 0;
        }
    }
}