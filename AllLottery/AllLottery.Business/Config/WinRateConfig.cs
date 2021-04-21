using AllLottery.Model;

namespace AllLottery.Business.Config
{
    public class WinRateConfig : BaseConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.WinRate;

        public override string Default => "0.1";

        public override string Name => "系统彩盈利比例(设置为0则代表完全随机)";
    }
}