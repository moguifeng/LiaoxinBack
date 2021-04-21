using System.Linq;

namespace AllLottery.Business.Cathectic.KuaiSan.Credit
{
    public class DaXiaoDanShuang : BaseKuaiSanCredit
    {
        public override long IsWinEach(string data, int[] openDatas)
        {
            var sum = openDatas.Sum();
            switch (data)
            {
                case "小":
                    if (sum >= 3 && sum <= 10)
                    {
                        return 20000;
                    }
                    break;
                case "大":
                    if (sum >= 11 && sum <= 18)
                    {
                        return 20000;
                    }
                    break;
                case "单":
                    if (sum % 2 == 1)
                    {
                        return 20000;
                    }
                    break;
                case "双":
                    if (sum % 2 == 0)
                    {
                        return 20000;
                    }
                    break;
                default:
                    break;
            }

            return 0;
        }

        public override string[] Values => new[] { "大", "小", "单", "双" };

        public override string Key => "大小单双";
    }
}