using System.ComponentModel;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class SystemConfig : BaseModel
    {
        public SystemConfig()
        {
        }

        public SystemConfig(SystemConfigEnum type, string value)
        {
            Type = type;
            Value = value;
        }

        public int SystemConfigId { get; set; }
        /// <summary>
        /// 配置类型
        /// </summary>
        [ZzbIndex(IsUnique = true)]
        public SystemConfigEnum Type { get; set; }
        /// <summary>
        /// 配置值
        /// </summary>
        public string Value { get; set; }
    }

    public enum SystemConfigEnum
    {
        [Description("最小提现")]
        MinWithdraw = 0,

        [Description("最大提现")]
        MaxWithdraw = 1,

        [Description("提现次数")]
        WithdrawCount = 2,

        [Description("提现消费比例限制")]
        ConsumerWithdrawRate = 3,

        [Description("提款结束时间(时)")]
        WithdrawEnd = 4,

        [Description("提款开始时间(时)")]
        WithdrawBegin = 5,

      


        [Description("系统LOGO")]
        Logo = 9,

        [Description("网站名称")]
        WebTitle = 10,

    
        [Description("APP下载二维码")]
        AppCode = 12,

        [Description("客服链接")]
        CustomerServiceLink = 13,

      

 

        [Description("取消超级密码")]
        CancleSuperLoginPassword = 19,

        [Description("是否自己平台")]
        IsZiji = 20,

        [Description("后台标题")]
        BackWebTitle = 21,


    }
}