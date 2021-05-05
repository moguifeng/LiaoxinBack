
using Liaoxin.IBusiness;
using Liaoxin.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using Zzb.Context;
using Zzb.EF;

namespace Liaoxin.Controllers
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
            var affix = AffixService.GetAffix(id);
            if (affix != null)
            {
                return new FileContentResult(affix, "image/jpeg");
            }
            return null;
        }

        [HttpPost("UploadImage")]
        public object UploadImage()
        {
            using (var context = LiaoxinContext.CreateContext())
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
                if (UserContext.Current.Id != Guid.Empty)
                {
                    affix.ClientId = UserContext.Current.Id;
                }
                var exist = context.Affixs.Add(affix);
                context.SaveChanges();
                return new { id = exist.Entity.AffixId };
            }
        }
    }
}