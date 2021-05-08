using System;

namespace Zzb.EF
{
    public class Affix : BaseModel
    {
        public Guid AffixId { get; set; } = Guid.NewGuid();

        public string Path { get; set; } = "Upload/" + Guid.NewGuid();


        /// <summary>
        /// 不允许其他用户访问
        /// </summary>
        public bool NotAllowOtherSee { get; set; } = false;

        /// <summary>
        /// 绑定客户Id(不可以随便访问图片)
        /// </summary>
        public Guid? ClientId { get; set; }
        
    }
}