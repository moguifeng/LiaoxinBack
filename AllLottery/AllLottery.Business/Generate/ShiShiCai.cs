using AllLottery.Business.Cathectic.ShiShiCai.XinYongPan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.Common;

namespace AllLottery.Business.Generate
{
    public class ShiShiCai : BaseGenerate
    {

        public override string RandomGenerate()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(RandomHelper.Next(10));
            }
            StringBuilder sb = new StringBuilder();
            foreach (int t in list)
            {
                sb.Append(t + ",");
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
                    if (s.Length != 1 || i > 9 || i < 0)
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
            int sum = numbers.Sum();
            string dx = sum >= 23 ? "大" : "小";
            string ds = sum % 2 == 0 ? "双" : "单";
            string longhu = numbers[0] > numbers[4] ? "龙" : numbers[0] == numbers[4] ? "和" : "虎";
            List<string> special = new List<string>() { BaseSpecial.WinningValue(numbers, 0), BaseSpecial.WinningValue(numbers, 1), BaseSpecial.WinningValue(numbers, 2) };
            return new { sum, dx, ds, longhu, special };
        }
    }
}