﻿using System;

namespace Zzb.BaseData.StringHandle
{
    public class GuidNullHandle : BaseStringHandle
    {
        protected override object GetValue(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return Guid.Parse(value);

        }
    }
}