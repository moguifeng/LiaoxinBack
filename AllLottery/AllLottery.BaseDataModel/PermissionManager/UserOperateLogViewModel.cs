using AllLottery.Model;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;

namespace AllLottery.BaseDataModel.PermissionManager
{
    public class UserOperateLogViewModel : BaseServiceNav
    {
        public override string NavName => "管理员操作记录";

        public override string FolderName => "后台管理";

        [NavField("Id", IsKey = true, IsDisplay = false)]
        public int UserOperateLogId { get; set; }

        [NavField("用户")]
        public string UserName { get; set; }

        [NavField("操作记录", 1000)]
        public string Message { get; set; }

        [NavField("操作时间", 200)]
        public DateTime CreateTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<UserOperateLog, UserOperateLogViewModel>(
                from u in Context.UserOperateLogs where u.IsEnable orderby u.CreateTime descending select u,
                (k, t) => { t.UserName = k.UserInfo.Name; }, (k, w) => w.Where(t => t.UserInfo.Name.Contains(k)), (k, w) => w.Where(t => t.Message.Contains(k)));
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new[] { new TextFieldAttribute("Name", "用户"), new TextFieldAttribute("Log", "操作记录"), };
        }
    }
}