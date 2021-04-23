using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using Zzb.Common;
using Zzb.EF;

namespace AllLottery.Model
{
    public class Configuration
    {
        private static LotteryContext _context;

        public static void Seed(LotteryContext context)
        {
            if (!context.Players.Any())
            {
                _context = context;
          //      context.Tests.Add(new Test() { Name = "TTEST" });


                //AddContract();
                //AddPlayers();
                //AddAnnouncement();


                AddLottery();
                AddBetMode();
                AddSystemBank();
                AddMerchantsBanks();
                AddConfig();
                AddPlatform();
                AddPictrueNews();
                context.SaveChanges();
            }
        }

        private static void AddPictrueNews()
        {
            _context.PictureNewses.Add(new PictureNews() { Affix = new Affix() { Path = "Image/News/1.jpg" }, SortIndex = 1 });
            _context.PictureNewses.Add(new PictureNews() { Affix = new Affix() { Path = "Image/News/2.jpg" }, SortIndex = 2 });
            _context.PictureNewses.Add(new PictureNews() { Affix = new Affix() { Path = "Image/News/3.png" }, SortIndex = 3 });
        }

        private static void AddPlatform()
        {
            _context.Platforms.Add(new Platform("AG 真人娱乐大厅", "AG", "真人娱乐场，美女荷官", PlatformEnum.LiveVideo, 0, new Affix() { Path = "Image/BI/ag.jpg" }));
            _context.Platforms.Add(new Platform("AB 真实赌场", "AB", "预先派牌，庄闲任选", PlatformEnum.LiveVideo, 1, new Affix() { Path = "Image/BI/ab.jpg" }));
            _context.Platforms.Add(new Platform("申博真人娱乐馆", "SunBet", "以客为先，优质游戏", PlatformEnum.LiveVideo, 3, new Affix() { Path = "Image/BI/申博.jpg" }));
            _context.Platforms.Add(new Platform("SA 多台投注", "SA", "单一视窗，多台投注", PlatformEnum.LiveVideo, 4, new Affix() { Path = "Image/BI/sa.jpg" }));
            _context.Platforms.Add(new Platform("BG 大游", "BG", "亚洲领先，真人视讯", PlatformEnum.LiveVideo, 5, new Affix() { Path = "Image/BI/bg.jpg" }));
            _context.Platforms.Add(new Platform("卡卡湾88娱乐场", "CG", "真人娱乐场，美女荷官", PlatformEnum.LiveVideo, 5, new Affix() { Path = "Image/BI/卡卡湾.jpg" }));
            _context.Platforms.Add(new Platform("EBET MACASINO", "EBET", "易犹未尽，博出精彩", PlatformEnum.LiveVideo, 5, new Affix() { Path = "Image/BI/ebet.jpg" }));
            _context.Platforms.Add(new Platform("DG 梦女郎", "DG", "互动娱乐平台", PlatformEnum.LiveVideo, 5, new Affix() { Path = "Image/BI/dg.jpg" }));
            _context.Platforms.Add(new Platform("HappyPenguin88", "PT", "国际视讯，美女荷官", PlatformEnum.LiveVideo, 4, new Affix() { Path = "Image/BI/hp88.jpg" }));

            _context.Platforms.Add(new Platform("BBin 体育", "BB", "BBin体育投注", PlatformEnum.SportsCompetition, 1, new Affix() { Path = "Image/BI/bbin.jpg" }));
            _context.Platforms.Add(new Platform("IBC 体育", "IBC", "IBC体育投注", PlatformEnum.SportsCompetition, 2, new Affix() { Path = "Image/BI/ibc.jpg" }));
            _context.Platforms.Add(new Platform("NewBB Sport", "NEWBB", "NEWBB体育投注", PlatformEnum.SportsCompetition, 2, new Affix() { Path = "Image/BI/newbb.jpg" }));
            _context.Platforms.Add(new Platform("SS 体育", "SS", "SS体育投注", PlatformEnum.SportsCompetition, 2, new Affix() { Path = "Image/BI/ss.jpg" }));
            _context.Platforms.Add(new Platform("HC皇朝电竞", "HC", "HC皇朝电竞投注", PlatformEnum.SportsCompetition, 2, new Affix() { Path = "Image/BI/ug.jpg" }));
            _context.Platforms.Add(new Platform("UG 体育", "UG", "UG体育投注", PlatformEnum.SportsCompetition, 2, new Affix() { Path = "Image/BI/hc.jpg" }));
            _context.Platforms.Add(new Platform("范亚电竞", "AVIA", "支持你喜爱的游戏队伍", PlatformEnum.SportsCompetition, 2, new Affix() { Path = "Image/BI/范亚.jpg" }));
            _context.Platforms.Add(new Platform("冠捷体育", "GJ", "冠捷体育投注", PlatformEnum.SportsCompetition, 2, new Affix() { Path = "Image/BI/冠捷.jpg" }));

            _context.Platforms.Add(new Platform("NWG 新世界棋牌", "NWG", "在线棋牌，亚洲第一", PlatformEnum.Chess, 2, new Affix() { Path = "Image/BI/nwg.jpg" }));
            _context.Platforms.Add(new Platform("KY 开元棋牌", "KY", "在线棋牌，万人在线", PlatformEnum.Chess, 2, new Affix() { Path = "Image/BI/ky.jpg" }));
            _context.Platforms.Add(new Platform("MT棋牌", "MT", "在线棋牌，精彩好玩", PlatformEnum.Chess, 2, new Affix() { Path = "Image/BI/mt.jpg" }));
            _context.Platforms.Add(new Platform("乐游棋牌", "LEG", "在线棋牌，多种棋牌任你玩", PlatformEnum.Chess, 2, new Affix() { Path = "Image/BI/乐游.jpg" }));

            _context.Platforms.Add(new Platform("JDB 夺宝电子", "JDB", "经典游戏，在线捕鱼", PlatformEnum.ElectronicGames, 3, new Affix() { Path = "Image/BI/jdb.jpg" }));
            _context.Platforms.Add(new Platform("夜戏貂蝉", "PG", "经典老虎机", PlatformEnum.ElectronicGames, 4, new Affix() { Path = "Image/BI/夜戏貂蝉.jpg" }));
            _context.Platforms.Add(new Platform("娱乐城 Casino", "MW", "移植街机，千炮捕鱼", PlatformEnum.ElectronicGames, 4, new Affix() { Path = "Image/BI/娱乐城.jpg" }));
            _context.Platforms.Add(new Platform("熊猫山羊老虎机", "SW", "经典老虎机", PlatformEnum.ElectronicGames, 4, new Affix() { Path = "Image/BI/熊猫山.jpg" }));
            _context.Platforms.Add(new Platform("拳皇98老虎机", "DT", "经典老虎机", PlatformEnum.ElectronicGames, 4, new Affix() { Path = "Image/BI/拳皇98.jpg" }));
            _context.Platforms.Add(new Platform("VIVOGAMING", "BS", "国际游戏，种类繁多", PlatformEnum.ElectronicGames, 4, new Affix() { Path = "Image/BI/vivo.jpg" }));
            _context.Platforms.Add(new Platform("PNG 老虎机", "PNG", "经典老虎机", PlatformEnum.ElectronicGames, 4, new Affix() { Path = "Image/BI/png.jpg" }));
            _context.Platforms.Add(new Platform("永宝老虎机", "CQ", "经典老虎机", PlatformEnum.ElectronicGames, 4, new Affix() { Path = "Image/BI/永宝.jpg" }));

        }

        private static void AddConfig()
        {
            var affix = _context.Affixs.Add(new Affix() { Path = "Image/logo.png" });
            _context.SaveChanges();
            _context.SystemConfigs.Add(new SystemConfig(SystemConfigEnum.Logo, affix.Entity.AffixId.ToString()));
        }

        private static void AddMerchantsBanks()
        {
            _context.MerchantsBanks.Add(new MerchantsBank()
            {
                Name = "微信",
                BannerAffix = new Affix() { Path = "Image/微信.png" },
                Description = "打开微信扫描二维码",
                ScanAffix = new Affix() { Path = "Image/微信二维码.png" },
                IndexSort = 0
            });
            _context.MerchantsBanks.Add(new MerchantsBank()
            {
                Name = "支付宝",
                BannerAffix = new Affix() { Path = "Image/支付宝.png" },
                Description = "打开支付宝扫描二维码",
                ScanAffix = new Affix() { Path = "Image/支付宝二维码.png" },
                IndexSort = 0
            });
        }

