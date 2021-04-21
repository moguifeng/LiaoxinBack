using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.RechargeManager
{
    public class GiftEventViewModel : BaseServiceNav
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string NavName => "充值活动";

        public override string FolderName => "充值管理";

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public int GiftEventId { get; set; }

        [NavField("开始时间", 150)]
        public DateTime BeginTime { get; set; }

        [NavField("结束时间", 150)]
        public DateTime EndTime { get; set; }

        [NavField("赠送对象")]
        public ReceivingTypeEnum ReceivingType { get; set; }

        [NavField("赠送规则")]
        public GiftRuleEnum Rule { get; set; }

        [NavField("赠送金额")]
        public decimal ReturnMoney { get; set; }

        [NavField("赠送比例")]
        public decimal ReturnRate { get; set; }

        [NavField("充值最低金额")]
        public decimal MinMoney { get; set; }

        [NavField("充值最高金额")]
        public decimal MaxMoney { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<GiftEvent, GiftEventViewModel>(from g in Context.GiftEvents where g.IsEnable orderby g.CreateTime descending select g,
               (p, t) =>
                {
                    t.ReturnRate = p.ReturnRate * 100;
                });
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new[] { new GiftEventAddModal("Add", "新增活动"), };
        }

        public override BaseButton[] CreateRowButtons()
        {
            return new BaseButton[] { new GiftEventEditModal("Edit", "修改活动"), new ConfirmActionButton("Delete", "删除", "是否确定删除？"), };
        }

        public void Delete()
        {
            var exist = (from g in Context.GiftEvents where g.GiftEventId == GiftEventId select g).First();
            exist.IsEnable = false;
            UserOperateLogService.Log($"删除充值活动,主键为[{GiftEventId}]", Context);
            Context.SaveChanges();
        }
    }
}