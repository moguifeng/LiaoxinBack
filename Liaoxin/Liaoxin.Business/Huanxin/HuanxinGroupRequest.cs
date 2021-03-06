using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zzb;
using Zzb.Common;

namespace Liaoxin.Business
{
    public class HuanxinGroupRequest

    {


        public class CreateGroupResponse
        {
            public CreateGroupDataResponse Data { get; set; }
        }

        public class CreateGroupDataResponse
        {
            public string GroupId { get; set; }
        }
        /// <summary>
        /// 创建群组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> CreateGroup(string groupName, string owner, string[] members)
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

            dic.Add("groupname", groupName);
            dic.Add("desc", " ");
            dic.Add("public", false);
            dic.Add("allowinvites", true);
            dic.Add("maxusers", 1000);
            dic.Add("owner", owner);
            dic.Add("members", members);
            var responseUrl = $"{HostService.url}/chatgroups";
            var res = HostService.Post(responseUrl, dic);
            if (res.ReturnCode == ServiceResultCode.Success)
            {
                CreateGroupResponse response = JsonHelper.Json<CreateGroupResponse>(res.Data);
                res.Data = response.Data.GroupId;

            }
            return res;
        }


        public static ServiceResult<string> AddGroupMembers(string chatGroupId, string[] members)
        {
            var responseUrl = $"{HostService.url}/chatgroups/{chatGroupId}/users";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("usernames", members);
            var res = HostService.Post(responseUrl, dic);
            return res;
        }




        /// <summary>
        /// 移除成员
        /// </summary>
        /// <param name="chatGroupId"></param>
        /// <param name="username"></param>
        /// <returns></returns>

        public static ServiceResult<string> RemoveGroupMember(string chatGroupId, string members)
        {
            var responseUrl = $"{HostService.url}/chatgroups/{chatGroupId}/users/{members}";
            var res = HostService.Delete(responseUrl);
            return res;

        }

        public static ServiceResult<string> ModifyGroup(string chatGroupId,string description, string groupname)
        {
            var responseUrl = $"{HostService.url}/chatgroups/{chatGroupId}";

            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrEmpty(description))
            {
                dic.Add("description", description);
            }

            if (!string.IsNullOrEmpty(groupname))
            {
                dic.Add("groupname", groupname);
            }

            var res = HostService.Put(responseUrl, dic);
            return res;
        }


        /// <summary>
        /// 解散群
        /// </summary>
        /// <param name="chatGroupId"></param>
        /// <returns></returns>
        public static ServiceResult<string> RemoveGroup(string chatGroupId)
        {
            var responseUrl = $"{HostService.url}/chatgroups/{chatGroupId}";
            var res = HostService.Delete(responseUrl);
            return res;


        }

        /// <summary>
        /// 转让群
        /// </summary>
        /// <param name="chatGroupId"></param>
        /// <param name="newowner"></param>
        /// <returns></returns>
        public static ServiceResult<string> TranferGroup(string chatGroupId,string newowner)
        {
            var responseUrl = $"{HostService.url}/chatgroups/{chatGroupId}";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("newowner", newowner);
            var res = HostService.Put(responseUrl, dic);
            return res;
        }

        }


}
