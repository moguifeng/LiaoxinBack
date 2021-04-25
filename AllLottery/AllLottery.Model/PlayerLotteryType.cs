using Zzb.EF;

namespace AllLottery.Model
{
    /// <summary>
    /// 玩家彩种表
    /// </summary>
    public class PlayerLotteryType : BaseModel
    {
        public int PlayerLotteryTypeId { get; set; }

        [ZzbIndex("WeiYi", IsUnique = true)]
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

     //   [ZzbIndex("WeiYi", 2, IsUnique = true)]
     //   public int LotteryTypeId { get; set; }

      //  public virtual LotteryType LotteryType { get; set; }
    }
}