using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class ClientEditModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public ClientEditModal()
        {
        }

        public ClientEditModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "编辑客户";


        [HiddenTextField]

        public int ClientId { get; set; }
        [TextField("聊信号", IsReadOnly = true)]
        public string LiaoxinNumber { get; set; }


        [TextField("绑定环信号",Placeholder ="空代表不修改")]
        public string HuanXinId { get; set; }

        [TextField("昵称", IsReadOnly = true)]
        public string NickName { get; set; }

        [TextField("手机号码", Placeholder = "空代表不修改")]
        public string Telephone { get; set; }

        [PasswordField("登录密码", Placeholder = "空代表不修改")]
        public string Password { get; set; }

        [PasswordField("资金密码", Placeholder = "空代表不修改")]
        public string CoinPassword { get; set; }

        [DropListField("允许提现")]
        public bool CanWithdraw { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            var entity = Context.Clients.Where(c => c.ClientId == this.ClientId).FirstOrDefault();
            if (!string.IsNullOrEmpty(this.CoinPassword))
            {
                entity.CoinPassword = SecurityHelper.Encrypt(CoinPassword);
            }
            if (!string.IsNullOrEmpty(this.Password))
            {
                entity.Password = SecurityHelper.Encrypt(Password);
            }
            if (!string.IsNullOrEmpty(this.Telephone))
            {
                entity.Telephone = this.Telephone;
            }
            if (!string.IsNullOrEmpty(this.HuanXinId))
            {
                entity.HuanXinId = this.HuanXinId;
            }
            entity.CanWithdraw = this.CanWithdraw;
            entity.UpdateTime = DateTime.Now;
            Context.Clients.Update(entity);
            UserOperateLogService.Log($"编辑[{entity.LiaoxinNumber}]聊信客户", Context);
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}