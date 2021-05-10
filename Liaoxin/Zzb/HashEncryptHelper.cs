using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Zzb
{
    public sealed class HashEncryptHelper
    {
        /// <summary>
        /// 哈希加密，采用256位加密
        /// </summary>
        /// <param name="source">待加密字符串</param>
        /// <returns>加密后字符串</returns>
        public static string Encrypt(string source)
        {
            byte[] passwordAndSaltBytes = System.Text.Encoding.UTF8.GetBytes(source);

            byte[] hashBytes = new SHA256Managed().ComputeHash(passwordAndSaltBytes);

            return string.Join(string.Empty, hashBytes.Select(i => i.ToString("x2")));
        }

        public static string MD5Encrypt(string source)
        {
            source += "awfjiwej5AE";
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(source);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return string.Join(string.Empty, hashBytes.Select(i => i.ToString("x2")));
            }
        }
    }
}
