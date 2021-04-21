using AllLottery.Model;
using System.Linq;

namespace AllLottery.Business.Cathectic.ShiShiCai.XinYongPan
{
    public class ZongHe : BaseSscCredit
    {
        public override long IsWinEach(string data, int[] openDatas)
        {
            var sum = openDatas.Sum();
            switch (data)
            {
                case "总单":
                    if (sum % 2 == 1)
                    {
                        return 20000;
                    }
                    break;
                case "总双":
                    if (sum % 2 == 0)
                    {
                        return 20000;
                    }
                    break;
                case "总大":
                    if (sum >= 23)
                    {
                        return 20000;
                    }
                    break;
                case "总小":
                    if (sum <= 22)
                    {
                        return 20000;
                    }
                    break;
                case "龙":
                    if (openDatas[0] == openDatas[4])
                    {
                        return 10000;
                    }
                    if (openDatas[0] > openDatas[4])
                    {
                        return 20000;
                    }
                    break;
                case "虎":
                    if (openDatas[0] == openDatas[4])
                    {
                        return 10000;
                    }
                    if (openDatas[0] < openDatas[4])
                    {
                        return 20000;
                    }
                    break;
                case "和":
                    if (openDatas[0] == openDatas[4])
                    {
                        return 100000;
                    }
                    break;
                default:
                    break;
            }

            return 0;
        }

        public override string[] Values => new[] { "总单", "总双", "总大", "总小", "龙", "虎", "和" };

        public override string Key => "总和";

        public override decimal MaxBetMoney(string value)
        {
            if (value == "和")
            {
                return 5000;
            }
            return 20000;
        }

        public override decimal CalculateWinMoney(Bet bet, decimal maxRate, int[] openDatas)
        {
            if (bet.WinBetCount == 10000)
            {
                return bet.BetMoney;
            }
            return base.CalculateWinMoney(bet, maxRate, openDatas);
        }
        
    }
}