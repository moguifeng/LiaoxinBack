
using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using Zzb;
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
        public IActionResult GetAffix(Guid id)
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
                if (file.Length == 0)
                {
                    throw new ZzbException("请上传文件.");
                }
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

        [HttpPost("UploadImageByBase64")]
        public object UploadImageByBase64(ImageBase64Request request)
        {
            using (var context = LiaoxinContext.CreateContext())
            {
             
            
                Affix affix = new Affix();

                if (!Directory.Exists(Path.Combine(HostingEnvironment.ContentRootPath, "Upload")))
                {
                    Directory.CreateDirectory(Path.Combine(HostingEnvironment.ContentRootPath, "Upload"));
                }
                var path = Path.Combine(HostingEnvironment.ContentRootPath, affix.Path);             
                try
                {
                    if (request.Base64Str.Length < 5)
                    {
                        throw new ZzbException("请上传文件.");
                    }
                    string[] imgStr = request.Base64Str.Split(',');
                    byte[] buffer = Convert.FromBase64String(imgStr[1].Replace(" ", "+"));
                    FileStream fs = new FileStream(path, FileMode.CreateNew);
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Close();
                     
                }
                catch (Exception ex)
                {
                    throw ex;
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