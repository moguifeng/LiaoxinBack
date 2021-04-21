using AllLottery.Model;
using System.Linq;
using Zzb;

namespace AllLottery.Business.Cathectic.ShiShiCai.XinYongPan
{
    public abstract class BaseQiu : BaseIndexOpenData
    {
        public override string[] Values => new[] { "大", "小", "单", "双", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (int.TryParse(data, out var number))
            {
                if (openDatas[Index] == number)
                {
                    return 100000;
                }
            }
            else
            {
                switch (data)
                {
                    case "大":
                        if (openDatas[Index] > 4)
                        {
                            return 20000;
                        }
                        break;
                    case "小":
                        if (openDatas[Index] < 5)
                        {
                            return 20000;
                        }
                        break;
                    case "单":
                        if (openDatas[Index] % 2 == 1)
                        {
                            return 20000;
                        }
                        break;
                    case "双":
                        if (openDatas[Index] % 2 == 0)
                        {
                            return 20000;
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
            if (int.TryParse(value, out _))
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