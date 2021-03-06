using System;
using Zzb.EF;

namespace Liaoxin.Model
{
    /// <summary>
    /// 滚动图
    /// </summary>
    public class PictureNews : BaseModel
    {
        public PictureNews()
        {
        }

        public PictureNews(Guid affixId, string url, int sortIndex)
        {
            AffixId = affixId;
            Url = url;
            SortIndex = sortIndex;
        }

        public int PictureNewsId { get; set; }

        public Guid AffixId { get; set; }

        public virtual Affix Affix { get; set; }

        public string Url { get; set; }

        public int SortIndex { get; set; }
    }
}