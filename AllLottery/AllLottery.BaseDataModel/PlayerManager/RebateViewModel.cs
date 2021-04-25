using AllLottery.Model;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.Common;

namespace AllLottery.BaseDataModel.PlayerManager
{
    //public class RebateViewModel : BaseServiceNav
    //{
    //    public override string NavName => "返点查询";

    //    public override string FolderName => "玩家管理";

    //    [NavField("返点玩家")]
    //    public string PlayerName { get; set; }

    //    [NavField("上级")]
    //    public string PName { get; set; }

    //    [NavField("投注单号")]
    //    public string Order { get; set; }

    //    [NavField("彩种")]
    //    public string LotteryType { get; set; }

    //    [NavField("投注期号")]
    //    public string Number { get; set; }

    //    [NavField("投注金额")]
    //    public decimal BetMoney { get; set; }

    //    [NavField("返点金额")]
    //    public decimal RebateMoney { get; set; }

    //    [NavField("返点比例差")]
    //    public string DiffRateStr { get; set; }

    //    protected override object[] DoGetNavDatas()
    //    {
    //        return CreateEfDatas<RebateLog, RebateViewModel>(
    //            from r in Context.RebateLogs where r.IsEnable orderby r.CreateTime descending select r,
    //            (k, t) =>
    //            {
    //                t.PlayerName = k.Player.Name;
    //                t.PName = k.Player.ParentPlayer?.Name;
    //                t.Order = k.Bet.Order;
    //                t.LotteryType = k.Bet.LotteryPlayDetail.LotteryPlayType.LotteryType.Name;
    //                t.Number = k.Bet.LotteryIssuseNo;
    //                t.BetMoney = k.Bet.BetMoney;
    //                t.DiffRateStr = k.DiffRate.ToPercenString();
    //            }, (k, w) => w.Where(t => t.Player.Name.Contains(k)),
    //            (k, w) => w.Where(t => t.Player.ParentPlayerId != null && t.Player.ParentPlayer.Name.Contains(k)),
    //            (k, w) => w.Where(t => t.Bet.Order.Contains(k)),
    //            (k, w) => w.Where(t => t.Bet.LotteryPlayDetail.LotteryPlayType.LotteryType.Name.Contains(k)),
    //            (k, w) => w.Where(t => t.Bet.LotteryIssuseNo.Contains(k)));
    //    }

    //    public override BaseFieldAttribute[] GetQueryConditionses()
    //    {
    //        return new[]
    //        {
    //            new TextFieldAttribute("Name", "投注玩家"), new TextFieldAttribute("PName", "上级"),
    //            new TextFieldAttribute("Order", "投注单号"), new TextFieldAttribute("Type", "彩种"),
    //            new TextFieldAttribute("Number", "期号")
    //        };
    //    }
    //}
}