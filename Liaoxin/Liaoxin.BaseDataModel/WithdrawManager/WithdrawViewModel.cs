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

namespace Liaoxin.BaseDataModel.WithdrawManager
{
    public class WithdrawViewModel : BaseServiceNav
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string NavName => "提款管理";

        public override string FolderName => "提款管理";

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public int WithdrawId { get; set; }

        [NavField("客户聊信号码", 200)]
        public string LiaoxinNumber { get; set; }

        [NavField("客户真实姓名", 200)]
        public string RealName { get; set; }


        [NavField("客户电话号码", 200)]
        public string Telephone { get; set; }


        [NavField("订单号", 200)]
        public string OrderNo { get; set; }

        [NavField("提现金额")]
        public string MoneyString { get; set; }

        [NavField("玩家余额")]
        public decimal Coin { get; set; }

        //[NavField("冻结金额")]
        //public decimal FCoin { get; set; }

        [NavField("收款银行", 200)]
        public string BankName { get; set; }

        [NavField("收款银行账号", 200)]
        public string BankAccount { get; set; }

        //[NavField("收款人姓名", 200)]
        //public string BankAccountName { get; set; }

        [NavField("状态")]
        public WithdrawStatusEnum Status { get; set; }

        [NavField("申请时间", 150)]
        public DateTime CreateTime { get; set; }


        [NavField("备注", 200)]
        public string Remark { get; set; }

        protected override object[] DoGetNavDatas()
        {
            var sources = CreateEfDatasedHandle(from w in Context.Withdraws where w.IsEnable orderby w.CreateTime descending select w,
                   (k, w) => w.Where(t => t.ClientBank.Client.RealName.Contains(k)),
                      (k, w) => w.Where(t => t.OrderNo.Contains(k)),
                 (k, w) => ConvertEnum<Withdraw, WithdrawStatusEnum>(w, k, m => w.Where(t => t.Status == m))
                 );

            List<WithdrawViewModel> lis = new List<WithdrawViewModel>();
            foreach (var item in sources)
            {
                WithdrawViewModel model = new WithdrawViewModel();

                model.MoneyString = $"<p style='font-weight:bold;margin:0'>{item.Money:#0.0000}</p>";
                model.LiaoxinNumber = item.ClientBank.Client.LiaoxinNumber;
                model.RealName = item.ClientBank.Client.RealName;
                model.Telephone = item.ClientBank.Client.Telephone;
                model.Coin = item.ClientBank.Client.Coin;
                model.OrderNo = item.OrderNo;
                model.BankName = item.ClientBank.SystemBank.Name;
                model.BankAccount = item.ClientBank.CardNumber;
                model.Status = item.Status;
                model.CreateTime = item.CreateTime;
                model.Remark = item.Remark;
                lis.Add(model);
                
            }
            return lis.ToArray();       
             
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[] { new TextFieldAttribute("RealName", "客户真实姓名"), new TextFieldAttribute("Order", "订单号"), new DropListFieldAttribute("status", "状态", WithdrawStatusEnum.Ok.GetDropListModels("全部")) };
        }

    }
}