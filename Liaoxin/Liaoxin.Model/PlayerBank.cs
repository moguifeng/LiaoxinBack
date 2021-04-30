using System.ComponentModel.DataAnnotations;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class ClientBank : BaseModel
    {
        public ClientBank()
        {
        }

        public ClientBank(int clientId, int systemBankId, string cardNumber)
        {
            ClientId = clientId;
            SystemBankId = systemBankId;
            CardNumber = cardNumber;
           
        }

        public int ClientBankId { get; set; }

        /// <summary>
        /// 玩家
        /// </summary>
        [ZzbIndex("WeiYi", IsUnique = true)]
        public int  ClientId { get; set; }

        public virtual Client Client { get; set; }

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
 
    }
}