using AllLottery.Business.Report;
using AllLottery.Business.Report.Team;
using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.ReportManager
{
    //public class ProxyRportViewModel : BaseServiceNav
    //{
    //    public override string NavName => "代理报表";

    //    public override string FolderName => "统计报表";

    //    [NavField("ID", IsKey = true, IsDisplay = false)]
    //    public int PlayerId { get; set; }

    //    [NavField("玩家", ActionType = "CeShi")]
    //    public string Name { get; set; }

    //    [NavField("当前团队余额")]
    //    public decimal TeamCoin { get; set; }

    //    [NavField("团队人数")]
    //    public int TeamCount { get; set; }

    //    [NavField("团队充值金额")]
    //    public decimal TeamRechargeMoney { get; set; }

    //    [NavField("团队提现金额")]
    //    public decimal TeamWithdrawMoney { get; set; }

    //    [NavField("团队投注金额")]
    //    public decimal TeamBetMoney { get; set; }

    //    [NavField("团队中奖金额")]
    //    public decimal TeamWinMoney { get; set; }

    //    [NavField("团队投注亏盈")]
    //    public decimal TeamBetWinMoney { get; set; }

    //    protected override object[] DoGetNavDatas()
    //    {
    //        int? id = null;
    //        if (Query.ContainsKey("PlayerId") && !string.IsNullOrEmpty(Query["PlayerId"]))
    //        {
    //            id = int.Parse(Query["PlayerId"]);
    //        }
    //        List<int> ids = new List<int>();
    //        if (id == null)
    //        {
    //            ids = Context.NotReportPlayers.Select(t => t.PlayerId).ToList();
    //        }
    //        var begin = new DateTime(2019, 1, 1);
    //        var end = DateTime.Now.AddDays(1);
    //        if (Query.ContainsKey("Begin") && !string.IsNullOrEmpty(Query["Begin"]))
    //        {
    //            begin = DateTime.Parse(Query["Begin"]);
    //        }
    //        if (Query.ContainsKey("End") && !string.IsNullOrEmpty(Query["End"]))
    //        {
    //            end = DateTime.Parse(Query["End"]).AddDays(1);
    //        }
    //        return CreateEfDatas<Player, ProxyRportViewModel>(from p in Context.Players where p.ParentPlayerId == id && (!ids.Contains(p.PlayerId) || p.Type == PlayerTypeEnum.Proxy) select p,
    //            (k, t) =>
    //            {
    //                var list = BaseReport.GetTeamPlayerIdsWhitoutSelf(k.PlayerId);
    //                list.Add(k.PlayerId);
    //                t.TeamCoin = (from p in Context.Players where p.IsEnable && list.Contains(p.PlayerId) select p.Coin)
    //                    .Sum();
    //                t.TeamCount = list.Count;
    //                t.TeamRechargeMoney = new TeamRechargeReport(k.PlayerId).GetReportData(begin, end);
    //                t.TeamWithdrawMoney = new TeamWithdrawReport(k.PlayerId).GetReportData(begin, end);
    //                t.TeamBetMoney = new TeamBetMoneyReport(k.PlayerId).GetReportData(begin, end);
    //                t.TeamWinMoney = new TeamWinMoneyReport(k.PlayerId).GetReportData(begin, end);
    //                t.TeamBetWinMoney = t.TeamWinMoney - t.TeamBetMoney;
    //            }, (k, w) => w, (k, w) => w.Where(t => t.Name.Contains(k)));
    //    }

    //    public override BaseFieldAttribute[] GetQueryConditionses()
    //    {
    //        return new BaseFieldAttribute[] { new HiddenTextFieldAttribute("PlayerId", "玩家"), new TextFieldAttribute("Name", "玩家"), new DateTimeFieldAttribute("Begin", "开始时间"), new DateTimeFieldAttribute("End", "结束时间"), };
    //    }

    //    public override BaseButton[] CreateViewButtons()
    //    {
    //        return new BaseButton[] { new JsActionButton("Up", "返回上级", "UpToTop"), };
    //    }
    //}
}