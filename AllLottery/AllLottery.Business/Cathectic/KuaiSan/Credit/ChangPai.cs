using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Cathectic.KuaiSan.Credit
{
    public class ChangPai : BaseKuaiSanCredit
    {
        public override long IsWinEach(string data, int[] openDatas)
        {
            int one = int.Parse(data[0].ToString());
            int two = int.Parse(data[1].ToString());
            if (openDatas.Contains(one) && openDatas.Contains(two))
            {
                return 72000;
            }

            return 0;
        }

        public override string[] Values
        {
            get
            {
                List<string> list = new List<string>();
                for (int i = 1; i < 7; i++)
                {
                    for (int j = i + 1; j < 7; j++)
                    {
                        list.Add(i.ToString() + j);
                    }
                }

                return list.ToArray();
            }
        }

        public override string Key => "长牌";

        public override decimal MaxBetMoney(string value)
        {
            return 20000;
        }
    }
}