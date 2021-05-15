
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
using System.Net.Http;
using System.Net.Http.Headers;

namespace Liaoxin.Business
{
    public class HostService
    {


        public static string url = "http://a1.easemob.com/1110210506180660/demo";

        private static  DateTime RecordTime { get; set; }
       public static string access_token = "";


        public static string GetToken()
        {
            //两个钟重新获取一次token
            if (RecordTime.AddHours(2) >DateTime.Now ||   string.IsNullOrEmpty( access_token))
            {
                var responseUrl = $"{url}/token";
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("grant_type", "client_credentials");
                dic.Add("client_id", "YXA6SMpBMaTKSQOXo1oPhHJFvg");
                dic.Add("client_secret", "YXA6A99XQbKOP8OspNX6himjlLqPgIg");

                var res = Post(responseUrl, dic, false);
                var tokenEntity = JsonHelper.Json<TokenResponse>(res.Data);
                if (tokenEntity != null)
                {
                    access_token = tokenEntity.access_token;
                    return tokenEntity.access_token;
                }
                RecordTime = DateTime.Now;


            }
            return access_token;


        }



        private static HttpClient GetClient(string url, bool needToken)
        {

            HttpClient client = new HttpClient();

            if (needToken)
            {
                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer  {access_token}");
            }


            return client;
        }

        #region  请求

        private static ServiceResult Request(string url, Dictionary<string, object> dic, bool needToken = true, string method = "POST")
        {
            var client = GetClient(url, needToken);
            Uri u = new Uri(url);

            Task<HttpResponseMessage> httpResponse = null;


            if (method.ToLower() == "post")
            {
                string jsonStr = JsonConvert.SerializeObject(dic);
                StringContent stringContent = new StringContent(jsonStr);
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpResponse = client.PostAsync(u, stringContent);

            }
            else if (method.ToLower() == "put")
            {
                string jsonStr = JsonConvert.SerializeObject(dic);
                StringContent stringContent = new StringContent(jsonStr);
                stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpResponse = client.PutAsync(u, stringContent);
            }
            else if (method.ToLower() == "get")
            {
                string jsonStr = JsonConvert.SerializeObject(dic);
                httpResponse = client.GetAsync(u);
            }
            else
            {
                string jsonStr = JsonConvert.SerializeObject(dic);
                httpResponse = client.DeleteAsync(u);
            }

            var res = httpResponse.Result;
            if (res.StatusCode == HttpStatusCode.OK)
            {
                var serviceRes = new ServiceResult<string>();
                serviceRes.Data = res.Content.ReadAsStringAsync().Result;
                return serviceRes;
            }
            else if (res.StatusCode == HttpStatusCode.Unauthorized)
            {
                var serviceRes = new ServiceResult<string>();
                serviceRes.Message = res.Content.ReadAsStringAsync().Result;
                serviceRes.ReturnCode = ServiceResultCode.UnAuth;
                return serviceRes;
            }
            else
            {
                var serviceRes = new ServiceResult<string>();
                serviceRes.Message = res.Content.ReadAsStringAsync().Result;
                serviceRes.ReturnCode = ServiceResultCode.ErrOperation;
                return serviceRes;
            }
        }


        private static ServiceResult<string> doubleCheck(string url, Dictionary<string, object> dic, bool needToken, string method)
        {
            if (needToken)
            {
                ServiceResult res = Request(url, dic, needToken, method);
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

        public static ServiceResult<string> Post(string url, Dictionary<string, object> dic, bool needToken = true)
        {
            return doubleCheck(url, dic, needToken, "POST");

        }




        public static ServiceResult<string> Put(string url, Dictionary<string, object> dic, bool needToken = true)
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
