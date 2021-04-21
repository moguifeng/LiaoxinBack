using AllLottery.Model;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.RechargeManager
{
    public class GiftReceiveViewModel : BaseServiceNav
    {
        public override string NavName => "活动赠送查询";

        public override string FolderName => "充值管理";

        [NavField("玩家")]
        public string PlayerName { get; set; }

        [NavField("赠送金额")]
        public decimal GiftMoney { get; set; }

        [NavField("赠送时间", 150)]
        public DateTime CreateTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<GiftReceive, GiftReceiveViewModel>(
                from g in Context.GiftReceives where g.IsEnable orderby g.CreateTime descending select g, (k, t) =>
                {
                    t.PlayerName = k.Player.Name;
                });
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new[] {new GiftReceiveAddModal("Add", "新增"),};
        }
    }
}