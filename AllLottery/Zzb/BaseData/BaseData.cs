using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zzb.BaseData.Model;
using Zzb.Common;
using Zzb.EF;
using Zzb.Mvc;

namespace Zzb.BaseData
{
    public static class BaseData
    {
        private static Dictionary<string, NavModel> _navs = new Dictionary<string, NavModel>();

        private static Dictionary<string, string> _customNavs = new Dictionary<string, string>();

        public static void ZzbBaseDataInit(this IServiceCollection services, params string[] assemblyName)
        {
            if (assemblyName == null)
            {
                return;
            }
            foreach (string name in assemblyName)
            {
                var assembly = Assembly.Load(name);
                foreach (Type type in assembly.GetTypes())
                {
                    if (!type.IsAbstract && (type.IsSubclassOf(typeof(BaseNav)) || type.IsSubclassOf(typeof(BaseModal))))
                    {
                        var navId = SecurityHelper.MD5Encrypt(type.FullName);
                        _navs.Add(navId, new NavModel(navId, type));
                    }

                    //添加自定义菜单
                    if (type.IsSubclassOf(typeof(ZzbHomeController)))
                    {
                        var home = type.Assembly.CreateInstance(type.FullName) as ZzbHomeController;
                        var routers = home?.CreateMenu();
                        if (routers != null)
                        {
                            foreach (RouterMenuModel router in routers)
                            {
                                if (router.Routes != null && router.Routes.Length > 0)
                                {
                                    foreach (RouterMenuModel subRouter in router.Routes)
                                    {
                                        _customNavs.Add(subRouter.Path, subRouter.Name);
                                    }
                                }
                                else
                                {
                                    _customNavs.Add(router.Path, router.Name);
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void ZzbBaseDataInit<T>(this IApplicationBuilder app) where T : ZzbDbContext
        {
            using (var scopet = app.ApplicationServices.CreateScope())
            {
                using (var db = scopet.ServiceProvider.GetRequiredService<T>())
                {
                    db.Permissions.ToList().ForEach(t => t.IsEnable = false);
                    db.SaveChanges();

                    foreach (NavModel value in _navs.Values)
                    {
                        var navsPermission = (from n in db.Permissions
                                              where n.NavId == value.NavId
                                              select n).FirstOrDefault();
                        if (navsPermission == null)
                        {
                            var one = new Permission();
                            one.NavId = value.NavId;
                            db.Permissions.Add(one);
                        }
                        else
                        {
                            navsPermission.IsEnable = true;
                            navsPermission.UpdateTime = DateTime.Now;
                        }

                    }

                    foreach (var nav in _customNavs)
                    {
                        var navsPermission = (from n in db.Permissions
                            where n.NavId == nav.Key
                            select n).FirstOrDefault();
                        if (navsPermission == null)
                        {
                            var one = new Permission();
                            one.NavId = nav.Key;
                            db.Permissions.Add(one);
                        }
                        else
                        {
                            navsPermission.IsEnable = true;
                            navsPermission.UpdateTime = DateTime.Now;
                        }
                    }

                    db.SaveChanges();

                    var admin = (from r in db.Roles where r.Name == "管理员" select r).FirstOrDefault();
                    if (admin != null)
                    {
                        admin.RolePermissions?.RemoveAll(t => true);
                        db.SaveChanges();
                        foreach (Permission permission in db.Permissions)
                        {
                            db.RolePermissions.Add(new RolePermission() { RoleId = admin.RoleId, PermissionId = permission.PermissionId });
                        }
                        db.SaveChanges();
                    }

                }
            }

        }

        public static List<NavModel> GetAllNavs()
        {
            return _navs.Values.ToList();
        }

        public static Dictionary<string, string> GetCustomNavs()
        {
            return _customNavs;
        }

        public static NavModel GetNavModel(string id)
        {
            if (!_navs.ContainsKey(id))
            {
                return null;
            }
            return _navs[id];
        }

        public static string GetNavName(string id)
        {
            var model = GetNavModel(id);
            if (model != null)
            {
                var ins = model.Type.Assembly.CreateInstance(model.Type.FullName);
                if (ins is BaseNav nav)
                {
                    return nav.NavName;
                }
                else if (ins is BaseModal modal)
                {
                    return modal.ModalName;
                }
            }

            return null;
        }
    }
}