using Liaoxin.IBusiness;
using Liaoxin.Model;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace Liaoxin.BaseDataModel.PlayerManager
{
    public class PlayerAddViewModel : BaseServiceModal
    {
        public PlayerAddViewModel()
        {
        }

        public PlayerAddViewModel(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        [TextField("玩家", IsRequired = true)]
        public string Name { get; set; }

        [PasswordField("登录密码", IsRequired = true)]
        public string Password { get; set; }

        [PasswordField("资金密码", IsRequired = true)]
        public string CoidPassword { get; set; }

 

        public override BaseButton[] Buttons()
        {
            return new BaseButton[] { new ActionButton("Save", "保存") };
        }

        public override string ModalName => "新增玩家";

        public ServiceResult Save()
        {
            Name = Name.Trim();
            if (Name.Length > 9)
            {
                return new ServiceResult(ServiceResultCode.Error, "名称长度不能超过10个字符");
            }
            var sql = from p in Context.Players where p.Name == Name select p;
            if (sql.Any())
            {
                return new ServiceResult(ServiceResultCode.Error, "该玩家已存在");
            }

            Context.Players.Add(
                new Player(Name, SecurityHelper.Encrypt(Password), SecurityHelper.Encrypt(CoidPassword))  );
       //   UserOperateLogService.Log($"新增玩家[{Name}],玩家类型为[{PlayerType.ToDescriptionString()}]", Context);
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}