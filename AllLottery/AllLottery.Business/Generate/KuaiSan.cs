using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.Common;

namespace AllLottery.Business.Generate
{
    public class KuaiSan : BaseGenerate
    {
        public override string RandomGenerate()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < 3; i++)
            {
                list.Add(RandomHelper.Next(1, 7));
            }
            list.Sort();
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

                if (int.Parse(datas[0]) > int.Parse(datas[1]) || int.Parse(datas[1]) > int.Parse(datas[2]))
                {
                    return false;
                }

                foreach (string s in datas)
                {
                    if (int.Parse(s) > 6 || int.Parse(s) < 0)
                    {
                        return false;
                    }
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
            string dxdc = string.Empty;
            if (sum >= 11)
            {
                dxdc += "大";
            }
            else
            {
                dxdc += "小";
            }

            if (sum % 2 == 0)
            {
                dxdc += "双";
            }
            else
            {
                dxdc += "单";
            }
            return new { sum, dxdc };
        }
    }
}