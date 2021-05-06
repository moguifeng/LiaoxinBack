using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Zzb.Context;
using Zzb.ZzbLog;

namespace Zzb.Mvc
{
    public class BaseApiController : ControllerBase
    {
        public UserContextManager _UserContext { get; set; }



        protected void SignOut()
        {
            _UserContext.RemoveUserContext();
            //HttpContext.SignOutAsync(ZzbAuthorizeAttribute.ZzbAuthenticationScheme);
        }

        protected ServiceResult Json(Func<ServiceResult> action, string error = null)
        {
            try
            {
                return action();
            }
            catch (ZzbException e)
            {
                LogHelper.Information(error, e);
                return new ServiceResult(ServiceResultCode.Error,
                    string.IsNullOrEmpty(error) ? e.Message : error + ":" + e.Message);
            }
            catch (DbUpdateConcurrencyException e)
            {
                LogHelper.Warning(error, e);
                return new ServiceResult(ServiceResultCode.Error, string.IsNullOrEmpty(error) ? e.Message : error + ":" + e.Message);
            }
            catch (Exception e)
            {
                LogHelper.Error(error, e);
                return new ServiceResult(ServiceResultCode.Error, string.IsNullOrEmpty(error) ? e.Message : error + ":" + e.Message);
            }

        }

        protected ServiceResult ObjectResult(object obj)
        {
            return new ServiceResult<object>(ServiceResultCode.Success, "OK", obj);
        }


        protected ServiceResult<List<K>> ListGenericityResult<K>(List<K> lis)
        {
            return new ServiceResult<List<K>>(ServiceResultCode.Success, "sucess", lis);


        }

        protected ServiceResult<K> ObjectGenericityResult<K>(K obj)
        {
            return new ServiceResult<K>(ServiceResultCode.Success, "OK", obj);

        }


        protected ServiceResult JsonObjectResult(object obj, string msg = null)
        {
            return Json(() => ObjectResult(obj), msg);
        }
        
        protected int UserId
        {
            get
            {
                if (UserContext.Current.IsAuthenticated)
                {
                    if (int.TryParse(UserContext.Current.Name, out var id))
                    {
                        return id;
                    }

                    return -1;
                }

                return -1;
            }
        }

        protected Guid CurrentClientId
        {
            get
            {
                if (UserContext.Current.IsAuthenticated)
                {
                    return UserContext.Current.Id;
                }

                return Guid.Empty;
            }
        }

    }
}