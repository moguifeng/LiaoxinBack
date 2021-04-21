using AllLottery.Business.Config;
using AllLottery.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Zzb;
using Zzb.Mvc;

namespace AllLottery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : BaseApiController
    {
        [HttpPost("GetConfigValues")]
        public ServiceResult GetConfigValues(string configKeys)
        {
            if (string.IsNullOrEmpty(configKeys))
            {
                return new ServiceResult() { Message = "你想干嘛?.", ReturnCode = ServiceResultCode.Error };
            }
            string[] setKeys = configKeys.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> dic = new Dictionary<string, string>();

            foreach (string keyStr in setKeys)
            {
                if (!Enum.TryParse(keyStr, out SystemConfigEnum type))
                {
                    return new ServiceResult(ServiceResultCode.Error, "传达的参数错误");
                }
                var val = BaseConfig.CreateInstance(type).Value;
                dic.Add(keyStr, val);
            }
            return Json(() => new ServiceResult<Dictionary<string, string>>() { Data = dic, ReturnCode = ServiceResultCode.Success }, "失败.");
        }
    }
}