using Liaoxin.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class RedPacketReceiveViewModel : BaseServiceNav
    {
        public IUserOperateLogService UserOperateLogService { get; set; }


        public override int OperaColumnWidth => 150;

        public override string NavName => "接收红包列表";

        public override string FolderName => "红包管理";

        public override int Sort => 5;

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public Guid RedPacketReceiveId { get; set; }

        [NavField("群号")]
        public string GroupUniqueId { get; set; }



        [NavField("红包发送者")]
        public string SourceLiaoxinNumber { get; set; }


        [NavField("红包接收者")]
        public string ToLiaoxinNumber { get; set; }


        [NavField("接收时间")]
        public DateTime CreateTime { get; set; }  


        [NavField("接收红包金额")]
        public decimal SnatchMoney { get; set; }      

        protected override object[] DoGetNavDatas()
        {
            List<RedPacketReceiveViewModel> lis = new List<RedPacketReceiveViewModel>();
            var sources = CreateEfDatasedHandle(from r in Context.RedPacketReceives where r.IsEnable orderby r.CreateTime descending select r,

                (k, w) => w.Where(t => t.RedPacket.Group.UnqiueId.Contains(k)),
                (k, w) => w.Where(t => t.RedPacket.Client.LiaoxinNumber.Contains(k)),
                 (k, w) => w.Where(t => t.Client.LiaoxinNumber.Contains(k)));

            foreach (var item in sources)
            {
                RedPacketReceiveViewModel model = new RedPacketReceiveViewModel();
                model.RedPacketReceiveId = item.RedPacketReceiveId;
                model.GroupUniqueId = item.RedPacket.Group.UnqiueId;
                model.SourceLiaoxinNumber = item.RedPacket.Client.LiaoxinNumber;
                model.ToLiaoxinNumber = item.Client.LiaoxinNumber;
                model.CreateTime = item.CreateTime;
                model.SnatchMoney = item.SnatchMoney;

               lis.Add(model);

            }
            return lis.ToArray();
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[]
            {   new TextFieldAttribute("UniqueId", "群号"),
                new TextFieldAttribute("SendLiaoxinNumber", "发送者聊信号"),
                new TextFieldAttribute("ReceiveLiaoxinNumber", "接收者聊信号"),

            };
        }     
    }
}