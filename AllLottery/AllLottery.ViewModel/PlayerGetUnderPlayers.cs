using AllLottery.Model;

namespace AllLottery.ViewModel
{
    public class PlayerGetUnderPlayers : PageViewModel
    {
        public int PlayerId { get; set; }

        public string Name { get; set; }

        public PlayerTypeEnum? Type { get; set; }

        public PlayerSortEnum Sort { get; set; } = PlayerSortEnum.CreateTime;

        /// <summary>
        /// 手机
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public string QQ { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        public string WeChat { get; set; }

        public bool? IsOnline { get; set; }
    }

    public enum PlayerSortEnum
    {
        CreateTime = 0,
        CreateTimeDesc = 1,
        Coin = 2,
        CoinDesc = 3
    }
}