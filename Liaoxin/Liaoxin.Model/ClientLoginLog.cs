using Zzb.EF;

namespace Liaoxin.Model
{
    public class ClientLoginLog : BaseModel
    {
        public int ClientLoginLogId { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public string IP { get; set; }

        public string Address { get; set; }
 
    }
}