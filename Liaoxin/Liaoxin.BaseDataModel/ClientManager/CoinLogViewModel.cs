using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.Common;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class CoinLogViewModel : BaseServiceNav
    {
        public override string NavName => "账变查询";

        public override string FolderName => "客户管理";

        public override int Sort => 5;

        [NavField("聊信号", IsKey = true,Width =250)]
        public string LiaoxinNumber { get; set; }


        [NavField("手机号码")]
        public string Telephone { get; set; }



        [NavField("摘要", 200)]
        public string Remark { get; set; }

        [NavField("账变类型")]
        public CoinLogTypeEnum Type { get; set; }

        [NavField("余额")]
        public decimal Coin { get; set; }

        [NavField("余额变化")]
        public string FlowCoinColor { get; set; }


        [NavField("交易时间", 150)]
        public DateTime CreateTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
            List<CoinLogViewModel> lis = new List<CoinLogViewModel>();
            var sources = CreateEfDatasedHandle(from c in Context.CoinLogs
                                                where c.IsEnable
                                                orderby c.CreateTime descending
                                                select c,
                     (k, w) => w.Where(t => t.Client.LiaoxinNumber.Contains(k)),
                      (k, w) => w.Where(t => t.Client.Telephone.Contains(k)),
                     (k, w) => w.Where(t => t.Remark.Contains(k)),
                     (k, w) => ConvertEnum<CoinLog, CoinLogTypeEnum>(w, k, m => w.Where(t => t.Type == m)));

            foreach (var item in sources)
            {
                CoinLogViewModel model = new CoinLogViewModel();
                model.Telephone = item.Client.Telephone;
                model.LiaoxinNumber = item.Client.LiaoxinNumber;
                model.Coin = item.Coin;
                model.FlowCoinColor = Color(item.FlowCoin);
                model.Remark = item.Remark;
                model.CreateTime = item.CreateTime;
                model.Type = item.Type;
                lis.Add(model);
            }
            return lis.ToArray();
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[] { new TextFieldAttribute("LiaoxinNumber", "聊信号"), new TextFieldAttribute("Telephone", "手机号码"), new TextFieldAttribute("Remark", "摘要"), new DropListFieldAttribute("status", "账变类型", CoinLogTypeEnum.Withdraw.GetDropListModels("全部")) };
        }

        private string Color(decimal money)
        {
            if (money > 0)
            {
                return $"<p style='color:red;margin:0'>{money:#0.00}</p>";
            }
            else if (money < 0)
            {
                return $"<p style='color:green;margin:0'>{money:#0.00}</p>";
            }
            else
            {
                return money.ToString("#0.0000");
            }

        }
    }
}
