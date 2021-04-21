using AllLottery.Business.Report;
using AllLottery.Business.Report.All;
using AllLottery.IBusiness;
using AllLottery.Model;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.ReportManager
{
    public class NotReportPlayerViewModel : BaseServiceNav
    {
        public override string NavName => "不统计玩家";

        public override string FolderName => "统计报表";

        public IUserOperateLogService UserOperateLogService { get; set; }

        [NavField("主键", IsDisplay = false, IsKey = true)]
        public int NotReportPlayerId { get; set; }

        [NavField("玩家")]
        public string Name { get; set; }

        [NavField("父级")]
        public string PName { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<NotReportPlayer, NotReportPlayerViewModel>(from n in Context.NotReportPlayers
                                                                            where n.IsEnable
                                                                            select n, (k, t) =>
                {
                    t.Name = k.Player.Name;
                    t.PName = k.Player.ParentPlayer?.Name;
                }, (k, w) => w.Where(t => t.Player.Name.Contains(k)));
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new[] { new PlayerReportAddModal("Add", "新增"), };
        }

        public override BaseButton[] CreateRowButtons()
        {
            return new[] { new ConfirmActionButton("Delete", "删除", "是否确定删除？"), };
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new[] { new TextFieldAttribute("Name", "玩家"), };
        }

        public void Delete()
        {
            Context.NotReportPlayers.RemoveRange(from n in Context.NotReportPlayers where n.NotReportPlayerId == NotReportPlayerId select n);
            UserOperateLogService.Log($"删除不统计玩家[{Name}]", Context);
            new AllBetMoneyReport().Clear(Context);
            new AllGiftReport().Clear(Context);
            new AllRechargeReport().Clear(Context);
            new AllWinMoneyReport().Clear(Context);
            new AllWithdrawReport().Clear(Context);
            new LotteryBetReport(0).Clear(Context);
            new LotteryWinReport(0).Clear(Context);
            Context.SaveChanges();
        }
    }
}