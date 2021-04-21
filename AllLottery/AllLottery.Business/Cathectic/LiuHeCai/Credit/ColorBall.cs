using AllLottery.Business.Config;
using AllLottery.Model;

namespace AllLottery.Business.Cathectic.LiuHeCai.Credit
{
    public class ColorBallL : BaseLiuHeCaiCredit
    {
        public ColorBallL(string helpKey) : base(helpKey)
        {
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            var color = data[0].ToString();
            if (GetColorNumber(color).Contains(openDatas[Index]))
            {
                if (BaseConfig.HasValue(SystemConfigEnum.IsBoYue))
                {
                    switch (data)
                    {
                        case "绿波":
                            return 29700;
                        case "蓝波":
                            return 29700;
                        case "红波":
                            return 27900;
                        default:
                            return 0;
                    }
                }
                switch (data)
                {
                    case "绿波":
                        return 32666;
                    case "蓝波":
                        return 30625;
                    case "红波":
                        return 28823;
                    default:
                        return 0;
                }
            }
            return 0;
        }

        public override string[] Values => new[] { "红波", "蓝波", "绿波" };

        public override string NewKey => "色波";

        public override decimal MaxBetMoney(string value)
        {
            return 20000;
        }
    }
}