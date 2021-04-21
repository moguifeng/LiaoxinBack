using System;
using Zzb.EF;

namespace AllLottery.Model
{
    public class ActivityAnnouncement : BaseModel
    {
        public ActivityAnnouncement()
        {
        }

        public ActivityAnnouncement(string title, DateTime beginTime, DateTime endTime, string content, int affixId)
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

        public int AffixId { get; set; }

        public virtual Affix Affix { get; set; }

        public int SortIndex { get; set; }
    }
}