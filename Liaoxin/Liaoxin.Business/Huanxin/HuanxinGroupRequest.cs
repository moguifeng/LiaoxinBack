using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zzb;

namespace Liaoxin.Business
{
    public class HuanxinGroupRequest

    {

        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> CreateGroup(string groupName, string friend_username)
        {

            // {
            //  "groupname": "string",
            //  "desc": "string",
            //  "public": true,
            //  "maxusers": 0,
            //  "approval": true,
            //  "owner": "string",
            //  "members": [
            //    "string"
            //  ]
            //}
            //            "groupname":群组名称，此属性为必须的。
            //                "desc":群组描述，此属性为必须的。
            //                "public":是否是公开群，此属性为必须的。 
            //                "maxusers":群组成员最大数（包括群主），
            //                值为数值类型，默认值200，最大值2000，此属性为可选的。
            //                "approval":加入公开群是否需要批准，默认值是false（加入公开群不需要群主批准），此属性为必选的，私有群必须为true。
            //                "owner":群组的管理员，此属性为必须的。 
            //                "members":群组成员，此属性为可选的，但是如果加了此项，数组元素至少一个（注：群主jma1不需要写入到members里面）。

            Dictionary<string, object> dic = new Dictionary<string, object>();
            var responseUrl = $"{HostService.url}/chatgroups";
            var res = HostService.Post(responseUrl, dic);
            return res;
        }
    }


}
