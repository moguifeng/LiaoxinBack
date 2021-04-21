using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.RechargeManager
{
    public class AddRechargeModal : BaseServiceModal
    {
        public AddRechargeModal()
        {
        }

        public AddRechargeModal(string id, string name) : base(id, name)
        {
        }


        public override string ModalName => "充值";

        public IUserOperateLogService UserOperateLogService { get; set; }

        [TextField("玩家", IsRequired = true)]
        public string Name { get; set; }

        [NumberField("充值金额", IsRequired = true)]
        public decimal Money { get; set; }

        [TextField("备注")]
        public string Remark { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            var player = (from p in Context.Players where p.Name == Name select p).FirstOrDefault();
            if (player == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "玩家不存在");
            }
            var exist = (from r in Context.Recharges where r.PlayerId == player.PlayerId && r.MerchantsBankId == null orderby r.CreateTime descending select r).FirstOrDefault();
            if (exist != null && exist.CreateTime.AddSeconds(10) > DateTime.Now)
            {
                return new ServiceResult(ServiceResultCode.Error, "该玩家充值太频繁，请稍后再试");
            }
            var recharge = Context.Recharges.Add(new Recharge(player.PlayerId, Money, Remark, RechargeStateEnum.Ok));
            player.AddMoney(Money, CoinLogTypeEnum.Recharge, recharge.Entity.RechargeId, out var log, Remark);
            player.UpdateReportDate();
            player.RechargeMoney += Money;
            Context.CoinLogs.Add(log);
            UserOperateLogService.Log($"手动给[{player.Name}]充值了[{recharge.Entity.Money}]元，订单号为[{recharge.Entity.OrderNo}]", Context);
            Context.SaveChanges();
            return new ServiceResult();
        }
    }
}