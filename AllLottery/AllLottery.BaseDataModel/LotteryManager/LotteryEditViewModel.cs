using AllLottery.IBusiness;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.LotteryManager
{
    public class LotteryEditViewModel : BaseServiceModal
    {
        public LotteryEditViewModel()
        {
        }

        public LotteryEditViewModel(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "编辑彩种";

        [HiddenTextField]
        public int LotteryTypeId { get; set; }

        [TextField("彩种名称")]
        public string Name { get; set; }

        [ImageField("彩种图片", BackColor = "#7474e4")]
        public int? IconId { get; set; }

        [DropListField("是否维护")]
        public bool IsStop { get; set; }

        [DropListField("是否热门")]
        public bool IsHot { get; set; }

        [NumberField("盈利比")]
        public decimal WinRate { get; set; }

        [NumberField("排序")]
        public int SortIndex { get; set; }

        public override void Init()
        {
            var type = (from t in Context.LotteryTypes where t.LotteryTypeId == LotteryTypeId select t)
                .First();
            IconId = type.IconId;
        }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            if (IconId == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "请上传彩种图片");
            }
            var type = (from t in Context.LotteryTypes where t.LotteryTypeId == LotteryTypeId select t)
                .First();
            type.Update();
            type.IconId = IconId.Value;
            type.Name = Name;
            type.IsStop = IsStop;
            type.IsHot = IsHot;
            type.SortIndex = SortIndex;
            type.WinRate = WinRate / 100;
            UserOperateLogService.Log($"编辑彩种{MvcHelper.LogDifferent(new LogDifferentViewModel(type.Name, Name, "彩种名称"), new LogDifferentViewModel(type.IsStop.ToString(), IsStop.ToString(), "是否维护"), new LogDifferentViewModel(type.SortIndex.ToString(), SortIndex.ToString(), "排序"), new LogDifferentViewModel(type.IsHot.ToString(), IsHot.ToString(), "是否热门"))}", Context);
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}