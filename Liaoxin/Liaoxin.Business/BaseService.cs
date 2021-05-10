using Liaoxin.IBusiness;
using Liaoxin.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Zzb.Context;

namespace Liaoxin.Business
{
    public class BaseService
    {
        public LiaoxinContext Context { get; set; }

        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public IHostingEnvironment HostingEnvironment { get; set; }

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
    }
}