using System;
using System.Collections.Generic;
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
            return null;
            //var responseUrl = $"{HostService.url}/users";
            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("username", id);
            //dic.Add("password", password);
            //dic.Add("nickname", nickName);
            //var res = HostService.Post(responseUrl, dic, true);
            //return res;
        }
    }
