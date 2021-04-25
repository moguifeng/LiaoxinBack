using AllLottery.IBusiness;
using System;
using System.Linq;
using Zzb.BaseData.Attribute.Field;
using Zzb.Common;

namespace AllLottery.BaseDataModel.RechargeManager
{
    //public class GiftEventEditModal : GiftEventAddModal
    //{
    //    public IUserOperateLogService OperateLogService { get; set; }

    //    public GiftEventEditModal()
    //    {
    //    }

    //    public GiftEventEditModal(string id, string name) : base(id, name)
    //    {
    //    }

    //    public override string ModalName => "修改活动";

    //    [HiddenTextField]
    //    public int GiftEventId { get; set; }

    //    public override void Save()
    //    {
    //        var setting = (from s in Context.GiftEvents
    //                       where s.GiftEventId == GiftEventId
    //                       select s).First();
    //        OperateLogService.Log($"修改充值活动{MvcHelper.LogDifferent(new LogDifferentViewModel(setting.BeginTime.ToDateString(), BeginTime.ToDateString(), "开始时间"), new LogDifferentViewModel(setting.EndTime.ToDateString(), EndTime.ToDateString(), "结束时间"), new LogDifferentViewModel(setting.ReceivingType.ToDescriptionString(), ReceivingType.ToString(), "赠送对象"), new LogDifferentViewModel(setting.Rule.ToDescriptionString(), Rule.ToDescriptionString(), "赠送规则"), new LogDifferentViewModel(setting.ReturnMoney.ToDecimalString(), ReturnMoney.ToDecimalString(), "赠送金额"), new LogDifferentViewModel(setting.ReturnRate.ToDecimalString(), ReturnRate.ToDecimalString(), "赠送比例"), new LogDifferentViewModel(setting.MinMoney.ToDecimalString(), MinMoney.ToDecimalString(), "最低充值金额"), new LogDifferentViewModel(setting.MaxMoney.ToDecimalString(), MaxMoney.ToDecimalString(), "最高充值金额"))},主键为[{GiftEventId}]", Context);
    //        setting.BeginTime = BeginTime;
    //        setting.EndTime = EndTime;
    //        setting.ReceivingType = ReceivingType;
    //        setting.Rule = Rule;
    //        setting.ReturnMoney = ReturnMoney;
    //        setting.ReturnRate = ReturnRate / 100;
    //        setting.MinMoney = MinMoney;
    //        setting.MaxMoney = MaxMoney;
    //        setting.UpdateTime = DateTime.Now;
    //        Context.SaveChanges();
    //    }
    //}
}