using Zzb.EF;

namespace AllLottery.Model
{
    public class GiftReceive : BaseModel
    {
        public GiftReceive()
        {
        }

        public GiftReceive(int playerId, decimal giftMoney)
        {
            PlayerId = playerId;
            GiftMoney = giftMoney;
        }

        public int GiftReceiveId { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        /// <summary>
        /// 赠送金额
        /// </summary>
        public decimal GiftMoney { get; set; }
    }
}