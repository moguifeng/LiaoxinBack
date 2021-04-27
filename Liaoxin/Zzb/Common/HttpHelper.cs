using System.IO;
using System.Net;
using System.Text;

namespace Zzb.Common
{
    public class HttpHelper
    {
        /// <summary>
        /// 获取html页面
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetPage(string url)
        {
            return GetPage(url, null);
        }

        public static string GetPage(string url, string userAgent)
        {
            var response = GetWebResponse(url, userAgent);
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string ss = sr.ReadToEnd();
            sr.Close();
            response.Close();
            return ss;
        }

        private static HttpWebResponse GetWebResponse(string url, string userAgent)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;
            if (string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/45.0.2454.101 Safari/537.36";
            }
            else
            {
                request.UserAgent = userAgent;
            }
            return request.GetResponse() as HttpWebResponse;
        }

        /// <summary>
        /// post请求到指定地址并获取返回的信息内容
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求参数</param>
        /// <param name="encodeType">编码类型如：UTF-8</param>
        /// <returns>返回响应内容</returns>
        public static string HttpPost(string POSTURL, string PostData)
        {
            //发送请求的数据
            WebRequest myHttpWebRequest = WebRequest.Create(POSTURL);
            myHttpWebRequest.Method = "POST";
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] byte1 = encoding.GetBytes(PostData);
            myHttpWebRequest.ContentType = "application/x-www-form-urlencoded";
            myHttpWebRequest.ContentLength = byte1.Length;
            Stream newStream = myHttpWebRequest.GetRequestStream();
            newStream.Write(byte1, 0, byte1.Length);
            newStream.Close();

            //发送成功后接收返回的XML信息
            HttpWebResponse response = (HttpWebResponse)myHttpWebRequest.GetResponse();
            string lcHtml = string.Empty;
            Encoding enc = Encoding.GetEncoding("UTF-8");
            Stream stream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream, enc);
            lcHtml = streamReader.ReadToEnd();
            return lcHtml;
        }
    }
}