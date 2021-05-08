using System;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class ActivityAnnouncement : BaseModel
    {
        public ActivityAnnouncement()
        {
        }

        public ActivityAnnouncement(string title, DateTime beginTime, DateTime endTime, string content, Guid affixId)
        {
            Title = title;
            BeginTime = beginTime;
            EndTime = endTime;
            Content = content;
            AffixId = affixId;
        }

        public int ActivityAnnouncementId { get; set; }

        public string Title { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Content { get; set; }

        public Guid AffixId { get; set; }

        public virtual Affix Affix { get; set; }

        public int SortIndex { get; set; }
    }
}