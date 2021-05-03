using Castle.Components.DictionaryAdapter;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class RateOfClientViewModel : BaseServiceNav
    {


        public IUserOperateLogService UserOperateLogService { get; set; }
        public override string NavName => "客户概率管理";

        public override string FolderName => "概率管理";

        public override int Sort => 3;


        [NavField("主键", IsKey = true, IsDisplay = false)]
        public Guid RateOfClientId { get; set; }


        [NavField("客户聊信号", Width = 170)]
        public string LiaoxinNumber { get; set; }

        [NavField("百分比")]
        public int Rate { get; set; }


        [NavField("优先级")]
        public int Priority { get; set; }


        [NavField("是否停用")]
        public bool IsStop { get; set; }
 

        [NavField("创建时间",width:170)]
        public DateTime CreateTime { get; set; }


        protected override object[] DoGetNavDatas()
        {
            List<RateOfClientViewModel> lis = new List<RateOfClientViewModel>();

            var sources = CreateEfDatasedHandle(from c in Context.RateOfClients
                                                where c.IsEnable
                                                orderby c.CreateTime descending
                                                select c,              
                      (k, w) => w.Where(t => t.Client.LiaoxinNumber.Contains(k)));; 




            foreach (var item in sources)
            {
                RateOfClientViewModel model = new RateOfClientViewModel();
                model.RateOfClientId = item.RateOfClientId;
                model.LiaoxinNumber = item.Client.LiaoxinNumber;
                model.Rate = item.Rate;
                model.Priority = item.Priority;
                model.IsStop = item.IsStop;
                model.CreateTime = item.CreateTime;
             
                lis.Add(model);
            }
            return lis.ToArray();
        }   

        public override BaseButton[] CreateRowButtons()
        {
            List<BaseButton> list = new EditableList<BaseButton>();
            list.Add(new RateOfClientEditModal("Save","编辑概率"));       
            return list.ToArray();

        }

        public override BaseButton[] CreateViewButtons()
        {
            return new BaseButton[] {
                new RateOfClientAddModal("Add", "新增概率"),

            };

        }


        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[] {
                                  
                new TextFieldAttribute("LiaoxinNumber", "客户聊信号")};


        }

    }
}
