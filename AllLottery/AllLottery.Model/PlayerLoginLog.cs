using Zzb.EF;

namespace AllLottery.Model
{
    public class PlayerLoginLog : BaseModel
    {
        public int PlayerLoginLogId { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        public string IP { get; set; }

        public string Address { get; set; }

        public bool IsApp { get; set; } = false;
    }
}