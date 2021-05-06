using Liaoxin.IBusiness;
using Liaoxin.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Mvc;

namespace Liaoxin.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[ZzbAuthorize]
    //public class AnnouncementController : BaseApiController
    //{
    //    public IAnnouncementService AnnouncementService { get; set; }

    //    [HttpPost("GetAnnouncements")]
    //    public ServiceResult GetAnnouncements(PageViewModel model)
    //    {
    //        return Json(() =>
    //        {
    //            var ts = AnnouncementService.GetAnnouncements(model.Size, model.Index, out var total);
    //            return ObjectResult(new
    //            {
    //                total,
    //                data = from t in ts
    //                       select new
    //                       {
    //                           t.Title,
    //                           t.AnnouncementId,
    //                           ShowOpenTime = t.ShowOpenTime.ToDateString()
    //                       }
    //            });
    //        }, "获取公告列表失败");
    //    }

    //    [HttpPost("GetAnnouncement")]
    //    public ServiceResult GetAnnouncement(AnnouncementGetAnnouncement model)
    //    {
    //        return Json(() =>
    //        {
    //            var a = AnnouncementService.GetAnnouncement(model.Id);
    //            return ObjectResult(new
    //            {
    //                a.Title,
    //                a.AnnouncementId,
    //                a.Content,
    //                ShowOpenTime = a.ShowOpenTime.ToDateString()
    //            });
    //        }, "获取公告失败");
    //    }

    //    [HttpPost("GetActivityAnnouncements")]
    //    public ServiceResult GetActivityAnnouncements()
    //    {
    //        return JsonObjectResult(from a in AnnouncementService.GetActivityAnnouncements()
    //                                select new
    //                                {
    //                                    a.AffixId,
    //                                    a.Title,
    //                                    BeginTime = a.BeginTime.ToDateString(),
    //                                    EndTime = a.EndTime.ToDateString(),
    //                                    a.Content
    //                                });
    //    }

    //    [HttpPost("GetPictureNewses")]
    //    public ServiceResult GetPictureNewses()
    //    {
    //        return JsonObjectResult(from p in AnnouncementService.GetPictureNewses() select new { p.PictureNewsId, p.AffixId, p.Url, p.SortIndex });
    //    }
    //}

    //public class AnnouncementGetAnnouncement
    //{
    //    public int Id { get; set; }
    //}
}