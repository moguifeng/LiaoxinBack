using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Zzb.Common
{
    public class AESEncryptHelper
    {
        private const string DefaultKey = "0594h3yq6wj2p9kfyovfp5v3c53by9xe";
        private const string DefaultVector = "2b6m7ykk6xr0979h";

        public static string Encrypt(string data, string key = null, string vector = null)
        {
            if (string.IsNullOrEmpty(key)) key = DefaultKey;
            var keyBytes = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(keyBytes.Length)), keyBytes, keyBytes.Length);
            if (string.IsNullOrEmpty(vector)) vector = DefaultVector;
            var vectorBytes = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(vector.PadRight(vectorBytes.Length)), vectorBytes, vectorBytes.Length);
            RijndaelManaged rijndael = new RijndaelManaged {Key = keyBytes, IV = vectorBytes};
            var encryptor = rijndael.CreateEncryptor();
            var inputBytes = Encoding.UTF8.GetBytes(data);
            var outputBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            return Convert.ToBase64String(outputBytes);
        }

        public static string Decrypt(string data, string key = null, string vector = null)
        {
            if (string.IsNullOrEmpty(key)) key = DefaultKey;
            var keyBytes = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(keyBytes.Length)), keyBytes, keyBytes.Length);
            if (string.IsNullOrEmpty(vector)) vector = DefaultVector;
            var vectorBytes = new byte[16];
            Array.Copy(Encoding.UTF8.GetBytes(vector.PadRight(vectorBytes.Length)), vectorBytes, vectorBytes.Length);
            RijndaelManaged rijndael = new RijndaelManaged{Key = keyBytes, IV = vectorBytes};
            var decryptor = rijndael.CreateDecryptor();
            var inputBytes = Convert.FromBase64String(data);
            var outputBytes = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            return Encoding.UTF8.GetString(outputBytes);
        }
    }
}
