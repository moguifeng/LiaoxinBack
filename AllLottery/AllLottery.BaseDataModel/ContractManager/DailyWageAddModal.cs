using AllLottery.IBusiness;
using AllLottery.Model;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.ContractManager
{
    public class DailyWageAddModal : BaseServiceModal
    {
        public DailyWageAddModal()
        {
        }

        public DailyWageAddModal(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "新增日工资配置";

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
            return new[] { new ActionButton("Save", "保存"), };
        }

        public void Save()
        {
            Context.DailyWages.Add(new DailyWage() { MenCount = MenCount, BetMoney = BetMoney, Rate = Rate / 100 });
            UserOperateLogService.Log($"新增日工资设置,最低有效人数为[{MenCount}],最低消费金额为[{BetMoney}],发放比例为[{Rate}]", Context);
            Context.SaveChanges();
        }
    }
}