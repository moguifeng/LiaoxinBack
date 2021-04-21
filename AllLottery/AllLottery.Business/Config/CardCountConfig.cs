using AllLottery.Model;

namespace AllLottery.Business.Config
{
    public class CardCountConfig : BaseConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.CardCount;

        public override string Default => "5";
    }
}