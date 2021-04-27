using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using Zzb.Common;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class Configuration
    {
        private static LotteryContext _context;

        public static void Seed(LotteryContext context)
        {
            if (!context.Players.Any())
            {
                _context = context;
                AddSystemBank();
                AddMerchantsBanks();
                AddConfig();
             
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
 

        public static void AddPlayers()
        {
            var ceshi = new Player("ceshi", SecurityHelper.Encrypt("1"), SecurityHelper.Encrypt("1"))
            { Coin = 111111111.1111111111M, Players = new List<Player>(),   Rebate = 0.1M, DailyWageRate = 0.02M, DividendRate = 0.3M, IsChangePassword = true };

            for (int i = 0; i < 100; i++)
            {
                ceshi.Players.Add(new Player("ceshi" + i, SecurityHelper.Encrypt("1"), SecurityHelper.Encrypt("1")) { Coin = 111111111.1111111111M, IsChangePassword = true });
            }

            _context.Players.Add(ceshi);
        }

 
 
 

    }

    public class IntiLottertTime
    {
        public TimeSpan BeginTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public TimeSpan Interval { get; set; } = new TimeSpan(0, 10, 0);
    }
}