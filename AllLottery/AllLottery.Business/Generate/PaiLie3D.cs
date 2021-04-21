using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.Common;

namespace AllLottery.Business.Generate
{
    public class PaiLie3D : BaseGenerate
    {
        public override string RandomGenerate()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 3; i++)
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
                if (datas.Length != 3)
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
            string bai = CreateOneStatus(numbers[0]), shi = CreateOneStatus(numbers[1]), ge = CreateOneStatus(numbers[2]);
            string status = CreateAllStatus(numbers);
            return new { sum, bai, shi, ge, status };
        }

        private string CreateOneStatus(int number)
        {
            string one = string.Empty;
            if (number > 4)
            {
                one += "大";
            }
            else
            {
                one += "小";
            }
            if (number % 2 == 0)
            {
                one += "双";
            }
            else
            {
                one += "单";
            }

            return one;
        }

        private string CreateAllStatus(int[] numbers)
        {
            int i = 0;
            if (numbers[0] == numbers[1])
            {
                i++;
            }
            if (numbers[0] == numbers[2])
            {
                i++;
            }
            if (numbers[2] == numbers[1])
            {
                i++;
            }

            if (i == 0)
            {
                return "组六";
            }

            if (i == 3)
            {
                return "豹子";
            }

            if (i == 1)
            {
                return "组三";
            }

            return "未知";
        }
    }
}
