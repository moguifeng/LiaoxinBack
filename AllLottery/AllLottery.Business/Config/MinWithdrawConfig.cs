﻿using AllLottery.Model;

namespace AllLottery.Business.Config
{
    public class MinWithdrawConfig : NumberCheckConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.MinWithdraw;

        protected override decimal Min => 0;

        public override string Default => "100";
    }
}