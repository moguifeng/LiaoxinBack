using System;
using System.ComponentModel.DataAnnotations;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class SystemBank : BaseModel
    {
        public Guid SystemBankId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// 银行名称
        /// </summary>
        [MaxLength(200)]
        [ZzbIndex(IsUnique = true)]
        public string Name { get; set; }

        public int SortIndex { get; set; }

        /// <summary>
        /// logo
        /// </summary>
        public int AffixId { get; set; }

        public virtual Affix Affix { get; set; }
    }
}