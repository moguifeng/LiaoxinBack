using Liaoxin.IBusiness;
using Liaoxin.Model;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace Liaoxin.BaseDataModel.WithdrawManager
{
    //public class WithdrawCancelModal : BaseServiceModal
    //{
    //    public IUserOperateLogService UserOperateLogService { get; set; }

    //    public WithdrawCancelModal()
    //    {
    //    }

    //    public WithdrawCancelModal(string id, string name) : base(id, name)
    //    {
    //    }

    //    public override string ModalName => "取消提款";

    //    [HiddenTextField]
    //    public int WithdrawId { get; set; }

    //    [TextField("备注")]
    //    public string Remark { get; set; }

    //    public override BaseButton[] Buttons()
    //    {
    //        return new[] { new ActionButton("Save", "拒绝提款"), };
    //    }

    //    public ServiceResult Save()
    //    {
    //        var withdraw = (from w in Context.Withdraws where w.WithdrawId == WithdrawId select w)
    //            .First();
    //        if (withdraw.Status != WithdrawStatusEnum.Wait)
    //        {
    //            return new ServiceResult(ServiceResultCode.Error, "当前状态不能取消提款");
    //        }
    //        withdraw.Remark = Remark;
    //        withdraw.Status = WithdrawStatusEnum.AdminCancel;
    //        withdraw.Update();
    //        var player = withdraw.PlayerBank.Player;
    //        player.Coin += withdraw.Money;
    //        player.FCoin -= withdraw.Money;
    //        player.Update();
    //        //Context.CoinLogs.Add(new CoinLog(player.PlayerId, withdraw.Money, 0, -withdraw.Money, player.FCoin, CoinLogTypeEnum.CancelWithdraw, withdraw.WithdrawId, $"提款失败，订单号[{withdraw.OrderNo}]"));
    //        UserOperateLogService.Log($"取消玩家[{player.Name}]的[{withdraw.Money}]提款,订单号为[{withdraw.OrderNo}]", Context);
    //        Context.SaveChanges();
    //        return new ServiceResult();
    //    }
    //}
}