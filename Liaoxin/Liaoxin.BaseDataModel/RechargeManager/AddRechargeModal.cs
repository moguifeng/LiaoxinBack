using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace Liaoxin.BaseDataModel.RechargeManager
{
    public class AddRechargeModal : BaseServiceModal
    {
        public AddRechargeModal()
        {
        }

        public AddRechargeModal(string id, string name) : base(id, name)
        {
        }


        public override string ModalName => "客户充值";

        public IUserOperateLogService UserOperateLogService { get; set; }

        [TextField("客户聊信号")]
        public string LiaoxinNumber { get; set; }

        //[TextField("客户真实名称", IsReadOnly = true)]
        //public string RealName { get; set; }

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
            var client = (from p in Context.Clients where p.LiaoxinNumber == this.LiaoxinNumber select p).FirstOrDefault();
            if (client == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "客户不存在");
            }
            var exist = (from r in Context.Recharges where r.ClientId == client.ClientId && r.ClientBankId == null orderby r.CreateTime descending select r).FirstOrDefault();
            if (exist != null && exist.CreateTime.AddSeconds(10) > DateTime.Now)
            {
                return new ServiceResult(ServiceResultCode.Error, "该客户充值太频繁，请稍后再试");
            }
            var recharge = Context.Recharges.Add(new Recharge(client.ClientId, Money, Remark,null, RechargeStateEnum.AdminOk));
            client.AddMoney(Money, CoinLogTypeEnum.InsteadRecharge, recharge.Entity.RechargeId, out var log, Remark);

            //player.UpdateReportDate();            
            Context.CoinLogs.Add(log);
            UserOperateLogService.Log($"手动给[{client.RealName}/{client.LiaoxinNumber}]充值了[{recharge.Entity.Money}]元，订单号为[{recharge.Entity.OrderNo}]", Context);
            Context.SaveChanges();
            return new ServiceResult();
        }
    }
}