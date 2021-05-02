﻿using Liaoxin.Model;

namespace Liaoxin.Business.Config
{
    public class MaxRebateConfig : NumberCheckConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.MaxRebate;

        public override string Default => "0.1";
    }
}