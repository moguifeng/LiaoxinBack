using AllLottery.Model;

namespace AllLottery.Business.Config
{
    public class WithdrawBeginConfig : BaseConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.WithdrawBegin;

        public override string Default => "9";

        public override string Name => "开始时间(小时)";

        protected override bool CheckValue(string value)
        {
            if (!int.TryParse(value, out var i))
            {
                return false;
            }

            if (i < 0 || i > 24)
            {
                return false;
            }

            return true;
        }
    }
}