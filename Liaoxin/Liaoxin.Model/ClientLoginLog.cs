using System;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class ClientLoginLog : BaseModel
    {
        public Guid ClientLoginLogId { get; set; } = Guid.NewGuid();

        public Guid ClientId { get; set; }

        public virtual Client Client { get; set; }

        public string IP { get; set; }

        public string Address { get; set; }
 
    }
}