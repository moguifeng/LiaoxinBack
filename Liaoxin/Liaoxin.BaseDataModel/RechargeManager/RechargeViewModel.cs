using Liaoxin.Business.Config;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace Liaoxin.BaseDataModel.RechargeManager
{
    public class RechargeViewModel : BaseServiceNav
    {
        public IRechargeService RechargeService { get; set; }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override int OperaColumnWidth => 170;

        public override string NavName => "充值管理";

        public override string FolderName => "充值管理";

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public int RechargeId { get; set; }

        [NavField("客户真实名称")]
        public string RealName { get; set; }

      
        [NavField("订单号", 150)]
        public string OrderNo { get; set; }

        [NavField("余额")]
        public decimal Coin { get; set; }

        [NavField("充值金额")]
        public decimal Money { get; set; }

        [NavField("充值银行卡")]
        public string Bank { get; set; }

        [NavField("备注")]
        public string Remark { get; set; }

 

        [NavField("状态")]
        public RechargeStateEnum State { get; set; }

        [NavField("充值时间", 150)]
        public DateTime CreateTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return null;
            //return CreateEfDatas<Recharge, RechargeViewModel>(from r in Context.Recharges where r.IsEnable orderby r.CreateTime descending select r,
            //    (k, t) =>
            //    {
            //        t.PlayerName = k.Player.Name;
            //        t.Coin = k.Player.Coin;                    
            //        t.Bank = k.MerchantsBank == null ? "管理员充值" : k.MerchantsBank.Name;
            //        if (k.State == RechargeStateEnum.Ok)
            //        {
            //            t.OkTime = k.UpdateTime.ToCommonString();
            //        }
            //    }, (k, w) => w.Where(t => t.Player.Name.Contains(k)));
        }

        //public override BaseButton[] CreateRowButtons()
        //{
        //    List<BaseButton> list = new List<BaseButton>();
        //    if (State == RechargeStateEnum.Wait)
        //    {
        //        list.Add(new ConfirmActionButton("Ok", "确定充值", "是否确定充值？"));
        //        list.Add(new ConfirmActionButton("Cancel", "取消充值", "是否取消充值？"));
        //    }

        //    if (State != RechargeStateEnum.Ok)
        //    {
        //        list.Add(new ConfirmActionButton("Delete", "删除", "是否确定删除？"));
        //    }
        //    return list.ToArray();
        //}

        //public override bool ShowOperaColumn => true;

       // public ServiceResult Ok()
       // {
       //     var exist = (from r in Context.Recharges where r.RechargeId == RechargeId select r).First();
       //     if (!exist.IsEnable || exist.State != RechargeStateEnum.Wait)
       //     {
       //         return new ServiceResult(ServiceResultCode.Success, "该充值订单状态已更变，无法确定充值");
       //     }

       //     exist.State = RechargeStateEnum.Ok;
       ////     exist.Player.AddMoney(exist.Money, CoinLogTypeEnum.Recharge, exist.RechargeId, out var log, $"充值[{exist.Money}]成功，订单号[{exist.OrderNo}]");
       //   //  Context.CoinLogs.Add(log);

       //  //   RechargeService.ReceiveGift(exist);

       //     exist.Update();
       //     var rate = BaseConfig.CreateInstance(SystemConfigEnum.ConsumerWithdrawRate).DecimalValue;
       //     if (rate > 0)
       //     {
       //         rate = rate / 100;
       //    //     exist.Player.UpdateLastBetMoney(exist.Money * rate);
       //     }
       // //    Context.Messages.Add(new Message(exist.PlayerId, MessageInfoTypeEnum.Player, MessageTypeEnum.Message, $"您的[{exist.Money}]元充值审核成功！") { Money = exist.Money });

       //     UserOperateLogService.Log($"确定玩家[{exist.Player.Name}]的[{exist.Money}]充值订单,订单号为[{exist.OrderNo}]", Context);

       //  //   exist.Player.UpdateReportDate();
       //     exist.Player.RechargeMoney += exist.Money;

       //     Context.SaveChanges();
       //     return new ServiceResult();
       // }

        //public ServiceResult Cancel()
        //{
        //    var exist = (from r in Context.Recharges where r.RechargeId == RechargeId select r).First();
        //    if (!exist.IsEnable || exist.State != RechargeStateEnum.Wait)
        //    {
        //        return new ServiceResult(ServiceResultCode.Success, "该充值订单状态已更变，无法确定充值");
        //    }

        //    exist.State = RechargeStateEnum.AdminCancel;
        //    exist.Update();
        //    UserOperateLogService.Log($"取消玩家[{exist.Player.Name}]的[{exist.Money}]充值订单,订单号为[{exist.OrderNo}]", Context);
        //    Context.SaveChanges();
        //    return new ServiceResult();
        //}

        //public ServiceResult Delete()
        //{
        //    var recharge = (from r in Context.Recharges where r.RechargeId == RechargeId select r)
        //        .First();
        //    if (State == RechargeStateEnum.Ok)
        //    {
        //        return new ServiceResult(ServiceResultCode.Error, "已确定的订单不能删除");
        //    }
        //    recharge.IsEnable = false;
        //    UserOperateLogService.Log($"删除玩家[{recharge.Player.Name}]的[{recharge.Money}]充值订单,订单号为[{recharge.OrderNo}]", Context);
        //    Context.SaveChanges();
        //    return new ServiceResult(ServiceResultCode.Success);
        //}

        public override BaseButton[] CreateViewButtons()
        {
            return new[] { new AddRechargeModal("Add", "代充值"), };
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new[] { new TextFieldAttribute("Name", "客户聊信号"), new TextFieldAttribute("RealName", "客户真实姓名"), };
        }
    }
}