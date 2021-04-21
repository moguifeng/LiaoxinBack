using AllLottery.IBusiness;
using System.Linq;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.ContractManager
{
    public class DailyWageEditModal : BaseServiceModal
    {
        public DailyWageEditModal()
        {
        }

        public DailyWageEditModal(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "编辑日工资";

        [HiddenTextField]
        public int DailyWageId { get; set; }

        [NumberField("最低有效人数")]
        public int MenCount { get; set; }

        /// <summary>
        /// 总额
        /// </summary>
        [NumberField("最低消费金额")]
        public decimal BetMoney { get; set; }

        /// <summary>
        /// 发放比例
        /// </summary>
        [NumberField("发放比例")]
        public decimal Rate { get; set; }

        public override BaseButton[] Buttons()
        {
            return new BaseButton[] { new ActionButton("Save", "保存"), };
        }

        public void Save()
        {
            var exist = (from d in Context.DailyWages where d.DailyWageId == DailyWageId select d).First();
            UserOperateLogService.Log($"编辑日工资配置{MvcHelper.LogDifferent(new LogDifferentViewModel(exist.MenCount.ToString(), MenCount.ToString(), "最低有效人数"), new LogDifferentViewModel(exist.BetMoney.ToDecimalString(), BetMoney.ToDecimalString(), "最低消费金额"), new LogDifferentViewModel(exist.Rate.ToDecimalString(), Rate.ToDecimalString(), "发放比例"))}", Context);
            exist.MenCount = MenCount;
            exist.BetMoney = BetMoney;
            exist.Rate = Rate / 100;
            Context.SaveChanges();
        }
    }
}