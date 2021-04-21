using System.ComponentModel.DataAnnotations;
using Zzb.EF;

namespace AllLottery.Model
{
    public class PlayerBank : BaseModel
    {
        public PlayerBank()
        {
        }

        public PlayerBank(int playerId, int systemBankId, string cardNumber, string payeeName)
        {
            PlayerId = playerId;
            SystemBankId = systemBankId;
            CardNumber = cardNumber;
            PayeeName = payeeName;
        }

        public int PlayerBankId { get; set; }

        /// <summary>
        /// 玩家
        /// </summary>
        [ZzbIndex("WeiYi", IsUnique = true)]
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        /// <summary>
        /// 系统银行ID
        /// </summary>
        public int SystemBankId { get; set; }

        public virtual SystemBank SystemBank { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        [ZzbIndex("WeiYi", 1, IsUnique = true)]
        [MaxLength(20)]
        public string CardNumber { get; set; }

        /// <summary>
        /// 收款人
        /// </summary>
        public string PayeeName { get; set; }
    }
}