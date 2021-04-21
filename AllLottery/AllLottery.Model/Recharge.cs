using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Zzb.Common;
using Zzb.EF;

namespace AllLottery.Model
{
    public class Recharge : BaseModel
    {
        public Recharge()
        {
        }

        public Recharge(int playerId, decimal money, int? merchantsBankId)
        {
            PlayerId = playerId;
            Money = money;
            MerchantsBankId = merchantsBankId;
        }

        public Recharge(int playerId, decimal money, string remark, RechargeStateEnum state)
        {
            PlayerId = playerId;
            Money = money;
            Remark = remark;
            State = state;
        }

        public int RechargeId { get; set; }

        [ZzbIndex(IsUnique = true)]
        [MaxLength(20)]
        public string OrderNo { get; set; } = RandomHelper.GetRandom("R");

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        public decimal Money { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public RechargeStateEnum State { get; set; } = RechargeStateEnum.Wait;

        public int? MerchantsBankId { get; set; }

        public virtual MerchantsBank MerchantsBank { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }

    public enum RechargeStateEnum
    {
        [Description("正在申请")]
        Wait,
        [Description("充值到帐")]
        Ok,
        [Description("充值失败")]
        AdminCancel
    }
}