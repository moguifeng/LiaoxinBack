using System;
using System.Collections.Generic;
using System.Text;
using Zzb;

namespace Liaoxin.Business
{
   public class HuanxinRequest
    {
        public static ServiceResult<string> RegisterClient(string id)
        {
            var responseUrl = $"{HostService.url}/users";
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("username", id);
            dic.Add("password", id + "liaoxin");
            //dic.Add("nickname", "哈哈");
            var res = HostService.Post(responseUrl, dic, true);
            return res;
        }

    }
}
