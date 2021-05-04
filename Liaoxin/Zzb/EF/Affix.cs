using System;

namespace Zzb.EF
{
    public class Affix : BaseModel
    {
        public int AffixId { get; set; }

        public string Path { get; set; } = "Upload/" + Guid.NewGuid();

        /// <summary>
        /// 绑定客户Id(不可以随便访问图片)
        /// </summary>
        public Guid? ClientId { get; set; }
    }
}