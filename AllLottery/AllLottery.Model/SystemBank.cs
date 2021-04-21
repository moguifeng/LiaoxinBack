﻿using System.ComponentModel.DataAnnotations;
using Zzb.EF;

namespace AllLottery.Model
{
    public class SystemBank : BaseModel
    {
        public int SystemBankId { get; set; }

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