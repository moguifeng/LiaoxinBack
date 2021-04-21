using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.PkShi.Credit
{
    public abstract class BasePkCredit : BaseCredit
    {
        public abstract int Index { get; }

        public override string[] Values
        {
            get
            {
                List<string> list = new List<string>() { "大", "小", "单", "双" };
                for (int i = 1; i < 11; i++)
                {
                    list.Add(i.ToString());
                }
                return list.ToArray();
            }
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (int.TryParse(data, out var number))
            {
                if (openDatas[Index] == number)
                {
                    return 10;
                }
            }
            else
            {
                switch (data)
                {
                    case "大":
                        if (openDatas[Index] > 5)
                        {
                            return 2;
                        }
                        break;
                    case "小":
                        if (openDatas[Index] <= 5)
                        {
                            return 2;
                        }
                        break;
                    case "单":
                        if (openDatas[Index] % 2 == 1)
                        {
                            return 2;
                        }
                        break;
                    case "双":
                        if (openDatas[Index] % 2 == 0)
                        {
                            return 2;
                        }
                        break;
                    default:
                        break;
                }
            }
            return 0;
        }

        public override decimal MaxBetMoney(string value)
        {
            if (int.TryParse(value,out _))
            {
                return 10000;
            }
            else
            {
                return 20000;
            }
        }
    }
}