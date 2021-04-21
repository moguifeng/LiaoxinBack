using AllLottery.IBusiness;
using Microsoft.AspNetCore.Http;
using Zzb.Common;
using Zzb.ZzbLog;

namespace AllLottery.Business
{
    public class ValidateCodeService : IValidateCodeService
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public byte[] CreateCode()
        {
            var input = SecurityCodeHelper.CreateRandomCode(4);
            var bytes = SecurityCodeHelper.CreateValidateGraphic(input);
            HttpContextAccessor.HttpContext.Session.SetString("code", input);
            return bytes;
        }

        public bool IsSameCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }
            var old = HttpContextAccessor.HttpContext.Session.GetString("code");
            HttpContextAccessor.HttpContext.Session.Remove("code");
            if (code.ToLower() != old?.ToLower())
            {
                LogHelper.Warning($"验证码错误，提交验证码[{code}]，正确验证码[{old?.ToLower()}]");
            }
            return code.ToLower() == old?.ToLower();
        }
    }
}