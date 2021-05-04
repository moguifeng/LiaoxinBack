using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using LIaoxin.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.Context;
using Zzb.Mvc;
using static Liaoxin.ViewModel.ClientViewModel;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientController : BaseApiController
    {
        public IClientService clientService { get; set; }
        public LiaoxinContext Context { get; set; }

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
        /// 客户登录
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
                _UserContext.SetUserContext(entity.ClientId, entity.NickName, entity.LiaoxinNumber);
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


    }
}