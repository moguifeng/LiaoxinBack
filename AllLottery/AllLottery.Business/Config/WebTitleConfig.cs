using AllLottery.Model;

namespace AllLottery.Business.Config
{
    public class WebTitleConfig : BaseConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.WebTitle;

        public override string Default => "雅福彩票";
    }
}