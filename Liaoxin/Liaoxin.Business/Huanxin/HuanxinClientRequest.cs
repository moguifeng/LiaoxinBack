using System;
using System.Collections.Generic;
using System.Text;
using Zzb;
using Zzb.Common;

namespace Liaoxin.Business
{
   public class HuanxinClientRequest
    {
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> RegisterClient(string id,string password,string nickName)
        {
            var responseUrl = $"{HostService.url}/users";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("username", id);
            dic.Add("password", password);
            dic.Add("nickname", nickName);
            var res = HostService.Post(responseUrl, dic, true);
            return res;
        }

        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> AddFriend(string owner_username, string friend_username)
        {
            var responseUrl = $"{HostService.url}/users/{owner_username}/contacts/users/{friend_username}";

            var res = HostService.Post(responseUrl, null);

            return res;
        }


        /// <summary>
        /// 解除好友
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> DeleteFriend(string owner_username, string friend_username)
        {
            var responseUrl = $"{HostService.url}/users/{owner_username}/contacts/users/{friend_username}";
            var res = HostService.Delete(responseUrl);
            return res;
        }


        /// <summary>
        /// 查询离线消息数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> OfflineMsgCount(string owner_username)
        {
            var responseUrl = $"{HostService.url}/users/{owner_username}/offline_msg_count";
            var res = HostService.Get(responseUrl);



            //          "data": {
            //              "123": 1
            //                    },

            return res;
        }



        public static ServiceResult<string> ModifyUserProperty(string username,Dictionary<string,object> dic)
        {
            var responseUrl = $"{HostService.url}/metadata/users/{username}";
            var res = HostService.Put(responseUrl,dic);
            return res;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> DeleteClient(string username)
        {
            var responseUrl = $"{HostService.url}/users/{username}";

            var res = HostService.Delete(responseUrl);
            return res;
        }


        /// <summary>
        /// 修改用户昵称
        /// </summary>        
        /// <returns></returns>
        public static ServiceResult<string> ModifyNickName(string username,string nickname)
        {
            var responseUrl = $"{HostService.url}/users/{username}";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("nickname", nickname);
            var res = HostService.Put(responseUrl, dic);
            return res;
        }


        /// <summary>
        /// 黑名单加人
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> AddBlockFriend(string owner_username,List<string> usernames)
        {
            var responseUrl = $"{HostService.url}/users/{owner_username}/blocks/users";
            Dictionary<string, object> dic = new Dictionary<string, object>();          
            dic.Add("usernames", usernames);      
            var res = HostService.Post(responseUrl,dic);
            return res;
        }

        /// <summary>
        /// 黑名单减人
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> DeleteBlockFriend(string owner_username,string blocked_username)
        {
            var responseUrl = $"{HostService.url}/users/{owner_username}/blocks/users/{blocked_username}";            
            var res = HostService.Delete(responseUrl);
            return res;
        }



        /// <summary>
        /// 用户账户解禁
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> UserActivate(string username)
        {
            var responseUrl = $"{HostService.url}/users/{username}/activate";
            var res = HostService.Post(responseUrl,null);
            return res;
        }



        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> ModifyUserPassword(string username,string password)
        {
            var responseUrl = $"{HostService.url}/users/{username}/password";
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("newpassword", password);
            var res = HostService.Put(responseUrl, null);
            return res;
        }


        /// <summary>
        /// 用户账户禁用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ServiceResult<string> UserDeactivate(string username)
        {
            var responseUrl = $"{HostService.url}/users/{username}/deactivate";
            var res = HostService.Post(responseUrl, null);
            return res;
        }
        }  
}
