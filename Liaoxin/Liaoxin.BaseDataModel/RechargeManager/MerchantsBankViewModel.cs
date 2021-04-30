using Liaoxin.IBusiness;
using Liaoxin.Model;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model;
using Zzb.BaseData.Model.Button;

namespace Liaoxin.BaseDataModel.RechargeManager
{
    //public class MerchantsBankViewModel : BaseServiceNav
    //{
    //    public IUserOperateLogService UserOperateLogService { get; set; }

    //    public override string NavName => "收款银行设置";

    //    public override string FolderName => "充值管理";

    //    [NavField("主键", IsKey = true, IsDisplay = false)]
    //    public int MerchantsBankId { get; set; }

    //    [NavField("标题")]
    //    public string Name { get; set; }

    //    [NavField("收款类型")]
    //    public MerchantsBankTypeEnum Type { get; set; }

    //    [NavField("最小充值金额")]
    //    public decimal Min { get; set; }

    //    [NavField("最大充值金额")]
    //    public decimal Max { get; set; }

    //    [NavField("排序")]
    //    public int IndexSort { get; set; }

    //    [NavField("是否启动")]
    //    public bool IsUseful { get; set; } = true;

    //    protected override object[] DoGetNavDatas()
    //    {
    //        return CreateEfDatas<MerchantsBank, MerchantsBankViewModel>(from b in Context.MerchantsBanks
    //                                                                    where b.IsEnable
    //                                                                    select b);
    //    }

    //    public override BaseButton[] CreateViewButtons()
    //    {
    //        return new BaseModal[] { new MerchantsBankScanModal("AddScan", "新增扫码"), new MerchantsBankNetCollectionModal("AddNetCollection", "新增转账"), new MerchantsBankThirdPayModal("AddThirdPay", "新增第三方") };
    //    }

    //    public override BaseButton[] CreateRowButtons()
    //    {
    //        List<BaseButton> list = new List<BaseButton>();
    //        switch (Type)
    //        {
    //            case MerchantsBankTypeEnum.Scan:
    //                list.Add(new MerchantsBankScanModal("Edit", "编辑"));
    //                break;
    //            case MerchantsBankTypeEnum.NetCollection:
    //                list.Add(new MerchantsBankNetCollectionModal("Edit", "编辑"));
    //                break;
    //            case MerchantsBankTypeEnum.ThirdPay:
    //                list.Add(new MerchantsBankThirdPayModal("Edit", "编辑"));
    //                break;
    //            default:
    //                break;
    //        }
    //        list.Add(new ConfirmActionButton("Delete", "删除", "是否确定删除?"));
    //        return list.ToArray();
    //    }

    //    public void Delete()
    //    {
    //        var exist = (from m in Context.MerchantsBanks where m.MerchantsBankId == MerchantsBankId select m).First();
    //        exist.IsEnable = false;
    //        exist.Update();
    //        UserOperateLogService.Log($"删除收款银行[{exist.Name}]", Context);
    //        Context.SaveChanges();
    //    }
    //}
}