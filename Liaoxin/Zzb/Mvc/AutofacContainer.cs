using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Reflection;
using Zzb.BaseData;
using Zzb.BaseData.Model;

namespace Zzb.Mvc
{
    public static class AutofacContainer
    {
        public static IServiceProvider ZzbAutofacInit(this IServiceCollection services, string instanceName, string interfaceName, Type[] types)
        {
            //替换整个ioc
            services.Replace(ServiceDescriptor.Transient<IControllerActivator, ServiceBasedControllerActivator>());
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            //添加控制器
            var assembly = Assembly.GetEntryAssembly();
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsSubclassOf(typeof(ControllerBase)))
                {
                    containerBuilder.RegisterType(type).InstancePerDependency().PropertiesAutowired();
                }
            }
            //添加IOC反射
            var interfaceAssembly = Assembly.Load(interfaceName);
            var instanceAssembly = Assembly.Load(instanceName);
            foreach (Type interType in interfaceAssembly.GetTypes())
            {
                foreach (Type insType in instanceAssembly.GetTypes())
                {
                    if (insType.GetInterface(interType.FullName) != null)
                    {
                        containerBuilder.RegisterType(insType).As(interType).InstancePerDependency().PropertiesAutowired();
                        break;
                    }
                }
            }

            //添加BaseData模板
            foreach (NavModel navModel in BaseData.BaseData.GetAllNavs())
            {
                containerBuilder.RegisterType(navModel.Type).AsSelf().InstancePerDependency().PropertiesAutowired();
            }
            containerBuilder.RegisterType<NavRow>().AsSelf().InstancePerDependency().PropertiesAutowired();
            containerBuilder.RegisterType<TableInfomation>().AsSelf().InstancePerDependency().PropertiesAutowired();
            containerBuilder.RegisterType<NavTrees>().AsSelf().InstancePerDependency().PropertiesAutowired();
            containerBuilder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().PropertiesAutowired();
            if (types != null)
            {
                foreach (Type type in types)
                {
                    containerBuilder.RegisterType(type).AsSelf().InstancePerDependency().PropertiesAutowired();
                }
            }
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }
    }
}