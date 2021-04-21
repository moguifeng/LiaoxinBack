using AllLottery.IBusiness;
using AllLottery.Model;
using System.Linq;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.PlayTypeManager
{
    public class PlayTypeDetailEditModal : BaseServiceModal
    {
        public PlayTypeDetailEditModal()
        {
        }

        public PlayTypeDetailEditModal(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "编辑玩法细节";

        [HiddenTextField]
        public int LotteryPlayDetailId { get; set; }

        [TextField("玩法", IsReadOnly = true)]
        public string Name { get; set; }

        [NumberField("最低赔率", Min = 0)]
        public decimal MinOdds { get; set; }

        [NumberField("最高赔率", Min = 0)]
        public decimal MaxOdds { get; set; }

        [NumberField("单期投注限额", Min = 0)]
        public decimal MaxBetMoney { get; set; }

        [NumberField("最高投注注数", Min = 1)]
        public int? MaxBetCount { get; set; }

        [TextField("描述")]
        public string Description { get; set; }

        [NumberField("排序")]
        public int SortIndex { get; set; }

        [DropListField("修改所有彩种")]
        public bool IsAll { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public void Save()
        {
            var detail = (from d in Context.LotteryPlayDetails
                          where d.LotteryPlayDetailId == LotteryPlayDetailId
                          select d).First();
            if (IsAll)
            {
                var details = (from d in Context.LotteryPlayDetails
                               where d.Name == detail.Name && d.LotteryPlayType.Name == detail.LotteryPlayType.Name &&
                                     d.LotteryPlayType.LotteryType.LotteryClassify.Type ==
                                     detail.LotteryPlayType.LotteryType.LotteryClassify.Type
                               select d);
                foreach (LotteryPlayDetail playDetail in details)
                {
                    playDetail.MinOdds = MinOdds;
                    playDetail.MaxOdds = MaxOdds;
                    playDetail.MaxBetMoney = MaxBetMoney;
                    playDetail.Description = Description;
                    playDetail.SortIndex = SortIndex;
                    playDetail.MaxBetCount = MaxBetCount;
                    playDetail.Update();
                }
                UserOperateLogService.Log($"修改所有[{detail.LotteryPlayType.LotteryType.LotteryClassify.Type.ToDescriptionString()}]彩种的玩法[{Name}]:最低赔率为[{MinOdds.ToDecimalString()}],最高赔率为[{MaxOdds.ToDecimalString()}],单期投注限额为[{MaxBetMoney.ToDecimalString()}],描述为[{Description}],排序为[{SortIndex}]", Context);
            }
            else
            {
                UserOperateLogService.Log($"编辑[{detail.LotteryPlayType.LotteryType.Name}]的玩法[{Name}]:{MvcHelper.LogDifferent(new LogDifferentViewModel(detail.MinOdds.ToDecimalString(), MinOdds.ToDecimalString(), "最低赔率"), new LogDifferentViewModel(detail.MaxOdds.ToDecimalString(), MaxOdds.ToDecimalString(), "最高赔率"), new LogDifferentViewModel(detail.MaxBetMoney.ToDecimalString(), MaxBetMoney.ToDecimalString(), "单期投注限额"), new LogDifferentViewModel(detail.Description, Description, "描述"), new LogDifferentViewModel(detail.SortIndex.ToString(), SortIndex.ToString(), "排序")).Trim(',')}", Context);
                detail.MinOdds = MinOdds;
                detail.MaxOdds = MaxOdds;
                detail.MaxBetMoney = MaxBetMoney;
                detail.Description = Description;
                detail.SortIndex = SortIndex;
                detail.MaxBetCount = MaxBetCount;
                detail.Update();
            }
            Context.SaveChanges();
        }
    }
}