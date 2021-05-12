
using Liaoxin.Business;
using Liaoxin.Business.Socket;
using Liaoxin.Business.ThirdPay;
using Liaoxin.Cache;

using Liaoxin.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Zzb.BaseData;
using Zzb.ICacheManger;
using Zzb.Mvc;
using Zzb.Redis;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Liaoxin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddCors(builder =>
            {
                builder.AddPolicy("AllowAllOrigin", b =>
                {
                    b.SetIsOriginAllowed(_ => true)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            #region 密钥Key

            var path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            services.AddDataProtection().PersistKeysToFileSystem(
                new DirectoryInfo(Path.Combine(path, @"temp-keys")));
            #endregion



            services.AddSwaggerGen(
                 c =>
                 {
                     c.SwaggerDoc("v1", new OpenApiInfo { Title = "LiaoXin api", Version = "v1", TermsOfService = null, });
                     c.AddSecurityDefinition("token", new OpenApiSecurityScheme
                     {
                         Name = "token",
                         In = ParameterLocation.Header,
                         Type = SecuritySchemeType.ApiKey,

                         Description = "Token Authentication"

                     });
                     // c.SchemaGeneratorOptions.SchemaIdSelector = type => type.FullName;
                     c.AddSecurityRequirement(new OpenApiSecurityRequirement
                        {{new OpenApiSecurityScheme {Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "token"}}, new string[] { }}});

                     var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                     DirectoryInfo d = new DirectoryInfo(basePath);
                     FileInfo[] files = d.GetFiles("*.xml");
                     var xmls = files.Select(a => Path.Combine(basePath, a.FullName)).ToList();
                     foreach (var item in xmls)
                     {
                         c.IncludeXmlComments(item);
                     }
                 });
            services.AddMvc(o => { o.Filters.Add<LogFilter>(); }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddMvc(options => { options.EnableEndpointRouting = false; });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddSession(o =>
            {
                o.IdleTimeout = TimeSpan.FromDays(1);
                o.Cookie.SameSite = SameSiteMode.None;
            });

            services.AddRedisCache(Configuration.GetSection("Redis"));


            services.ZzbMvcInit<LiaoxinContext>();

            services.ZzbBaseDataInit("Liaoxin.BaseDataModel", "Liaoxin");
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            List<Type> list = new List<Type>();
            foreach (Type type in Assembly.Load("Liaoxin.Business").GetTypes())
            {
                if (type.IsSubclassOf(typeof(BaseThirdPay)) && !type.IsAbstract)
                {
                    list.Add(type);
                }
            }


            services.AddTransient<AreaCacheManager>();
            services.AddTransient<EnumCacheManager>();

            services.AddTransient<RateOfClientCacheManager>();
            services.AddTransient<RateOfGroupCacheManager>();
            services.AddTransient<RateOfClientGroupCacheManager>();

            return services.ZzbAutofacInit("Liaoxin.Business", "Liaoxin.IBusiness", list.ToArray());



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use((context, next) => { context.Request.EnableBuffering(); return next(); });
            app.UseCors("AllowAllOrigin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //添加socket
            //app.UseWebSockets();
            //app.UseMiddleware<UserSocketMiddleware>();
            //app.UseMiddleware<PlayerSocketMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelp V1");
            });


            app.UseHttpsRedirection();

            app.ZzbMvcInit();
            app.ZzbBaseDataInit<LiaoxinContext>();


            using (var scope = app.ApplicationServices.CreateScope())
            {
                var areaCache = scope.ServiceProvider.GetService<AreaCacheManager>();
                areaCache.Load();
                var enumCache = scope.ServiceProvider.GetService<EnumCacheManager>();
                enumCache.Load();

                var rateOfClientCache = scope.ServiceProvider.GetService<RateOfClientCacheManager>();
                rateOfClientCache.Load();

                var rateOfGroupCache = scope.ServiceProvider.GetService<RateOfGroupCacheManager>();
                rateOfGroupCache.Load();


                var rateOfGroupClientCache = scope.ServiceProvider.GetService<RateOfClientGroupCacheManager>();
                rateOfGroupClientCache.Load();

 
            }
            app.UseSession();
            //   MessageService.Start();
            // app.UseAuthentication();
            //  app.UseAuthorization();
            app.UseMvc();
         
        }
    }
}
