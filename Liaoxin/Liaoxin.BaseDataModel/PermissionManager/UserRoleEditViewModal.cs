using Liaoxin.IBusiness;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.EF;

namespace Liaoxin.BaseDataModel.PermissionManager
{
    public class UserRoleEditViewModal : BaseServiceModal
    {
        public UserRoleEditViewModal()
        {
        }

        public UserRoleEditViewModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "编辑用户角色";

        public IUserOperateLogService UserOperateLogService { get; set; }

        [HiddenTextField]
        public int UserInfoId { get; set; }

        [RoleField]
        public string[] Roles { get; set; }

        public override void Init()
        {

            var permission = from r in Context.UserInfoRoles where r.UserInfoId == UserInfoId select r;
            if (permission.Any())
            {
                List<string> list = new List<string>();
                foreach (UserInfoRole userInfoRole in permission)
                {
                    list.Add(userInfoRole.RoleId.ToString());
                }
                Roles = list.ToArray();
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
                Context.UserInfoRoles.RemoveRange(from u in Context.UserInfoRoles where u.UserInfoId == UserInfoId select u);
                Context.SaveChanges();
                if (Roles != null)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var number in Roles)
                    {
                        var id = int.Parse(number);
                        var role = (from r in Context.Roles where r.RoleId == id select r).First();
                        sb.Append($"[{role.Name}]");
                        Context.UserInfoRoles.Add(new UserInfoRole()
                        {
                            RoleId = int.Parse(number),
                            UserInfoId = UserInfoId
                        });
                    }
                    UserOperateLogService.Log($"编辑用户[{user.Name}]的角色:{sb}", Context);
                    Context.SaveChanges();
                }
                scope.Commit();
            }
        }
    }

    public class RoleFieldAttribute : TreeServiceFieldAttribute
    {
        public override List<TreesModel> Source
        {
            get
            {
                List<TreesModel> list = new List<TreesModel>();
                foreach (Role role in from r in Context.Roles where r.IsEnable select r)
                {
                    list.Add(new TreesModel() { Title = role.Name, Value = role.RoleId.ToString() });
                }
                return list;
            }
        }
    }
}