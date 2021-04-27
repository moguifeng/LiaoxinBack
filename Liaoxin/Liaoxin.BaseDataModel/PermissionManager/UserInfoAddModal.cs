using Liaoxin.IBusiness;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;
using Zzb.EF;

namespace Liaoxin.BaseDataModel.PermissionManager
{
    public class UserInfoAddModal : BaseServiceModal
    {
        public UserInfoAddModal()
        {
        }

        public UserInfoAddModal(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "新增玩家";

        [TextField("用户名", IsRequired = true)]
        public string Name { get; set; }

        [PasswordField("密码", IsRequired = true)]
        public string Password { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            var exist = (from u in Context.UserInfos where u.Name == Name select u).FirstOrDefault();
            if (exist != null)
            {
                return new ServiceResult(ServiceResultCode.Error, "该用户名已存在，请重新输入");
            }

            UserOperateLogService.Log($"新增用户[{Name}]", Context);
            Context.UserInfos.Add(new UserInfo() { Name = Name, Password = SecurityHelper.Encrypt(Password) });
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}