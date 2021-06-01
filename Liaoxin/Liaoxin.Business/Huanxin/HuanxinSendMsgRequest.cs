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

            if (string.IsNullOrEmpty(HostService.access_token))
            {
                HostService.GetToken();
            }
            var res = HuanxinSendMsgRequest.SelfSendMsg(mobiles, code);
            if (res.ReturnCode != ServiceResultCode.Success)
            {
                //如果没有登录返回500的.
                HostService.GetToken();
                res = HuanxinSendMsgRequest.SelfSendMsg(mobiles, code);
            }
            return res;



        }

        private static ServiceResult<string> SelfSendMsg(string[] mobiles, string code)
        {
            var responseUrl = $"{HostService.url}/sms/send";

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
