using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zzb.Common;
using Zzb.EF;
using Zzb.Redis;
using Zzb.Token;
using Zzb.ZzbLog;

namespace Zzb.Mvc
{
    public static class Init
    {
        /// <summary>
        /// Mvc ConfigureServices
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        public static void ZzbMvcInit<T>(this IServiceCollection services) where T : DbContext
        {

            services.AddAuthentication( 
                TokenDefaults.AuthenticationScheme
            ).AddToken();
       

            services.AddDbContext<T>();
        }

        /// <summary>
        /// Mvc Configure
        /// </summary>
        /// <param name="app"></param>
        public static void ZzbMvcInit(this IApplicationBuilder app)
        {
            // app.UseAuthentication();
           
            app.UseTokenAuthentication();
        }

        public static void ZzbInitEf<T>(this IWebHost host, Action<T> init = null) where T : ZzbDbContext
        {
            try
            {
                using (var scope = host.Services.CreateScope())
                {
                    using (var db = scope.ServiceProvider.GetRequiredService<T>())
                    {
                        if (db.Database != null)
                        {
                            db.Database.Migrate();
                            if (!db.UserInfos.Any())
                            {
                                db.UserInfos.Add(new UserInfo() { Name = "admin", Password = SecurityHelper.Encrypt("admin"), UserInfoRoles = new List<UserInfoRole>() { new UserInfoRole() { Role = new Role() { Name = "管理员" } } } });
                                db.SaveChanges();
                            }

                            init?.Invoke(db);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogHelper.Error("初始化失败", e);
            }

        }


    }
}