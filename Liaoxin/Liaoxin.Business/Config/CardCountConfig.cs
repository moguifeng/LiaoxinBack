using Liaoxin.Model;

namespace Liaoxin.Business.Config
{
    public class CardCountConfig : BaseConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.CardCount;

        public override string Default => "5";
    }
}