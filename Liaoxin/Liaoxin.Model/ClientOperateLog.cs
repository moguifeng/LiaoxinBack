using Zzb.EF;

namespace Liaoxin.Model
{
    public class ClientOperateLog : BaseModel
    {
        public ClientOperateLog()
        {
        }

        public ClientOperateLog(int clientId, string message)
        {
            ClientId = clientId;
            Message = message;
        }

        public int ClientOperateLogId { get; set; }

        public string Message { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}