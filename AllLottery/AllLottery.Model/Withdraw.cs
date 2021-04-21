using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zzb.Common;
using Zzb.EF;

namespace AllLottery.Model
{
    public class Withdraw : BaseModel
    {
        public Withdraw()
        {
        }

        public Withdraw(int playerBankId, decimal money)
        {
            PlayerBankId = playerBankId;
            Money = money;
        }

        public int WithdrawId { get; set; }

        [ZzbIndex(IsUnique = true)]
        [MaxLength(20)]
        public string OrderNo { get; set; } = RandomHelper.GetRandom("W");

        public int PlayerBankId { get; set; }

        public virtual PlayerBank PlayerBank { get; set; }

        [ZzbIndex]
        public WithdrawStatusEnum Status { get; set; } = WithdrawStatusEnum.Wait;

        public decimal Money { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public string Remark { get; set; }
    }

    public enum WithdrawStatusEnum
    {
        [Description("正在申请")]
        Wait,
        [Description("提现成功")]
        Ok,
        [Description("提现失败")]
        AdminCancel,
        [Description("用户取消")]
        UserCancel
    }
}