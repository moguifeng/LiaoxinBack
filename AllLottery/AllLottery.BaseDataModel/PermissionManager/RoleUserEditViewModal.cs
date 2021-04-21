using AllLottery.IBusiness;
using Castle.Components.DictionaryAdapter;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.EF;

namespace AllLottery.BaseDataModel.PermissionManager
{
    public class RoleUserEditViewModal : BaseServiceModal
    {
        public RoleUserEditViewModal()
        {
        }

        public RoleUserEditViewModal(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "编辑角色用户";

        [HiddenTextField("主键")]
        public int RoleId { get; set; }

        [UserField]
        public string[] Users { get; set; }

        public override void Init()
        {
            var users = from p in Context.UserInfoRoles where p.RoleId == RoleId select p;
            if (users.Any())
            {
                Users = (from p in users select p.UserInfoId.ToString()).ToArray();
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
                var exist = (from r in Context.Roles where r.RoleId == RoleId select r).First();
                Context.UserInfoRoles.RemoveRange(from ui in Context.UserInfoRoles where ui.RoleId == RoleId select ui);
                Context.SaveChanges();
                if (Users != null)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string number in Users)
                    {
                        var id = int.Parse(number);
                        var user = (from u in Context.UserInfos where u.UserInfoId == id select u).First();
                        sb.Append($"[{user.Name}]");
                        Context.UserInfoRoles.Add(new UserInfoRole() { RoleId = RoleId, UserInfoId = id });
                    }
                    UserOperateLogService.Log($"编辑角色[{exist.Name}]的用户:{sb}", Context);
                    Context.SaveChanges();
                }
                scope.Commit();
            }
        }
    }

    public class UserFieldAttribute : TreeServiceFieldAttribute
    {
        public override List<TreesModel> Source
        {
            get
            {
                List<TreesModel> list = new EditableList<TreesModel>();
                foreach (var user in from u in Context.UserInfos where u.IsEnable select u)
                {
                    list.Add(new TreesModel() { Title = user.Name, Value = user.UserInfoId.ToString() });
                }
                return list;
            }
        }
    }
}