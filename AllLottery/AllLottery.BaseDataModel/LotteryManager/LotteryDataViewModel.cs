using AllLottery.Model;
using AllLottery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.LotteryManager
{
    //public class LotteryDataViewModel : BaseServiceNav
    //{
    //    public override string NavName => "开奖数据";

    //    public override string FolderName => "彩种管理";

    //    [NavField("主键", IsDisplay = false, IsKey = true)]
    //    public int LotteryDataId { get; set; }

    //    [NavField("期号")]
    //    public string Number { get; set; }

    //    [NavField("开奖号码", 200)]
    //    public string Data { get; set; }

    //    [NavField("开奖时间", 200)]
    //    public DateTime Time { get; set; }

    //    [NavField("是否已开奖")]
    //    public bool IsEnable { get; set; }

    //    protected override object[] DoGetNavDatas()
    //    {
    //        return CreateEfDatas<LotteryData, LotteryDataViewModel>(from d in Context.LotteryDatas
    //                                                                orderby d.Time descending
    //                                                                select d, (k, w) =>
    //            {
    //                var id = int.Parse(k);
    //                return w.Where(t => t.LotteryTypeId == id);
    //            }, (k, w) => ConvertEnum<LotteryData, TrueFalseEnum>(w, k, m => w.Where(t => t.IsEnable == (m == TrueFalseEnum.True))));
    //    }

    //    public override BaseFieldAttribute[] GetQueryConditionses()
    //    {
    //        return new[] { new LotteryTypeDropListFieldAttribute("Type", "彩种"), new DropListFieldAttribute("IsEnable", "是否已开奖", TrueFalseEnum.False.GetDropListModels("全部")), };
    //    }

    //    public override BaseButton[] CreateViewButtons()
    //    {
    //        List<BaseButton> list = new List<BaseButton>() { new LotteryDataAddModal("Add", "预设"), new OpenTimePresetModal("AddDay", "预设一天") };

    //        return list.ToArray();
    //    }

    //    public override BaseButton[] CreateRowButtons()
    //    {
    //        if (!IsEnable)
    //        {
    //            return new[] { new LotteryDataEditModal("Edit", "编辑"), };
    //        }

    //        return null;
    //    }

    //    public override bool ShowOperaColumn => true;
    //}
}