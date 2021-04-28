using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Zzb.Common
{
    public class SecurityHelper
    {
        public static string Encrypt(string p)
        {
            var md5 = MD5Encrypt(p + "LiaoxinApiApp");
            return md5;
        }

        public static string MD5Encrypt(string myString)
        {
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(myString));
                return BitConverter.ToString(data).Replace("-", string.Empty).ToLower(CultureInfo.CurrentCulture);
            }

        }
    }
}