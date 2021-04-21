using System.Collections.Generic;
using System.Linq;

namespace AllLottery.Business.Cathectic.KuaiSan.Credit
{
    public class DianShu : BaseKuaiSanCredit
    {
        public override long IsWinEach(string data, int[] openDatas)
        {
            var sum = openDatas.Sum();
            if (int.Parse(data) == sum)
            {
                return GetNumberCount(sum);
            }
            return 0;
        }

        private long GetNumberCount(int sum)
        {
            switch (sum)
            {
                case 3:
                    return 2160000;
                case 4:
                    return 720000;
                case 5:
                    return 360000;
                case 6:
                    return 216000;
                case 7:
                    return 144000;
                case 8:
                    return 102857;
                case 9:
                    return 86400;
                case 10:
                    return 80000;
                case 11:
                    return 80000;
                case 12:
                    return 86400;
                case 13:
                    return 102857;
                case 14:
                    return 144000;
                case 15:
                    return 216000;
                case 16:
                    return 360000;
                case 17:
                    return 720000;
                case 18:
                    return 2160000;
                default:
                    return 0;
            }
        }

        public override string[] Values
        {
            get
            {
                List<string> list = new List<string>();
                for (int i = 3; i < 19; i++)
                {
                    list.Add(i.ToString());
                }

                return list.ToArray();
            }
        }

        public override string Key => "点数";

        public override decimal MaxBetMoney(string value)
        {
            var number = int.Parse(value);
            long sum = GetNumberCount(number);
            if (sum > 1500000)
            {
                return 100;
            }
            if (sum > 800000)
            {
                return 200;
            }
            if (sum > 450000)
            {
                return 1000;
            }
            if (sum > 230000)
            {
                return 3000;
            }
            if (sum >= 100000)
            {
                return 5000;
            }
            return 20000;
        }
    }
}