using Liaoxin.IBusiness;
using Liaoxin.Model;
using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace Liaoxin.BaseDataModel.WithdrawManager
{
    public class ClientBankViewModel : BaseServiceNav
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string NavName => "客户银行列表";

        public override string FolderName => "提款管理";

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public Guid ClientBankId { get; set; }

        [NavField("聊信号码")]
        public string LiaoxinNumber { get; set; }

        [NavField("真实姓名")]
        public string PayeeName { get; set; }

        [NavField("银行名称")]
        public string BankName { get; set; }

        [NavField("卡号")]
        public string CardNumber { get; set; }

        [NavField("添卡时间", 150)]
        public DateTime CreateTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
           var sources=  CreateEfDatasedHandle(
                from b in Context.ClientBanks where b.IsEnable  orderby b.CreateTime descending select b,
            
                (k, w) => w.Where(t => t.Client.RealName.Contains(k)),
                (k, w) => w.Where(t => t.SystemBank.Name.Contains(k)),
                (k, w) => w.Where(t => t.CardNumber.Contains(k)));
            List<ClientBankViewModel> lis = new List<ClientBankViewModel>();
            foreach (var item in sources)
            {
                ClientBankViewModel model = new ClientBankViewModel();
                model.BankName = item.SystemBank.Name;
                model.CardNumber = item.CardNumber;
                model.LiaoxinNumber = item.Client.LiaoxinNumber;
                model.PayeeName = item.Client.RealName;
                model.CreateTime = item.CreateTime;
                model.ClientBankId = item.ClientBankId;
                lis.Add(model);
            }
            return lis.ToArray();
            
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[] { new TextFieldAttribute("RealName", "客户真实姓名"), new TextFieldAttribute("Bank", "银行"), new TextFieldAttribute("Card", "卡号"), };
        }

 

 
    }
}