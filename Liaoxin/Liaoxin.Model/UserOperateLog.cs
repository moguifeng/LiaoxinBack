using Zzb.EF;

namespace Liaoxin.Model
{
    public class UserOperateLog : BaseModel
    {
        public UserOperateLog()
        {
        }

        public UserOperateLog(string message, int userInfoId)
        {
            Message = message;
            UserInfoId = userInfoId;
        }

        public int UserOperateLogId { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        public int UserInfoId { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}