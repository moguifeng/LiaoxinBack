using AllLottery.Business.Report;
using AllLottery.Business.Report.All;
using AllLottery.IBusiness;
using AllLottery.Model;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.ReportManager
{
    public class PlayerReportAddModal : BaseServiceModal
    {
        public PlayerReportAddModal()
        {
        }

        public PlayerReportAddModal(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "新增";

        [TextField("玩家")]
        public string Name { get; set; }

        [DropListField("包括下级")]
        public bool IsUnder { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            var player = (from p in Context.Players where p.Name == Name select p).FirstOrDefault();
            if (player == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "未找到该玩家");
            }

            if (IsUnder)
            {
                var playerids = BaseReport.GetTeamPlayerIdsWhitoutSelf(player.PlayerId);
                playerids.Add(player.PlayerId);

                foreach (int playerid in playerids)
                {
                    var not =
                        (from p in Context.NotReportPlayers where p.PlayerId == playerid select p);
                    if (!not.Any())
                    {
                        Context.NotReportPlayers.Add(new NotReportPlayer() { PlayerId = playerid });
                    }
                }
            }
            else
            {
                var not =
                    (from p in Context.NotReportPlayers where p.PlayerId == player.PlayerId select p);
                if (not.Any())
                {
                    return new ServiceResult(ServiceResultCode.Error, "该玩家已经添加，不能重复添加");
                }

                Context.NotReportPlayers.Add(new NotReportPlayer() { PlayerId = player.PlayerId });
            }
            UserOperateLogService.Log($"新增不统计玩家:[{Name}],{(IsUnder ? "" : "不")}包括下级", Context);
            new AllBetMoneyReport().Clear(Context);
            new AllGiftReport().Clear(Context);
            new AllRechargeReport().Clear(Context);
            new AllWinMoneyReport().Clear(Context);
            new AllWithdrawReport().Clear(Context);
            new LotteryBetReport(0).Clear(Context);
            new LotteryWinReport(0).Clear(Context);
            Context.SaveChanges();
            return new ServiceResult();
        }
    }
}