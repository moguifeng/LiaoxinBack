using Liaoxin.IBusiness;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model;
using Zzb.BaseData.Model.Button;
using Zzb.EF;

namespace Liaoxin.BaseDataModel.PermissionManager
{
    public class UserInfoViewModel : BaseServiceNav
    {
        public override string NavName => "用户管理";

        public override string FolderName => "后台管理";

        public override int OperaColumnWidth => 250;

        public IUserOperateLogService UserOperateLogService { get; set; }

        [NavField("主键", IsDisplay = false, IsKey = true)]
        public int UserInfoId { get; set; }

        [NavField("用户")]
        public string Name { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<UserInfo, UserInfoViewModel>(from r in Context.UserInfos where r.IsEnable select r);
        }

        public override bool ShowOperaColumn => true;

        public override BaseButton[] CreateRowButtons()
        {
            List<BaseButton> list = new List<BaseButton>() { new UserRoleEditViewModal("EditRole", "编辑角色"), new UserPermissionEditViewModal("EditPermission", "编辑权限") };
            if (UserInfoId.ToString() != HttpContextAccessor.HttpContext.User.Identity.Name)
            {
                list.Add(new ConfirmActionButton("Delete", "删除", "是否确定删除？"));
                list.Add(new UserInfoEditModal("Edit", "修改密码"));
            }
            return list.ToArray();
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new[] { new UserInfoAddModal("Add", "新增"), };
        }

        public void Delete()
        {
            var exist = (from u in Context.UserInfos where u.UserInfoId == UserInfoId select u).First();
            exist.IsEnable = false;
            UserOperateLogService.Log($"删除用户[{exist.Name}]", Context);
            Context.SaveChanges();
        }
    }
}