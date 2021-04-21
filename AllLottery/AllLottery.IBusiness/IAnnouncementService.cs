using AllLottery.Model;

namespace AllLottery.IBusiness
{
    public interface IAnnouncementService
    {
        Announcement[] GetAnnouncements(int size, int index, out int total);

        Announcement GetAnnouncement(int id);

        ActivityAnnouncement[] GetActivityAnnouncements();

        PictureNews[] GetPictureNewses();
    }
}