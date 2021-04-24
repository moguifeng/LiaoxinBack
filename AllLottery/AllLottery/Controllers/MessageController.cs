using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllLottery.IBusiness;
using AllLottery.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Zzb;
using Zzb.Mvc;

//作废啦
namespace AllLottery.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //[ZzbAuthorize]
    //public class MessageController : BaseApiController
    //{
    //    public IMessageService MessageService { get; set; }

    //    [HttpPost("GetMessages")]
    //    public ServiceResult GetMessages(PageViewModel model)
    //    {
    //        return Json(() =>
    //        {
    //            var messages = MessageService.GetPlayerMessages(UserId, model.Index, model.Size, out var total);
    //            return ObjectResult(new
    //            {
    //                total,
    //                data = from m in messages select new {m.MessageId, m.Description, m.CreateTime, m.IsLook}
    //            });
    //        }, "获取信息失败");
    //    }
    //}
}