using Zzb.EF;

namespace AllLottery.Model
{
    public class BetMode : BaseModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int BetModeId { get; set; }

        /// <summary>
        /// 模式名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 模式金额
        /// </summary>
        public decimal Money { get; set; }

        /// <summary>
        /// 排序分组
        /// </summary>
        public int Index { get; set; }

        /// <summary>
        /// 是否信用
        /// </summary>
        public bool IsCredit { get; set; } = false;
    }
}