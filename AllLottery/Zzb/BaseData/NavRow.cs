using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Reflection;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model;
using Zzb.BaseData.StringHandle;
using Zzb.Common;

namespace Zzb.BaseData
{
    public class NavRow
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        /// <summary>
        /// 获取表格数据
        /// </summary>
        /// <param name="navId">主键</param>
        /// <param name="limit">获取多少条数据</param>
        /// <param name="offset">跳过多少条数据</param>
        public NavRowDatasModel GetRowsData(string navId, int limit, int offset, Dictionary<string, string> query)
        {
            var nav = BaseData.GetNavModel(navId);
            if (nav == null)
            {
                throw new ZzbException("navId不正确，无法找到对应菜单");
            }

            if (!(HttpContextAccessor.HttpContext.RequestServices.GetService(nav.Type) is BaseNav baseNav))
            {
                throw new ZzbException("BaseData反射失败");
            }

            NavRowDatasModel model = new NavRowDatasModel();

            var datas = baseNav.GetNavDatas(limit, offset, query, out var total);

            model.Total = total;

            //获取数据
            if (datas != null && datas.Any())
            {
                model.Rows = new Dictionary<string, object>[datas.Length];
                for (int i = 0; i < datas.Length; i++)
                {
                    model.Rows[i] = new Dictionary<string, object>();
                    //添加字段数据
                    foreach (PropertyInfo propertyInfo in nav.Type.GetProperties())
                    {
                        var field = propertyInfo.GetCustomAttribute<NavFieldAttribute>();
                        if (field != null)
                        {
                            var value = BaseStringHandle.Handle(propertyInfo.PropertyType, propertyInfo.GetValue(datas[i]));
                            if (field.IsKey)
                            {
                                model.Rows[i].Add("key", value);
                            }
                            model.Rows[i].Add(propertyInfo.Name, value);
                            if (propertyInfo.PropertyType == typeof(bool))
                            {
                                model.Rows[i].Add(propertyInfo.Name + "_Value", value == "1" ? "是" : "否");
                            }
                            if (propertyInfo.PropertyType.BaseType == typeof(Enum))
                            {
                                model.Rows[i].Add(propertyInfo.Name + "_Value", ((Enum)Enum.Parse(propertyInfo.PropertyType, value.ToString())).ToDescriptionString());
                            }
                        }
                    }

                    if (datas[i] is BaseNav navOne)
                    {
                        var buttons = navOne.CreateRowButtons();
                        if (buttons != null && buttons.Length > 0)
                        {
                            model.Rows[i].Add("Zzb_Buttons", buttons);
                        }
                    }
                    else
                    {
                        throw new ZzbException(nav.Type + "类型的GetNavDatas返回的不是本类型数组！");
                    }


                }
            }

            return model;
        }
    }

    public class NavRowDatasModel
    {
        public int Total { get; set; }

        public Dictionary<string, object>[] Rows { get; set; }
    }
}