        private static void AddSystemBank()
        {
            _context.SystemBanks.Add(new SystemBank() { Name = "工商银行", Affix = new Affix() { Path = "Image/Bank/工商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "北京银行", Affix = new Affix() { Path = "Image/Bank/北京银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "广发银行", Affix = new Affix() { Path = "Image/Bank/广发银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "华夏银行", Affix = new Affix() { Path = "Image/Bank/华夏银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "交通银行", Affix = new Affix() { Path = "Image/Bank/交通银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "浦发银行", Affix = new Affix() { Path = "Image/Bank/浦发银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "上海银行", Affix = new Affix() { Path = "Image/Bank/上海银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "兴业银行", Affix = new Affix() { Path = "Image/Bank/兴业银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "招商银行", Affix = new Affix() { Path = "Image/Bank/招商银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "中国光大银行", Affix = new Affix() { Path = "Image/Bank/中国光大银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "中国建设银行", Affix = new Affix() { Path = "Image/Bank/中国建设银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "中国民生银行", Affix = new Affix() { Path = "Image/Bank/中国民生银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "中国农业银行", Affix = new Affix() { Path = "Image/Bank/中国农业银行.png" } });
            _context.SystemBanks.Add(new SystemBank() { Name = "中国银行", Affix = new Affix() { Path = "Image/Bank/中国银行.png" } });
        }

        public static void AddBetMode()
        {
            _context.BetModes.Add(new BetMode() { Name = "元", Money = 2, Index = 0 });
            _context.BetModes.Add(new BetMode() { Name = "角", Money = 0.2M, Index = 1 });
            _context.BetModes.Add(new BetMode() { Name = "分", Money = 0.02M, Index = 2 });
            _context.BetModes.Add(new BetMode() { Name = "厘", Money = 0.002M, Index = 3 });
            _context.BetModes.Add(new BetMode() { Name = "信用", Money = 1, Index = 0, IsCredit = true });
        }

        public static void AddContract()
        {
            _context.DailyWages.Add(new DailyWage() { MenCount = 0, BetMoney = 0, Rate = 0.02M });
            _context.DividendSettings.Add(new DividendSetting(0, 0, 0, 0.3M));
        }

        public static void AddPlayers()
        {
            var ceshi = new Player("ceshi", SecurityHelper.Encrypt("1"), SecurityHelper.Encrypt("1"))
            { Coin = 111111111.1111111111M, Players = new List<Player>(), Type = PlayerTypeEnum.Proxy, Rebate = 0.1M, DailyWageRate = 0.02M, DividendRate = 0.3M, IsChangePassword = true };

            for (int i = 0; i < 100; i++)
            {
                ceshi.Players.Add(new Player("ceshi" + i, SecurityHelper.Encrypt("1"), SecurityHelper.Encrypt("1")) { Coin = 111111111.1111111111M, IsChangePassword = true });
            }

            _context.Players.Add(ceshi);
        }

        public static void AddAnnouncement()
        {
            _context.Announcements.Add(new Announcement("欢迎加盟", @"<p>各位尊敬的游客：</p>
<p>大家好！欢迎大家来到雅福娱乐！</p>
<p>雅福娱乐长期欢迎<span style='color: rgb(226,80,65);font-size: 24px;'>加盟</span>合作，<span style='color: rgb(226,80,65);font-size: 24px;'>开网</span>，有需要请联系<span style='color: rgb(226,80,65);font-size: 24px;'>QQ</span></p>
<p><span style='color: rgb(226,80,65);font-size: 24px;'>1657086043</span></p>
<p><span style='color: rgb(226,80,65);font-size: 24px;'>2594772463</span></p>
<p><span style='color: rgb(226,80,65);font-size: 24px;'>2071544132</span></p>
", DateTime.Now));
        }

        public static List<LotteryPlayType> CreateSsc(bool isNotTengXun = true)
        {
            return new List<LotteryPlayType>()
            {
                new LotteryPlayType()
                {
                    Name = "五星",
                    SortIndex = -2,
                    IsEnable = isNotTengXun,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "五星复式",
                            MinOdds = 90000,
                            MaxOdds = 100000,
                            SortIndex = 0,
                            Description = "每位至少选择一个号码，竞猜开奖号码的全部五位，号码和位置都对应即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticFiveStartDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "五星单式",
                            MinOdds = 90000,
                            MaxOdds = 100000,
                            SortIndex = 1,
                            Description = "每位至少选择一个号码，竞猜开奖号码的全部五位，号码和位置都对应即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticFiveStartSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选120",
                            MinOdds = 750,
                            MaxOdds = 833.333333m,
                            SortIndex = 2,
                            Description = "至少选择五个号码投注，竞猜开奖号码的全部五位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFiveChoose120"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选60",
                            MinOdds = 1500,
                            MaxOdds = 1666.666666M,
                            SortIndex = 3,
                            Description = "至少选择1个二重号码和3个单号号码组成一注，竞猜开奖号码的全部五位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFiveChoose60"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选30",
                            MinOdds = 3000,
                            MaxOdds = 3333.333333M,
                            SortIndex = 4,
                            Description = "至少选择2个二重号码和1个单号号码组成一注，竞猜开奖号码的全部五位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFiveChoose30"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选20",
                            MinOdds = 4500,
                            MaxOdds = 5000,
                            SortIndex = 5,
                            Description = "至少选择1个三重号码和2个单号号码组成一注，竞猜开奖号码的全部五位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFiveChoose20"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选10",
                            MinOdds = 9000,
                            MaxOdds = 10000,
                            SortIndex = 6,
                            Description = "至少选择1个三重号码和1个二重号码组成一注，竞猜开奖号码的全部五位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFiveChoose10"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选5",
                            MinOdds = 18000,
                            MaxOdds = 20000,
                            SortIndex = 7,
                            Description = "至少选择1个四重号码和1个单号号码组成一注，竞猜开奖号码的全部五位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFiveChoose5"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "四星",
                    SortIndex = -1,
                    IsEnable = isNotTengXun,
                    LotteryPlayDetails=new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "复式",
                            MinOdds = 9000,
                            MaxOdds = 10000,
                            SortIndex = 1,
                            Description = "每位至少选择一个号码，竞猜开奖号码的后四位，号码和位置都对应即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticFourBehindDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "单式",
                            MinOdds = 9000,
                            MaxOdds = 10000,
                            SortIndex = 2,
                            Description = "每位至少选择一个号码，竞猜开奖号码的后四位，号码和位置都对应即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticFourBehindSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选24",
                            MinOdds = 375,
                            MaxOdds = 416.666666M,
                            SortIndex = 3,
                            Description = "至少选择4个号码投注，竞猜开奖号码的后4位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFourChoose24"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选12",
                            MinOdds = 750,
                            MaxOdds = 833.333333M,
                            SortIndex = 4,
                            Description = "至少选择1个二重号码和2个单号号码，竞猜开奖号码的后四位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFourChoose24"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选6",
                            MinOdds = 1500,
                            MaxOdds = 1666.666666M,
                            SortIndex = 5,
                            Description = "至少选择2个二重号码，竞猜开奖号码的后四位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFourChoose24"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选4",
                            MinOdds = 2250,
                            MaxOdds = 2500,
                            SortIndex = 6,
                            Description = "至少选择1个三重号码和1个单号号码，竞猜开奖号码的后四位，号码一致顺序不限即中奖，奖金 [jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupFourChoose4"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "前三",
                    SortIndex = 0,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 0,
                            Description = "每位各选1个或多个号码，选号与奖号前三位一一对应，中奖[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeDuplex.CathecticThreeFrontDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组三",
                            MinOdds = 300,
                            MaxOdds = 333.333333M,
                            SortIndex = 1,
                            Description = "组三是指开奖号码前三位任意两位号码相同，如188。至少选2个号码投注，开奖号前三位为组三号且包含在投注号码中，即中[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeThree.CathecticGroupThreeFrontThree"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组三单式",
                            MinOdds = 300,
                            MaxOdds = 333.333333M,
                            SortIndex = 1,
                            Description = "手动输入号码，至少输入 1 个三位数号码。(三个数字当中有二个数字相同),顺序不限，奖金 [jj]元",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeThree.CathecticGroupThreeFrontThreeSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组六",
                            MinOdds = 150,
                            MaxOdds = 166.666666m,
                            SortIndex = 2,
                            Description = "组六是指开奖号码前三位三个号码各不相同，如135。至少选3个号码投注，开奖号前三位为组六号且包含在投注号码中，即中[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSix.CathecticGroupThreeFrontSix"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组六单式",
                            MinOdds = 150,
                            MaxOdds = 166.666666m,
                            SortIndex = 2,
                            Description = "手动输入号码，3个数字为一注，所选号码与开奖号码的前三位相同，顺序不限，奖金 [jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSix.CathecticGroupThreeFrontSixSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选和值",
                            MinOdds = 129,
                            MaxOdds = 143.333333m,
                            SortIndex = 3,
                            Description = "所选和值与开奖号码前三位和值一致即为中奖。奖金[jj]元；组三奖金两倍；豹子奖金三倍。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSum.CathecticGroupThreeFrontSum"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "直选单式",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 4,
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeSingle.CathecticThreeFrontSingle"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "中三",
                    SortIndex = 1,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 0,
                            Description = "每位各选1个或多个号码，选号与奖号中三位一一对应，中奖[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeDuplex.CathecticThreeMiddleDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组三",
                            MinOdds = 300,
                            MaxOdds = 333.333333M,
                            SortIndex = 1,
                            Description = "组三是指开奖号码中三位任意两位号码相同，如188。至少选2个号码投注，开奖号中三位为组三号且包含在投注号码中，即中[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeThree.CathecticGroupThreeMiddleThree"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组三单式",
                            MinOdds = 300,
                            MaxOdds = 333.333333M,
                            SortIndex = 1,
                            Description = "手动输入号码，至少输入 1 个三位数号码。(三个数字当中有二个数字相同),顺序不限，奖金 [jj]元",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeThree.CathecticGroupThreeMiddleThreeSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组六",
                            MinOdds = 150,
                            MaxOdds = 163.5m,
                            SortIndex = 2,
                            Description = "组六是指开奖号码中三位三个号码各不相同，如135。至少选3个号码投注，开奖号中三位为组六号且包含在投注号码中，即中[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSix.CathecticGroupThreeMiddleSix"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组六单式",
                            MinOdds = 150,
                            MaxOdds = 163.5m,
                            SortIndex = 2,
                            Description = "手动输入号码，3个数字为一注，所选号码与开奖号码的中三位相同，顺序不限，奖金 [jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSix.CathecticGroupThreeMiddleSixSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选和值",
                            MinOdds = 129,
                            MaxOdds = 143.333333m,
                            SortIndex = 3,
                            Description = "所选和值与开奖号码中三位和值一致即为中奖。奖金[jj]元；组三奖金两倍；豹子奖金三倍。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSum.CathecticGroupThreeMiddleSum"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "直选单式",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 4,
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeSingle.CathecticThreeMiddleSingle"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "后三",
                    SortIndex = 2,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 0,
                            Description = "每位各选1个或多个号码，选号与奖号后三位一一对应，中奖[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeDuplex.CathecticThreeBehindDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组三",
                            MinOdds = 300,
                            MaxOdds = 333.333333m,
                            Description = "组三是指开奖号码后三位任意两位号码相同，如188。至少选2个号码投注，开奖号后三位为组三号且包含在投注号码中，即中[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeThree.CathecticGroupThreeBehindThree"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组三单式",
                            MinOdds = 300,
                            MaxOdds = 333.333333m,
                            Description = "手动输入号码，至少输入 1 个三位数号码。(三个数字当中有二个数字相同),顺序不限，奖金 [jj]元",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeThree.CathecticGroupThreeBehindThreeSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组六",
                            MinOdds = 150,
                            MaxOdds = 166.666666m,
                            SortIndex = 2,
                            Description = "组六是指开奖号码后三位三个号码各不相同，如135。至少选3个号码投注，开奖号后三位为组六号且包含在投注号码中，即中[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSix.CathecticGroupThreeBehindSix"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组六单式",
                            MinOdds = 150,
                            MaxOdds = 166.666666m,
                            SortIndex = 2,
                            Description = "手动输入号码，3个数字为一注，所选号码与开奖号码的后三位相同，顺序不限，奖金 [jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSix.CathecticGroupThreeBehindSixSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选和值",
                            MinOdds = 129,
                            MaxOdds = 143.333333m,
                            SortIndex = 3,
                            Description = "所选和值与开奖号码后三位和值一致即为中奖。奖金[jj]元；组三奖金两倍；豹子奖金三倍。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupThreeSum.CathecticGroupThreeBehindSum"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "直选单式",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 4,
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticThreeSingle.CathecticThreeBehindSingle"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "二星",
                    SortIndex = 3,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 90,
                            MaxOdds = 100,
                            SortIndex = 0,
                            Description = "每位各选1个或多个号，所选号与开奖号后两位相同（且顺序一致），即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticTwoBehindDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选",
                            MinOdds = 45,
                            MaxOdds = 50,
                            SortIndex = 1,
                            Description = "从0～9中选2个或多个号码，选号与奖号后二位相同（顺序不限，不含对子号），即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupTwoBehindDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选单式",
                            MinOdds = 45,
                            MaxOdds = 50,
                            SortIndex = 2,
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticGroupTwoBehindSingle"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "定位胆",
                    SortIndex = 4,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "五星定位胆",
                            MinOdds = 9,
                            MaxOdds = 10,
                            Description = "在万位、千位、百位、十位、个位任意位置上任意选择1个或1个以上号码即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticFixedBileFiveStart"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "趣味",
                    SortIndex = 5,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "一帆风顺",
                            MinOdds = 2.197748m,
                            MaxOdds = 2.441942m,
                            SortIndex = 0,
                            Description = "至少出现一个号码，即中一帆风顺，中奖可得[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticFunny.CathecticFunnyYFFS"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "好事成双",
                            MinOdds = 11.048367m,
                            MaxOdds = 12.275963m,
                            SortIndex = 1,
                            Description = "至少出现两个相同号码，即中好事成双，中奖可得[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticFunny.CathecticFunnyHSCS"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "三星报喜",
                            MinOdds = 105.140186m,
                            MaxOdds = 116.822429m,
                            SortIndex = 2,
                            Description = "至少出现三个相同号码，即中三星报喜，中奖可得[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticFunny.CathecticFunnySXBX"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "四季发财",
                            MinOdds = 1956.521739m,
                            MaxOdds = 2173.913043m,
                            SortIndex = 3,
                            Description = "至少出现四个相同号码，即中四季发财，中奖可得[jj]元。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiShiCai.CathecticFunny.CathecticFunnySJFC"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "龙虎",
                    SortIndex = 6,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "龙",
                            MinOdds = 2,
                            MaxOdds = 2.222222m,
                            SortIndex = 0,
                            Description = "从对应两个位上选择一个形态组成一注，前者大于后者为“龙”，反之为“虎”，相等为“和”，中奖可得[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticDT.CathecticDTDragon"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "虎",
                            MinOdds = 2,
                            MaxOdds = 2.222222m,
                            SortIndex = 1,
                            Description = "从对应两个位上选择一个形态组成一注，前者大于后者为“龙”，反之为“虎”，相等为“和”中奖可得[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticDT.CathecticDTTiger"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "和",
                            MinOdds = 9,
                            MaxOdds = 10,
                            SortIndex = 2,
                            Description = "从对应两个位上选择一个形态组成一注，前者大于后者为“龙”，反之为“虎”，相等为“和”中奖可得[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.CathecticDT.CathecticDTSum"
                        },
                    }
                },
                new LotteryPlayType()
                {
                    Name = "信用玩法",
                    SortIndex = 7,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "整合",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 0,
                            ReflectClass = "AllLottery.Business.Cathectic.ShiShiCai.XinYongPan.ShuangMianPan"
                        }
                    }
                }
            };
        }

        public static List<LotteryPlayType> CreateKuai3()
        {
            return new List<LotteryPlayType>()
            {
                new LotteryPlayType()
                {
                    Name = "和值",
                    SortIndex = 0,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "普通投注",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 0,
                            Description = "猜3个开奖号相加的和，3-10为小，11-18为大。",
                            ReflectClass = "AllLottery.Business.Cathectic.KuaiSan.CathecticSum"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "三同号通选",
                    SortIndex = 1,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "普通投注",
                            MinOdds = 32.4M,
                            MaxOdds = 36,
                            SortIndex = 0,
                            Description = "对所有相同的三个号码（111、222、333、444、555、666）进行投注，任意号码开即中奖[jj]元",
                            ReflectClass = "AllLottery.Business.Cathectic.KuaiSan.CathecticThreeTogether"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "三同号单选",
                    SortIndex = 2,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "普通投注",
                            MinOdds = 194.4M,
                            MaxOdds = 216,
                            SortIndex = 0,
                            Description = "对相同的三个号码（111、222、333、444、555、666）中的任意一个进行投注，所选号码开出即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.KuaiSan.CathecticThreeSingle"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "三不同号",
                    SortIndex = 3,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "普通投注",
                            MinOdds = 32.4M,
                            MaxOdds = 36,
                            SortIndex = 0,
                            Description = "从1～6中任选3个或多个号码，所选号码与开奖号码的3个号码相同即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.KuaiSan.CathecticThreeDifferent"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "三连号通选",
                    SortIndex = 3,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "普通投注",
                            MinOdds = 8.1M,
                            MaxOdds = 9,
                            SortIndex = 0,
                            Description = "对所有3个相连的号码（123、234、345、456）进行投注，任意号码开出即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.KuaiSan.CathecticThreeContinueTogether"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "二同号复选",
                    SortIndex = 4,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "普通投注",
                            MinOdds = 12.15M,
                            MaxOdds = 13.5M,
                            SortIndex = 0,
                            Description = "从11～66中任选1个或多个号码，选号与奖号（包含11～66，不限顺序）相同即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.KuaiSan.CathecticTwoTogether"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "二同号单选",
                    SortIndex = 4,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "普通投注",
                            MinOdds = 64.8M,
                            MaxOdds = 72,
                            SortIndex = 0,
                            Description = "选择1对相同号码和1个不同号码投注，选号与奖号相同（顺序不限）即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.KuaiSan.CathecticTwoSingle"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "二不同号",
                    SortIndex = 5,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "普通投注",
                            MinOdds = 6.48M,
                            MaxOdds = 7.2M,
                            SortIndex = 0,
                            Description = "从1～6中任选2个或多个号码，所选号码与开奖号码任意2个号码相同即中奖[jj]元。！",
                            ReflectClass = "AllLottery.Business.Cathectic.KuaiSan.CathecticTwoDifferent"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "信用玩法",
                    SortIndex = 6,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "大小骰宝",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 0,
                            ReflectClass = "AllLottery.Business.Cathectic.KuaiSan.Credit.KuaiSanCredit"
                        }
                    }
                }
            };
        }

        public static List<LotteryPlayType> CreateSaiChe()
        {
            return new List<LotteryPlayType>()
            {
                new LotteryPlayType()
                {
                    Name = "猜冠军",
                    SortIndex = 0,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "猜冠军",
                            MinOdds = 9,
                            MaxOdds = 10,
                            SortIndex = 0,
                            Description = "选择1个数字号码竞猜全部开奖号码，投注号码与开奖号码前1相同即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.PkShi.CathecticGuessChampion"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "猜冠亚军",
                    SortIndex = 1,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "猜冠亚军",
                            MinOdds = 81,
                            MaxOdds = 90,
                            SortIndex = 0,
                            Description = "选择2个数字号码竞猜全部开奖号码，投注号码与开奖号码前2相同即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.PkShi.CathecticGuessChampionAndRunnerUp"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "猜前三名",
                    SortIndex = 2,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "猜前三名",
                            MinOdds = 648,
                            MaxOdds = 720,
                            SortIndex = 0,
                            Description = "选择3个数字号码竞猜全部开奖号码，投注号码与开奖号码前3相同即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.PkShi.CathecticGuessTopThreeQuota"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "定位胆选",
                    SortIndex = 3,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "定位胆选",
                            MinOdds = 9,
                            MaxOdds = 10,
                            SortIndex = 0,
                            Description = "任意选择1个或1个以上号码。投注号码与开奖号码相同即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.PkShi.CathecticFixedBilePosition"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "信用玩法",
                    SortIndex = 4,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "整合",
                            MinOdds = 0.9M,
                            MaxOdds = 1M,
                            SortIndex = 0,
                            ReflectClass = "AllLottery.Business.Cathectic.PkShi.Credit.PkCredit"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "冠亚组合",
                            MinOdds = 40.5M,
                            MaxOdds = 45,
                            SortIndex = 1,
                            ReflectClass = "AllLottery.Business.Cathectic.PkShi.Credit.GuanYaZuHe"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "冠亚军和",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 2,
                            ReflectClass = "AllLottery.Business.Cathectic.PkShi.Credit.GuanYaJunHe"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "龙虎斗",
                            MinOdds = 1.8M,
                            MaxOdds = 2,
                            SortIndex = 3,
                            ReflectClass = "AllLottery.Business.Cathectic.PkShi.Credit.LongHuDou"
                        }
                    }
                }
            };
        }

