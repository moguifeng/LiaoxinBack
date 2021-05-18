
using Liaoxin.Business;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using LIaoxin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Zzb;
using Zzb.Common;
using Zzb.Context;
using Zzb.ICacheManger;
using Zzb.Mvc;
using Zzb.Utility;
using static Liaoxin.ViewModel.ClientViewModel;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : LiaoxinBaseController
    {        
        public IClientService clientService { get; set; }

        public   ICacheManager _cacheManager { get; set; }


        private Client GetCurrentClient()
        {
            var entity = Context.Clients.Where(c => c.ClientId == CurrentClientId).FirstOrDefault();
            return entity;
        }

        /// <summary>
        /// 发送验证码  重要:发送类型(Type) 0:登录  1:找回密码  4:修改手机号码   5:注册用户
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("SendCode")]
        public ServiceResult SendCode(ClientSendCodeRequest request)
        {
            return Json(() =>
            {
                if (!StringHelper.IsMobile(request.Telephone))
                {
                    throw new ZzbException("请输入正确的手机号码");
                }
                var code = GenerateRandomCode();
                var cacheKey = string.Format($"sendCode:{request.Type}:{request.Telephone}");
                _cacheManager.Set(cacheKey, code, 2);

                var res =   HuanxinSendMsgRequest.SendMsg(new string[] { request.Telephone },code);
                if (res.ReturnCode == ServiceResultCode.Success)
                {

                    return ObjectResult(code);
                }
                throw new ZzbException("验证码过期,无法发送,请联系管理员");
            }, "验证码发送失败");
        }


        /// <summary>
        ///  用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("ResgierClient")]
        public ServiceResult ResgierClient(ResgerClientRequest request)
        {
            return Json(() =>
            {
                if (!StringHelper.IsMobile(request.Telephone))
                {
                    throw new ZzbException("请输入正确的手机号码");
                }

                if (request.Password.Length < 8)
                {
                    throw new ZzbException("请输入最小8位的密码");
                }


                if (string.IsNullOrEmpty(request.NickName)) 
                {
                    throw new ZzbException("昵称不可以为空");
                }


                var cacheKey = $"sendCode:{VerificationCodeTypes.RegisterClient}:{request.Telephone}";
                var cacheCode = _cacheManager.Get<string>(cacheKey);

                if (string.IsNullOrEmpty(cacheCode))
                    throw new ZzbException("验证码已过期");
                if (cacheCode != request.Code)
                    throw new ZzbException("验证码错误");

                var entity = clientService.RegisterClient(request);
                _UserContext.SetUserContext(entity.ClientId, entity.HuanXinId, entity.LiaoxinNumber);
                string token = UserContext.Current.Token;
                _cacheManager.Remove(cacheKey);
                return ObjectResult(token);


            }, "登录失败");

        }


        /// <summary>
        /// 获取当前登录用户
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetCurrenClient")]
        public ServiceResult<ClientBaseInfoResponse> GetClient()
        {
            var entity = clientService.GetClient();
            return (ServiceResult<ClientBaseInfoResponse>)Json(() =>
            {
                return ObjectGenericityResult(entity);
            }, "获取客户失败");

        }

        /// <summary>
        /// 客户手机号码登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
 
        [AllowAnonymous]
        [HttpPost("LoginByCode")]
        public ServiceResult LoginByCode(ClientLoginByCodeRequest request)
        {
            return Json(() =>
            {
                if (!StringHelper.IsMobile(request.Telephone))
                {
                    throw new ZzbException("请输入正确的手机号码");
                }

                var cacheKey = $"sendCode:{VerificationCodeTypes.Login}:{request.Telephone}";
                var cacheCode = _cacheManager.Get<string>(cacheKey);

                if (string.IsNullOrEmpty(cacheCode))
                    throw new ZzbException("验证码已过期");
                if (cacheCode != request.Code)
                    throw new ZzbException("验证码错误");

                var entity = (from p in Context.Clients where p.Telephone == request.Telephone select p).FirstOrDefault();
                if (entity == null)
                {
                    throw new ZzbException("账户未注册");
                }
                if (entity.IsFreeze)
                {
                    throw new ZzbException("账户已被禁用");
                }
                _UserContext.SetUserContext(entity.ClientId, entity.HuanXinId, entity.LiaoxinNumber);
                string token = UserContext.Current.Token;
                _cacheManager.Remove(cacheKey);
                return ObjectResult(token);
            }, "登录失败");

        }


        private static string GenerateRandomCode(int length = 4)
        {
            Random ra = new Random();
            string randString = "";

            for (int i = 0; i < length; i++)
                randString += ra.Next(9).ToString();

            return randString;
        }



        /// <summary>
        /// 客户账号登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public ServiceResult Login(ClientLoginRequest request)
        {
            return Json(() =>
            {
                var entity = clientService.Login(request);
                if (entity.IsFreeze)
                {
                    throw new ZzbException("账户已被禁用");
                }
                _UserContext.SetUserContext(entity.ClientId, entity.HuanXinId, entity.LiaoxinNumber);
                string token = UserContext.Current.Token;
                return ObjectResult(token);
            }, "登录失败");

        }


        /// <summary>
        /// 修改客户密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        public ServiceResult ChangePassword(ClientChangePasswordRequest request)
        {

            return Json(() =>
            {
                var res = clientService.ChangePassword(request);
                return ObjectResult(res);
            }, "修改密码失败");

        }


        /// <summary>
        /// 修改昵称
        /// </summary>        
        /// <returns></returns>
        [HttpPost("ModifyNickName")]
        public ServiceResult ModifyNickName(SetClientNickNameRequest request)
        {
            return Json(() =>
            {

                var entity = Context.Clients.Where(c => c.ClientId == CurrentClientId).First();
                entity.NickName = request.NickName;
                Context.Clients.Update(entity);
                var res = HuanxinClientRequest.ModifyNickName(CurrentHuanxinId,request.NickName);
                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    return ObjectResult(Context.SaveChanges() > 0);
                }
                else
                {
                    return res;
                }

            }, "修改昵称失败");
        }


        /// <summary>
        /// 修改头像
        /// </summary>        
        /// <returns></returns>
        [HttpPost("ModifyCover")]
        public ServiceResult ModifyCover(Guid coverId)
        {
            return Json(() =>
            {
                var entity = GetCurrentClient();
                entity.Cover = coverId;
                Context.Clients.Update(entity);     
                return ObjectResult(Context.SaveChanges() > 0);             
            }, "修改头像失败");

        }
        

        /// <summary>
        /// 基本设置修改:个性签名/震动/提醒/字体大小等字段普通更改
        /// </summary>        
        /// <returns></returns>
        [HttpPost("ModifyBaseInfo")]
        public ServiceResult ModifyBaseInfo(ClientBaseInfoResponse request)
        {
            return Json(() =>
            {
                if (request.ClientId != CurrentClientId)
                {
                    throw new ZzbException("你想干嘛?");
                }
                List<string> ingores = new List<string>();
                ingores.Add("Cover");
                ingores.Add("HuanXinId");
                ingores.Add("RealName");
                ingores.Add("UniqueNo");
                ingores.Add("UniqueFrontImg");
                ingores.Add("UniqueBackImg");
                ingores.Add("Password");
                ingores.Add("CoinPassword");
                ingores.Add("LiaoxinNumber");
                ingores.Add("NickName");
                ingores.Add("Coin");
                ingores.Add("Telephone");
                IList<string> updateFieldList = GetPostBodyFiledKey(ingores);
                Client entity = ConvertHelper.ConvertToModel<ClientBaseInfoResponse, Client>(request, updateFieldList);
                Context.ClientOperateLogs.Add(new ClientOperateLog(CurrentClientId, $"进行了基本设置修改"));
                return ObjectResult(base.Update(entity, "ClientId", updateFieldList) > 0);
            }, "修改失败");

        }
        
        /// <summary>
        /// 修改客户资金密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("ChangeCoinPassword")]
        public ServiceResult ChangeCoinPassword(ClientChangeCoinPasswordRequest request)
        {
            return Json(() =>
            {
                var res = clientService.ChangeCoinPassword(request);
                return ObjectResult(res);
            }, "修改密码失败");

        }

        /// <summary>
        /// 设置支付密码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("SetCoinPassword")]
        public ServiceResult SetCoinPassword(SetClientCoinPasswordRequest request)
        {
            return Json(() =>
            {
                if (request.CoinPsssword == null || request.CoinPsssword.Length != 6)
                {
                    throw new ZzbException("支付密码必须6位");
                }
                var client = (from p in Context.Clients where p.ClientId == CurrentClientId select p).FirstOrDefault();
                if (client == null)
                {
                    throw new ZzbException("找不到当前登录用户");
                }

              
                client.CoinPassword = SecurityHelper.Encrypt(request.CoinPsssword);
                client.Update();
                Context.ClientOperateLogs.Add(new ClientOperateLog(client.ClientId, "设置资金密码"));
                return ObjectResult(Context.SaveChanges() > 0);

            }, "设置资金密码");


        }


        /// <summary>
        /// 实名认证
        /// </summary>        
        /// <returns></returns>
        [HttpPost("RealNameAuth")]
        public ServiceResult RealNameAuth(ClientRealNameRequest request)
        {
            return Json(() =>
            {

                if (string.IsNullOrEmpty(request.RealName))
                {
                    throw new ZzbException("请输入真实姓名");
                }

                if (string.IsNullOrEmpty(request.UniqueNo))
                {
                    throw new ZzbException("请输入身份证号码");
                }
                if ((!Regex.IsMatch(request.UniqueNo, @"^(\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$", RegexOptions.IgnoreCase)))
                {
                    throw new ZzbException("请输入正确身份证号码");
                }

                if (request.FrontCover == Guid.Empty)
                {
                    throw new ZzbException("请上传身份证正面");
                }
                if (request.BackCover == Guid.Empty)
                {
                    throw new ZzbException("请上传身份证背面");
                }

                var entity = this.GetCurrentClient();
                if (!string.IsNullOrEmpty(entity.RealName))
                {
                    throw new ZzbException("已绑定身份认证,不能更改了");
                }
                
                entity.UniqueNo = request.UniqueNo;
                entity.RealName = request.RealName;
                entity.UniqueBackImg = request.BackCover;
                entity.UniqueFrontImg = request.FrontCover;
                Context.Clients.Update(entity);
                return ObjectResult(Context.SaveChanges() > 0);
            }, "修改实名认证失败");

        }


        /// <summary>
        /// 通过手机号找回密码
        /// </summary>        
        /// <returns></returns>
        [HttpPost("FindPasswordByPhoneRequest")]
        [AllowAnonymous]

        public ServiceResult FindPasswordByPhone(FindPasswordByPhoneRequest request)
        {
            return Json(() =>
            {
                if (!StringHelper.IsMobile(request.Telephone))
                {
                    throw new ZzbException("请输入正确的手机号码");
                }

                if (request.NewPassword.Length<8)
                {
                    throw new ZzbException("请输入至少8位的密码");
                }

                var cacheKey = $"sendCode:{VerificationCodeTypes.ForgetPassword}:{request.Telephone}";
                var cacheCode = _cacheManager.Get<string>(cacheKey);

                if (string.IsNullOrEmpty(cacheCode))
                    throw new ZzbException("验证码已过期");
                if (cacheCode != request.Code)
                    throw new ZzbException("验证码错误");

               var entity =   Context.Clients.Where(c => c.Telephone == request.Telephone).FirstOrDefault();
                if (entity == null)
                {
                    throw new ZzbException("不存在用户");
                }
                //if (entity.ClientId != CurrentClientId)
                //{
                //    throw new ZzbException("账户手机不匹配");
                //}

                entity.Password = SecurityHelper.Encrypt(request.NewPassword);
                Context.Clients.Update(entity);
                return ObjectResult(Context.SaveChanges() > 0);
            }, "找回密码失败");

        }


        /// <summary>
        /// 修改手机号码
        /// </summary>        
        /// <returns></returns>
        [HttpPost("ModifyClientTelephone")]
        public ServiceResult ModifyClientTelephone(ModifyClientTelephoneRequest request)
        {
            return Json(() =>
            {
                if (!StringHelper.IsMobile(request.OldTelephone))
                {
                    throw new ZzbException("请输入正确的手机号码");
                }
                if (!StringHelper.IsMobile(request.NewTelephone))
                {
                    throw new ZzbException("请输入正确的手机号码");
                }       
                var cacheKey = $"sendCode:{VerificationCodeTypes.ChangeTelephone}:{request.NewTelephone}";
                var cacheCode = _cacheManager.Get<string>(cacheKey);

                if (string.IsNullOrEmpty(cacheCode))
                    throw new ZzbException("验证码已过期");
                if (cacheCode != request.Code)
                    throw new ZzbException("验证码错误");

                var entity = Context.Clients.Where(c => c.Telephone == request.OldTelephone).FirstOrDefault();
                if (entity.ClientId != CurrentClientId)
                {
                    throw new ZzbException("账户的旧手机不匹配");
                }
                 var exists = Context.Clients.Where(c => c.Telephone == request.NewTelephone.Trim()).Any();
                if (exists)
                {
                    throw new ZzbException("已存在手机号码,无法更新");
                }

                var res =  HuanxinClientRequest.ModifyUserPassword(entity.HuanXinId,request.NewTelephone);
                if (res.ReturnCode == ServiceResultCode.Success)
                {
                    entity.Telephone = request.NewTelephone;
                    Context.Clients.Update(entity);
                    return ObjectResult(Context.SaveChanges() > 0);
                }
                throw new ZzbException(res.Message);


            }, "找回密码失败");

        }



    }
}