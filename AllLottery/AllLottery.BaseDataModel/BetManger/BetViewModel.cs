using AllLottery.Model;
using AllLottery.ViewModel;
using System;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.BetManger
{
    //public class BetViewModel : BaseServiceNav
    //{
    //    public override string NavName => "投注管理";

    //    public override string FolderName => "投注管理";

    //    public override bool NavIsShow => true;

    //    [NavField("主键", IsKey = true, IsDisplay = false)]
    //    public int BetId { get; set; }

    //    [NavField("投注用户")]
    //    public string PlayerName { get; set; }

    //    [NavField("余额")]
    //    public decimal Coin { get; set; }

    //    [NavField("彩种")]
    //    public string LotteryType { get; set; }

    //    [NavField("玩法")]
    //    public string PlayType { get; set; }

    //    [NavField("期号")]
    //    public string LotteryIssuseNo { get; set; }

    //    [NavField("投注号码", Width = 200)]
    //    public string BetNo { get; set; }

    //    [NavField("投注金额")]
    //    public decimal BetMoney { get; set; }

    //    [NavField("状态")]
    //    public BetStatusEnum Status { get; set; }

    //    [NavField("中奖金额")]
    //    public decimal WinMoney { get; set; }

    //    [NavField("开奖号码")]
    //    public string OpenData { get; set; }

    //    [NavField("投注时间", Width = 150)]
    //    public DateTime CreateTime { get; set; }

    //    [NavField("倍数")]
    //    public int Times { get; set; }

    //    [NavField("注数")]
    //    public int BetCount { get; set; }

    //    [NavField("投注模式")]
    //    public string BetModeName { get; set; }

    //    [NavField("订单号", Width = 150)]
    //    public string Order { get; set; }

    //    protected override object[] DoGetNavDatas()
    //    {

    //        IQueryable<Bet> query = null;
    //        if ((Query.ContainsKey("IsShowTestPlayer") && bool.Parse(Query["IsShowTestPlayer"])) || !Query.ContainsKey("IsShowTestPlayer"))
    //        {
    //            query = from b in Context.Bets where b.IsEnable && b.Player.Type != PlayerTypeEnum.TestPlay orderby b.CreateTime descending select b;
    //        }
    //        else
    //        {
    //            query = from b in Context.Bets where b.IsEnable && b.Player.Type != PlayerTypeEnum.TestPlay && !(from d in Context.NotReportPlayers select d.PlayerId).Contains(b.PlayerId) orderby b.CreateTime descending select b;
    //        }
    //        return CreateEfDatas<Bet, BetViewModel>(
    //           query,
    //    (k, t) =>
    //    {
    //        t.PlayerName = k.Player.Name;
    //        t.LotteryType = k.LotteryPlayDetail.LotteryPlayType.LotteryType.Name;
    //        t.PlayType = k.LotteryPlayDetail.LotteryPlayType.Name + "-" + k.LotteryPlayDetail.Name;
    //        t.OpenData = k.LotteryData?.Data;
    //        t.Coin = k.Player.Coin;
    //        t.BetModeName = k.BetMode.Name;
    //    }, (k, w) => w.Where(t => t.Order.Contains(k)),
    //    (k, w) => w.Where(t => t.Player.Name.Contains(k)),
    //    (k, w) => w.Where(t => t.LotteryPlayDetail.LotteryPlayType.LotteryType.Name.Contains(k)),
    //    (k, w) => w.Where(t => t.LotteryPlayDetail.Name.Contains(k) || t.LotteryPlayDetail.LotteryPlayType.Name.Contains(k)),
    //    (k, w) => w.Where(t => t.LotteryIssuseNo.Contains(k)),
    //    (k, w) => ConvertEnum<Bet, BetStatusEnum>(w, k, m => w.Where(t => t.Status == m))

    //    );
    //    }

    //    public override BaseFieldAttribute[] GetQueryConditionses()
    //    {
    //        return new BaseFieldAttribute[]
    //        {
    //            new TextFieldAttribute("Order", "订单号"), new TextFieldAttribute("Name", "投注用户"),
    //            new TextFieldAttribute("Type", "彩种"), new TextFieldAttribute("PlayType", "玩法"),
    //            new TextFieldAttribute("Iss", "期号"), new DropListFieldAttribute("status", "状态",BetStatusEnum.Win.GetDropListModels("全部")),
    //            new DropListFieldAttribute("IsShowTestPlayer", "是否显示未统计玩家", TrueFalseEnum.True.GetDropListModels())
    //        };
    //    }

    //    public override BaseButton[] CreateRowButtons()
    //    {
    //        if (Status == BetStatusEnum.Wait || Status == BetStatusEnum.Error)
    //        {
    //            return new[] { new ConfirmActionButton("Cancel", "撤单", "是否确定撤单？"), };
    //        }
    //        return null;
    //    }

    //    public ServiceResult Cancel()   
    //    {
    //        var exist = (from b in Context.Bets where b.BetId == BetId select b).First();
    //        if (exist.Status != BetStatusEnum.Wait && exist.Status != BetStatusEnum.Error)
    //        {
    //            return new ServiceResult(ServiceResultCode.Error, "该投注单状态已经改变，不能撤单。");
    //        }
    //        exist.Status = BetStatusEnum.Revoke;
    //        exist.Update();
    //        exist.Player.AddMoney(exist.BetMoney, CoinLogTypeEnum.Revoke, exist.BetId, out var log, $"撤单投注单号[{exist.LotteryIssuseNo}]");
    //        Context.CoinLogs.Add(log);
    //        Context.SaveChanges();
    //        return new ServiceResult();
    //    }
    //}
}