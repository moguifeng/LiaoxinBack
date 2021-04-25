using AllLottery.IBusiness;
using System.Linq;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.PlayTypeManager
{
    //public class PlayTypeEditModal : BaseServiceModal
    //{
    //    public PlayTypeEditModal()
    //    {
    //    }

    //    public PlayTypeEditModal(string id, string name) : base(id, name)
    //    {
    //    }

    //    public IUserOperateLogService UserOperateLogService { get; set; }

    //    public override string ModalName => "玩法编辑";

    //    [HiddenTextField]
    //    public int LotteryPlayTypeId { get; set; }

    //    [NumberField("排序")]
    //    public int SortIndex { get; set; }

    //    [DropListField("是否启动")]
    //    public bool IsEnable { get; set; }

    //    public override BaseButton[] Buttons()
    //    {
    //        return new[] { new ActionButton("Save", "保存"), };
    //    }

    //    public void Save()
    //    {
    //        var play = (from p in Context.LotteryPlayTypes where p.LotteryPlayTypeId == LotteryPlayTypeId select p)
    //            .First();
    //        play.SortIndex = SortIndex;
    //        play.IsEnable = IsEnable;
    //        play.Update();
    //        UserOperateLogService.Log($"修改[{play.LotteryType.Name}-{play.Name}]玩法，排序为[{SortIndex}]，是否启动[{IsEnable}]", Context);
    //        Context.SaveChanges();
    //    }
    //}
}