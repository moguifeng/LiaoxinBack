using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace Zzb.BaseData.Model
{
    public abstract class BaseNav
    {
        #region 菜单属性

        /// <summary>
        /// 菜单名
        /// </summary>
        public abstract string NavName { get; }

        /// <summary>
        /// 文件夹名
        /// </summary>
        public virtual string FolderName => null;

        /// <summary>
        /// 是否显示
        /// </summary>
        public virtual bool NavIsShow => true;


        /// <summary>
        /// 排序
        /// </summary>
        public virtual int Sort => 100;
        /// <summary>
        /// 操作列宽
        /// </summary>
        public virtual int OperaColumnWidth => 100;

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        #endregion

        #region 查询参数

        protected int Limit { get; set; }

        protected int Offset { get; set; }

        protected Dictionary<string, string> Query { get; set; }

        public int Total { get; set; } = -1;

        public virtual object[] GetNavDatas(int limit, int offset, Dictionary<string, string> query, out int total)
        {
            Limit = limit;
            Offset = offset;
            Query = query;
            object[] os = DoGetNavDatas();
            if (Total == -1)
            {
                total = 0;
            }
            else
            {
                total = Total;
            }
            return os;
        }

        protected virtual object[] DoGetNavDatas()
        {
            return null;
        }

        protected TF[] CreateEfDatas<T, TF>(IQueryable<T> queryable,
            params Func<string, IQueryable<T>, IQueryable<T>>[] pf) where T : class where TF : BaseNav
        {
            return CreateEfDatas<T, TF>(queryable, null, pf);
        }

        protected TF[] CreateEfDatas<T, TF>(IQueryable<T> queryable, Action<T, TF> eachChange,
            params Func<string, IQueryable<T>, IQueryable<T>>[] pf) where T : class where TF : BaseNav
        {
            T[] ts = CreateEfDatas(queryable, null, pf);
            List<TF> f = new List<TF>();
            foreach (T t in ts)
            {
                var o = HttpContextAccessor.HttpContext.RequestServices.GetService(typeof(TF)) as TF;
                foreach (PropertyInfo property in t.GetType().GetProperties())
                {
                    foreach (PropertyInfo fProperty in typeof(TF).GetProperties())
                    {
                        if (property.Name == fProperty.Name)
                        {
                            fProperty.SetValue(o, property.GetValue(t));
                        }
                    }
                }
                eachChange?.Invoke(t, o);
                o.HttpContextAccessor = HttpContextAccessor;
                f.Add(o);
            }
            return f.ToArray();
        }

        protected T[] CreateEfDatas<T>(IQueryable<T> queryable, System.Func<IQueryable<T>, IOrderedQueryable<T>> sort, params Func<string, IQueryable<T>, IQueryable<T>>[] pf) where T : class
        {
            var bf = GetQueryConditionses();
            if (bf != null && bf.Length > 0 && Query != null)
            {
                for (int i = 0; i < bf.Length; i++)
                {
                    if (Query.ContainsKey(bf[i].Id) && !string.IsNullOrEmpty(Query[bf[i].Id]))
                    {
                        if (pf != null && pf.Length > i)
                        {
                            queryable = pf[i](Query[bf[i].Id], queryable);
                        }
                    }
                }
            }
            Total = queryable.Count();
            if (sort != null)
            {
                //var t = sort(queryable).Skip(Offset).Take(Limit).ToString();
                return sort(queryable).Skip(Offset).Take(Limit).ToArray();
            }
            return queryable.Skip(Offset).Take(Limit).ToArray();
        }

        public virtual BaseFieldAttribute[] GetQueryConditionses()
        {
            return null;
        }

        protected IQueryable<T> ConvertEnum<T, TF>(IQueryable<T> w, string key, Func<TF, IQueryable<T>> predicate) where T : class where TF : struct
        {
            if (System.Enum.TryParse(key, out TF type))
            {
                return predicate(type);
            }
            return w;
        }

        protected IQueryable<T> ConvertDateTime<T>(IQueryable<T> w, string key, Func<DateTime, IQueryable<T>> predicate)
            where T : class
        {
            if (DateTime.TryParse(key, out var data))
            {
                return predicate(data);
            }
            return w;
        }

        #endregion

        #region 页面按钮

        public virtual BaseButton[] CreateViewButtons()
        {
            return null;
        }

        public virtual BaseButton[] CreateRowButtons()
        {
            return null;
        }

        public virtual bool ShowOperaColumn => CreateRowButtons() != null && CreateRowButtons().Length > 0;

        #endregion
    }
}