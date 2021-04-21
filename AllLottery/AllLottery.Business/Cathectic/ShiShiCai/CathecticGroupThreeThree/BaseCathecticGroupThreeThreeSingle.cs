using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeThree
{
    public abstract class BaseCathecticGroupThreeThreeSingle : BaseCathectic
    {
        protected abstract int Index { get; }

        protected virtual int Count => 2;

        public override bool Valid(string data)
        {
            foreach (var datas in data.Split("_"))
            {
                var dd = datas.Split(",");
                if (dd.Length != 3)
                {
                    return false;
                }
                List<int> list = new List<int>();
                foreach (var d in datas.Split(","))
                {
                    if (!int.TryParse(d, out var i))
                    {
                        return false;
                    }

                    if (i > 9 || i < 0)
                    {
                        return false;
                    }

                    if (!list.Contains(i))
                    {
                        list.Add(i);
                    }
                }

                if (list.Count != Count)
                {
                    return false;
                }
            }
            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            return datas.Split("_");
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            List<int> list1 = new List<int>();
            List<int> list2 = new List<int>();
            foreach (string s in data.Split(","))
            {
                list1.Add(int.Parse(s));
            }
            for (int i = Index; i < Index + 3; i++)
            {
                list2.Add(openDatas[i]);
            }

            list1.Sort();
            list2.Sort();
            for (int i = 0; i < 3; i++)
            {
                if (list1[i] != list2[i])
                {
                    return 0;
                }
            }
            return 1;
        }
    }
}