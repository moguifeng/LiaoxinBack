using AllLottery.Business.Config;
using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Zzb;
using Zzb.Common;
using Zzb.ZzbLog;

namespace AllLottery.Business
{
    public class BiService : BaseService, IBiService
    {
        private static object _lock = new object();

        private static string Url => "https://apis.bisoft.me/";

        private string GetPassword(string value)
        {
            return "86809223";
        }

        private string WebSiteCode
        {
            get
            {
                var siteNo = BaseConfig.CreateInstance(SystemConfigEnum.SiteNo).Value;
                if (string.IsNullOrEmpty(siteNo))
                {
                    throw new ZzbException("后台未设置BI参数");
                }
                return siteNo;
            }
        }

        public IPlayerService PlayerService { get; set; }

        private string DesKey
        {
            get
            {
                var siteKey = BaseConfig.CreateInstance(SystemConfigEnum.SiteKey).Value;
                if (string.IsNullOrEmpty(siteKey))
                {
                    throw new ZzbException("后台未设置BI参数");
                }
                return siteKey;
            }
        }

        public string CreatePlatformUrl(int playerId, int platformId)
        {
            var platform = GetPlatform(platformId);

            var player = CheckPlayer(playerId);

            var url = Url + "Api/Login";

            return $"{url}?parameter={HttpUtility.UrlEncode(Encode($"platformCode={platform.Value}&userName={player.Name}&userPassWord={GetPassword(platform.Value)}", DesKey))}&WebSiteCode={WebSiteCode}";
        }

        private Platform GetPlatform(int platformId)
        {
            var playform = (from p in Context.Platforms where p.IsEnable && p.PlatformId == platformId select p)
                .FirstOrDefault();
            if (playform == null)
            {
                throw new ZzbException("未找到该平台");
            }
            return playform;
        }

        public Platform[] GetAllPlatforms()
        {
            if (string.IsNullOrEmpty(BaseConfig.CreateInstance(SystemConfigEnum.SiteKey).Value) || string.IsNullOrEmpty(BaseConfig.CreateInstance(SystemConfigEnum.SiteNo).Value))
            {
                return new Platform[0];
            }
            return (from p in Context.Platforms where p.IsEnable orderby p.SortIndex select p).ToArray();
        }

        public void Transfer(int playerId, int platformId, decimal money)
        {
            lock (_lock)
            {
                var platform = GetPlatform(platformId);
                var player = CheckPlayer(playerId);
                var url = Url + "Api/Transfer";
                bool isIn = money >= 0;
                if (isIn && player.Coin < money)
                {
                    throw new ZzbException("当前用户余额不足");
                }
                player.TransferPlatform(platform, money, out var log);
                Context.CoinLogs.Add(log);
                SignIn(platform, player);
                Context.SaveChanges();

                try
                {
                    var retData = HttpWebRequestAsync($"{url}?parameter={HttpUtility.UrlEncode(Encode($"platformCode={platform.Value}&userName={player.Name}&userPassWord={GetPassword(platform.Value)}&transferType={(isIn ? "IN" : "OUT")}&credit={Math.Abs(money)}", DesKey))}&WebSiteCode={WebSiteCode}");
                    var json = JsonHelper.Json<PlatformMessage>(retData);
                    if (json.RetCode != "0")
                    {
                        throw new ZzbException(json.RetMsg);
                    }
                }
                catch (Exception)
                {
                    RunAgain(() =>
                    {
                        using (var context = LotteryContext.CreateContext())
                        {
                            var np = (from p in context.Players where p.PlayerId == playerId select p).FirstOrDefault();
                            np.TransferPlatformFail(platform, money, out var logF);
                            context.CoinLogs.Add(logF);
                            context.SaveChanges();
                        }
                    });
                    throw;
                }


                RunAgain(() =>
                {
                    using (var context = LotteryContext.CreateContext())
                    {
                        var np = (from p in context.Players where p.PlayerId == playerId select p).FirstOrDefault();
                        np.TransferPlatformSuccess(platform, money, out var logS);
                        context.CoinLogs.Add(logS);
                        context.PlatformMoneyLogs.Add(new PlatformMoneyLog(playerId, platform.PlatformId, money));
                        context.SaveChanges();
                    }
                });
            }
        }

        private void RunAgain(Action action)
        {
            int i = 0;
            while (true)
            {
                try
                {
                    action();
                    return;
                }
                catch (Exception e)
                {
                    i++;
                    if (i >= 10)
                    {
                        throw new ZzbException("转账发生多次错误", e);
                    }
                    LogHelper.Error("BI转账失败", e);
                }
            }
        }

        public decimal CheckMoney(int playerId, int platformId)
        {
            var platform = GetPlatform(platformId);
            var player = CheckPlayer(playerId);
            SignIn(platform, player);
            var url = Url + "Api/GetUserBalance";
            var retData = HttpWebRequestAsync($"{url}?parameter={HttpUtility.UrlEncode(Encode($"platformCode={platform.Value}&userName={player.Name}&TimeStamp={DateTime.Now.Ticks}", DesKey))}&WebSiteCode={WebSiteCode}");
            var json = JsonHelper.Json<PlatformMessage>(retData);
            if (json.RetCode != "0")
            {
                throw new Exception(json.RetMsg);
            }
            return Decimal.Parse(json.RetMsg);
        }

        private void SignIn(Platform platform, Player player)
        {
            int playerId = player.PlayerId;
            if (!(from p in Context.PlatformMoneyLogs where p.PlayerId == playerId select p).Any())
            {
                var url1 = Url + "Api/Register";
                using (WebClient client = new WebClient())
                {
                    Encoding.UTF8.GetString(client.DownloadData($"{url1}?parameter={HttpUtility.UrlEncode(Encode($"platformCode={platform.Value}&userName={player.Name}&userPassWord={GetPassword(platform.Value)}", DesKey))}&WebSiteCode={WebSiteCode}"));
                }
            }
        }

        private string HttpWebRequestAsync(string url)
        {
            using (WebClient client = new WebClient())
            {
                return Encoding.UTF8.GetString(client.DownloadData(url));
            }
        }

        private Player CheckPlayer(int playerId)
        {
            var player = (from p in Context.Players where p.PlayerId == playerId select p)
                .FirstOrDefault();
            if (player == null)
            {
                throw new ZzbException("未找到该用户");
            }

            if (player.Name.Length > 9)
            {
                throw new ZzbException("该用户名的长度超过10，无法使用平台");
            }
            return player;
        }

        private string Encode(string decryptString, string encryptKey = "ABCDEFJH")
        {
            StringBuilder sb = new StringBuilder();
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] key = ASCIIEncoding.ASCII.GetBytes(encryptKey);
                byte[] iv = ASCIIEncoding.ASCII.GetBytes(encryptKey);
                byte[] dataByteArray = Encoding.UTF8.GetBytes(decryptString);
                des.Mode = System.Security.Cryptography.CipherMode.CBC;
                des.Key = key;
                des.IV = iv;
                string encrypt = "";
                using (MemoryStream ms = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataByteArray, 0, dataByteArray.Length);
                    cs.FlushFinalBlock();
                    encrypt = Convert.ToBase64String(ms.ToArray());
                }
                return encrypt;
            }
        }
    }

    public class PlatformMessage
    {
        public string RetCode { get; set; }

        public string RetMsg { get; set; }
    }

}