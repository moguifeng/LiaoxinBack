using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.Common;

namespace AllLottery.Business.Generate
{
    public class ShiYiXuanWu : BaseGenerate
    {
        public override string RandomGenerate()
        {
            List<string> list = new List<string>();
            for (int i = 1; i <= 11; i++)
            {
                list.Add(i.ToString().PadLeft(2, '0'));
            }
            StringBuilder sb = new StringBuilder();
            while (list.Count > 6)
            {
                int i = RandomHelper.Next(list.Count);
                sb.Append(list[i] + ",");
                list.RemoveAt(i);
            }
            return sb.ToString().Trim(',');
        }

        public override bool CheckDatas(string data)
        {
            try
            {
                var datas = data.Split(',');
                if (datas.Length != 5)
                {
                    return false;
                }
                List<int> l = new List<int>();
                foreach (string s in datas)
                {
                    int i = int.Parse(s);
                    if (s.Length != 2 || i > 11 || i < 1 || l.Contains(i))
                    {
                        return false;
                    }
                    l.Add(i);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public override object CreatTrend(int[] numbers)
        {
            return new { sum = numbers.Sum() };
        }
    }
}