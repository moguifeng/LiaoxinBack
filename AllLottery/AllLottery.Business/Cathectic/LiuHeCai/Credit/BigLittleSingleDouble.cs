using AllLottery.Business.Config;
using AllLottery.Model;

namespace AllLottery.Business.Cathectic.LiuHeCai.Credit
{
    public class BigLittleSingleDouble : BaseLiuHeCaiCredit
    {
        public BigLittleSingleDouble(string helpKey) : base(helpKey)
        {
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            long count = 20416;
            switch (data)
            {
                case "大":
                    if (openDatas[Index] >= 25 && openDatas[Index] < 49)
                    {
                        return count;
                    }
                    break;
                case "小":
                    if (openDatas[Index] <= 24)
                    {
                        return count;
                    }
                    break;
                case "单":
                    if (openDatas[Index] % 2 == 1 && openDatas[Index] < 49)
                    {
                        return count;
                    }
                    break;
                case "双":
                    if (openDatas[Index] % 2 == 0 && openDatas[Index] < 49)
                    {
                        return count;
                    }
                    break;
            }
            return 0;
        }

        public override string[] Values => new[] { "大", "小", "单", "双" };

        public override string NewKey => "大小单双";

        public override decimal MaxBetMoney(string value)
        {
            return 20000;
        }

        
    }
}