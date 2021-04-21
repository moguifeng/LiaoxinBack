using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.PkShi.Credit
{
    public class FirstSecondSum : BaseCredit
    {
        public override long IsWinEach(string data, int[] openDatas)
        {
            var sum = openDatas[0] + openDatas[1];
            if (int.TryParse(data, out var val))
            {
                if (sum == val)
                {
                    return GetNumberSum(val);
                }
            }
            else
            {
                return GetShuangMian(data, sum);
            }

            return 0;
        }

        private long GetNumberSum(int sum)
        {
            switch (sum)
            {
                case 3:
                    return 450000;
                case 4:
                    return 450000;
                case 5:
                    return 225000;
                case 6:
                    return 225000;
                case 7:
                    return 150000;
                case 8:
                    return 150000;
                case 9:
                    return 112500;
                case 10:
                    return 112500;
                case 11:
                    return 90000;
                case 12:
                    return 112500;
                case 13:
                    return 112500;
                case 14:
                    return 150000;
                case 15:
                    return 150000;
                case 16:
                    return 225000;
                case 17:
                    return 225000;
                case 18:
                    return 450000;
                case 19:
                    return 450000;
            }
            return 0;
        }

        private long GetShuangMian(string data, int sum)
        {
            switch (data)
            {
                case "大":
                    if (sum > 11)
                    {
                        return 22500;
                    }
                    break;
                case "小":
                    if (sum <= 11)
                    {
                        return 18000;
                    }
                    break;
                case "单":
                    if (sum % 2 == 1)
                    {
                        return 18000;
                    }
                    break;
                case "双":
                    if (sum % 2 == 0)
                    {
                        return 22500;
                    }
                    break;
                case "大单":
                    if (sum > 11 && sum % 2 == 1)
                    {
                        return 45000;
                    }
                    break;
                case "大双":
                    if (sum > 11 && sum % 2 == 0)
                    {
                        return 45000;
                    }
                    break;
                case "小单":
                    if (sum <= 11 && sum % 2 == 1)
                    {
                        return 30000;
                    }
                    break;
                case "小双":
                    if (sum <= 11 && sum % 2 == 0)
                    {
                        return 45000;
                    }
                    break;
                default:
                    return 0;
            }
            return 0;
        }

        public override string[] Values
        {
            get
            {
                List<string> list = new List<string>() { "大", "小", "单", "双", "大单", "小单", "大双", "小双" };
                for (int i = 3; i < 20; i++)
                {
                    list.Add(i.ToString());
                }

                return list.ToArray();
            }
        }

        public override string Key => "冠亚军和";

        public override decimal MaxBetMoney(string value)
        {
            if (int.TryParse(value,out var number))
            {
                long sum = GetNumberSum(number);
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
            else
            {
                return 20000;
            }
          
        }
    }
}