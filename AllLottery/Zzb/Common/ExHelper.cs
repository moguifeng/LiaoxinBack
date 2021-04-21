using Castle.Components.DictionaryAdapter;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Zzb.BaseData.Attribute.Field;

namespace Zzb.Common
{
    public static class ExHelper
    {
        /// <summary>
        /// 返回常规日期格式
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToDateString(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 返回常规时间格式yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string ToCommonString(this DateTime time)
        {
            return time.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToDecimalString(this decimal d)
        {
            return d.ToString("0.00");
        }

        public static string ToPercenString(this decimal d)
        {
            return (d * 100).ToString("0.##") + "%";
        }

        /// <summary>
        /// 返回枚举说明
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToDescriptionString(this Enum obj)
        {
            string objName = obj.ToString();
            Type t = obj.GetType();
            FieldInfo fi = t.GetField(objName);
            return GetDescription(t, fi);
        }

        internal static string GetDescription(Type t, FieldInfo fi)
        {
            DescriptionAttribute[] arrDesc = fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if (arrDesc != null && arrDesc.Length > 0)
            {
                return arrDesc[0].Description;
            }
            return String.Empty;
        }

        public static string ToLogString(this int[] t)
        {
            if (t == null || t.Length == 0)
            {
                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            foreach (int i in t)
            {
                sb.Append(i + ",");
            }
            return sb.ToString().Trim(',');
        }

        public static List<DropListModel> GetDropListModels(this Enum obj, string empty = null)
        {
            List<DropListModel> list = new EditableList<DropListModel>();
            if (!string.IsNullOrEmpty(empty))
            {
                list.Add(new DropListModel(null, empty));
            }
            foreach (Enum e in Enum.GetValues(obj.GetType()))
            {
                list.Add(new DropListModel(e.ToString(), e.ToDescriptionString()));
            }

            return list;
        }

        public static string GetDefaultUrl(this HttpRequest request)
        {
            return request.Scheme + "://" + request.Host + "/";
        }
    }
}