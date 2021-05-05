
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Zzb.ICacheManger;
using Newtonsoft.Json;
using Zzb;
using Zzb.Common;

namespace Liaoxin.Business
{
    public class HostService
    {


          public static string url = "http://a1.easemob.com/1116210407091395/testapp";


          static string access_token = "";


        private static string GetToken()
        {
            var responseUrl = $"{url}/token";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("grant_type", "client_credentials");
            dic.Add("client_id", "YXA6WZPgq60iTuSXL4QZHSY4Lg");
            dic.Add("client_secret", "YXA6KyN7SFU_ipKp582klBDK0HSLrOY");

             var  res = Post(responseUrl, dic, false);
            var tokenEntity =  JsonHelper.Json<TokenResponse>(res.Data);
            if (tokenEntity != null)
            {
                access_token = tokenEntity.access_token;
                return tokenEntity.access_token;
            }
            return string.Empty;         
        }

     
        #region  请求

        private static ServiceResult Request(string url, Dictionary<string, string> dic, bool needToken = true, string method ="POST")
        {
            string result = "";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = method;
            if (needToken)
            {
                req.Headers.Add("Authorization", access_token);
            }
            //req.ContentType = "application/x-www-form-urlencoded";
            //req.ContentType = "application/json";

            if (dic != null && (method.ToLower() != "get" || method.ToLower() != "delete"))
            {
                StringBuilder builder = new StringBuilder();
                int i = 0;
                foreach (var item in dic)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }
                byte[] data = Encoding.UTF8.GetBytes(builder.ToString());
                req.ContentLength = data.Length;
                using (Stream reqStream = req.GetRequestStream())
                {
                    reqStream.Write(data, 0, data.Length);
                    reqStream.Close();
                }
            }
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    Stream stream = resp.GetResponseStream();

                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }
                    var res = new ServiceResult<string>();
                    res.Data = result;
                    return res;
                }
                else
                {
                    return new ServiceResult();
                }
            }
            catch (WebException ex)
            {
                HttpWebResponse resp = (HttpWebResponse)ex.Response;                         
                if (resp.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return new ServiceResult(ServiceResultCode.UnAuth);
                }

                Stream stream = resp.GetResponseStream();
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(result);
                    if (errorResponse != null)
                    {
                        var res =  new ServiceResult<string>();
                        res.Message = errorResponse.error_description;
                        res.ReturnCode = ServiceResultCode.ErrOperation;
                        return res;
                    }
                }
                return new ServiceResult();

            }
        }


        private static ServiceResult<string> doubleCheck(string url, Dictionary<string, string> dic, bool needToken, string method)
        {
            if (needToken)
            {
                 ServiceResult res  = Request(url, dic, needToken, method);
                if (res.ReturnCode == ServiceResultCode.UnAuth)
                {
                    GetToken();
                    res = Request(url, dic, needToken, method);
                }
                return (ServiceResult<string>)res;

            }
            else
            {
                return (ServiceResult<string>)Request(url, dic, needToken, method);
            }

        }

        public static ServiceResult<string> Post(string url, Dictionary<string, string> dic, bool needToken = true)
        {
           return  doubleCheck(url,dic,needToken,"POST");
         
        }

      


        public static ServiceResult<string> Put(string url, Dictionary<string, string> dic, bool needToken = true)
        {
            return doubleCheck(url, dic, needToken, "PUT");
        }

        public static ServiceResult<string> Get(string url, bool needToken = true)
        {

            return doubleCheck(url, null, needToken, "GET");


        }

        public static ServiceResult<string> Delete(string url, bool needToken = true)
        {
            return doubleCheck(url, null, needToken, "DELETE");
        }

        #endregion

    }

 
}
