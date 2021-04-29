using Castle.Components.DictionaryAdapter;
using Liaoxin.BaseDataModel.ContentManager;
using Liaoxin.Business.Config;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;
using static Liaoxin.Model.Client;

namespace Liaoxin.BaseDataModel.RechargeManager
{
    public class ClientViewModel : BaseServiceNav
    {
        public IRechargeService RechargeService { get; set; }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override int OperaColumnWidth => 140;

        public override string NavName => "客户管理";

        public override string FolderName => "客户管理";

        public override int Sort => 1;

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public int ClientId { get; set; }



        [NavField("聊信号")]
        public string LiaoxinNumber { get; set; }

        [NavField("绑定环信号")]
        public string HuanXinId { get; set; }
        [NavField("昵称")]
        public string NickName { get; set; }

        [NavField("余额")]
        public decimal Coin { get; set; }

        [NavField("电话号码")]
        public string Telephone { get; set; }

        [NavField("国家/地区")]
        public string Area { get; set; }

        [NavField("当前使用设备")]
        public string CurrenUserDevide { get; set; }

        [NavField("邮箱地址", 150)]
        public string Email { get; set; }

        [NavField("创建时间", 150)]
        public DateTime CreateTime { get; set; }

        [NavField("是否启用", 150)]
        public bool IsFreeze { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<Client, ClientViewModel>(from r in Context.Clients where r.IsEnable orderby r.CreateTime descending select r,
                (k, t) =>
                {
                    t.Area = "广东深圳";
                    t.IsFreeze = k.IsFreeze;

                },
                (k, w) => w.Where(t => t.LiaoxinNumber.Contains(k)),
                (k, w) => w.Where(t => t.Telephone.Contains(k)), (k, w) => w.Where(t => t.NickName.Contains(k)), (k, w) => ConvertEnum<Client, TrueFalseEnum>(w, k, m => w.Where(t => t.IsEnable == (m == TrueFalseEnum.True) ? true : false)));
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[]
            {   new TextFieldAttribute("LiaoxinNumber", "聊信号"),
                new TextFieldAttribute("Telephone", "手机号码"),
                new TextFieldAttribute("NickName", "昵称"),
                new DropListFieldAttribute("IsFreeze", "是否启用",TrueFalseEnum.True.GetDropListModels("全部")),
            };
        }

        public override BaseButton[] CreateRowButtons()
        {
            List<BaseButton> list = new EditableList<BaseButton>();
            string title = this.IsFreeze ? "禁用" : "启用";
            list.Add(new ConfirmActionButton("EnableOperation", title, "是否确认操作?"));
            list.Add(new ClientEditModal("Save", "编辑客户"));
            return list.ToArray();
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new BaseButton[] {
                new ClientAddModal("Add", "新增客户"),

            };
        }

        public ServiceResult EnableOperation()
        {
            var entity = Context.Clients.Where(c => c.ClientId == this.ClientId).FirstOrDefault();
            if (entity == null)
            {
                return new ServiceResult(ServiceResultCode.QueryNull, "找不到客户");
            }

            entity.IsFreeze = !this.IsFreeze;
            Context.Clients.Update(entity);
            int res = Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }



    }
}