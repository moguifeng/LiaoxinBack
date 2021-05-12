using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Zzb;
using Zzb.Common;

namespace Liaoxin.Business
{
    public class HuanxinSendMsgRequest
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> SendMsg(string[] mobiles, string code)
        {





            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("mobiles", mobiles);
            dic.Add("tid", "164");


            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("p1", code);


            dic.Add("tmap", map);

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer YWMtPZoRTFk0EeukvYVwaay5gAAAAAAAAAAAAAAAAAAAAAGEbAIHRQdI85XWWn9IAKURAgMAAAF3E059-QBPGgC29yIdZgUINP3vg5IapIANY6z_qQpTbxbUXEX928VpPQ");

            var responseUrl = $"{HostService.url}/sms/send";
            Uri u = new Uri(responseUrl);

            string jsonStr = JsonConvert.SerializeObject(dic);
            StringContent stringContent = new StringContent(jsonStr);
            var res = client.PostAsync(u, stringContent);

            return null;

            //var serviceRes = new ServiceResult<string>();
            //serviceRes.Message = res.Result.Content.ReadAsStringAsync().Result;
            //serviceRes.ReturnCode = ServiceResultCode.UnAuth;
            //return serviceRes;





            //var responseUrl = $"{HostService.msgUrl}/sms/send";


            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("mobiles", mobiles);
            //dic.Add("tid", "164");


            //Dictionary<string, string> map = new Dictionary<string, string>();
            //map.Add("p1", code);


            //dic.Add("tmap", map);
            //var res = HostService.Post(responseUrl, dic, true);
            //return res;




        }
    }
}
