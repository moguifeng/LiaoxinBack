using AllLottery.IBusiness;
using AllLottery.Model;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.ContractManager
{
    public class DailyWageViewModel : BaseServiceNav
    {
        public override string NavName => "日工资设置";

        public override string FolderName => "工资分红管理";

        public IUserOperateLogService UserOperateLogService { get; set; }

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public int DailyWageId { get; set; }

        [NavField("最低有效人数")]
        public int MenCount { get; set; }

        /// <summary>
        /// 总额
        /// </summary>
        [NavField("最低消费金额")]
        public decimal BetMoney { get; set; }

        /// <summary>
        /// 发放比例
        /// </summary>
        [NavField("发放比例", IsDisplay = false)]
        public decimal Rate { get; set; }

        [NavField("发放比例")]
        public string RateStr { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<DailyWage, DailyWageViewModel>(from d in Context.DailyWages where d.IsEnable select d,
                (k, t) =>
                {
                    t.Rate = k.Rate * 100;
                    t.RateStr = (k.Rate * 100).ToDecimalString() + "%";
                });
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new[] { new DailyWageAddModal("Add", "新增设置"), };
        }

        public override BaseButton[] CreateRowButtons()
        {
            return new BaseButton[] { new DailyWageEditModal("Edit", "编辑设置"), new ConfirmActionButton("Delete", "删除", "是否确定删除？"), };
        }

        public void Delete()
        {
            var exist = (from d in Context.DailyWages where d.DailyWageId == DailyWageId select d).First();
            exist.IsEnable = false;
            UserOperateLogService.Log($"删除日工资配置，主键为[{exist.DailyWageId}]", Context);
            Context.SaveChanges();
        }
    }
}