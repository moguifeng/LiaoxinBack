using Liaoxin.IBusiness;
using Liaoxin.Model;
using System.Linq;
using Zzb;

namespace Liaoxin.Business
{
    public class AnnouncementService : BaseService, IAnnouncementService
    {
        public Announcement[] GetAnnouncements(int size, int index, out int total)
        {
            var sql = from a in Context.Announcements where a.IsEnable orderby a.ShowOpenTime descending select a;
            total = sql.Count();
            return sql.Skip((index - 1) * size).Take(size).ToArray();
        }

        public Announcement GetAnnouncement(int id)
        {
            var exist = (from a in Context.Announcements where a.AnnouncementId == id select a).FirstOrDefault();
            if (exist == null)
            {
                throw new ZzbException("无法找到该公告");
            }

            if (!exist.IsEnable)
            {
                throw new ZzbException("该公告已删除");
            }

            return exist;
        }

        public ActivityAnnouncement[] GetActivityAnnouncements()
        {
            return (from a in Context.ActivityAnnouncements where a.IsEnable orderby a.SortIndex select a)
                .ToArray();
        }

        public PictureNews[] GetPictureNewses()
        {
            return (from p in Context.PictureNewses where p.IsEnable orderby p.SortIndex select p).ToArray();
        }
    }
}