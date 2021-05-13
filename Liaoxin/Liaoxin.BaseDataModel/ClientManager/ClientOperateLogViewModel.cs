using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class ClientOperateLogViewModel : BaseServiceNav
    {
        public override string NavName => "客户操作记录";

        public override string FolderName => "客户管理";

        [NavField("客户聊信号", 250)]
        public string LiaoxinNumber { get; set; }

        [NavField("客户手机号码",150)]
        public string Telephone { get; set; }


        [NavField("操作记录", 500)]
        public string Message { get; set; }

        [NavField("操作时间", 200)]
        public DateTime CreateTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
            var sources =  CreateEfDatasedHandle(
                from p in Context.ClientOperateLogs where p.IsEnable orderby p.CreateTime descending select p
                 , (k, w) => w.Where(t => t.Client.LiaoxinNumber.Contains(k)),
                (k, w) => w.Where(t => t.Client.Telephone.Contains(k)),
                    (k, w) => w.Where(t => t.Message.Contains(k)));

            foreach (var item in sources)
            {
                ClientOperateLogViewModel model = new ClientOperateLogViewModel();
                model.LiaoxinNumber = item.Client.LiaoxinNumber;
                model.Telephone = item.Client.Telephone;
                model.Message = item.Message;
                item.CreateTime = item.CreateTime;
            }

            List<ClientOperateLogViewModel> lis = new List<ClientOperateLogViewModel>();return lis.ToArray();
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new[] { new TextFieldAttribute("LiaoxinNumber", "聊信号"), new TextFieldAttribute("Telephone", "手机号码"), new TextFieldAttribute("Message", "操作记录"), };
        }
    }
}