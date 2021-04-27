using System;

namespace Zzb.EF
{
    public class Affix : BaseModel
    {
        public int AffixId { get; set; }

        public string Path { get; set; } = "Upload/" + Guid.NewGuid();
    }
}