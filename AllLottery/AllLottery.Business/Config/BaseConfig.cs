
using AllLottery.Model;
using AllLottery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Zzb;
using Zzb.Common;

namespace AllLottery.Business.Config
{
    public abstract class BaseConfig
    {
        private static Dictionary<SystemConfigEnum, BaseConfig> _cache = new Dictionary<SystemConfigEnum, BaseConfig>();

        public static BaseConfig CreateInstance(SystemConfigEnum type)
        {
            lock (_cache)
            {
                if (!_cache.ContainsKey(type))
                {
                    Assembly assembly = Assembly.Load("AllLottery.Business");
                    var instance =
                        assembly.CreateInstance($"AllLottery.Business.Config.{type.ToString()}Config") as BaseConfig;
                    if (instance == null)
                    {
                        throw new ZzbException($"反射对象失败[AllLottery.Business.Config.{type.ToString()}Config]");
                    }
                    _cache.Add(type, instance);
                }

                return _cache[type];
            }
        }

        public static bool HasValue(SystemConfigEnum type)
        {
            return !string.IsNullOrEmpty(CreateInstance(type).Value);
        }

        public static List<BaseConfig> CreateInstances(params SystemConfigEnum[] type)
        {
            if (type == null)
            {
                return null;
            }
            List<BaseConfig> list = new List<BaseConfig>();
            foreach (SystemConfigEnum e in type)
            {
                list.Add(CreateInstance(e));
            }
            return list;
        }

        public abstract SystemConfigEnum Type { get; }

        public string Value => GetValue(Type);

        public virtual string Name => Type.ToDescriptionString();

        public virtual string Default => null;

        private string GetValue(SystemConfigEnum type)
        {
            using (var context = LotteryContext.CreateContext())
            {
                var config = (from d in context.SystemConfigs where d.Type == type select d)
                    .FirstOrDefault();
                if (config == null)
                {
                    return Default;
                }
                return config.Value;
            }

        }

        public void Save(string value, LotteryContext context)
        {
            if (!CheckValue(value))
            {
                throw new ZzbException($"[{Name}]的值不能为[{value}]，请重新输入");
            }

            var config = (from c in context.SystemConfigs where c.Type == Type select c)
                .FirstOrDefault();
            if (config == null)
            {
                config = new SystemConfig(Type, value);
                context.SystemConfigs.Add(config);
            }
            else
            {
                config.Value = value;
            }
        }

        public static void Save(ConfigSaveViewModel[] datas, int userId)
        {
            if (datas == null)
            {
                return;
            }

            using (var context = LotteryContext.CreateContext())
            {
                StringBuilder sb = new StringBuilder();
                foreach (ConfigSaveViewModel data in datas)
                {
                    if (!Enum.TryParse(data.Id, out SystemConfigEnum type))
                    {
                        throw new ZzbException("转换系统配置枚举失败");
                    }
                    var ins = CreateInstance(type);
                    ins.Save(data.Value, context);
                    sb.Append($",[{type.ToDescriptionString()}]设置为[{data.Value}]");
                }

                context.UserOperateLogs.Add(new UserOperateLog($"修改系统配置{sb}", userId));
                context.SaveChanges();
            }
        }

        protected virtual bool CheckValue(string value)
        {
            return true;
        }

        public decimal DecimalValue
        {
            get
            {
                if (decimal.TryParse(Value, out var d))
                {
                    return d;
                }
                return 0;
            }
        }

        public DateTime DateTimeValue
        {
            get
            {
                if (DateTime.TryParse(Value, out var d))
                {
                    return d;
                }
                return DateTime.MinValue;
            }
        }
    }
}