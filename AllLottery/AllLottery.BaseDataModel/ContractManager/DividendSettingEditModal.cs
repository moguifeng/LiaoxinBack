using AllLottery.IBusiness;
using System.Linq;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.ContractManager
{
    public class DividendSettingEditModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public DividendSettingEditModal()
        {
        }

        public DividendSettingEditModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "修改分红设置";

        [HiddenTextField]
        public int DividendSettingId { get; set; }

        [NumberField("有效人数", Min = 0)]
        public int MenCount { get; set; }

        [NumberField("投注达标金额", Min = 0)]
        public decimal BetMoney { get; set; }

        [NumberField("亏损达标金额", Min = 0)]
        public decimal LostMoney { get; set; }

        [NumberField("发放比例(%)", Min = 0)]
        public decimal Rate { get; set; }

        public override BaseButton[] Buttons()
        {
            return new BaseButton[] { new ActionButton("Save", "保存"), };
        }

        public void Save()
        {
            var exist = (from d in Context.DividendSettings where d.DividendSettingId == DividendSettingId select d).First();
            exist.MenCount = MenCount;
            exist.BetMoney = BetMoney;
            exist.LostMoney = LostMoney;
            exist.Rate = Rate;
            exist.DividendSettingId = DividendSettingId;
            exist.Update();
            UserOperateLogService.Log($"修改分红设置:有效人数[{MenCount}]，投注达标金额[{BetMoney}]，亏损达标金额[{LostMoney}]，发放比例(%)[{Rate}]，ID为[{DividendSettingId}]", Context);
            Context.SaveChanges();
        }
    }
}