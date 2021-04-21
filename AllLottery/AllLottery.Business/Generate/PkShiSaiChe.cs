using System;
using System.Collections.Generic;
using System.Text;
using Zzb.Common;

namespace AllLottery.Business.Generate
{
    public class PkShiSaiChe : BaseGenerate
    {
        public override string RandomGenerate()
        {
            List<string> list = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                list.Add(i.ToString().PadLeft(2, '0'));
            }
            StringBuilder sb = new StringBuilder();
            while (list.Count > 0)
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
                if (datas.Length != 10)
                {
                    return false;
                }
                List<int> l = new List<int>();
                foreach (string s in datas)
                {
                    int i = int.Parse(s);
                    if (s.Length != 2 || i > 10 || i < 1 || l.Contains(i))
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
            int sum = numbers[0] + numbers[1];
            string dx = sum > 11 ? "大" : "小";
            string ds = sum % 2 == 0 ? "双" : "单";
            List<string> longhu = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                if (numbers[i] > numbers[numbers.Length - 1 - i])
                {
                    longhu.Add("龙");
                }
                else
                {
                    longhu.Add("虎");
                }
            }
            return new { sum, dx, ds, longhu };
        }
    }
}