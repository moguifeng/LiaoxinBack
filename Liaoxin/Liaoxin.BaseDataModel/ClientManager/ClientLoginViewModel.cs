using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class ClientLogViewModel : BaseServiceNav
    {
        public override string NavName => "登录日志";

        public override string FolderName => "客户管理";

        [NavField("ID", IsKey = true, IsDisplay = false)]
        public Guid CliengLoginId { get; set; }

        [NavField("客户手机号码")]
        public string Telephone { get; set; }

        [NavField("客户聊信号")]
        public string LiaoxinNumber { get; set; }

        [NavField("客户昵称")]
        public string NickName { get; set; }


        [NavField("IP")]
        public string IP { get; set; }

        [NavField("地址", 200)]
        public string Address { get; set; }

        [NavField("登录时间", 150)]
        public DateTime CreateTime { get; set; }



        protected override object[] DoGetNavDatas()
        {

            List<ClientLogViewModel> lis = new List<ClientLogViewModel>();
             var sources =   CreateEfDatasedHandle(from l in Context.ClientLoginLogs
                                         orderby l.CreateTime descending
                                         select l,

                (k, w) => w.Where(t => t.Client.LiaoxinNumber.Contains(k)),
                   (k, w) => w.Where(t => t.Client.Telephone.Contains(k)),
                (k, w) => w.Where(t => t.Address.Contains(k)));
            foreach (var item in sources)
            {
                ClientLogViewModel model = new ClientLogViewModel();
                model.Address = item.Address;
                model.CliengLoginId = item.ClientLoginLogId;
                model.CreateTime = item.CreateTime;
                model.IP = item.IP;
                model.Telephone = item.Client.Telephone;
                model.NickName = item.Client.NickName;
                model.LiaoxinNumber = item.Client.LiaoxinNumber;
                lis.Add(model);
            }

            return lis.ToArray();
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new[] { new TextFieldAttribute("Name", "客户聊信号"), new TextFieldAttribute("Name", "客户手机"), new TextFieldAttribute("Address", "地址"), };
        }
    }
}