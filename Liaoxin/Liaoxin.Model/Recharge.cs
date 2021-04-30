using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Zzb.Common;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class Recharge : BaseModel
    {
        public Recharge()
        {
        }

        public Recharge(int clientId, decimal money, int? clientBankId)
        {
            ClientId = clientId;
            Money = money;
            ClientBankId = clientBankId;
        }

        public Recharge(int clientId, decimal money, string remark,  int? clientBankId,RechargeStateEnum state)
        {
            ClientId = clientId;
            Money = money;
            Remark = remark;
            State = state;
            ClientBankId = clientBankId;
        }

        public int RechargeId { get; set; }

        [ZzbIndex(IsUnique = true)]
        [MaxLength(20)]
        public string OrderNo { get; set; } = RandomHelper.GetRandom("R");

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public decimal Money { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public RechargeStateEnum State { get; set; } = RechargeStateEnum.Ok;

        public int? ClientBankId { get; set; }

        public virtual ClientBank ClientBank { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
    }

    public enum RechargeStateEnum
    {
        [Description("充值成功")]
        Ok,
        [Description("代充值成功")]
        AdminOk,
    }
}