using Liaoxin.Business.Config;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.Mvc;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SystemConfigController : BaseApiController
    {
        [HttpPost("GetSystemConfigSettings")]
        public ServiceResult GetSystemConfigSettings()
        {
            return ObjectResult(new SystemConfigSettingsGroup[]
            {
                new SystemConfigSettingsGroup()
                {
                    Name = "提款设置",
                    Items = CreateSettingGroupItems(SystemConfigEnum.MinWithdraw, SystemConfigEnum.MaxWithdraw, SystemConfigEnum.ConsumerWithdrawRate, SystemConfigEnum.WithdrawCount,SystemConfigEnum.WithdrawBegin,SystemConfigEnum.WithdrawEnd)
                },
                new SystemConfigSettingsGroup()
                {
                    Name = "网站设置",
                    Items = CreateSettingGroupItems(SystemConfigEnum.WebTitle,SystemConfigEnum.CustomerServiceLink)
                },
                new SystemConfigSettingsGroup()
                {
                    Name = "注册链接",
                    Items = CreateSettingGroupItems(SystemConfigEnum.RegisterNumber)
                },
                new SystemConfigSettingsGroup()
                {
                    Name = "BI设置",
                    Items = CreateSettingGroupItems(SystemConfigEnum.SiteNo,SystemConfigEnum.SiteKey)
                },
            });
        }

        private SystemConfigItem[] CreateSettingGroupItems(params SystemConfigEnum[] type)
        {
            return (from i in BaseConfig.CreateInstances(type)
                    select new SystemConfigItem()
                    {
                        Title = i.Name,
                        Name = i.Type.ToString(),
                        Value = i.Value
                    }).ToArray();
        }

        [HttpPost("Save")]
        public ServiceResult Save(Dictionary<string, string> datas)
        {
            return Json(() =>
            {
                List<ConfigSaveViewModel> list = new List<ConfigSaveViewModel>();
                foreach (var data in datas)
                {
                    if (!string.IsNullOrEmpty(data.Key))
                    {
                        list.Add(new ConfigSaveViewModel() { Id = data.Key, Value = data.Value });
                    }
                }
                BaseConfig.Save(list.ToArray(), UserId);
                return new ServiceResult(ServiceResultCode.Success);
            }, "保存失败");
        }

        [HttpPost("GetImageSetting")]
        public ServiceResult GetImageSetting()
        {
            return JsonObjectResult((from i in BaseConfig.CreateInstances(SystemConfigEnum.Logo, SystemConfigEnum.AppCode)
                                     select new SystemConfigItem
                                     {
                                         Title = i.Name,
                                         Name = i.Type.ToString(),
                                         Value = i.Value
                                     }), "获取图片设置失败");
        }
    }

    public class SystemConfigSettingsGroup
    {
        public string Name { get; set; }

        public SystemConfigItem[] Items { get; set; }
    }


    public class SystemConfigItem
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Value { get; set; }
    }
}