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
    public class HuanxinRobotRequest
    {
 

        public static ServiceResult<string>  RobotSendMsg(string groupId,string fromId,string remindClientHuanxinId)
        {
            var responseUrl = $"{HostService.url}/messages";

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("target_type", "chatgroups");
            dic.Add("target",  new object[] { groupId });
            dic.Add("msg",  new { type="text",msg="恭喜你中了" });         
            dic.Add("from", fromId);
            dic.Add("ext", new { em_at_list = remindClientHuanxinId });   
            var res = HostService.Post(responseUrl, dic, true);
            return res;
        }
        }
}
