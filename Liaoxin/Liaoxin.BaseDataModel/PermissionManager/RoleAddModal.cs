using Liaoxin.IBusiness;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.EF;

namespace Liaoxin.BaseDataModel.PermissionManager
{
    public class RoleAddModal : BaseServiceModal
    {
        public RoleAddModal()
        {
        }

        public RoleAddModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "新增角色";

        public IUserOperateLogService UserOperateLogService { get; set; }

        [TextField("角色")]
        public string Name { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            var role = from r in Context.Roles where r.Name == Name select r;
            if (role.Any())
            {
                return new ServiceResult(ServiceResultCode.Error, "已存在相同的角色，无法添加");
            }
            Context.Roles.Add(new Role() { Name = Name });
            UserOperateLogService.Log($"新增角色[{Name}]", Context);
            Context.SaveChanges();
            return new ServiceResult();
        }
    }
}