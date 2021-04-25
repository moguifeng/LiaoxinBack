using AllLottery.IBusiness;
using AllLottery.Model;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.ContractManager
{
    //public class DividendSettingAddModal : BaseServiceModal
    //{
    //    public IUserOperateLogService UserOperateLogService { get; set; }

    //    public DividendSettingAddModal()
    //    {
    //    }

    //    public DividendSettingAddModal(string id, string name) : base(id, name)
    //    {
    //    }

    //    public override string ModalName => "新增分红设置";

    //    [NumberField("有效人数", Min = 0)]
    //    public int MenCount { get; set; }

    //    [NumberField("投注达标金额", Min = 0)]
    //    public decimal BetMoney { get; set; }

    //    [NumberField("亏损达标金额", Min = 0)]
    //    public decimal LostMoney { get; set; }

    //    [NumberField("发放比例(%)", Min = 0)]
    //    public decimal Rate { get; set; }

    //    public override BaseButton[] Buttons()
    //    {
    //        return new BaseButton[] { new ActionButton("Save", "保存"), };
    //    }

    //    public void Save()
    //    {
    //        UserOperateLogService.Log($"新增分红设置:有效人数[{MenCount}]，投注达标金额[{BetMoney}]，亏损达标金额[{LostMoney}]，发放比例(%)[{Rate}]", Context);
    //        Context.DividendSettings.Add(new DividendSetting(MenCount, BetMoney, LostMoney, Rate));
    //        Context.SaveChanges();
    //    }
    //}
}