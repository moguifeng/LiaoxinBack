using AllLottery.IBusiness;
using AllLottery.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Zzb.EF;

namespace AllLottery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public IHostingEnvironment HostingEnvironment { get; set; }

        public IAffixService AffixService { get; set; }

        [HttpGet("GetAffix")]
        public IActionResult GetAffix(int id)
        {
            Response.Headers.Add("ETag", new[] { id.ToString() });
            if (Request.Headers.Keys.Contains("If-None-Match") && Request.Headers["If-None-Match"].ToString() == id.ToString())
            {
                return StatusCode(304, "没改变啊");
            }
            var a = AffixService.GetAffix(id);
            if (a != null)
            {
                return new FileContentResult(AffixService.GetAffix(id), "image/jpeg");
            }
            return null;
        }

        [HttpPost("UploadImage")]
        public object UploadImage()
        {
            using (var context = LotteryContext.CreateContext())
            {
                var file = Request.Form.Files[0];
                Affix affix = new Affix();
                if (!Directory.Exists(Path.Combine(HostingEnvironment.ContentRootPath, "Upload")))
                {
                    Directory.CreateDirectory(Path.Combine(HostingEnvironment.ContentRootPath, "Upload"));
                }
                using (var strean = new FileStream(Path.Combine(HostingEnvironment.ContentRootPath, affix.Path), FileMode.CreateNew))
                {
                    file.CopyTo(strean);
                }
                var exist = context.Affixs.Add(affix);
                context.SaveChanges();
                return new { id = exist.Entity.AffixId };
            }
        }
    }
}