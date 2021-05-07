using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Context;
using Zzb.Mvc;

namespace Liaoxin.Controllers
{
    public class LiaoxinBaseController : BaseApiController
    {
        public LiaoxinContext Context { get; set; }


        protected Guid CurrentClientId
        {
            get
            {
                if (UserContext.Current.IsAuthenticated)
                {
                    return UserContext.Current.Id;
                }

                return Guid.Empty;
            }
        }


        protected string CurrentHuanxinId
        {
            get
            {
                if (UserContext.Current.IsAuthenticated)
                {
                    return UserContext.Current.Name;
                }

                return "";
            }
        }

        public int Update<T>(T t, string keyName, IList<string> updateFieldList) where T : class
        {
            Context.Attach(t);
            var userm = Context.Entry<T>(t);
            userm.State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            foreach (var updateField in updateFieldList)
            {
                //不是主键才去修改，如果是主键就不需要修改了
                if (!string.Equals(updateField, keyName, StringComparison.OrdinalIgnoreCase))
                {
                    //表示该字段需要更新
                    userm.Property(updateField).IsModified = true;
                }
            }
            return Context.SaveChanges();
        }


        protected IList<string> GetPostBodyFiledKey( List<string> ingoreFields = null, IList<string> defaultUpdateFieldList = null, string objKey = "")
        {
            List<string> list = new List<string>();
            list.Add("UpdateTime");
            if (defaultUpdateFieldList != null)
            {
                list.AddRange(defaultUpdateFieldList);
            }
            Stream postData = Request.Body;
            string postContent = "";
            using (StreamReader sr = new StreamReader(postData))
            {
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
                postContent = sr.ReadToEndAsync().Result;
                sr.BaseStream.Seek(0, SeekOrigin.Begin);
            }
            if (string.IsNullOrEmpty(postContent))
            {
                return null;
            }
            else
            {

                Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(postContent);
                if (!string.IsNullOrEmpty(objKey))
                {
                    jo = jo[objKey].ToObject<JObject>();
                }
                if (jo.HasValues)
                {
                    foreach (JProperty item in jo.Properties())
                    {
                        if (list.FirstOrDefault(p => string.Equals(p, item.Name, StringComparison.OrdinalIgnoreCase)) == null)
                        {

                            list.Add(item.Name.ToLower()) ;
                        }
                    }
                }
                if (ingoreFields != null)
                {                    
                    ingoreFields.ForEach(i => {

                        if (list.Where(l => l== i.ToLower()).Count() > 0)
                        {
                            list.Remove(i.ToLower());
                        }
                      
                    });
                }
                return list;
            }
        }

    }


}