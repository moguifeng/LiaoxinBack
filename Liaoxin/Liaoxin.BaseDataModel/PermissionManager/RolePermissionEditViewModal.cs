using Liaoxin.IBusiness;
using System.Linq;
using System.Text;
using Zzb;
using Zzb.BaseData;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.EF;

namespace Liaoxin.BaseDataModel.PermissionManager
{
    public class RolePermissionEditViewModal : BaseServiceModal
    {
        public RolePermissionEditViewModal()
        {
        }

        public RolePermissionEditViewModal(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "编辑权限";

        [HiddenTextField("主键")]
        public int RoleId { get; set; }

        [PermissionField]
        public string[] PermissionDatas { get; set; }

        public ServiceResult Save()
        {
            using (var scope = Context.Database.BeginTransaction())
            {
                var exist = (from r in Context.Roles where r.RoleId == RoleId select r).First();
                Context.RolePermissions.RemoveRange(from rp in Context.RolePermissions where rp.RoleId == RoleId select rp);
                Context.SaveChanges();
                var permissions = from p in Context.Permissions where PermissionDatas.Contains(p.NavId) select p;
                StringBuilder sb = new StringBuilder();
                foreach (Permission permission in permissions)
                {
                    sb.Append($"[{BaseData.GetNavName(permission.NavId)}]");
                    Context.RolePermissions.Add(new RolePermission()
                    {
                        RoleId = RoleId,
                        PermissionId = permission.PermissionId
                    });
                }
                UserOperateLogService.Log($"编辑角色[{exist.Name}]的权限:{sb}", Context);
                Context.SaveChanges();
                scope.Commit();
            }

            return new ServiceResult(ServiceResultCode.Success);
        }

        public override void Init()
        {
            var permissions = from p in Context.RolePermissions where p.RoleId == RoleId select p;
            if (permissions.Any())
            {
                PermissionDatas = (from p in permissions select p.Permission.NavId).ToArray();
            }
        }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }
    }


}