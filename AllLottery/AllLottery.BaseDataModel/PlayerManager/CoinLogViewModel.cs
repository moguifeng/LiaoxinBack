using AllLottery.Model;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.Common;

namespace AllLottery.BaseDataModel.PlayerManager
{
    public class CoinLogViewModel : BaseServiceNav
    {
        public override string NavName => "账变查询";

        public override string FolderName => "玩家管理";

        [NavField("用户名", IsKey = true)]
        public string PlayerName { get; set; }

        [NavField("摘要", 200)]
        public string Remark { get; set; }

        [NavField("账变类型")]
        public CoinLogTypeEnum Type { get; set; }

        [NavField("余额")]
        public decimal Coin { get; set; }

        [NavField("余额变化")]
        public string FlowCoinColor { get; set; }

        [NavField("冻结资金")]
        public decimal FCoin { get; set; }

        [NavField("冻结资金变化")]
        public string FlowFCoinColor { get; set; }

        [NavField("交易时间", 150)]
        public DateTime CreateTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<CoinLog, CoinLogViewModel>(from c in Context.CoinLogs
                                                            where c.IsEnable && c.Player.Type != PlayerTypeEnum.TestPlay
                                                            orderby c.CreateTime descending
                                                            select c, (k, t) =>
                {
                    t.PlayerName = k.Player.Name;
                    t.FlowCoinColor = Color(k.FlowCoin);
                    t.FlowFCoinColor = Color(k.FlowFCoin);
                }, (k, w) => w.Where(t => t.Player.Name.Contains(k)), (k, w) => w.Where(t => t.Remark.Contains(k)), (k, w) => ConvertEnum<CoinLog, CoinLogTypeEnum>(w, k, m => w.Where(t => t.Type == m)));
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[] { new TextFieldAttribute("Name", "玩家"), new TextFieldAttribute("Remark", "摘要"), new DropListFieldAttribute("status", "账变类型", CoinLogTypeEnum.Win.GetDropListModels("全部")) };
        }

        private string Color(decimal money)
        {
            if (money > 0)
            {
                return $"<p style='color:red;margin:0'>{money:#0.0000}</p>";
            }
            else if (money < 0)
            {
                return $"<p style='color:green;margin:0'>{money:#0.0000}</p>";
            }
            else
            {
                return money.ToString("#0.0000");
            }

        }
    }
}