        public static List<LotteryPlayType> CreateXuan5()
        {
            return new List<LotteryPlayType>()
            {
                new LotteryPlayType()
                {
                    Name = "前一",
                    SortIndex = 0,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 9.9m,
                            MaxOdds = 11,
                            SortIndex = 0,
                            Description = "从01～11中任选1个或多个号码，所选号码与开奖号码第一位号码相同，即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticFrontOne"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "前二",
                    SortIndex = 1,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 99,
                            MaxOdds = 110,
                            SortIndex = 0,
                            Description = "前2位各选1个或多个号码投注，所选号码与开奖号码前两位号码相同（且顺序一致），即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticFrontTwoVertical"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选投注",
                            MinOdds = 49.5m,
                            MaxOdds = 55,
                            SortIndex = 1,
                            Description = "从01～11中任选2个或多个号码，所选号码与开奖号码前两位号码相同（顺序不限），即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticFrontTwoGroupChoose"
                        },
                    }
                },
                new LotteryPlayType()
                {
                    Name = "前三",
                    SortIndex = 2,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 891,
                            MaxOdds = 990,
                            SortIndex = 0,
                            Description = "前3位各选1个或多个号码投注，选号与开奖号码前三位相同（且顺序一致），即中[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticFrontThreeVertical"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选投注",
                            MinOdds = 148.5m,
                            MaxOdds = 165,
                            SortIndex = 0,
                            Description = "从01～11中选3或多个号码，选号与开奖号码前三位相同（顺序不限），即中奖[jj]元！",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticFrontThreeGroupChoose"
                        },
                    }
                },
                new LotteryPlayType()
                {
                    Name = "单式",
                    SortIndex = 3,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "前二单式",
                            MinOdds = 99,
                            MaxOdds = 110,
                            SortIndex = 0,
                            Description = "与前后任选二星单复式同理，只是下注号改为2位。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticSingle.CathecticTwoFrontSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "后二单式",
                            MinOdds = 99,
                            MaxOdds = 110,
                            SortIndex = 0,
                            Description = "与前后任选二星单复式同理，只是下注号改为2位。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticSingle.CathecticTwoBehindSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "前三单式",
                            MinOdds = 891,
                            MaxOdds = 990,
                            SortIndex = 0,
                            Description = "与前后任选三星单复式同理，只是下注号改为3位。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticSingle.CathecticThreeFrontSingle"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "后三单式",
                            MinOdds = 891,
                            MaxOdds = 990,
                            SortIndex = 0,
                            Description = "与前后任选三星单复式同理，只是下注号改为3位。",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticSingle.CathecticThreeBehindSingle"
                        },
                    }
                },
                new LotteryPlayType()
                {
                    Name = "定位胆",
                    SortIndex = 4,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "定位胆",
                            MinOdds = 9.9m,
                            MaxOdds = 11,
                            SortIndex = 0,
                            Description = "从第一位，第二位，第三位任意位置上任意选择1个或1个以上号码。所选号码与开奖号码相同，即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticFixedBilePosition"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "任选复式",
                    SortIndex = 5,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "任选一中一",
                            MinOdds =1.98m ,
                            MaxOdds = 2.2m,
                            SortIndex = 0,
                            Description = "从01-11共11个号码中选择1个号码进行购买，当期的5个开奖号码中包含所选号码，即中奖[jj]元！",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticAnyHit.CathecticAnyOneHitOne"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "任选二中二",
                            MinOdds = 4.95m,
                            MaxOdds = 5.5m,
                            SortIndex = 0,
                            Description = "从01-11共11个号码中选择2个号码进行购买，只要当期的5个开奖号码中包含所选号，即中奖[jj]元！",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticAnyHit.CathecticAnyTwoHitTwo"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "任选三中三",
                            MinOdds = 14.85m,
                            MaxOdds = 16.5m,
                            SortIndex = 0,
                            Description = "从01-11共11个号码中选择3个号码进行购买，只要当期的5个开奖号码中包含所选号码，即中奖[jj]元！",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticAnyHit.CathecticAnyThreeHitThree"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "任选四中四",
                            MinOdds = 59.4m,
                            MaxOdds = 66,
                            SortIndex = 0,
                            Description = "从01-11共11个号码中选择4个号码进行购买，只要当期的5个开奖号码中包含所选号码，即中奖[jj]元！",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticAnyHit.CathecticAnyFourHitFour"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "任选五中五",
                            MinOdds = 415.8m,
                            MaxOdds = 462,
                            SortIndex = 0,
                            Description = "从01-11共11个号码中选择5个号码进行购买，只要当期的5个开奖号码中包含所选号码，即中奖[jj]元！",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticAnyHit.CathecticAnyFiveHitFive"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "任选六中五",
                            MinOdds = 69.3m,
                            MaxOdds = 77,
                            SortIndex = 0,
                            Description = "从01-11共11个号码中选择6个号码进行购买，只要当期的5个开奖号码中包含所选号码，即中奖[jj]元！",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticAnyHit.CathecticAnySixHitFive"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "任选七中五",
                            MinOdds = 19.8m,
                            MaxOdds = 22,
                            SortIndex = 0,
                            Description = "从01-11共11个号码中选择7个号码进行购买，只要当期的5个开奖号码中包含所选号码，即中奖[jj]元！",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticAnyHit.CathecticAnySevenHitFive"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "任选八中五",
                            MinOdds = 7.425m,
                            MaxOdds = 8.25m,
                            SortIndex = 0,
                            Description = "从01-11共11个号码中选择8个号码进行购买，只要当期的5个开奖号码中包含所选号码，即中奖[jj]元！",
                            ReflectClass =
                                "AllLottery.Business.Cathectic.ShiYiXuanWu.CathecticAnyHit.CathecticAnyEightHitFive"
                        },
                    }
                },
                new LotteryPlayType()
                {
                    Name = "信用玩法",
                    SortIndex = 6,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "整合",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 0,
                            ReflectClass = "AllLottery.Business.Cathectic.ShiYiXuanWu.Credit.Xuan5Credit"
                        }
                    }
                }
            };
        }

        public static List<LotteryPlayType> Create3D()
        {
            return new List<LotteryPlayType>()
            {
                new LotteryPlayType()
                {
                    Name = "三星",
                    SortIndex = 0,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 0,
                            Description = "每位各选1个或多个号码，选号与奖号一一对应，中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticThreeDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组三",
                            MinOdds = 300,
                            MaxOdds = 333.333333m,
                            SortIndex = 1,
                            Description = "组三是指开奖号码任意两位号码相同，如188。至少选2个号码投注，开奖号为组三号且包含在投注号码中，即中[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticGroupThreeThree"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组六",
                            MinOdds = 150,
                            MaxOdds = 166.666666m,
                            SortIndex = 2,
                            Description = "组六是指开奖号码三个号码各不相同，如135。至少选3个号码投注，开奖号为组六号且包含在投注号码中，即中[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticGroupSix"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选和值",
                            MinOdds = 129,
                            MaxOdds = 143.333333m,
                            SortIndex = 3,
                            Description = "所选和值与开奖号码三位和值一致即为中奖。奖金[jj]元；组三奖金两倍；豹子奖金三倍。",
                            ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticGroupSum"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "单式上传",
                            MinOdds = 900,
                            MaxOdds = 1000,
                            SortIndex = 4,
                            ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticThreeSingle"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "二星",
                    SortIndex = 3,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 90,
                            MaxOdds = 100,
                            SortIndex = 0,
                            Description = "每位各选1个或多个号，所选号与开奖号后两位相同（且顺序一致），即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticTwoBehindDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "组选",
                            MinOdds = 45,
                            MaxOdds = 50,
                            SortIndex = 1,
                            Description = "从0～9中选2个或多个号码，选号与奖号后二位相同（顺序不限，不含对子号），即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticGroupTwoBehindDuplex"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "单式上传",
                            MinOdds = 45,
                            MaxOdds = 50,
                            SortIndex = 2,
                            ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticGroupTwoBehindSingle"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "定位胆",
                    SortIndex = 4,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "定位胆",
                            MinOdds = 9,
                            MaxOdds = 10,
                            Description = "在百位、十位、个位任意位置上任意选择1个或1个以上号码即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticFixedBileThreeStart"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "不定胆",
                    SortIndex = 5,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "不定胆",
                            MinOdds = 3.321033M,
                            MaxOdds = 3.690036M,
                            Description = "开奖号码中，至少出现一个你所选择号码，即中[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticUnFixed"
                        }
                    }
                },
                new LotteryPlayType()
                {
                    Name = "大小单双",
                    SortIndex = 6,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "大小单双",
                            MinOdds = 3.6M,
                            MaxOdds = 4,
                            Description = "大（56789）小（01234）、单（13579）双（02468）形态进行购买，所选号码的形态与开奖号码的位置、形态相同，即中[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.PaiLie3D.CathecticBSSB"
                        }
                    }
                },
            };
        }

        public static List<LotteryPlayType> CreateLiuHeCai()
        {
            return new List<LotteryPlayType>(){
                new LotteryPlayType()
                {
                    Name = "特码",
                    SortIndex = 0,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "直选",
                            MinOdds = 44.1M,
                            MaxOdds = 49,
                            Description = "从1-49中任选1个或多个号码，每个号码为一注，所选号码中包含特码，即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticZhiXuan.CathecticZhiXuanSeven"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "双面",
                            MinOdds = 0.000044M,
                            MaxOdds = 0.000049M,
                            Description = "开奖号码最后一位为特码。大于或等于25为特码大，小于或等于24为特码小；奇数为单，偶数为双；特码两个数相加后得数，奇数为合单，偶数为合双，小于等于6为合小，大于6为合大；尾大尾小即看特码个位数值，小于等于4为小，大于4为大；特码为49时为和，不算任何大小单双，但算波色。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticShuangMian.CathecticShuangMianSeven"
                        }
                    },
                },
                new LotteryPlayType()
                {
                    Name = "正码",
                    SortIndex = 1,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "任选",
                            MinOdds = 7.35M,
                            MaxOdds = 8.166666M,
                            SortIndex = 0,
                            Description = "从1-49中任选1个或多个号码，每个号码为一注，所选号码在开奖号码前六位中存在，即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticRenXuan"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正1特",
                            MinOdds = 44.1M,
                            MaxOdds = 49,
                            SortIndex = 1,
                            Description = "从1-49中任选1个或多个号码，每个号码为一注，所选号码与开奖号码第一位相同，即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticZhiXuan.CathecticZhiXuanOne"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正1双面",
                            MinOdds = 0.000044M,
                            MaxOdds = 0.000049M,
                            SortIndex = 2,
                            Description = "开奖号码第一位，大于或等于25为大，小于或等于24为小；奇数为单，偶数为双；两个数相加后得数，奇数为合单，偶数为合双，小于等于6为合小，大于6为合大；尾大尾小即看个位数值，小于等于4为小，大于4为大；为49时为和，不算任何大小单双，但算波色。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticShuangMian.CathecticShuangMianOne"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正2特",
                            MinOdds = 44.1M,
                            MaxOdds = 49,
                            SortIndex = 3,
                            Description = "从1-49中任选1个或多个号码，每个号码为一注，所选号码与开奖号码第二位相同，即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticZhiXuan.CathecticZhiXuanTwo"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正2双面",
                            MinOdds = 0.000044M,
                            MaxOdds = 0.000049M,
                            SortIndex = 4,
                            Description = "开奖号码第二位，大于或等于25为大，小于或等于24为小；奇数为单，偶数为双；两个数相加后得数，奇数为合单，偶数为合双，小于等于6为合小，大于6为合大；尾大尾小即看个位数值，小于等于4为小，大于4为大；为49时为和，不算任何大小单双，但算波色。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticShuangMian.CathecticShuangMianTwo"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正3特",
                            MinOdds = 44.1M,
                            MaxOdds = 49,
                            SortIndex = 5,
                            Description = "从1-49中任选1个或多个号码，每个号码为一注，所选号码与开奖号码第三位相同，即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticZhiXuan.CathecticZhiXuanThree"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正3双面",
                            MinOdds = 0.000044M,
                            MaxOdds = 0.000049M,
                            SortIndex = 6,
                            Description = "开奖号码第三位，大于或等于25为大，小于或等于24为小；奇数为单，偶数为双；两个数相加后得数，奇数为合单，偶数为合双，小于等于6为合小，大于6为合大；尾大尾小即看个位数值，小于等于4为小，大于4为大；为49时为和，不算任何大小单双，但算波色。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticShuangMian.CathecticShuangMianThree"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正4特",
                            MinOdds = 44.1M,
                            MaxOdds = 49,
                            SortIndex = 7,
                            Description = "从1-49中任选1个或多个号码，每个号码为一注，所选号码与开奖号码第四位相同，即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticZhiXuan.CathecticZhiXuanFour"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正4双面",
                            MinOdds = 0.000044M,
                            MaxOdds = 0.000049M,
                            SortIndex = 8,
                            Description = "开奖号码第四位，大于或等于25为大，小于或等于24为小；奇数为单，偶数为双；两个数相加后得数，奇数为合单，偶数为合双，小于等于6为合小，大于6为合大；尾大尾小即看个位数值，小于等于4为小，大于4为大；为49时为和，不算任何大小单双，但算波色。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticShuangMian.CathecticShuangMianFour"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正5特",
                            MinOdds = 44.1M,
                            MaxOdds = 49,
                            SortIndex = 9,
                            Description = "从1-49中任选1个或多个号码，每个号码为一注，所选号码与开奖号码第五位相同，即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticZhiXuan.CathecticZhiXuanFive"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正5双面",
                            MinOdds = 0.000044M,
                            MaxOdds = 0.000049M,
                            SortIndex = 10,
                            Description = "开奖号码第五位，大于或等于25为大，小于或等于24为小；奇数为单，偶数为双；两个数相加后得数，奇数为合单，偶数为合双，小于等于6为合小，大于6为合大；尾大尾小即看个位数值，小于等于4为小，大于4为大；为49时为和，不算任何大小单双，但算波色。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticShuangMian.CathecticShuangMianFive"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正6特",
                            MinOdds = 44.1M,
                            MaxOdds = 49,
                            SortIndex = 11,
                            Description = "从1-49中任选1个或多个号码，每个号码为一注，所选号码与开奖号码第六位相同，即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticZhiXuan.CathecticZhiXuanSix"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正6双面",
                            MinOdds = 0.000044M,
                            MaxOdds = 0.000049M,
                            SortIndex = 12,
                            Description = "开奖号码第六位，大于或等于25为大，小于或等于24为小；奇数为单，偶数为双；两个数相加后得数，奇数为合单，偶数为合双，小于等于6为合小，大于6为合大；尾大尾小即看个位数值，小于等于4为小，大于4为大；为49时为和，不算任何大小单双，但算波色。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticShuangMian.CathecticShuangMianSix"
                        },
                    },
                },
                new LotteryPlayType()
                {
                    Name = "连码",
                    SortIndex = 2,
                    LotteryPlayDetails = new EditableList<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "三全中",
                            MinOdds = 829.08M,
                            MaxOdds = 921.2M,
                            SortIndex = 1,
                            Description = "至少选择三个号码，每三个号码为一组合，若三个号码都是开奖号码之正码，即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticSanQuanZhong"
                        },
                        new LotteryPlayDetail()
                        {
                            Name="三中二",
                            MinOdds = 2.7M,
                            MaxOdds = 3,
                            SortIndex = 2,
                            Description = "至少选择三个号码，每三个号码为一组合，若其中至少有两个是开奖号码中的正码，即为中奖。若中两码，叫三中二之中二;若三码全中，叫三中二之中三。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticSanZhongEr"
                        },
                        new LotteryPlayDetail()
                        {
                            Name="二全中",
                            MinOdds = 70.56M,
                            MaxOdds = 78.4M,
                            SortIndex = 3,
                            Description = "至少选择两个号码，每二个码号为一组合，二个号码都是开奖码号之正码（不含特码），即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticErQuanZhong"
                        },
                        new LotteryPlayDetail()
                        {
                            Name="二中特",
                            MinOdds = 1.8M,
                            MaxOdds = 2,
                            SortIndex = 4,
                            Description = "至少选择两个号码，每二个号码为一组合，二个号码都是开奖号码（含特码），即为中奖。若两个都是正码，叫二中特之二中。若选号中包含特码，叫二中特之中特。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticErZhongTe"
                        },
                        new LotteryPlayDetail()
                        {
                            Name="特串",
                            MinOdds = 176.4M,
                            MaxOdds = 196,
                            SortIndex = 5,
                            Description = "至少选择两个号码，每二个号码为一组合，其中一个是正码，一个是特别号码，即中奖[jj]元！",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticTeChuan"
                        },
                    }
                },
                new LotteryPlayType()
                {
                    Name = "半波",
                    SortIndex = 3,
                    LotteryPlayDetails = new EditableList<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "特码半波",
                            MinOdds = 0.0175M,
                            MaxOdds = 0.019444M,
                            SortIndex = 1,
                            Description = "根据特码对应的特性来区分。分为红蓝绿三个波色，以及号码大于或等于25为大，小于或等于24为小；奇数为单，偶数为双；合单合双为开奖号的十位与个位相加后得数的单双。下注内容与号码特性完全吻合即为中奖。特码为49时为和,不算任何大小单双。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticTeMaBanBo"
                        },
                    }
                },
                new LotteryPlayType()
                {
                    Name = "生肖",
                    SortIndex = 4,
                    LotteryPlayDetails = new EditableList<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "特肖",
                            MinOdds = 2.205M,
                            MaxOdds = 2.45M,
                            SortIndex = 1,
                            Description = "从十二生肖中任选1个或多个，每个生肖为一注，所选生肖与特码对应的生肖相同，即为中奖。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticShengXiao.CathecticTeXiao"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "一肖",
                            MinOdds = 0.0009M,
                            MaxOdds = 0.001M,
                            SortIndex = 2,
                            Description = "从十二生肖中任选1个或多个，每个生肖为一注，开奖号码（含特码）中含有投注所属生肖，即为中奖。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticShengXiao.CathecticYiXiao"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "二肖连",
                            MinOdds = 0.018181M,
                            MaxOdds = 0.020202M,
                            SortIndex = 3,
                            Description = "至少选择两个生肖，每二个生肖为一组合，开奖号码（含特码）中含有投注所属全部生肖，即为中奖。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticShengXiao.CathecticErXiaoLian"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "三肖连",
                            MinOdds = 0.009090M,
                            MaxOdds = 0.010101M,
                            SortIndex = 4,
                            Description = "至少选择三个生肖，每三个生肖为一组合，开奖号码（含特码）中含有投注所属全部生肖，即为中奖。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticShengXiao.CathecticSanXiaoLian"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "四肖连",
                            MinOdds = 0.009090M,
                            MaxOdds = 0.010101M,
                            SortIndex = 5,
                            Description = "至少选择四个生肖，每四个生肖为一组合，开奖号码（含特码）中含有投注所属全部生肖，即为中奖。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticShengXiao.CathecticSiXiaoLian"
                        },
                    },
                },
                new LotteryPlayType()
                {
                    Name = "尾数",
                    SortIndex = 5,
                    LotteryPlayDetails = new EditableList<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "特码尾数",
                            MinOdds = 0.245M,
                            MaxOdds = 0.272222M,
                            SortIndex = 1,
                            Description = "选择特码头（十位）尾（个位）的数字进行投注，与特码相同，即为中奖",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticWeiShu.CathecticTeMaWeiShu"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "二尾连",
                            MinOdds = 0.018181M,
                            MaxOdds = 0.020202M,
                            SortIndex = 2,
                            Description = "至少选择两个尾数，每两个尾数为一组合，开奖号码（含特码）中含有投注对应全部尾数，即为中奖。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticWeiShu.CathecticErWeiLian"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "三尾连",
                            MinOdds = 0.009090M,
                            MaxOdds = 0.010101M,
                            SortIndex = 3,
                            Description = "至少选择三个尾数，每三个尾数为一组合，开奖号码（含特码）中含有投注对应全部尾数，即为中奖。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticWeiShu.CathecticSanWeiLian"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "四尾连",
                            MinOdds = 0.036363M,
                            MaxOdds = 0.040404M,
                            SortIndex = 4,
                            Description = "至少选择四个尾数，每四个尾数为一组合，开奖号码（含特码）中含有投注对应全部尾数，即为中奖。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticWeiShu.CathecticSiWeiLian"
                        },
                    }
                },
                new LotteryPlayType()
                {
                    Name = "不中",
                    SortIndex = 6,
                    LotteryPlayDetails = new EditableList<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "五不中",
                            MinOdds = 2.017468M,
                            MaxOdds = 2.241631M,
                            SortIndex = 1,
                            Description = "至少选择五个号码，每五个号码为一注，所有号码均未在开奖号码中出现，即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticBuZhong.CathecticWuBuZhong"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "六不中",
                            MinOdds = 2.399150M,
                            MaxOdds = 2.665723M,
                            SortIndex = 2,
                            Description = "至少选择六个号码，每六个号码为一注，所有号码均未在开奖号码中出现，即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticBuZhong.CathecticLiuBuZhong"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "七不中",
                            MinOdds = 2.865652M,
                            MaxOdds = 3.184058M,
                            SortIndex = 3,
                            Description = "至少选择七个号码，每七个号码为一注，所有号码均未在开奖号码中出现，即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticBuZhong.CathecticQiBuZhong"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "八不中",
                            MinOdds = 3.438783M,
                            MaxOdds = 3.820870M,
                            SortIndex = 4,
                            Description = "至少选择八个号码，每八个号码为一注，所有号码均未在开奖号码中出现，即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticBuZhong.CathecticBaBuZhong"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "九不中",
                            MinOdds = 4.146768M,
                            MaxOdds = 4.607520M,
                            SortIndex = 5,
                            Description = "至少选择九个号码，每九个号码为一注，所有号码均未在开奖号码中出现，即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticBuZhong.CathecticJiuBuZhong"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "十不中",
                            MinOdds = 5.026385M,
                            MaxOdds = 5.584873M,
                            SortIndex = 6,
                            Description = "至少选择十个号码，每十六个号码为一注，所有号码均未在开奖号码中出现，即中奖[jj]元。",
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.CathecticBuZhong.CathecticShiBuZhong"
                        },
                    }
                },
                new LotteryPlayType()
                {
                    Name = "信用玩法",
                    SortIndex = 7,
                    LotteryPlayDetails = new List<LotteryPlayDetail>()
                    {
                        new LotteryPlayDetail()
                        {
                            Name = "正码一",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 0,
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.Credit.LiuHeCaiCredit"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正码二",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 1,
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.Credit.LiuHeCaiCredit"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正码三",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 2,
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.Credit.LiuHeCaiCredit"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正码四",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 3,
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.Credit.LiuHeCaiCredit"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正码五",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 4,
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.Credit.LiuHeCaiCredit"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "正码六",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 5,
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.Credit.LiuHeCaiCredit"
                        },
                        new LotteryPlayDetail()
                        {
                            Name = "特码",
                            MinOdds = 0.00009M,
                            MaxOdds = 0.0001M,
                            SortIndex = 6,
                            ReflectClass = "AllLottery.Business.Cathectic.LiuHeCai.Credit.LiuHeCaiCredit"
                        }
                    }
                }
            };
        }

        public static void AddLottery()
        {
            _context.LotteryClassifies.Add(new LotteryClassify()
            {
                Type = LotteryClassifyType.Ssc,
                LotteryTypes = new List<LotteryType>()
                {
                    new LotteryType()
                    {
                        Name = "雅福时时彩",
                        CalType = LotteryCalNumberTypeEnum.Automatic,
                        NumberLength = 7,
                        RiskTime = new TimeSpan(0, 0, 1),
                        Icon = new Affix() {Path = "Image/Lottery/雅福时时彩.png"},
                        LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime(){BeginTime = new TimeSpan(0,0,0),EndTime = new TimeSpan(23,59,0),Interval = new TimeSpan(0,1,0)}),
                        LotteryPlayTypes=CreateSsc(),
                        Description = "1分钟1期",
                        SortIndex = 0,
                        IsHot = true
                    },
                    new LotteryType()
                    {
                        Name = "雅福三分彩",
                        CalType = LotteryCalNumberTypeEnum.Automatic,
                        NumberLength = 7,
                        RiskTime = new TimeSpan(0, 0, 1),
                        Icon = new Affix() {Path = "Image/Lottery/雅福时时彩.png"},
                        LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime(){BeginTime = new TimeSpan(0,0,0),EndTime = new TimeSpan(23,59,0),Interval = new TimeSpan(0,3,0)}),
                        LotteryPlayTypes=CreateSsc(),
                        Description = "3分钟1期",
                        SortIndex = 0,
                        IsHot = true
                    },
                    new LotteryType()
                    {
                        Name = "雅福五分彩",
                        CalType = LotteryCalNumberTypeEnum.Automatic,
                        NumberLength = 7,
                        RiskTime = new TimeSpan(0, 0, 1),
                        Icon = new Affix() {Path = "Image/Lottery/雅福时时彩.png"},
                        LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime(){BeginTime = new TimeSpan(0,0,0),EndTime = new TimeSpan(23,59,0),Interval = new TimeSpan(0,5,0)}),
                        LotteryPlayTypes=CreateSsc(),
                        Description = "5分钟1期",
                        SortIndex = 0,
                        IsHot = true
                    },
                    new LotteryType(){Name = "重庆时时彩",SpiderName = "10060",SortIndex = 0,Description = "20分钟1期",NumberLength=3,LotteryOpenTimes = GetLotteryOpenTimes(
                            new []{new IntiLottertTime(){BeginTime = new TimeSpan(0,30,0),EndTime = new TimeSpan(3,10,0),Interval = new TimeSpan(0,20,0)},
                                new IntiLottertTime(){BeginTime = new TimeSpan(7,30,0),EndTime = new TimeSpan(23,50,0),Interval = new TimeSpan(0,20,0)}}),
                        CalType = LotteryCalNumberTypeEnum.FromZeroEveryDay,DateFormat="yyyyMMdd",Icon = new Affix(){Path = "Image/Lottery/重庆时时彩.png"},LotteryPlayTypes=CreateSsc(),},
                    new LotteryType(){Name = "天津时时彩",SpiderName = "10003",SortIndex = 0,Description = "20分钟1期",NumberLength=3,LotteryOpenTimes = GetLotteryOpenTimes(
                            new []{new IntiLottertTime(){BeginTime = new TimeSpan(9,20,0),EndTime = new TimeSpan(23,0,0),Interval = new TimeSpan(0,20,0)}}),
                        CalType = LotteryCalNumberTypeEnum.FromZeroEveryDay,DateFormat="yyyyMMdd",Icon = new Affix(){Path = "Image/Lottery/天津时时彩.png"},LotteryPlayTypes=CreateSsc()},
                    new LotteryType(){Name = "新疆时时彩",SpiderName = "10004",SortIndex = 0,Description = "20分钟1期",NumberLength=2,LotteryOpenTimes = GetLotteryOpenTimes(
                            new []{new IntiLottertTime(){BeginTime = new TimeSpan(10,20,0),EndTime = new TimeSpan(1,2,0,0),Interval = new TimeSpan(0,20,0)}}),
                        CalType = LotteryCalNumberTypeEnum.FromZeroEveryDay,DateFormat="yyyyMMdd",Icon = new Affix(){Path = "Image/Lottery/新疆时时彩.png"},LotteryPlayTypes=CreateSsc()},
                    new LotteryType(){Name = "北京快乐8",SpiderName = "10014",SortIndex = 0,Description = "5分钟1期",LotteryOpenTimes = GetLotteryOpenTimes(
                            new []{new IntiLottertTime(){BeginTime = new TimeSpan(9,4,0),EndTime = new TimeSpan(23,54,0),Interval = new TimeSpan(0,5,0)}}),
                        CalType = LotteryCalNumberTypeEnum.Increase,Icon = new Affix(){Path = "Image/Lottery/北京快乐8.png"},LotteryPlayTypes=CreateSsc()},
                    new LotteryType()
                    {
                        Name = "腾讯分分彩",
                        CalType = LotteryCalNumberTypeEnum.FromZeroEveryDay,
                        SpiderName = "txffc",
                        NumberLength = 4,
                        DateFormat="yyyyMMdd-",
                        RiskTime = new TimeSpan(0, 0, 5),
                        Icon = new Affix() {Path = "Image/彩种.png"},
                        LotteryOpenTimes = GetLotteryOpenTimes(true,new IntiLottertTime(){BeginTime = new TimeSpan(0,0,0),EndTime = new TimeSpan(23,59,0),Interval = new TimeSpan(0,1,0)}),
                        LotteryPlayTypes=CreateSsc(false),
                        Description = "1分钟1期",
                        SortIndex = 0,
                        IsEnable =false
                    },
                    //new LotteryType()
                    //{
                    //    Name="越南河内时时彩(快5)",
                    //    CalType = LotteryCalNumberTypeEnum.FromZeroEveryDay,
                    //    SpiderName = "viffc5",
                    //    NumberLength = 3,
                    //    LotteryPlayTypes=CreateSsc(),
                    //    Description = "5分钟1期",
                    //    DateFormat="yyyyMMdd",
                    //    LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime(){BeginTime = new TimeSpan(0,4,0),EndTime = new TimeSpan(23,59,0),Interval = new TimeSpan(0,5,0)}),
                    //    Icon = new Affix() {Path = "Image/彩种.png"},
                    //    SortIndex = 0
                    //}
                },
            });
            _context.LotteryClassifies.Add(new LotteryClassify()
            {
                Type = LotteryClassifyType.Kuai3,
                LotteryTypes = new List<LotteryType>()
                {
                    new LotteryType() { Name = "雅福快三", NumberLength = 7, CalType = LotteryCalNumberTypeEnum.Automatic, RiskTime = new TimeSpan(0, 0, 1), Icon = new Affix() { Path = "Image/Lottery/雅福快三.png" }, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(23, 59, 0),Interval = new TimeSpan(0,1,0) }),
                        LotteryPlayTypes=CreateKuai3(),
                        Description = "1分钟1期",
                        SortIndex = 1,
                        IsHot = true
                    },
                    new LotteryType() { Name = "雅福3分快三", NumberLength = 7, CalType = LotteryCalNumberTypeEnum.Automatic, RiskTime = new TimeSpan(0, 0, 1), Icon = new Affix() { Path = "Image/Lottery/雅福快三.png" }, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(23, 59, 0),Interval = new TimeSpan(0,3,0) }),
                        LotteryPlayTypes=CreateKuai3(),
                        Description = "3分钟1期",
                        SortIndex = 1,
                        IsHot = true
                    },
                    new LotteryType() { Name = "雅福5分快三", NumberLength = 7, CalType = LotteryCalNumberTypeEnum.Automatic, RiskTime = new TimeSpan(0, 0, 1), Icon = new Affix() { Path = "Image/Lottery/雅福快三.png" }, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(23, 59, 0),Interval = new TimeSpan(0,5,0) }),
                        LotteryPlayTypes=CreateKuai3(),
                        Description = "5分钟1期",
                        SortIndex = 1,
                        IsHot = true
                    },
                    new LotteryType()
                    {
                        Description = "20分钟一期",
                        SpiderName = "10007",
                        Name = "江苏快三" ,
                        SortIndex = 1,
                        NumberLength =3,
                        LotteryPlayTypes=CreateKuai3(),
                        LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime(){BeginTime = new TimeSpan(8,50,0),EndTime = new TimeSpan(22,10,0),Interval = new TimeSpan(0,20,0)}),
                        Icon = new Affix(){Path = "Image/Lottery/江苏快三.png"},
                    },
                    new LotteryType()
                    {
                        Description = "20分钟一期",
                        SpiderName = "10026",
                        Name = "广西快三" ,
                        SortIndex = 1,
                        NumberLength =3,
                        DateFormat="yyMMdd",
                        LotteryPlayTypes=CreateKuai3(),
                        LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime(){BeginTime = new TimeSpan(9,30,0),EndTime = new TimeSpan(22,30,0),Interval = new TimeSpan(0,20,0)}),
                        Icon = new Affix(){Path = "Image/Lottery/广西快三.png"},
                    },
                    new LotteryType()
                    {
                        Description = "20分钟一期",
                        SpiderName = "10028",
                        Name = "河北快三" ,
                        SortIndex = 1,
                        NumberLength =3,
                        DateFormat="yyyyMMdd",
                        LotteryPlayTypes=CreateKuai3(),
                        LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime(){BeginTime = new TimeSpan(8,50,0),EndTime = new TimeSpan(22,10,0),Interval = new TimeSpan(0,20,0)}),
                        Icon = new Affix(){Path = "Image/Lottery/河北快三.png"},
                    },
                    new LotteryType()
                    {
                        Description = "20分钟一期",
                        SpiderName = "10027",
                        Name = "吉林快三" ,
                        SortIndex = 1,
                        NumberLength =3,
                        DateFormat="yyMMdd",
                        LotteryPlayTypes=CreateKuai3(),
                        LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime(){BeginTime = new TimeSpan(8,40,0),EndTime = new TimeSpan(21,40,0),Interval = new TimeSpan(0,20,0)}),
                        Icon = new Affix(){Path = "Image/Lottery/吉林快三.png"},
                    },
                    new LotteryType()
                    {
                        Description = "20分钟一期",
                        SpiderName = "10030",
                        Name = "安徽快三" ,
                        SortIndex = 1,
                        NumberLength =3,
                        DateFormat="yyyyMMdd",
                        LotteryPlayTypes=CreateKuai3(),
                        LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime(){BeginTime = new TimeSpan(9,00,0),EndTime = new TimeSpan(22,0,0),Interval = new TimeSpan(0,20,0)}),
                        Icon = new Affix(){Path = "Image/Lottery/安徽快三.png"},
                    },
                    new LotteryType()
                    {
                        Description = "20分钟一期",
                        SpiderName = "10032",
                        Name = "湖北快三" ,
                        SortIndex = 1,
                        NumberLength =3,
                        DateFormat="yyyyMMdd",
                        LotteryPlayTypes=CreateKuai3(),
                        LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime(){BeginTime = new TimeSpan(9,20,0),EndTime = new TimeSpan(22,0,0),Interval = new TimeSpan(0,20,0)}),
                        Icon = new Affix(){Path = "Image/Lottery/河北快三.png"},
                    },
                    new LotteryType()
                    {
                        Description = "20分钟一期",
                        SpiderName = "10033",
                        Name = "北京快三" ,
                        SortIndex = 1,
                        NumberLength =6,
                        CalType = LotteryCalNumberTypeEnum.Increase,
                        LotteryPlayTypes=CreateKuai3(),
                        LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime(){BeginTime = new TimeSpan(9,20,0),EndTime = new TimeSpan(23,40,0),Interval = new TimeSpan(0,20,0)}),
                        Icon = new Affix(){Path = "Image/Lottery/北京快三.png"},
                    },
                },
            });
            _context.LotteryClassifies.Add(new LotteryClassify()
            {
                Type = LotteryClassifyType.SaiChe,
                LotteryTypes = new List<LotteryType>()
                {
                    new LotteryType() { Name = "雅福赛车",IsHot = true, NumberLength = 7, CalType = LotteryCalNumberTypeEnum.Automatic, RiskTime = new TimeSpan(0, 0, 1), Icon = new Affix() { Path = "Image/Lottery/雅福赛车.png" }, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(23, 59, 0), Interval = new TimeSpan(0, 1, 0) }), LotteryPlayTypes = CreateSaiChe(), Description = "1分钟1期", SortIndex = 2 },
                    new LotteryType() { Name = "雅福3分赛车",IsHot = true, NumberLength = 7, CalType = LotteryCalNumberTypeEnum.Automatic, RiskTime = new TimeSpan(0, 0, 1), Icon = new Affix() { Path = "Image/Lottery/雅福赛车.png" }, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(23, 59, 0), Interval = new TimeSpan(0, 3, 0) }), LotteryPlayTypes = CreateSaiChe(), Description = "3分钟1期", SortIndex = 2 },
                    new LotteryType() { Name = "雅福5分赛车",IsHot = true, NumberLength = 7, CalType = LotteryCalNumberTypeEnum.Automatic, RiskTime = new TimeSpan(0, 0, 1), Icon = new Affix() { Path = "Image/Lottery/雅福赛车.png" }, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(23, 59, 0), Interval = new TimeSpan(0, 5, 0) }), LotteryPlayTypes = CreateSaiChe(), Description = "5分钟1期", SortIndex = 2 },
                    new LotteryType() { Description = "每20分钟一期", SpiderName = "10001", Name = "北京赛车(pk10)", SortIndex = 2, CalType = LotteryCalNumberTypeEnum.Increase, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(9, 30, 0), EndTime =  new TimeSpan(23, 50, 0), Interval = new TimeSpan(0, 20, 0) }) ,Icon = new Affix(){Path = "Image/Lottery/北京PK10.png"}, LotteryPlayTypes = CreateSaiChe()},
                    new LotteryType()
                    {
                        Description = "5分钟一期",
                        SpiderName = "10057",
                        Name = "幸运飞艇" ,
                        SortIndex = 2,
                        NumberLength =3,
                        DateFormat="yyyyMMdd",
                        LotteryPlayTypes=CreateSaiChe(),
                        LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime(){BeginTime = new TimeSpan(13, 09, 0),EndTime = new TimeSpan(1, 4, 04, 0),Interval = new TimeSpan(0, 5, 0)}),
                        Icon = new Affix(){Path = "Image/Lottery/幸运飞艇.png"},
                    },
                },
            });
            _context.LotteryClassifies.Add(new LotteryClassify()
            {
                Type = LotteryClassifyType.Xuan5,
                LotteryTypes = new List<LotteryType>()
                {
                    new LotteryType() { Name = "雅福11选5",IsHot = true, NumberLength = 7, CalType = LotteryCalNumberTypeEnum.Automatic, RiskTime = new TimeSpan(0, 0, 1), Icon = new Affix() { Path = "Image/Lottery/雅福十一选五.png" }, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(23, 59, 0), Interval = new TimeSpan(0, 1, 0) }), LotteryPlayTypes = CreateXuan5(), Description = "1分钟1期", SortIndex = 3 },
                    new LotteryType() { Name = "雅福3分11选5",IsHot = true, NumberLength = 7, CalType = LotteryCalNumberTypeEnum.Automatic, RiskTime = new TimeSpan(0, 0, 1), Icon = new Affix() { Path = "Image/Lottery/雅福十一选五.png" }, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(23, 59, 0), Interval = new TimeSpan(0, 3, 0) }), LotteryPlayTypes = CreateXuan5(), Description = "3分钟1期", SortIndex = 3 },
                    new LotteryType() { Name = "雅福5分11选5",IsHot = true, NumberLength = 7, CalType = LotteryCalNumberTypeEnum.Automatic, RiskTime = new TimeSpan(0, 0, 1), Icon = new Affix() { Path = "Image/Lottery/雅福十一选五.png" }, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(23, 59, 0), Interval = new TimeSpan(0, 5, 0) }), LotteryPlayTypes = CreateXuan5(), Description = "5分钟1期", SortIndex = 3 },
                    new LotteryType() { Description = "20分钟一期", SpiderName = "10006", Name = "广东11选5", NumberLength = 2, SortIndex = 3, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(9, 30, 0), EndTime = new TimeSpan(23, 10, 0), Interval = new TimeSpan(0, 20, 0) }),Icon = new Affix(){Path = "Image/Lottery/广东十一选五.png"}, LotteryPlayTypes = CreateXuan5()},
                    new LotteryType() { Description = "20分钟一期",CalType=LotteryCalNumberTypeEnum.FromZeroEveryDay,DateFormat="yyyyMMdd" ,SpiderName = "10015", Name = "江西11选5", NumberLength = 2, SortIndex = 3, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(9, 30, 0), EndTime = new TimeSpan(23, 10, 0), Interval = new TimeSpan(0, 20, 0) }),Icon = new Affix(){Path = "Image/Lottery/江西十一选五.png"}, LotteryPlayTypes = CreateXuan5()},
                    new LotteryType() { Description = "20分钟一期",CalType=LotteryCalNumberTypeEnum.FromZeroEveryDay,DateFormat="yyMMdd" ,SpiderName = "10016", Name = "江苏11选5", NumberLength = 2, SortIndex = 3, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(8, 46, 0), EndTime = new TimeSpan(22, 6, 0), Interval = new TimeSpan(0, 20, 0) }),Icon = new Affix(){Path = "Image/Lottery/江苏十一选五.png"}, LotteryPlayTypes = CreateXuan5()},
                    new LotteryType() { Description = "20分钟一期",CalType=LotteryCalNumberTypeEnum.FromZeroEveryDay,DateFormat="yyMMdd" ,SpiderName = "10017", Name = "安徽11选5", NumberLength = 2, SortIndex = 3, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(9, 01, 0), EndTime = new TimeSpan(22, 1, 0), Interval = new TimeSpan(0, 20, 0) }),Icon = new Affix(){Path = "Image/Lottery/安徽十一选五.png"}, LotteryPlayTypes = CreateXuan5()},
                    new LotteryType() { Description = "20分钟一期",CalType=LotteryCalNumberTypeEnum.FromZeroEveryDay,DateFormat="yyyyMMdd" ,SpiderName = "10018", Name = "上海11选5", NumberLength = 2, SortIndex = 3, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(9, 21, 0), EndTime = new TimeSpan(1,0, 1, 0), Interval = new TimeSpan(0, 20, 0) }),Icon = new Affix(){Path = "Image/Lottery/上海十一选五.png"}, LotteryPlayTypes = CreateXuan5()},

                    new LotteryType() { Description = "20分钟一期",CalType=LotteryCalNumberTypeEnum.FromZeroEveryDay,DateFormat="yyMMdd" ,SpiderName = "10008", Name = "十一运夺金", NumberLength = 2, SortIndex = 3, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(9, 1, 40), EndTime = new TimeSpan(23, 01, 40), Interval = new TimeSpan(0, 20, 0) }),Icon = new Affix(){Path = "Image/Lottery/十一运夺金.png"}, LotteryPlayTypes = CreateXuan5()},

                    new LotteryType() { Description = "20分钟一期",CalType=LotteryCalNumberTypeEnum.FromZeroEveryDay,DateFormat="yyMMdd" ,SpiderName = "10019", Name = "辽宁11选5", NumberLength = 2, SortIndex = 3, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(9, 09, 0), EndTime = new TimeSpan(22, 29, 0), Interval = new TimeSpan(0, 20, 0) }),Icon = new Affix(){Path = "Image/Lottery/辽宁十一选五.png"}, LotteryPlayTypes = CreateXuan5()},
                },
            });
            _context.LotteryClassifies.Add(new LotteryClassify()
            {
                Type = LotteryClassifyType.PaiLie3D,
                LotteryTypes = new List<LotteryType>()
                {
                    new LotteryType() { Name = "雅福3D",IsHot = true, NumberLength = 7, CalType = LotteryCalNumberTypeEnum.Automatic, RiskTime = new TimeSpan(0, 0, 1), Icon = new Affix() { Path = "Image/Lottery/福彩3D.png" }, LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(0, 0, 0), EndTime = new TimeSpan(23, 59, 0), Interval = new TimeSpan(0, 1, 0) }), LotteryPlayTypes = Create3D(), Description = "1分钟1期", SortIndex = 4 },
                    new LotteryType()
                    {
                        Name = "福彩3D", SortIndex = 4,LotteryPlayTypes = Create3D(),SpiderName = "10041", Description = "1天1期"  ,NumberLength = 7,CalType = LotteryCalNumberTypeEnum.Increase,LotteryOpenTimes = new List<LotteryOpenTime>(){new LotteryOpenTime(){OpenNumber=1,OpenTime=new TimeSpan(21,15,0)}},RiskTime = new TimeSpan(0,0,0),Icon = new Affix(){Path = "Image/Lottery/福彩3D.png"}
                    },
                    new LotteryType()
                    {
                        Name = "PC蛋蛋",
                        SortIndex = 6,
                        LotteryPlayTypes = Create3D(),
                        SpiderName = "10046",
                        CalType = LotteryCalNumberTypeEnum.Increase,
                        RiskTime = new TimeSpan(0,0,15),
                        Icon = new Affix() { Path = "Image/Lottery/PC蛋蛋.png" },
                        LotteryOpenTimes = GetLotteryOpenTimes(new IntiLottertTime() { BeginTime = new TimeSpan(9, 05, 0), EndTime = new TimeSpan(23, 55, 0), Interval = new TimeSpan(0, 5, 0) }),
                        NumberLength = 6,
                        Description = "5分钟一期"
                    }
                }
            });
            _context.LotteryClassifies.Add(new LotteryClassify()
            {
                Type = LotteryClassifyType.LiuHeCai,
                LotteryTypes = new List<LotteryType>()
                {
                    new LotteryType()
                    {
                        Name = "香港六合彩",
                        CalType = LotteryCalNumberTypeEnum.AccordingToOpenData,
                        Icon = new Affix() {Path = "Image/Lottery/香港六合彩.png"},
                        LotteryPlayTypes = CreateLiuHeCai(),
                        Description = "一周3期",
                        SortIndex = 1,
                        SpiderName = "10048"
                    },
                    new LotteryType()
                    {
                        Name = "雅福六合彩",
                        NumberLength = 7,
                        CalType = LotteryCalNumberTypeEnum.Automatic,
                        RiskTime = new TimeSpan(0, 0, 1),
                        Icon = new Affix() {Path = "Image/Lottery/香港六合彩.png"},
                        LotteryOpenTimes =
                            GetLotteryOpenTimes(new IntiLottertTime()
                            {
                                BeginTime = new TimeSpan(0, 0, 0),
                                EndTime = new TimeSpan(23, 59, 0),
                                Interval = new TimeSpan(0, 1, 0)
                            }),
                        LotteryPlayTypes = CreateLiuHeCai(),
                        Description = "1分钟1期",
                        SortIndex = 5,
                        IsHot = true
                    },
                    new LotteryType()
                    {
                        Name = "雅福5分六合彩",
                        NumberLength = 7,
                        CalType = LotteryCalNumberTypeEnum.Automatic,
                        RiskTime = new TimeSpan(0, 0, 1),
                        Icon = new Affix() {Path = "Image/Lottery/香港六合彩.png"},
                        LotteryOpenTimes =
                            GetLotteryOpenTimes(new IntiLottertTime()
                            {
                                BeginTime = new TimeSpan(0, 0, 0),
                                EndTime = new TimeSpan(23, 59, 0),
                                Interval = new TimeSpan(0, 5, 0)
                            }),
                        LotteryPlayTypes = CreateLiuHeCai(),
                        Description = "5分钟1期",
                        SortIndex = 6,
                        IsHot = true
                    }
                }
            });
        }

        public static List<LotteryOpenTime> GetLotteryOpenTimes(params IntiLottertTime[] times)
        {
            return GetLotteryOpenTimes(false, times);
        }

        public static List<LotteryOpenTime> GetLotteryOpenTimes(bool isZero, params IntiLottertTime[] times)
        {
            if (times == null)
            {
                return null;
            }
            List<LotteryOpenTime> list = new List<LotteryOpenTime>();
            foreach (IntiLottertTime time in times)
            {
                for (TimeSpan i = time.BeginTime; i <= time.EndTime; i = i + time.Interval)
                {
                    LotteryOpenTime openTime = new LotteryOpenTime();
                    openTime.OpenNumber = list.Count;
                    if (!isZero)
                    {
                        openTime.OpenNumber += 1;
                    }
                    if (i >= new TimeSpan(1, 0, 0, 0))
                    {
                        openTime.OpenTime = i - new TimeSpan(1, 0, 0, 0);
                        openTime.IsTomorrow = true;
                    }
                    else
                    {
                        openTime.OpenTime = i;
                    }
                    list.Add(openTime);
                }
            }

            return list;
        }

    }

    public class IntiLottertTime
    {
        public TimeSpan BeginTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan Interval { get; set; } = new TimeSpan(0, 10, 0);
    }
}