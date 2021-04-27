using System.ComponentModel;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class Message : BaseModel
    {
        public Message()
        {
        }

        public Message(int infoId, MessageInfoTypeEnum infoType, MessageTypeEnum type, string description)
        {
            InfoId = infoId;
            InfoType = infoType;
            Type = type;
            Description = description;
        }

        public int MessageId { get; set; }

        public MessageTypeEnum Type { get; set; }

        public string Description { get; set; }

        public int InfoId { get; set; }

        public MessageInfoTypeEnum InfoType { get; set; } = MessageInfoTypeEnum.Player;

        public decimal Money { get; set; }

        [ZzbIndex]
        public bool IsLook { get; set; } = false;

        [ZzbIndex]
        public bool IsSend { get; set; } = false;

        public byte[] Version { get; set; }
    }

    public enum MessageTypeEnum
    {
        [Description("消息通知")]
        Message = 0,
        [Description("用户登录")]
        Login = 1,
        [Description("另类消息")]
        LongMessage = 2,
        [Description("后台消息")]
        BackMessage = 3,
    }

    public enum MessageInfoTypeEnum
    {
        [Description("玩家")]
        Player = 0,
        [Description("用户")]
        User = 1
    }
}