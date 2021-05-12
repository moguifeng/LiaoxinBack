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

            var responseUrl = $"{HostService.msgUrl}/sms/send";


            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("mobiles", mobiles);
            dic.Add("tid", "164");


            Dictionary<string, string> map = new Dictionary<string, string>();
            map.Add("p1", code);


            dic.Add("tmap", map);
            var res = HostService.Post(responseUrl, dic, true);
            return res;




        }
    }
}
