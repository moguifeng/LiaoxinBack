using Liaoxin.Cache;
using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;
using static Liaoxin.Model.ClientRelation;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class ClientRelationViewModel : BaseServiceNav
    {
 

        public override int OperaColumnWidth => 150;

        public override string NavName => "客户关系表";

        public override string FolderName => "客户管理";

        public override int Sort => 2;

        [NavField("ID", IsKey = true, IsDisplay = false)]
        public Guid ClientRelationId { get; set; }

        [NavField("源聊信号")]
        public string SourceLiaoxinNumber { get; set; }

        [NavField("目标聊信号")]
        public string ToLiaoxinNumber { get; set; }



        [NavField("目标昵称")]
        public string NickName { get; set; }
      

        [NavField("目标电话号码")]
        public string Telephone { get; set; }

        [NavField("目标国家/地区",Width =170)]
        public string Area { get; set; }


        [NavField("目标关系")]
        public RelationTypeEnum RelationType { get; set; }

        [NavField("创建关系时间")]
        public DateTime CreateTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
            List<ClientRelationViewModel> lis = new List<ClientRelationViewModel>();
            var sources  = CreateEfDatasedHandle(from r in Context.ClientRelationDetails where r.IsEnable  orderby r.CreateTime 
                                                                       descending select r,

                (k, w) => w.Where(t => t.ClientRelation.Client.LiaoxinNumber == k),
                (k, w) => w.Where(t => t.ClientRelation.Client.Telephone.Contains(k)),
                (k, w) => ConvertEnum<ClientRelationDetail, RelationTypeEnum>(w, k, m => w.Where(t =>(int) t.ClientRelation.RelationType ==(int) m))
                );

            foreach (var item in sources)
            {
                ClientRelationViewModel model = new ClientRelationViewModel();
                var source = item.ClientRelation.Client;
                var to = item.Client;
                model.ClientRelationId = item.ClientRelationDetailId;
                model.SourceLiaoxinNumber = source.LiaoxinNumber;
                model.ToLiaoxinNumber = to.LiaoxinNumber;
                model.NickName = to.NickName;
                model.Telephone = to.Telephone;
                model.Area = to.AreaCode.ToAreaFullName();
                model.RelationType = item.ClientRelation.RelationType;
                model.CreateTime = item.CreateTime;
                lis.Add(model);
            }
            return lis.ToArray();
           
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new BaseButton[] {
                new ClientRelationApplyModel("Add", "申请添加关系"),

            };

        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[]
            {   new TextFieldAttribute("LiaoxinNumber", "源聊信号"),
                new TextFieldAttribute("Telephone", "手机号码"),           
                new DropListFieldAttribute("RelationType", "关系类型",RelationTypeEnum.Friend.GetDropListModels("全部")),
            };
        }    
    }
}