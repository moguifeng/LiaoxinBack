using Liaoxin.Model;

namespace Liaoxin.Business.Config
{
    public class MaxWithdrawConfig : NumberCheckConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.MaxWithdraw;

        protected override decimal Min => 0;

        public override string Default => "1000000";
    }
}