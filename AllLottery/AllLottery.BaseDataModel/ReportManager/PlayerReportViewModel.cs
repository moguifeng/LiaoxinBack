using AllLottery.Business.Report;
using AllLottery.Business.Report.Self;
using AllLottery.Model;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;

namespace AllLottery.BaseDataModel.ReportManager
{
    public class PlayerReportViewModel : BaseServiceNav
    {
        public override string NavName => "会员报表";

        public override string FolderName => "统计报表";

        [NavField("ID", IsKey = true, IsDisplay = false)]
        public int PlayerId { get; set; }

        [NavField("玩家")]
        public string Name { get; set; }

        [NavField("上级")]
        public string PName { get; set; }

        [NavField("当前余额")]
        public decimal Coin { get; set; }

        [NavField("充值金额")]
        public decimal RechargeMoney { get; set; }

        [NavField("提现金额")]
        public decimal WithdrawMoney { get; set; }

        [NavField("返点金额")]
        public decimal RebateMoney { get; set; }

        [NavField("活动金额")]
        public decimal GiftMoney { get; set; }

        [NavField("投注金额")]
        public decimal BetMoney { get; set; }

        [NavField("中奖金额")]
        public decimal WinMoney { get; set; }

        [NavField("投注亏盈")]
        public decimal BetWinMoney { get; set; }

        [NavField("总亏盈")]
        public decimal AllWinMoney { get; set; }


        protected override object[] DoGetNavDatas()
        {
            var begin = new DateTime(2019, 1, 1);
            var end = DateTime.Now;
            if (Query.ContainsKey("Begin") && !string.IsNullOrEmpty(Query["Begin"]))
            {
                begin = DateTime.Parse(Query["Begin"]);
            }
            if (Query.ContainsKey("End") && !string.IsNullOrEmpty(Query["End"]))
            {
                end = DateTime.Parse(Query["End"]).AddDays(1);
            }
            return CreateEfDatas<Player, PlayerReportViewModel>(from p in Context.Players where p.IsEnable && p.Type != PlayerTypeEnum.TestPlay select p,
                (k, t) =>
                {
                    t.PName = k.ParentPlayer?.Name;
                    t.RechargeMoney = new RechargeReport(k.PlayerId).GetReportData(begin, end);
                    t.WithdrawMoney = new WithdrawReport(k.PlayerId).GetReportData(begin, end);
                    t.BetMoney = new BetMoneyReport(k.PlayerId).GetReportData(begin, end);
                    t.WinMoney = new WinMoneyReport(k.PlayerId).GetReportData(begin, end);
                    t.RebateMoney = new RebateReport(k.PlayerId).GetReportData(begin, end);
                    t.GiftMoney = new GiftReport(k.PlayerId).GetReportData(begin, end);
                    t.BetWinMoney = t.WinMoney - t.BetMoney;
                    t.AllWinMoney = t.WithdrawMoney + t.Coin - t.RechargeMoney;

                }, (k, w) => w.Where(t => t.Name.Contains(k)));
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[] { new TextFieldAttribute("Name", "玩家"), new DateTimeFieldAttribute("Begin", "开始时间"), new DateTimeFieldAttribute("End", "结束时间") };
        }
    }
}