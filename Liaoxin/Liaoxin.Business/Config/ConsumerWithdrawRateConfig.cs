using Liaoxin.Model;

namespace Liaoxin.Business.Config
{
    public class ConsumerWithdrawRateConfig : NumberCheckConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.ConsumerWithdrawRate;

        public override string Name => "提现消费比例限制(单位:%)";

        protected override decimal Min => 0;

        public override string Default => "50";
    }
}