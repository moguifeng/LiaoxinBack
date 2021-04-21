using AllLottery.Model;

namespace AllLottery.Business.Config
{
    public class WithdrawEndConfig : BaseConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.WithdrawEnd;

        public override string Default => "22";

        public override string Name => "结束时间(小时)";

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