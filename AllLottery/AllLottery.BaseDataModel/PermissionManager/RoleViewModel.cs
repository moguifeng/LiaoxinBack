using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model;
using Zzb.BaseData.Model.Button;
using Zzb.EF;

namespace AllLottery.BaseDataModel.PermissionManager
{
    public class RoleViewModel : BaseServiceNav
    {
        public override string NavName => "角色管理";

        public override string FolderName => "后台管理";

        public override int OperaColumnWidth => 150;

        [NavField("主键", IsDisplay = false, IsKey = true)]
        public int RoleId { get; set; }

        [NavField("角色")]
        public string Name { get; set; }

        public override bool ShowOperaColumn => true;

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<Role, RoleViewModel>(from r in Context.Roles where r.IsEnable select r);
        }

        public override BaseButton[] CreateRowButtons()
        {
            return new BaseButton[] { new RolePermissionEditViewModal("EditPermission", "编辑权限"), new RoleUserEditViewModal("EditUser", "编辑用户"), };
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new BaseModal[] { new RoleAddModal("Add", "新增") };
        }
    }
}