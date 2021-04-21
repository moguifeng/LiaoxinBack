using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace AllLottery.Model
{
    /// <summary>
    /// 玩家表
    /// </summary>
    public class Player : BaseModel
    {
        public Player()
        {
        }

        public Player(string name, string password, string coidPassword)
        {
            Name = name;
            Password = password;
            CoinPassword = coidPassword;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// 玩家名
        /// </summary>
        [MaxLength(20)]
        [ZzbIndex(IsUnique = true)]
        public string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [MaxLength(20)]
        public string Title { get; set; }

        /// <summary>
        /// 返点
        /// </summary>
        [ZzbIndex]
        public decimal Rebate { get; set; } = 0;

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 资金密码
        /// </summary>
        public string CoinPassword { get; set; }

        /// <summary>
        /// 余额
        /// </summary>
        public decimal Coin { get; set; }

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

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 是否更改过密码
        /// </summary>
        public bool IsChangePassword { get; set; } = false;

        /// <summary>
        /// 是否冻结
        /// </summary>
        public bool IsFreeze { get; set; } = false;

        /// <summary>
        /// 是否可以提款
        /// </summary>
        public bool CanWithdraw { get; set; } = true;

        /// <summary>
        /// 剩余消费
        /// </summary>
        public decimal LastBetMoney { get; set; }

        /// <summary>
        /// 流动资金
        /// </summary>
        public decimal FCoin { get; set; }

        /// <summary>
        /// 父级代理
        /// </summary>
        public int? ParentPlayerId { get; set; }

        /// <summary>
        /// 父级代理人
        /// </summary>
        [ForeignKey("ParentPlayerId")]
        public virtual Player ParentPlayer { get; set; }

        /// <summary>
        /// 下级代理
        /// </summary>
        [InverseProperty("ParentPlayer")]
        public virtual List<Player> Players { get; set; }

        /// <summary>
        /// 标准日工资
        /// </summary>
        [ZzbIndex]
        public decimal? DailyWageRate { get; set; }

        /// <summary>
        /// 分红比率
        /// </summary>
        [ZzbIndex]
        public decimal? DividendRate { get; set; }

        /// <summary>
        /// 充值金额
        /// </summary>
        public decimal RechargeMoney { get; set; }

        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal WithdrawMoney { get; set; }

        /// <summary>
        /// 投注金额
        /// </summary>
        public decimal BetMoney { get; set; }

        /// <summary>
        /// 中奖金额
        /// </summary>
        public decimal WinMoney { get; set; }

        /// <summary>
        /// 返点金额
        /// </summary>
        public decimal RebateMoney { get; set; }

        /// <summary>
        /// 活动金额
        /// </summary>
        public decimal GiftMoney { get; set; }

        [ZzbIndex]
        public DateTime ReportDate { get; set; }

        public void UpdateReportDate()
        {
            if (ReportDate < DateTime.Today)
            {
                RechargeMoney = 0;
                WithdrawMoney = 0;
                BetMoney = 0;
                WinMoney = 0;
                RebateMoney = 0;
                GiftMoney = 0;
                ReportDate = DateTime.Today;
                UpdateTime = DateTime.Now;
            }
        }

        /// <summary>
        /// 玩家类型
        /// </summary>
        public PlayerTypeEnum Type { get; set; } = PlayerTypeEnum.Member;

        [Timestamp]
        public byte[] Version { get; set; }

        public void AddMoney(decimal money, CoinLogTypeEnum type, int aboutId, out CoinLog coinLog, string remark)
        {
            Coin += money;
            coinLog = new CoinLog(PlayerId, money, Coin, 0, FCoin, type, aboutId, remark);
        }

        public void UpdateLastBetMoney(decimal money)
        {
            LastBetMoney += money;
            if (LastBetMoney < 0)
            {
                LastBetMoney = 0;
            }
        }

        /// <summary>
        /// 计算返点
        /// </summary>
        /// <param name="bet"></param>
        /// <param name="logs"></param>
        /// <param name="rebateLogs"></param>
        public void CalculateRebate(Bet bet, List<CoinLog> logs, List<RebateLog> rebateLogs)
        {
            if (ParentPlayerId == null)
            {
                return;
            }

            if (ParentPlayerId.Value == PlayerId)
            {
                return;
            }

            //比例差
            var diff = ParentPlayer.Rebate - Rebate;

            if (diff > 0)
            {
                //返点
                var rebateMoney = bet.BetMoney * diff;

                ParentPlayer.Coin += rebateMoney;

                ParentPlayer.UpdateReportDate();
                ParentPlayer.RebateMoney += rebateMoney;

                UpdateTime = DateTime.Now;

                rebateLogs.Add(new RebateLog(PlayerId, bet.BetId, diff, rebateMoney));

                logs.Add(new CoinLog(ParentPlayerId.Value, rebateMoney, ParentPlayer.Coin, 0, ParentPlayer.FCoin, CoinLogTypeEnum.Rebate, bet.BetId, $"玩家[{bet.Player.Name}]的[{bet.LotteryPlayDetail.LotteryPlayType.LotteryType.Name}]的[{bet.LotteryIssuseNo}]期投注单返点"));

            }

            ParentPlayer.CalculateRebate(bet, logs, rebateLogs);
        }

        public void AddFMoney(decimal money, CoinLogTypeEnum type, int aboutId, out CoinLog coinLog, string remark)
        {
            FCoin += money;
            coinLog = new CoinLog(PlayerId, 0, Coin, money, FCoin, type, aboutId, remark);
        }

        public void TransferPlatform(Platform platform, decimal money, out CoinLog coinLog)
        {
            bool isIn = money >= 0;
            if (isIn)
            {
                Coin -= money;
                FCoin += money;
                coinLog = new CoinLog(PlayerId, -money, Coin, money, FCoin, CoinLogTypeEnum.PlatformTransfer, 0, $"申请转入[{platform.Name}]金额");
            }
            else
            {
                AddFMoney(-money, CoinLogTypeEnum.PlatformTransfer, 0, out coinLog, $"申请转出[{platform.Name}]金额");
            }
        }

        public void TransferPlatformFail(Platform platform, decimal money, out CoinLog coinLog)
        {
            bool isIn = money >= 0;
            if (isIn)
            {
                Coin += money;
                FCoin -= money;
                coinLog = new CoinLog(PlayerId, money, Coin, -money, FCoin, CoinLogTypeEnum.PlatformTransfer, 0, $"转入[{platform.Name}]金额失败，退还金额");
            }
            else
            {
                AddFMoney(money, CoinLogTypeEnum.PlatformTransfer, 0, out coinLog, $"转出[{platform.Name}]金额失败，退还金额");
            }
        }

        public void TransferPlatformSuccess(Platform platform, decimal money, out CoinLog coinLog)
        {
            bool isIn = money >= 0;
            if (isIn)
            {
                AddFMoney(-money, CoinLogTypeEnum.PlatformTransfer, 0, out coinLog, $"成功转入[{platform.Name}]金额");
            }
            else
            {
                Coin -= money;
                FCoin += money;
                coinLog = new CoinLog(PlayerId, -money, Coin, money, FCoin, CoinLogTypeEnum.PlatformTransfer, 0, $"成功转出[{platform.Name}]金额");
            }
        }
    }

    public enum PlayerTypeEnum
    {
        [Description("代理")]
        Proxy = 0,
        [Description("会员")]
        Member = 1,
        [Description("试玩")]
        TestPlay = 2
    }
}