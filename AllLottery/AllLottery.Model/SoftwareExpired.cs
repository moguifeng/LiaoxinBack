using System;
using Zzb.EF;

namespace AllLottery.Model
{
    public class SoftwareExpired : BaseModel
    {
        public int SoftwareExpiredId { get; set; }

        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }

        public DateTime Expired { get; set; }
    }
}