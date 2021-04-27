using Liaoxin.Model;

namespace Liaoxin.Business.Config
{
    public class WithdrawCountConfig : NumberCheckConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.WithdrawCount;

        public override string Name => "每日提现次数限制";

        protected override decimal Min => 0;

        public override string Default => "5";
    }
}