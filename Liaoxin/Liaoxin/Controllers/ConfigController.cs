using Liaoxin.Business.Config;
using Liaoxin.Cache;
using Liaoxin.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Zzb;
using Zzb.Mvc;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : LiaoxinBaseController
    {

        
        /// <summary>
        /// 获取配置表键值对 多个用 | 分隔
        /// </summary>
        /// <param name="configKeys"></param>
        /// <returns></returns>
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


        /// <summary>
        /// 获取枚举值
        /// </summary>
        /// <param name="enumName">枚举名称</param>
        /// <returns></returns>
        [HttpPost("GetEnums")]
        public ServiceResult GetEnums(string enumName)
        {
            var lis =  enumName.ToEnums();            
            return Json(() => new ServiceResult<List<CacheDetail>>() { Data = lis, ReturnCode = ServiceResultCode.Success }, "失败.");

        }

        /// <summary>
        /// 获取地区
        /// </summary>        
        /// <returns></returns>
        [HttpPost("GetAreas")]
        public ServiceResult GetAreas()
        {
            var lis =  CacheAreaEx.Areas;
            return Json(() => new ServiceResult<List<CacheArea>>() { Data = lis, ReturnCode = ServiceResultCode.Success }, "失败.");

        }


    }
}