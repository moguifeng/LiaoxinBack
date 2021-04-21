using System;
using Zzb.EF;

namespace AllLottery.Model
{
    /// <summary>
    /// 公告
    /// </summary>
    public class Announcement : BaseModel
    {
        public Announcement()
        {
        }

        public Announcement(string title, string content, DateTime showOpenTime)
        {
            Title = title;
            Content = content;
            ShowOpenTime = showOpenTime;
        }

        public int AnnouncementId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        public DateTime ShowOpenTime { get; set; }=DateTime.Now;
    }
}