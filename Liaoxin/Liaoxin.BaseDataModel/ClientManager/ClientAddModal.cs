using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class ClientAddModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public ClientAddModal()
        {
        }

        public ClientAddModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "新增客户";

        /// <summary>
        /// 标题
        /// </summary>
        [TextField("绑定环信号", IsRequired = true)]
        public string HuanXinId { get; set; }

        [TextField("昵称", IsRequired = true)]
        public string NickName { get; set; }

        [TextField("手机号码", IsRequired = true)]
        public string Telephone { get; set; }

        [PasswordField("登录密码", IsRequired = true)]
        public string Password { get; set; }

        [PasswordField("资金密码", IsRequired = true)]
        public string CoinPassword { get; set; }


        [ImageField("背景头像", IsRequired = true)]
        public int? Cover { get; set; }



        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            var client = new Client()
            {
                LiaoxinNumber = SecurityCodeHelper.CreateRandomCode(11),
                NickName = NickName,
                Password = SecurityHelper.Encrypt(Password),
                CoinPassword = SecurityHelper.Encrypt(CoinPassword),
                Telephone = Telephone,
                HuanXinId = HuanXinId,
                Cover = Cover.Value,

            };
            Context.Clients.Add(client);
            UserOperateLogService.Log($"新增[{client.LiaoxinNumber}]聊信客户", Context);
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}