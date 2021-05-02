﻿using Liaoxin.Model;

namespace Liaoxin.Business.Config
{
    public class DailyWageTimeConfig : BaseConfig
    {
        public override SystemConfigEnum Type => SystemConfigEnum.DailyWageTime;

        public override string Default => "2019-3-5";
    }
}