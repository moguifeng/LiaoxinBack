using AllLottery.Business.Report.Self;
using AllLottery.Business.Socket;
using AllLottery.IBusiness;
using AllLottery.Model;
using AllLottery.ViewModel;
using System;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.PlayerManager
{
    public class PlayerViewModel : BaseServiceNav
    {
        public override string NavName => "玩家管理";

        public override string FolderName => "玩家管理";

        public IUserOperateLogService UserOperateLogService { get; set; }

        [NavField("主键", false, IsKey = true)]
        public int PlayerId { get; set; }

        [NavField("玩家")]
        public string Name { get; set; }

        [NavField("上级")]
        public string ParentName { get; set; }

        [NavField("余额")]
        public decimal Coin { get; set; }

        [NavField("冻结资金")]
        public decimal FCoin { get; set; }

        [NavField("剩余消费")]
        public decimal LastBetMoney { get; set; }

        [NavField("玩家类型")]
        public PlayerTypeEnum Type { get; set; }

        [NavField("是否在线")]
        public bool IsOnline { get; set; }

        [NavField("返点", IsDisplay = false)]
        public decimal Rebate { get; set; }

        [NavField("返点")]
        public string RebateString { get; set; }

        [NavField("标准日工资", IsDisplay = false)]
        public decimal? DailyWageRate { get; set; }

        [NavField("标准日工资")]
        public string DailyWageRateString { get; set; }

        [NavField("分红比例", IsDisplay = false)]
        public decimal? DividendRate { get; set; }

        [NavField("分红比例")]
        public string DividendRateString { get; set; }

        [NavField("注册时间", 150)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [NavField("手机")]
        public string Phone { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        [NavField("QQ")]
        public string QQ { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        [NavField("微信")]
        public string WeChat { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        [NavField("充值金额")]
        public decimal RechargeMoney { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        [NavField("提现金额")]
        public decimal WithdrawMoney { get; set; }

        /// <summary>
        /// 投注金额
        /// </summary>
        [NavField("投注金额")]
        public decimal BetMoney { get; set; }

        /// <summary>
        /// 中奖金额
        /// </summary>
        [NavField("中奖金额")]
        public decimal WinMoney { get; set; }

        [NavField("玩家总亏盈")]
        public decimal AllWinMoney { get; set; }

        [NavField("是否冻结")]
        public bool IsFreeze { get; set; }

        [NavField("是否可以提款")]
        public bool CanWithdraw { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<Player, PlayerViewModel>(from p in Context.Players where p.IsEnable && p.Type != PlayerTypeEnum.TestPlay orderby p.CreateTime descending select p,
                (k, t) =>
                {
                    t.ParentName = k.ParentPlayer?.Name;
                    if (t.DailyWageRate != null)
                    {
                        t.DailyWageRate *= 100;
                    }
                    t.DailyWageRateString = k.DailyWageRate.HasValue ? (k.DailyWageRate.Value * 100).ToString("0.##") + "%" : "";
                    if (t.DividendRate != null)
                    {
                        t.DividendRate *= 100;
                    }
                    t.Rebate = k.Rebate * 100;
                    t.DividendRateString = k.DividendRate.HasValue ? (k.DividendRate.Value * 100).ToString("0.##") + "%" : "";
                    t.RebateString = (k.Rebate * 100).ToString("0.##") + "%";

                    t.IsOnline = new PlayerSocketMiddleware(null).IsConnect(k.PlayerId);
                    t.RechargeMoney = new RechargeReport(k.PlayerId).GetReportData();
                    t.WithdrawMoney = new WithdrawReport(k.PlayerId).GetReportData();
                    t.BetMoney = new BetMoneyReport(k.PlayerId).GetReportData();
                    t.WinMoney = new WinMoneyReport(k.PlayerId).GetReportData();
                    t.AllWinMoney = t.WithdrawMoney + t.Coin + t.FCoin - t.RechargeMoney;
                },
                (k, w) => w.Where(t => t.Name.Contains(k)),
                (k, w) => w.Where(t => t.ParentPlayer != null && t.ParentPlayer.Name.Contains(k)),
                (k, w) => ConvertEnum<Player, TrueFalseEnum>(w, k, p =>
                     {
                         var list = new PlayerSocketMiddleware(null).ConnectList();
                         if (p == TrueFalseEnum.True)
                         {
                             return w.Where(t => list.Contains(t.PlayerId));
                         }
                         else
                         {
                             return w.Where(t => !list.Contains(t.PlayerId));
                         }
                     }), (k, w) => w.Where(t => t.WeChat.Contains(k)), (k, w) => w.Where(t => t.QQ.Contains(k)), (k, w) => w.Where(t => t.Phone.Contains(k)));
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[] { new TextFieldAttribute("Name", "玩家"), new TextFieldAttribute("Parent", "父级"), new DropListFieldAttribute("IsOnline", "是否在线", TrueFalseEnum.True.GetDropListModels("全部")), new TextFieldAttribute("WeChat", "微信"), new TextFieldAttribute("QQ", "QQ"), new TextFieldAttribute("Phone", "手机"), };
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new[] { new PlayerAddViewModel("Add", "新建") { Icon = "plus" } };
        }

        public override BaseButton[] CreateRowButtons()
        {
            return new BaseButton[] { new PlayerEditViewModel("Edit", "编辑"), new ConfirmActionButton("Delete", "删除", "是否确定删除？"), };
        }

        public override bool ShowOperaColumn => true;

        public void Delete()
        {
            var player = (from p in Context.Players where p.PlayerId == PlayerId select p).First();
            player.IsEnable = false;
            player.Update();
            UserOperateLogService.Log($"删除[{player.Name}]玩家", Context);
            Context.SaveChanges();
        }
    }
}
