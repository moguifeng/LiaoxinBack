using AllLottery.Business.Config;
using AllLottery.Model;

namespace AllLottery.Business.Cathectic.LiuHeCai.Credit
{
    public class ShengXiao : BaseLiuHeCaiCredit
    {
        public ShengXiao(string helpKey) : base(helpKey)
        {
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            if (GetAnimalNumber(data).Contains(openDatas[Index]))
            {
                if (BaseConfig.HasValue(SystemConfigEnum.IsBoYue))
                {
                    if (data == ToyearShuXiang())
                    {
                        return 95000;
                    }
                    else
                    {
                        return 118800;
                    }
                }
                if (data==ToyearShuXiang())
                {
                    return 98000;
                }
                else
                {
                    return 122500;
                }
            }
            return 0;
        }

        public override string[] Values => Shuxiang;

        public override string NewKey => "生肖";

        public override decimal MaxBetMoney(string value)
        {
            return 5000;
        }
    }
}