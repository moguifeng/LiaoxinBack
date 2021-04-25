using AllLottery.Model;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.PlayTypeManager
{
    //public class PlayTypeDetailViewModel : BaseServiceNav
    //{
    //    public override string NavName => "赔率设置";

    //    public override string FolderName => "玩法管理";

    //    [NavField("Id", IsKey = true, IsDisplay = false)]
    //    public int LotteryPlayDetailId { get; set; }

    //    [NavField("玩法")]
    //    public string Name { get; set; }

    //    [NavField("最低赔率")]
    //    public decimal MinOdds { get; set; }

    //    [NavField("最高赔率")]
    //    public decimal MaxOdds { get; set; }

    //    [NavField("单期投注限额")]
    //    public decimal MaxBetMoney { get; set; }

    //    [NavField("最高投注注数")]
    //    public int? MaxBetCount { get; set; }

    //    [NavField("描述", 400)]
    //    public string Description { get; set; }

    //    [NavField("排序")]
    //    public int SortIndex { get; set; }

    //    protected override object[] DoGetNavDatas()
    //    {
    //        return CreateEfDatas<LotteryPlayDetail, PlayTypeDetailViewModel>(from d in Context.LotteryPlayDetails orderby d.LotteryPlayType.SortIndex, d.SortIndex select d,
    //            (k, t) =>
    //            {
    //                t.Name = k.LotteryPlayType.Name + "-" + k.Name;
    //            }, (k, w) =>
    //            {
    //                int id = int.Parse(k);
    //                return w.Where(t => t.LotteryPlayType.LotteryTypeId == id);
    //            }, (k, w) => w.Where(t => t.LotteryPlayType.Name.Contains(k) || t.Name.Contains(k)));
    //    }

    //    public override BaseFieldAttribute[] GetQueryConditionses()
    //    {
    //        return new BaseFieldAttribute[] { new LotteryTypeDropListFieldAttribute("Type", "彩种"), new TextFieldAttribute("Text", "玩法"), };
    //    }

    //    public override BaseButton[] CreateRowButtons()
    //    {
    //        return new[] { new PlayTypeDetailEditModal("Edit", "编辑"), };
    //    }
    //}
}