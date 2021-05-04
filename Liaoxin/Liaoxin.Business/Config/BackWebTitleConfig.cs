using Liaoxin.Model;

namespace Liaoxin.Business.Config
{
    public class BackWebTitleConfig : BaseConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.BackWebTitle;

        public override string Default => "聊信";
    }
}