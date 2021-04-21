using AllLottery.Model;

namespace AllLottery.Business.Config
{
    public class BackWebTitleConfig : BaseConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.BackWebTitle;

        public override string Default => "雅福";
    }
}