using Zzb.EF;

namespace Liaoxin.Model
{
    public class NotReportPlayer : BaseModel
    {
        public int NotReportPlayerId { get; set; }

        [ZzbIndex(IsUnique = true)]
        public int PlayerId { get; set; }

        public virtual Player Player { get; set; }
    }
}