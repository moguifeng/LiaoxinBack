using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Model;
using Zzb.EF;

namespace Zzb.Mvc
{
    public class NavTrees
    {
        public List<NavTree> GetNavTrees(int userId)
        {
            List<NavTree> list = new List<NavTree>();
            //添加基础数据菜单
            foreach (NavModel nav in BaseData.BaseData.GetAllNavs())
            {
                if (!nav.Type.IsAbstract && IsExistPermission(userId, nav.NavId))
                {
                    if (nav.Type.Assembly.CreateInstance(nav.Type.FullName) is BaseNav baseNav)
                    {
                        if (baseNav.NavIsShow)
                        {
                            if (baseNav.FolderName == null)
                            {
                                list.Add(new NavTree() { Name = baseNav.NavName, NavId = nav.NavId });
                            }
                            else
                            {
                                var exist = (from n in list where n.Name == baseNav.FolderName select n).FirstOrDefault();
                                if (exist != null)
                                {
                                    exist.Children.Add(new NavTree() { Name = baseNav.NavName, NavId = nav.NavId });
                                }
                                else
                                {
                                    list.Add(new NavTree() { Name = baseNav.FolderName, Children = new List<NavTree>() { new NavTree() { Name = baseNav.NavName, NavId = nav.NavId } } });
                                }
                            }
                        }
                    }
                }
            }
            return list;
        }

        public bool IsExistPermission(int userId, string navId)
        {
            using (var context = new ZzbDbContext())
            {
                if ((from u in context.UserInfoPermissions where u.UserInfoId == userId && u.Permission.NavId == navId select u).Any())
                {
                    return true;
                }

                if ((from u in context.UserInfoRoles
                     from p in u.Role.RolePermissions
                     where u.UserInfoId == userId && p.Permission.NavId == navId
                     select u).Any())
                {
                    return true;
                }

                return false;
            }
        }
    }
}