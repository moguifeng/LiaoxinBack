using AllLottery.IBusiness;
using System.Linq;
using System.Text;
using Zzb.BaseData;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.EF;

namespace AllLottery.BaseDataModel.PermissionManager
{
    public class UserPermissionEditViewModal : BaseServiceModal
    {
        public UserPermissionEditViewModal()
        {
        }

        public UserPermissionEditViewModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "编辑用户权限";

        public IUserOperateLogService UserOperateLogService { get; set; }

        [HiddenTextField]
        public int UserInfoId { get; set; }

        [PermissionField]
        public string[] Permission { get; set; }

        public override void Init()
        {
            var permissions = from p in Context.UserInfoPermissions where p.UserInfoId == UserInfoId select p;
            if (permissions.Any())
            {
                Permission = (from p in permissions select p.Permission.NavId).ToArray();
            }
        }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public void Save()
        {
            using (var scope = Context.Database.BeginTransaction())
            {
                var user = (from u in Context.UserInfos where u.UserInfoId == UserInfoId select u).First();
                Context.UserInfoPermissions.RemoveRange(from p in Context.UserInfoPermissions where p.UserInfoId == UserInfoId select p);
                Context.SaveChanges();
                if (Permission != null)
                {
                    var permissions = from p in Context.Permissions where Permission.Contains(p.NavId) select p;

                    StringBuilder sb = new StringBuilder();
                    foreach (Permission permission in permissions)
                    {
                        sb.Append($"[{BaseData.GetNavName(permission.NavId)}]");
                        Context.UserInfoPermissions.Add(new UserInfoPermission()
                        {
                            UserInfoId = UserInfoId,
                            PermissionId = permission.PermissionId
                        });
                    }
                    UserOperateLogService.Log($"编辑用户[{user.Name}]的权限:{sb}", Context);
                    Context.SaveChanges();
                }

                scope.Commit();
            }
        }
    }
}