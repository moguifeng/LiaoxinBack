using AllLottery.Business;
using AllLottery.Business.Socket;
using AllLottery.Business.ThirdPay;
using AllLottery.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Zzb.BaseData;
using Zzb.Mvc;
using Zzb.Redis;

namespace AllLottery
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
                    b.WithOrigins("http://localhost:8000",
                        "http://localhost:3000",
                        "http://localhost:3001",
                        "http://daili.bit36.cn",
                        "http://api.bit36.cn",
                        "http://back.bit36.cn",
                        "http://wap.bit36.cn",
                        "http://localhost:12312",
                        "http://localhost:12311",
                        "http://localhost:12313",
                        "http://ffc1255.cn",
                        "http://wap.ffc1255.cn",
                        "http://pc.ffc1255.cn",
                        "http://api.ffc1255.cn",
                        "http://pc-shenjianttaqwebvc.ffc1255.cn",
                        "https://ffc1255.cn",
                        "https://wap.ffc1255.cn",
                        "https://pc.ffc1255.cn",
                        "https://api.ffc1255.cn",
                        "https://pc-shenjianttaqwebvc.ffc1255.cn",
                        "http://16812345678.com",
                        "http://wap.16812345678.com",
                        "http://pc.16812345678.com",
                        "http://1685697.com",
                        "http://wap.1685697.com",
                        "http://pc.1685697.com",
                        "http://168ui.com",
                        "http://wap.168ui.com",
                        "http://pc.168ui.com",
                        "http://5250001.com",
                        "http://wap.5250001.com",
                        "http://pc.5250001.com",
                        "http://api.5250001.com",
                        "http://60069988.com",
                        "http://wap.60069988.com",
                        "http://pc.60069988.com",
                        "http://api.60069988.com",
                        "http://ffc226.cn",
                        "http://wap.ffc226.cn",
                        "http://pc.ffc226.cn",
                        "http://bbs26.cn",
                        "http://wap.bbs26.cn",
                        "http://pc.bbs26.cn",
                        "http://ffc209.cn",
                        "http://wap.ffc209.cn",
                        "http://pc.ffc209.cn",
                        "http://920980.com",
                        "http://yafu.920980.com",
                        "http://yafuwap.920980.com",
                        "http://yafupc.920980.com",
                        "http://cdn.920980.com",
                        "http://001chaye.cn",
                        "http://wap.001chaye.cn",
                        "http://pc.001chaye.cn",
                        "http://api.001chaye.cn",
                        "http://pc-shenjianttaqwebvc.001chaye.cn",
                        "https://wap.001chaye.cn",
                        "https://pc.001chaye.cn",
                        "https://api.001chaye.cn",
                        "https://pc-shenjianttaqwebvc.001chaye.cn",
                        "http://yitianwap.001chaye.cn",
                        "http://yitianpc.001chaye.cn",
                        "http://yitianapi.001chaye.cn",
                        "http://yitian.001chaye.cn",

                        "http://yitianwap.920980.com",
                        "http://yitianpc.920980.com",
                        "http://yitianapi.920980.com",
                        "http://yitian.920980.com",

                        "http://yafuwap.85858188.com",
                        "http://yafuapi.85858188.com",
                        "http://pc-shenjianttaqwebvc.85858188.com",
                        "http://111jx.cn",
                        "http://wap.111jx.cn",
                        "http://pc.111jx.cn",

                        "http://wap.youguo365.cn",
                        "http://xtdweb.com",
                            "http://socketapi.xtdweb.com",
                            "http://pc.xtdweb.com",
                        "http://api.youguo365.cn",
                         "http://pc-shenjianffzaqtzv.youguo365.cn"
                        )
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
                     var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录
                     var xmlPath = Path.Combine(basePath, "AllLottery.xml");
                     c.IncludeXmlComments(xmlPath);

          
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


            services.ZzbMvcInit<LotteryContext>();

            services.ZzbBaseDataInit("AllLottery.BaseDataModel", "AllLottery");

            //services.Configure<MvcOptions>(options =>
            //{
            //    options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigin"));
            //});

            List<Type> list = new List<Type>();
            foreach (Type type in Assembly.Load("AllLottery.Business").GetTypes())
            {
                if (type.IsSubclassOf(typeof(BaseThirdPay)) && !type.IsAbstract)
                {
                    list.Add(type);
                }
            }

            return services.ZzbAutofacInit("AllLottery.Business", "AllLottery.IBusiness", list.ToArray());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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
            app.ZzbBaseDataInit<LotteryContext>();
            app.UseSession();
            //   MessageService.Start();
            // app.UseAuthentication();
            //  app.UseAuthorization();
            app.UseMvc();
         
        }
    }
}
