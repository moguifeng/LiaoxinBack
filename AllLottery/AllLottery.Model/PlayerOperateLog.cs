using Zzb.EF;

namespace AllLottery.Model
{
    public class PlayerOperateLog:BaseModel
    {
        public PlayerOperateLog()
        {
        }

        public PlayerOperateLog(int playerId, string message)
        {
            PlayerId = playerId;
            Message = message;
        }

        public int PlayerOperateLogId { get; set; }

        public string Message { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}