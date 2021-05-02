using System;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class ClientOperateLog : BaseModel
    {
        public ClientOperateLog()
        {
        }

        public ClientOperateLog(Guid clientId, string message)
        {
            ClientId = clientId;
            Message = message;
        }

        public Guid ClientOperateLogId { get; set; } = Guid.NewGuid();

        public string Message { get; set; }

        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }
    }
}