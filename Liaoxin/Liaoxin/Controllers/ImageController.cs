
using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
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


                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();



                }
                if (!Directory.Exists(Path.Combine(HostingEnvironment.ContentRootPath, "Upload")))
                {
                    Directory.CreateDirectory(Path.Combine(HostingEnvironment.ContentRootPath, "Upload"));
                }
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    var isAllow = IsAllowedExtension(fileBytes);
                    if (isAllow == false)
                    {
                        throw new ZzbException("只能上传图片.");

                    }
                    var compress = CompressImage(fileBytes, Path.Combine(HostingEnvironment.ContentRootPath, affix.Path));
                    if (!compress)
                    {
                        throw new ZzbException("上传附件失败.");

                    }
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
                    var isAllow = IsAllowedExtension(buffer);
                    if (isAllow == false)
                    {
                        throw new ZzbException("只能上传图片.");
                    }

                
                    var compress = CompressImage(buffer, path);
                    if (!compress)
                    {
                        throw new ZzbException("上传附件失败.");
                    }


                }
                catch (Exception ex)
                {
                    throw new ZzbException("上传附件失败,请上传正确的图片格式.");
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

        private bool IsAllowedExtension(byte[] buffer)
        {

            FileExtension[] fileEx = new FileExtension[] { FileExtension.BMP, FileExtension.GIF, FileExtension.JPG, FileExtension.PNG };
            MemoryStream ms = new MemoryStream(buffer);
            System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
            string fileclass = "";
            byte buffer1;
            try
            {
                buffer1 = br.ReadByte();
                fileclass = buffer1.ToString();
                buffer1 = br.ReadByte();
                fileclass += buffer1.ToString();
            }
            catch { }
            br.Close();
            ms.Close();
            foreach (FileExtension fe in fileEx)
            {
                if (Int32.Parse(fileclass) == (int)fe) return true;
            }
            return false;

        }



        /// <summary>
        /// 无损压缩图片
        /// </summary>
        /// <param name="sFile">原图片地址</param>
        /// <param name="dFile">压缩后保存图片地址</param>
        /// <param name="flag">压缩质量（数字越小压缩率越高）1-100</param>
        /// <param name="size">压缩后图片的最大大小</param>
        /// <param name="sfsc">是否是第一次调用</param>
        /// <returns></returns>
        private static bool CompressImage(byte[] buffer, string dFile, int flag = 100, int size = 300)
        {
            if (buffer.Length == 0)
            {
                return false;
            }
            if (buffer.Length / 1024 < 250)
            {
                var file = new FileStream(dFile, FileMode.CreateNew);
                file.Write(buffer);
                file.Close();
                return true;

            }
            MemoryStream ms = new MemoryStream(buffer);

            Image iSource = Image.FromStream(ms); ImageFormat tFormat = iSource.RawFormat;

            int dHeight = iSource.Height / 2;
            int dWidth = iSource.Width / 2;
            int sW = 0, sH = 0;
            //按比例缩放
            Size tem_size = new Size(iSource.Width, iSource.Height);
            if (tem_size.Width > dHeight || tem_size.Width > dWidth)
            {
                if ((tem_size.Width * dHeight) > (tem_size.Width * dWidth))
                {
                    sW = dWidth;
                    sH = (dWidth * tem_size.Height) / tem_size.Width;
                }
                else
                {
                    sH = dHeight;
                    sW = (tem_size.Width * dHeight) / tem_size.Height;
                }
            }
            else
            {
                sW = tem_size.Width;
                sH = tem_size.Height;
            }

            Bitmap ob = new Bitmap(dWidth, dHeight);
            Graphics g = Graphics.FromImage(ob);

            g.Clear(Color.WhiteSmoke);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(iSource, new Rectangle((dWidth - sW) / 2, (dHeight - sH) / 2, sW, sH), 0, 0, iSource.Width, iSource.Height, GraphicsUnit.Pixel);

            g.Dispose();

            //以下代码为保存图片时，设置压缩质量
            EncoderParameters ep = new EncoderParameters();
            long[] qy = new long[1];
            qy[0] = flag;//设置压缩的比例1-100
            EncoderParameter eParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, qy);
            ep.Param[0] = eParam;

            try
            {
                ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                ImageCodecInfo jpegICIinfo = null;
                for (int x = 0; x < arrayICI.Length; x++)
                {
                    if (arrayICI[x].FormatDescription.Equals("JPEG"))
                    {
                        jpegICIinfo = arrayICI[x];
                        break;
                    }
                }
                if (jpegICIinfo != null)
                {
                    ob.Save(dFile, jpegICIinfo, ep);//dFile是压缩后的新路径
                                                    //   FileInfo fi = new FileInfo(dFile);
                                                    //if (fi.Length > 1024 * size)
                                                    //{
                                                    //    flag = flag - 10;
                                                    //    CompressImage(sFile, dFile, flag, size, false);
                                                    //}
                }
                else
                {
                    ob.Save(dFile, tFormat);
                }
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                iSource.Dispose();
                ob.Dispose();
            }
        }

    }

    public enum FileExtension
    {
        JPG = 255216,
        GIF = 7173,
        BMP = 6677,
        PNG = 13780,
        //RAR = 8297,
        //   jpg = 255216,
        //exe = 7790,
        //xml = 6063,
        //html = 6033,
        //aspx = 239187,
        //cs = 117115,
        //js = 119105,
        //txt = 210187,
        //sql = 255254
    }

}