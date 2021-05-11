using System;
using System.Collections.Generic;
using System.Text;

namespace Zzb.Common
{
    public enum VerificationCodeTypes
    {
        /// <summary>
        /// 登录
        /// </summary>
        Login = 0,
        /// <summary>
        /// 忘记密码
        /// </summary>
        ForgetPassword = 1,
        /// <summary>
        /// 设置资金密码
        /// </summary>
        SetTradePassword = 2,
        /// <summary>
        /// 设置登录密码
        /// </summary>
        SetPassword = 3,

        /// <summary>
        /// 更改手机号码
        /// </summary>
        ChangeTelephone = 4,

        /// <summary>
        ///注册用户
        /// </summary>
        RegisterClient = 5,

    }
}
