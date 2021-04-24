using AllLottery.Business.Report;
using AllLottery.Business.Socket;
using AllLottery.IBusiness;
using AllLottery.Model;
using AllLottery.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.Common;
using Zzb.Mvc;

//作废啦
namespace AllLottery.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[ZzbAuthorize]
    //public class PlayerController : BaseApiController
    //{
    //    public IPlayerService PlayerService { get; set; }

    //    public IReportService ReportService { get; set; }

    //    [HttpPost("Login")]
    //    [AllowAnonymous]
    //    public ServiceResult Login(PlayerLogin model)
    //    {
    //        return Json(() =>
    //        {
    //            var player = PlayerService.Login(model.Name, model.Password, model.Code, model.IsApp);
    //            SignIn(player.PlayerId.ToString());
    //            return new ServiceResult<int>(ServiceResultCode.Success, "OK", player.PlayerId);
    //        }, "登陆失败");
    //    }

    //    [HttpGet("GetCookies")]
    //    public ServiceResult GetCookies(string name = ".AspNetCore.Zzb")
    //    {
    //        return Json(() => ObjectResult(Request.Cookies[name]), "获取Cookies失败");
    //    }

    //    [HttpPost("GetPlayerInfo")]
    //    public ServiceResult GetPlayerInfo()
    //    {
    //        return Json(() =>
    //        {
    //            var player = PlayerService.GetPlayer(UserId);
    //            return ObjectResult(new
    //            {
    //                player.PlayerId,
    //                player.Name,
    //                Coin = player.Coin.ToDecimalString(),
    //                player.Rebate,
    //                player.Title,
    //                player.IsChangePassword,
    //                Birthday = player.Birthday?.ToDateString(),
    //                player.QQ,
    //                player.WeChat,
    //                player.Phone,
    //                IsSettingCoinPassword = !string.IsNullOrEmpty(player.CoinPassword),
    //                IsSettingBaseInfo = !(string.IsNullOrEmpty(player.QQ) || string.IsNullOrEmpty(player.WeChat) || string.IsNullOrEmpty(player.Phone) || player.Birthday == null),
    //                player.DailyWageRate,
    //                player.DividendRate,
    //                Type = player.Type.ToDescriptionString()
    //            });
    //        });
    //    }

    //    [HttpPost("SetPlayerTitle")]
    //    public ServiceResult SetPlayerTitle(PlayerSetPlayerTitle model)
    //    {
    //        return Json(() =>
    //        {
    //            PlayerService.SetPlayerTitle(UserId, model.Title);
    //            return new ServiceResult(ServiceResultCode.Success, "修改成功");
    //        }, "设置昵称失败");
    //    }

    //    [HttpPost("ChangePassword")]
    //    public ServiceResult ChangePassword(PlayerChangePassword model)
    //    {
    //        return Json(() =>
    //        {
    //            PlayerService.ChangePassword(UserId, model.OldPassword, model.NewPassword);
    //            return new ServiceResult(ServiceResultCode.Success);
    //        }, "修改密码失败：");
    //    }


    //    [HttpPost("ChangeCoinPassword")]
    //    public ServiceResult ChangeCoinPassword(PlayerChangePassword model)
    //    {
    //        return Json(() =>
    //        {
    //            PlayerService.ChangeCoinPassword(UserId, model.OldPassword, model.NewPassword);
    //            return new ServiceResult(ServiceResultCode.Success);
    //        }, "修改资金密码失败：");
    //    }

    //    [HttpPost("SignOut")]
    //    public new ServiceResult SignOut()
    //    {
    //        return Json(() =>
    //        {
    //            base.SignOut();
    //            return new ServiceResult(ServiceResultCode.Success);
    //        }, "注销失败");
    //    }

    //    [HttpPost("GetNewLoginLogs")]
    //    public ServiceResult GetNewLoginLogs(PlayerGetNewLoginLogs model)
    //    {
    //        return JsonObjectResult(from o in PlayerService.GetNewLoginLogs(UserId, model.Size) select new { o.Address, o.IP, CreateTime = o.CreateTime.ToCommonString() });
    //    }

    //    [HttpPost("SetInformation")]
    //    public ServiceResult SetInformation(PlayerSetInformation model)
    //    {
    //        return Json(() =>
    //        {
    //            PlayerService.SetInformation(UserId, model.Phone, model.QQ, model.WeChat, model.Birthday);
    //            return new ServiceResult(ServiceResultCode.Success);
    //        }, "设置资料失败");
    //    }

    //    [HttpPost("GetPlayerType")]
    //    public ServiceResult GetPlayerType()
    //    {
    //        return new ServiceResult<object>(ServiceResultCode.Success, "OK", new List<DropListModel>() { new DropListModel(PlayerTypeEnum.Member.ToString(), PlayerTypeEnum.Member.ToDescriptionString()), new DropListModel(PlayerTypeEnum.Proxy.ToString(), PlayerTypeEnum.Proxy.ToDescriptionString()) });
    //    }

    //    [HttpPost("CreateRegisterLink")]
    //    public ServiceResult CreateRegisterLink(PlayerCreateRegisterLink model)
    //    {
    //        return Json(() =>
    //        {
    //            PlayerService.CreateRegisterLink(UserId, model.Type, model.Rebate, model.Remark);
    //            return new ServiceResult(ServiceResultCode.Success);
    //        }, "创建推广链接失败");
    //    }
    //    [HttpPost("GetRegisterLinks")]
    //    public ServiceResult GetRegisterLinks(PageViewModel model)
    //    {
    //        return Json(() =>
    //        {
    //            var links = PlayerService.GetRegisterLink(UserId, model.Index, model.Size, out var total);
    //            return ObjectResult(new
    //            {
    //                total,
    //                data = from l in links
    //                       select new
    //                       {
    //                           l.ProxyRegisterId,
    //                           l.Number,
    //                           Rebate = (l.Rebate * 100).ToString("0.#") + "%",
    //                           Type = l.Type.ToDescriptionString(),
    //                           l.Remark,
    //                           l.UseCount
    //                       }
    //            });
    //        }, "获取推广链接失败");
    //    }

    //    [HttpPost("CreatePlayerByLink")]
    //    [AllowAnonymous]
    //    public ServiceResult CreatePlayerByLink(PlayerCreatePlayerByLink model)
    //    {
    //        return Json(() =>
    //        {
    //            var player = PlayerService.RegisterPlayer(model.Number, model.Name, model.Password, model.QQ);
    //            SignOut();
    //            SignIn(player.PlayerId.ToString());
    //            return new ServiceResult(ServiceResultCode.Success);
    //        }, "注册失败");
    //    }

    //    [HttpPost("GetUnderPlayers")]
    //    public ServiceResult GetUnderPlayers(PlayerGetUnderPlayers model)
    //    {
    //        return Json(() =>
    //        {
    //            var exist = PlayerService.GetPlayer(model.PlayerId);
    //            if (exist == null)
    //            {
    //                return new ServiceResult(ServiceResultCode.Error, "不存在下级");
    //            }
    //            while (exist.PlayerId != UserId)
    //            {
    //                if (exist.ParentPlayerId == null)
    //                {
    //                    return new ServiceResult(ServiceResultCode.Error, "查询玩家不是当前登录玩家的下级");
    //                }
    //                exist = exist.ParentPlayer;
    //            }

    //            var players = PlayerService.GetUnderPlayers(model.PlayerId, model.Name, model.Type, model.Index, model.Size,
    //                out var total, model.Sort, model.WeChat, model.QQ, model.Phone, model.IsOnline);
    //            return ObjectResult(new
    //            {
    //                total,
    //                data = from p in players
    //                       select new
    //                       {
    //                           p.PlayerId,
    //                           p.Name,
    //                           Type = p.Type.ToDescriptionString(),
    //                           p.Rebate,
    //                           p.DailyWageRate,
    //                           p.DividendRate,
    //                           p.Coin,
    //                           TeamCoin = ReportService.GetTeamCoin(p.PlayerId),
    //                           TeamCount = BaseReport.GetTeamPlayerIdsWhitoutSelf(p.PlayerId).Count + 1,
    //                           p.Players.Count,
    //                           LoginTime = PlayerService.GetNewLoginLogs(p.PlayerId, 1).FirstOrDefault()?.CreateTime
    //                               .ToCommonString(),
    //                           IsOnline = new PlayerSocketMiddleware(null).IsConnect(p.PlayerId) ? "是" : "否",
    //                           p.WeChat,
    //                           p.QQ,
    //                           p.Phone,
    //                           CreateTime = p.CreateTime.ToCommonString()
    //                       }
    //            });
    //        }, "获取下级玩家列表失败");
    //    }

    //    [HttpPost("Transfer")]
    //    public ServiceResult Transfer(PlayerTransfer model)
    //    {
    //        return Json(() =>
    //        {
    //            PlayerService.Transfer(UserId, model.PlayerId, model.Money, model.Password, model.Remark);
    //            return new ServiceResult(ServiceResultCode.Success);
    //        }, "转账失败");
    //    }

    //    [HttpPost("SettingRebate")]
    //    public ServiceResult SettingRebate(PlayerSettingRate model)
    //    {
    //        return Json(() =>
    //        {
    //            PlayerService.SettingRebate(UserId, model.PlayerId, model.Rate);
    //            return new ServiceResult(ServiceResultCode.Success);
    //        }, "设置返点失败");
    //    }

    //    [HttpPost("SettingDailyWageRate")]
    //    public ServiceResult SettingDailyWageRate(PlayerSettingRate model)
    //    {
    //        return Json(() =>
    //        {
    //            PlayerService.SettingDailyWageRate(UserId, model.PlayerId, model.Rate);
    //            return new ServiceResult(ServiceResultCode.Success);
    //        }, "设置标准日工资失败");
    //    }

    //    [HttpPost("SettingDividendRate")]
    //    public ServiceResult SettingDividendRate(PlayerSettingRate model)
    //    {
    //        return Json(() =>
    //        {
    //            PlayerService.SettingDividendRate(UserId, model.PlayerId, model.Rate);
    //            return new ServiceResult(ServiceResultCode.Success);
    //        }, "设置分红比例失败");
    //    }

    //    [HttpPost("DeleteLink")]
    //    public ServiceResult DeleteLink(PlayerDeleteLink model)
    //    {
    //        return Json(() =>
    //        {
    //            PlayerService.DeleteLink(UserId, model.Id);
    //            return new ServiceResult(ServiceResultCode.Success);
    //        }, "删除链接失败");
    //    }

    //    [HttpPost("GetDividendLogs")]
    //    public ServiceResult GetDividendLogs(PlayerGetDividendLogs model)
    //    {
    //        return Json(() =>
    //        {
    //            DividendLog[] dividends = null;
    //            int total = 0;
    //            if (model.PlayerId == null)
    //            {
    //                dividends = PlayerService.GetDividendLogs(UserId, model.Begin, model.End, model.Index,
    //                    model.Size, out total);
    //            }
    //            else
    //            {
    //                var exist = PlayerService.GetPlayer(model.PlayerId.Value);
    //                if (exist == null)
    //                {
    //                    return new ServiceResult(ServiceResultCode.Error, "不存在下级");
    //                }
    //                while (exist.PlayerId != UserId)
    //                {
    //                    if (exist.ParentPlayerId == null)
    //                    {
    //                        return new ServiceResult(ServiceResultCode.Error, "查询玩家不是当前登录玩家的下级");
    //                    }
    //                    exist = exist.ParentPlayer;
    //                }

    //                dividends = PlayerService.GetTeamDividendLogs(model.PlayerId.Value, model.Name, model.Begin, model.End, model.Index,
    //                    model.Size, out total);
    //            }

    //            return ObjectResult(new
    //            {
    //                total,
    //                data = from d in dividends
    //                       select new
    //                       {
    //                           d.PlayerId,
    //                           d.Player.Name,
    //                           Begin = d.CalBeginDate.ToDateString(),
    //                           End = d.DividendDate.SettleTime.ToDateString(),
    //                           d.BetMoney,
    //                           d.LostMoney,
    //                           Rate = (d.Rate * 100).ToString("0.#") + "%",
    //                           d.DividendMoney,
    //                           d.BetMen
    //                       }
    //            });

    //        }, "获取分红记录失败");
    //    }

    //    [HttpPost("GetDailyWageLogs")]
    //    public ServiceResult GetDailyWageLogs(PlayerGetDividendLogs model)
    //    {
    //        return Json(() =>
    //        {
    //            DailyWageLog[] dividends = null;
    //            int total = 0;
    //            if (model.PlayerId == null)
    //            {
    //                dividends = PlayerService.GetDailyWageLogs(UserId, model.Begin, model.End, model.Index,
    //                    model.Size, out total);
    //            }
    //            else
    //            {
    //                var exist = PlayerService.GetPlayer(model.PlayerId.Value);
    //                if (exist == null)
    //                {
    //                    return new ServiceResult(ServiceResultCode.Error, "不存在下级");
    //                }
    //                while (exist.PlayerId != UserId)
    //                {
    //                    if (exist.ParentPlayerId == null)
    //                    {
    //                        return new ServiceResult(ServiceResultCode.Error, "查询玩家不是当前登录玩家的下级");
    //                    }
    //                    exist = exist.ParentPlayer;
    //                }

    //                dividends = PlayerService.GetTeamDailyWageLogs(model.PlayerId.Value, model.Name, model.Begin, model.End, model.Index,
    //                    model.Size, out total);
    //            }

    //            return ObjectResult(new
    //            {
    //                total,
    //                data = from d in dividends
    //                       select new
    //                       {
    //                           d.PlayerId,
    //                           d.Player.Name,
    //                           CalDate = d.CalDate.ToDateString(),
    //                           d.BetMoney,
    //                           Rate = (d.Rate * 100).ToString("0.#") + "%",
    //                           d.DailyMoney,
    //                           d.BetMen
    //                       }
    //            });

    //        }, "获取日工资记录失败");
    //    }

    //    [HttpPost("CreatePlayer")]
    //    public ServiceResult CreatePlayer(PlayerCreatePlayer model)
    //    {
    //        return Json(() =>
    //        {
    //            PlayerService.CreatePlayer(UserId, model.Name, model.Password, model.Type, model.Rebate);
    //            return new ServiceResult(ServiceResultCode.Success);
    //        }, "开户失败");
    //    }

    //    [HttpPost("CreateTestPlayer")]
    //    [AllowAnonymous]
    //    public ServiceResult CreateTestPlayer(PlayerLogin model)
    //    {
    //        return Json(() =>
    //        {
    //            var player = PlayerService.CreateTestPlayer(model.Code);
    //            SignIn(player.PlayerId.ToString());
    //            return new ServiceResult<int>(ServiceResultCode.Success, "OK", player.PlayerId);
    //        }, "创建试玩玩家失败");
    //    }

    //    [HttpPost("GetSoftwareExpired")]
    //    [AllowAnonymous]
    //    public ServiceResult GetSoftwareExpired(PlayerDeleteLink model)
    //    {
    //        return Json(() =>
    //        {
    //            var player = PlayerService.GetPlayer(model.Id);
    //            if (player == null)
    //            {
    //                return ObjectResult(DateTime.MinValue);
    //            }

    //            if (player.Type == PlayerTypeEnum.TestPlay)
    //            {
    //                return ObjectResult(DateTime.Now.AddDays(30));
    //            }

    //            var soft = PlayerService.GetSoftwareExpired(model.Id);
    //            if (soft == null)
    //            {
    //                return ObjectResult(DateTime.MinValue);
    //            }

    //            return ObjectResult(soft.Expired);
    //        }, "获取软件期限失败");
    //    }
    //}

    //public class PlayerCreatePlayer
    //{
    //    public string Name { get; set; }

    //    public string Password { get; set; }

    //    public decimal Rebate { get; set; }

    //    public PlayerTypeEnum Type { get; set; }
    //}

    //public class PlayerGetDividendLogs : PageViewModel
    //{
    //    public DateTime? Begin { get; set; }

    //    public DateTime? End { get; set; }

    //    public string Name { get; set; }

    //    public int? PlayerId { get; set; }
    //}

    //public class PlayerDeleteLink
    //{
    //    public int Id { get; set; }
    //}

    //public class PlayerSettingRate
    //{
    //    public decimal Rate { get; set; }

    //    public int PlayerId { get; set; }
    //}

    //public class PlayerTransfer
    //{
    //    public int PlayerId { get; set; }

    //    public decimal Money { get; set; }

    //    public string Password { get; set; }

    //    public string Remark { get; set; }
    //}

    //public class PlayerCreatePlayerByLink
    //{
    //    public string Number { get; set; }

    //    public string Name { get; set; }

    //    public string Password { get; set; }

    //    public string QQ { get; set; }
    //}

    //public class PlayerCreateRegisterLink
    //{
    //    public PlayerTypeEnum Type { get; set; }

    //    public decimal Rebate { get; set; }

    //    public string Remark { get; set; }
    //}

    //public class PlayerLogin
    //{
    //    public string Name { get; set; }

    //    public string Password { get; set; }

    //    public string Code { get; set; }

    //    public bool IsApp { get; set; }
    //}

    //public class PlayerChangePassword
    //{
    //    public string OldPassword { get; set; }

    //    public string NewPassword { get; set; }
    //}

    //public class PlayerSetPlayerTitle
    //{
    //    public string Title { get; set; }
    //}

    //public class PlayerGetNewLoginLogs
    //{
    //    public int Size { get; set; }
    //}

    //public class PlayerSetInformation
    //{
    //    /// <summary>
    //    /// 手机
    //    /// </summary>
    //    public string Phone { get; set; }

    //    /// <summary>
    //    /// QQ
    //    /// </summary>
    //    public string QQ { get; set; }

    //    /// <summary>
    //    /// 微信
    //    /// </summary>
    //    public string WeChat { get; set; }

    //    /// <summary>
    //    /// 生日
    //    /// </summary>
    //    public DateTime? Birthday { get; set; }
    //}
}