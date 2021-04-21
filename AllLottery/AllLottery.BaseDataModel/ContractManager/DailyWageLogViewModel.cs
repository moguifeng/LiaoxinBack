using AllLottery.Model;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;

namespace AllLottery.BaseDataModel.ContractManager
{
    public class DailyWageLogViewModel : BaseServiceNav
    {
        public override string NavName => "日工资发放记录";

        public override string FolderName => "工资分红管理";

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public int DailyWageLogId { get; set; }

        [NavField("发放人")]
        public string ParentName { get; set; }

        [NavField("收款人")]
        public string PlayerName { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        [NavField("日工资日期", 150)]
        public DateTime CalDate { get; set; }

        [NavField("日工资比率")]
        public string RateString { get; set; }

        [NavField("团队投注金额")]
        public decimal BetMoney { get; set; }

        /// <summary>
        /// 总投注人数
        /// </summary>
        [NavField("总投注人数")]
        public int BetMen { get; set; }

        /// <summary>
        /// 日工资总额
        /// </summary>
        [NavField("日工资金额")]
        public decimal DailyMoney { get; set; }

        [NavField("发放时间", 150)]
        public DateTime CreateTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<DailyWageLog, DailyWageLogViewModel>(from d in Context.DailyWageLogs where d.IsEnable orderby d.CreateTime descending select d,
                 (k, t) =>
                 {
                     t.ParentName = k.Player.ParentPlayer?.Name;
                     t.PlayerName = k.Player.Name;
                     t.RateString = (k.Rate * 100).ToString("0.#") + "%";
                 });
        }
    }
}