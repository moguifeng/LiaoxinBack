using System;
using System.Collections.Generic;
using System.Text;
using Zzb.Common;

namespace AllLottery.Business.Generate
{
    public class LiuHeCai : BaseGenerate
    {
        public override string RandomGenerate()
        {
            List<string> list = new List<string>();
            for (int i = 1; i <= 49; i++)
            {
                list.Add(i.ToString());
            }
            StringBuilder sb = new StringBuilder();
            while (list.Count > 42)
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
                if (datas.Length != 7)
                {
                    return false;
                }
                List<int> l = new List<int>();
                foreach (string s in datas)
                {
                    int i = int.Parse(s);
                    if (i > 49 || i < 1 || l.Contains(i))
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
            return null;
        }
    }
}