using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.Common;
using static Liaoxin.Model.RedPacket;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class RedPacketViewModel : BaseServiceNav
    {
        public IUserOperateLogService UserOperateLogService { get; set; }


        public override int OperaColumnWidth => 150;

        public override string NavName => "发红包列表";

        public override string FolderName => "红包管理";

        public override int Sort => 4;

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public Guid RedPacketId { get; set; }



        [NavField("红包发送者")]
        public string LiaoxinNumber { get; set; }


        [NavField("祝福语(尾数)")]
        public string Greeting { get; set; }



        [NavField("发送时间")]
        public DateTime SendTime { get; set; }
        [NavField("红包类型")]
        public RedPacketTypeEnum Type { get; set; }

        [NavField("红包个数")]
        public int Count { get; set; }

        [NavField("红包金额")]
        public decimal Money { get; set; }


    
            [NavField("剩余红包金额")]
        public decimal Over { get; set; }

        [NavField("红包状态")]
        public RedPacketStatus Status { get; set; }


        protected override object[] DoGetNavDatas()
        {
            List<RedPacketViewModel> lis = new List<RedPacketViewModel>();
            var sources =  CreateEfDatasedHandle(from r in Context.RedPackets where r.IsEnable orderby r.CreateTime descending select r,
            
                (k, w) => w.Where(t => t.Group.UnqiueId.Contains(k)),
                (k, w) => w.Where(t => t.Client.LiaoxinNumber.Contains(k)),                
                (k, w) => ConvertEnum<RedPacket, RedPacketTypeEnum>(w, k, m => w.Where(t => t.Type == m)),
                (k, w) => ConvertEnum<RedPacket, RedPacketStatus>(w, k, m => w.Where(t => t.Status == m)));

            foreach (var item in sources)
            {
                RedPacketViewModel model = new RedPacketViewModel();
                model.RedPacketId = item.RedPacketId;
                model.LiaoxinNumber = item.Client.LiaoxinNumber;
                model.Greeting = item.Greeting;
                model.SendTime = item.SendTime;
                model.Type = item.Type;
                model.Count = item.Count;
                model.Money = item.Money;
                model.Over = item.Over;
                model.Status = item.Status;
                lis.Add(model);

            }
            return lis.ToArray();
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[]
            {   new TextFieldAttribute("UniqueId", "群号"),
                new TextFieldAttribute("LiaoxinNumber", "发送者聊信号"),                
                new DropListFieldAttribute("Type", "红包类型",RedPacketTypeEnum.Lucky.GetDropListModels("全部")),
                new DropListFieldAttribute("Status", "红包状态",RedPacketStatus.Send.GetDropListModels("全部")),
            };
        }     
    }
}