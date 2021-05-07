using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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

        protected IList<string> GetPostBodyFiledKey(IList<string> defaultUpdateFieldList=null, string objKey = "")
        {
            List<string> list = new List<string>();
            if (defaultUpdateFieldList != null)
            {
                list.AddRange(defaultUpdateFieldList);
            }
            Stream postData = Request.Body;
            string postContent = "";
            using (StreamReader sr = new StreamReader(postData))
            {
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                postContent = sr.ReadToEndAsync().Result;
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
            }
            if (string.IsNullOrEmpty(postContent))
            {
                return null;
            }
            else
            {

                Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(postContent);
                if (!string.IsNullOrEmpty(objKey))
                {
                    jo = jo[objKey].ToObject<JObject>();
                }
                if (jo.HasValues)
                {
                    foreach (JProperty item in jo.Properties())
                    {
                        if (list.FirstOrDefault(p => string.Equals(p, item.Name, StringComparison.OrdinalIgnoreCase)) == null)
                        {
                            list.Add(item.Name);
                        }
                    }
                }
                return list;
            }
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


        protected string CurrentHuanxinId 
        {
            get
            {
                if (UserContext.Current.IsAuthenticated)
                {
                    return UserContext.Current.Name;
                }

                return "" ;
            }
        }

    }
}