using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Zzb.Common
{
    //public class IpHelper
    //{
    //    private IHttpContextAccessor HttpContextAccessor { get; set; }

    //    public IpHelper(IHttpContextAccessor accessor)
    //    {
    //        HttpContextAccessor = accessor;
    //    }

    //    /// <summary>
    //    /// 获取Ip
    //    /// </summary>
    //    public string Ip
    //    {
    //        get
    //        {
    //            var result = string.Empty;
    //            if (HttpContextAccessor != null)
    //                result = GetWebClientIp();
    //            if (string.IsNullOrWhiteSpace(result))
    //                result = GetLanIp();
    //            return result;
    //        }
    //    }

    //    private string GetWebRemoteIp()
    //    {
    //        return HttpContextAccessor.HttpContext.Request.Headers["X_FORWARDED_FOR"].FirstOrDefault() ?? HttpContextAccessor.HttpContext.Request.Headers["REMOTE_ADDR"];
    //    }

    //    private string GetWebClientIp()
    //    {
    //        var ip = GetWebRemoteIp();
    //        foreach (var hostAddress in Dns.GetHostAddresses(ip))
    //        {
    //            if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
    //                return hostAddress.ToString();
    //        }
    //        return string.Empty;
    //    }

    //    private string GetLanIp()
    //    {
    //        foreach (var hostAddress in Dns.GetHostAddresses(Dns.GetHostName()))
    //        {
    //            if (hostAddress.AddressFamily == AddressFamily.InterNetwork)
    //                return hostAddress.ToString();
    //        }
    //        return string.Empty;
    //    }
    //}

    public static class IpAddressHelper
    {
        static IpAddressHelper()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        #region Ip城市(获取Ip城市)
        /// <summary>
        /// 获取IP地址信息
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static string GetLocation(string ip)
        {
            string res = "";

            try
            {
                string url = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?query=" + ip + "&resource_id=6006&ie=utf8&oe=gbk&format=json";
                res = HttpGet(url, Encoding.GetEncoding("GBK"));
                var resjson = JsonHelper.Json<obj>(res);
                res = resjson.data[0].location;
            }
            catch
            {
                res = "";
            }
            return res;
        }
        /// <summary>
        /// 百度接口
        /// </summary>
        public class obj
        {
            public List<dataone> data { get; set; }
        }
        public class dataone
        {
            public string location { get; set; }
        }
        /// <summary>
        /// 聚合数据
        /// </summary>
        public class objex
        {
            public string resultcode { get; set; }
            public dataoneex result { get; set; }
            public string reason { get; set; }
            public string error_code { get; set; }
        }
        public class dataoneex
        {
            public string area { get; set; }
            public string location { get; set; }
        }
        #endregion

        public static string HttpGet(string url, Encoding encodeing, Hashtable headht = null)
        {
            HttpWebRequest request;

            request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;
            WebResponse response = null;
            string responseStr = null;
            if (headht != null)
            {
                foreach (DictionaryEntry item in headht)
                {
                    request.Headers.Add(item.Key.ToString(), item.Value.ToString());
                }
            }
            try
            {
                response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), encodeing);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return responseStr;
        }
    }
}