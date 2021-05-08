
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
using Zzb;
using Zzb.Common;
using Zzb.Context;
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


        /// <summary>
        /// 获取客户
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetClient")]
        public ServiceResult<ClientBaseInfoResponse> GetClient()
        {
            var entity = clientService.GetClient();
            return (ServiceResult<ClientBaseInfoResponse>)Json(() =>
            {
                return ObjectGenericityResult<ClientBaseInfoResponse>(entity);
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
                var entity = clientService.LoginByCode(request);
                _UserContext.SetUserContext(entity.ClientId, entity.HuanXinId, entity.LiaoxinNumber);
                string token = UserContext.Current.Token;
                return ObjectResult(token);
            }, "登录失败");

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
                entity.NickName = request.nickName;
                Context.Clients.Update(entity);
                var res = HuanxinClientRequest.ModifyNickName(CurrentHuanxinId,request.nickName);
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
                var entity = Context.Clients.Where(c => c.ClientId == CurrentClientId).First();
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

                if (request.FrontCover == Guid.Empty)
                {
                    throw new ZzbException("请上传身份证正面");
                }
                if (request.BackCover == Guid.Empty)
                {
                    throw new ZzbException("请上传身份证背面");
                }

                var entity = Context.Clients.Where(c => c.ClientId == CurrentClientId).FirstOrDefault();
                entity.UniqueNo = request.UniqueNo;
                entity.UniqueBackImg = request.FrontCover;
                entity.UniqueFrontImg = request.BackCover;                
                Context.Clients.Update(entity);
                return ObjectResult(Context.SaveChanges() > 0);
            }, "修改头像失败");

        }
    }
}