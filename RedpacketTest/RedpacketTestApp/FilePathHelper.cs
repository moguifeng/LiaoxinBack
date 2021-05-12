using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Web;

namespace RedpacketTestApp
{
    public class FilePathHelper
    {
        /// <summary>
        /// 获取物理路径
        /// </summary>
        /// <param name="strPath"></param>
        /// <returns></returns>
        public static string GetPath(string strPath)
        {
            strPath = strPath.Replace("/", "\\");
            strPath = strPath.Replace("~", "");
            if (strPath.StartsWith("\\"))
            {
                strPath = strPath.TrimStart('\\');
            }
            return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);

        }
    }
}