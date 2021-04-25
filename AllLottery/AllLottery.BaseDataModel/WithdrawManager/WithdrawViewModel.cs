using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.WithdrawManager
{
    public class WithdrawViewModel : BaseServiceNav
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string NavName => "提款管理";

        public override string FolderName => "提款管理";

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public int WithdrawId { get; set; }

        [NavField("玩家")]
        public string PlayerName { get; set; }

        [NavField("上级")]
        public string ParentName { get; set; }

        [NavField("订单号", 200)]
        public string OrderNo { get; set; }

        [NavField("提现金额")]
        public string MoneyString { get; set; }

        [NavField("玩家余额")]
        public decimal Coin { get; set; }

        [NavField("冻结金额")]
        public decimal FCoin { get; set; }

        [NavField("收款银行")]
        public string BankName { get; set; }

        [NavField("收款银行账号", 200)]
        public string BankAccount { get; set; }

        [NavField("收款人姓名")]
        public string BankAccountName { get; set; }

        [NavField("状态")]
        public WithdrawStatusEnum Status { get; set; }

        [NavField("申请提现时间", 150)]
        public DateTime CreateTime { get; set; }

        [NavField("审核通过时间", 150)]
        public string OkTime { get; set; }

        [NavField("备注")]
        public string Remark { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<Withdraw, WithdrawViewModel>(from w in Context.Withdraws where w.IsEnable orderby w.CreateTime descending select w,
                (k, t) =>
                {
                    t.PlayerName = k.PlayerBank.Player.Name;
                    t.ParentName = k.PlayerBank.Player.ParentPlayer?.Name;
                    t.Coin = k.PlayerBank.Player.Coin;
                    t.BankName = k.PlayerBank.SystemBank.Name;
                    t.BankAccount = k.PlayerBank.CardNumber;
                    t.BankAccountName = k.PlayerBank.PayeeName;
                    t.MoneyString = $"<p style='font-weight:bold;margin:0'>{k.Money:#0.0000}</p>";
                    t.OkTime = k.Status == WithdrawStatusEnum.Ok ? k.UpdateTime.ToCommonString() : null;
                    t.FCoin = k.PlayerBank.Player.FCoin;
                }, (k, w) => w.Where(t => t.PlayerBank.Player.Name.Contains(k)), (k, w) => w.Where(t => t.OrderNo.Contains(k))
                , (k, w) => ConvertEnum<Withdraw, WithdrawStatusEnum>(w, k, m => w.Where(t => t.Status == m)));
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[] { new TextFieldAttribute("Name", "玩家"), new TextFieldAttribute("Order", "订单号"), new DropListFieldAttribute("status", "状态", WithdrawStatusEnum.Ok.GetDropListModels("全部")) };
        }

        public override BaseButton[] CreateRowButtons()
        {
            if (Status == WithdrawStatusEnum.Wait)
            {
                return new BaseButton[] {
                    new ConfirmActionButton("SureWithdraw", "确定提款", "是否确认提款"),new WithdrawCancelModal("Cancel","取消提款"),
                };
            }
            return base.CreateRowButtons();
        }

        public ServiceResult SureWithdraw()
        {
            return null;
            //var withdraw = (from w in Context.Withdraws
            //                where w.IsEnable && w.WithdrawId == WithdrawId
            //                select w).First();
            //if (withdraw.Status != WithdrawStatusEnum.Wait)
            //{
            //    return new ServiceResult(ServiceResultCode.Error, "该订单已被处理");
            //}

            //withdraw.Status = WithdrawStatusEnum.Ok;
            //withdraw.UpdateTime = DateTime.Now;

            //withdraw.PlayerBank.Player.AddFMoney(-withdraw.Money, CoinLogTypeEnum.Withdraw, withdraw.WithdrawId, out var log, $"提现成功，订单号[{withdraw.OrderNo}]");
            //Context.CoinLogs.Add(log);
            //Context.Messages.Add(new Message(withdraw.PlayerBank.PlayerId, MessageInfoTypeEnum.Player,
            //    MessageTypeEnum.Message, $"您的[{withdraw.Money}]元提现审核成功！"));
            //UserOperateLogService.Log($"确定玩家[{withdraw.PlayerBank.Player.Name}]的[{withdraw.Money}]订单，订单号为[{withdraw.OrderNo}]", Context);        
            //withdraw.PlayerBank.Player.WithdrawMoney += withdraw.Money;
            //Context.SaveChanges();
            //return new ServiceResult(ServiceResultCode.Success);
        }
    }
}