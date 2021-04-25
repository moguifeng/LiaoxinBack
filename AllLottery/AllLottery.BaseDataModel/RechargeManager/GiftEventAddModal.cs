using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.RechargeManager
{
    //public class GiftEventAddModal : BaseServiceModal
    //{
    //    public IUserOperateLogService UserOperateLogService { get; set; }

    //    public GiftEventAddModal()
    //    {
    //    }

    //    public GiftEventAddModal(string id, string name) : base(id, name)
    //    {
    //    }

    //    public override string ModalName => "新增活动";

    //    [DateTimeField("开始时间")]
    //    public DateTime BeginTime { get; set; } = DateTime.Now;

    //    [DateTimeField("结束时间")]
    //    public DateTime EndTime { get; set; } = DateTime.Now.AddDays(5);

    //    [DropListField("赠送对象")]
    //    public ReceivingTypeEnum ReceivingType { get; set; }

    //    [DropListField("赠送规则")]
    //    public GiftRuleEnum Rule { get; set; }

    //    [NumberField("赠送金额")]
    //    public decimal ReturnMoney { get; set; }

    //    [NumberField("赠送比例", 0, 100)]
    //    public decimal ReturnRate { get; set; }

    //    [NumberField("充值最低金额")]
    //    public decimal MinMoney { get; set; }

    //    [NumberField("充值最高金额")]
    //    public decimal MaxMoney { get; set; } = 100000;

    //    public override BaseButton[] Buttons()
    //    {
    //        return new[] { new ActionButton("Save", "保存"), };
    //    }

    //    public virtual void Save()
    //    {
    //        Context.GiftEvents.Add(new GiftEvent(BeginTime, EndTime, ReceivingType, Rule,
    //            ReturnMoney, ReturnRate / 100, MinMoney, MaxMoney));
    //        UserOperateLogService.Log($"新增充值活动,开始时间[{BeginTime.ToDateString()}],结束时间[{EndTime.ToDateString()}],赠送对象[{ReceivingType.ToDescriptionString()}],赠送规则[{Rule.ToDescriptionString()}],赠送金额[{ReturnMoney}],赠送比例[{ReturnRate}],充值最低金额[{MinMoney}],充值最高金额[{MaxMoney}]", Context);
    //        Context.SaveChanges();
    //    }
    //}
}