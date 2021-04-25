using AllLottery.IBusiness;
using AllLottery.Model;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.ContractManager
{
    //public class DividendSettingViewModel : BaseServiceNav
    //{
    //    public IUserOperateLogService UserOperateLogService { get; set; }

    //    public override string NavName => "分红设置";

    //    public override string FolderName => "工资分红管理";

    //    [NavField("主键", IsKey = true, IsDisplay = false)]
    //    public int DividendSettingId { get; set; }

    //    /// <summary>
    //    /// 有效人数
    //    /// </summary>
    //    [NavField("有效人数")]
    //    public int MenCount { get; set; }

    //    /// <summary>
    //    /// 总额
    //    /// </summary>
    //    [NavField("投注总额")]
    //    public decimal BetMoney { get; set; }

    //    /// <summary>
    //    /// 亏盈
    //    /// </summary>
    //    [NavField("亏损")]
    //    public decimal LostMoney { get; set; }

    //    /// <summary>
    //    /// 发放比例
    //    /// </summary>
    //    [NavField("发放比例", IsDisplay = false)]
    //    public decimal Rate { get; set; }

    //    /// <summary>
    //    /// 发放比例
    //    /// </summary>
    //    [NavField("发放比例")]
    //    public string RateStr { get; set; }

    //    protected override object[] DoGetNavDatas()
    //    {
    //        return CreateEfDatas<DividendSetting, DividendSettingViewModel>(from d in Context.DividendSettings
    //                                                                        where d.IsEnable
    //                                                                        orderby d.Rate descending
    //                                                                        select d, (k, t) =>
    //             {
    //                 t.RateStr = k.Rate.ToPercenString();
    //             });
    //    }

    //    public override BaseButton[] CreateViewButtons()
    //    {
    //        return new BaseModal[] { new DividendSettingAddModal("Add", "新增"), };
    //    }

    //    public override BaseButton[] CreateRowButtons()
    //    {
    //        return new BaseButton[] { new ConfirmActionButton("Delete", "删除", "是否确定删除"), new DividendSettingEditModal("Edit", "修改"), };
    //    }

    //    public void Delete()
    //    {
    //        UserOperateLogService.Log($"删除分红设置，主键为[{DividendSettingId}]", Context);
    //        var setting = (from d in Context.DividendSettings where d.DividendSettingId == DividendSettingId select d)
    //            .First();
    //        setting.IsEnable = false;
    //        setting.Update();
    //        Context.SaveChanges();
    //    }
    //}
}