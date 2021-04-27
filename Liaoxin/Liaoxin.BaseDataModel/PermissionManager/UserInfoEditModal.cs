using Liaoxin.IBusiness;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace Liaoxin.BaseDataModel.PermissionManager
{
    public class UserInfoEditModal : BaseServiceModal
    {
        public UserInfoEditModal()
        {
        }

        public UserInfoEditModal(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "编辑用户";

        [HiddenTextField]
        public int UserInfoId { get; set; }

        [PasswordField("登录密码")]
        public string Password { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            if (UserInfoId.ToString() == HttpContextAccessor.HttpContext.User.Identity.Name)
            {
                return new ServiceResult(ServiceResultCode.Error, "不能修改当前账号的密码");
            }

            var user = (from u in Context.UserInfos where u.UserInfoId == UserInfoId select u).First();
            user.Password = SecurityHelper.Encrypt(Password);
            user.Update();
            UserOperateLogService.Log($"修改用户[{user.Name}]的密码", Context);
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}