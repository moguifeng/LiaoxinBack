using System.ComponentModel.DataAnnotations;
using Zzb.EF;

namespace AllLottery.Model
{
    /// <summary>
    /// 代理注册链接
    /// </summary>
    public class ProxyRegister : BaseModel
    {
        public ProxyRegister()
        {
        }

        public ProxyRegister(int playerId, string number,decimal rebate, string remark)
        {
            PlayerId = playerId;
            Number = number;            
            Rebate = rebate;
            Remark = remark;
        }

        public int ProxyRegisterId { get; set; }

        /// <summary>
        /// 推荐码
        /// </summary>
        [ZzbIndex(IsUnique = true)]
        [MaxLength(20)]
        public string Number { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        /// <summary>
        /// 注册次数
        /// </summary>
        public int UseCount { get; set; } = 0;

 

        /// <summary>
        /// 返点
        /// </summary>
        public decimal Rebate { get; set; }

        public string Remark { get; set; }
    }
}