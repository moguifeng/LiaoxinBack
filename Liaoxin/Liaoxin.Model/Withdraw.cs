using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zzb.Common;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class Withdraw : BaseModel
    {
        public Withdraw()
        {
        }

        public Withdraw(Guid clientBankId, decimal money)
        {
            ClientBankId = clientBankId;
            Money = money;
        }

        public Guid WithdrawId { get; set; } = Guid.NewGuid();

        [ZzbIndex(IsUnique = true)]
        [MaxLength(20)]
        public string OrderNo { get; set; } = RandomHelper.GetRandom("W");

        public Guid ClientBankId { get; set; }

        public virtual ClientBank ClientBank { get; set; }

        [ZzbIndex]
        public WithdrawStatusEnum Status { get; set; } = WithdrawStatusEnum.Ok;

        public decimal Money { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public string Remark { get; set; }
    }

    public enum WithdrawStatusEnum
    {
      
        [Description("提现成功")]
        Ok,
        [Description("提现失败")]
        Fail,
        [Description("用户取消")]
        Cancel,
    }
}