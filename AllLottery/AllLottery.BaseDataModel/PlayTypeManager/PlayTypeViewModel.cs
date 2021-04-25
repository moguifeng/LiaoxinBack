using AllLottery.Model;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.PlayTypeManager
{
    //public class PlayTypeViewModel : BaseServiceNav
    //{
    //    public override string NavName => "总玩法管理";

    //    public override string FolderName => "玩法管理";

    //    [NavField("ID", IsKey = true, IsDisplay = false)]
    //    public int LotteryPlayTypeId { get; set; }

    //    [NavField("玩法名称")]
    //    public string Name { get; set; }

    //    [NavField("排序")]
    //    public int SortIndex { get; set; }

    //    [NavField("是否启动")]
    //    public bool IsEnable { get; set; }

    //    protected override object[] DoGetNavDatas()
    //    {
    //        return CreateEfDatas<LotteryPlayType, PlayTypeViewModel>(from t in Context.LotteryPlayTypes orderby t.SortIndex select t, (k, w) =>
    //            {
    //                var id = int.Parse(k);
    //                return w.Where(t => t.LotteryTypeId == id);
    //            });
    //    }

    //    public override BaseFieldAttribute[] GetQueryConditionses()
    //    {
    //        return new BaseFieldAttribute[] { new LotteryTypeDropListFieldAttribute("LotteryType", "彩种"), };
    //    }

    //    public override BaseButton[] CreateRowButtons()
    //    {
    //        return new BaseButton[] { new PlayTypeEditModal("Edit", "编辑"), };
    //    }
    //}
}