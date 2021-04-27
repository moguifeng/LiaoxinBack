using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Security.Claims;
using System.Text;
using Zzb.BaseData;
using Zzb.BaseData.Model;
using Zzb.Common;
using Zzb.Context;
using System.Text.Json;
using Microsoft.AspNetCore.Cors;

namespace Zzb.Mvc
{
    public abstract class ZzbHomeController : BaseApiController
    {
        public NavRow NavRow { get; set; }

        public TableInfomation TableInfomation { get; set; }


        public IConfiguration Config { get; set; }


        public NavTrees NavTrees { get; set; }

        [HttpGet("GetMenu")]
        public ServiceResult GetMenu()
        {
            return Json(() =>
            {
                var list = NavTrees.GetNavTrees(UserId);
                List<RouterMenuModel> routers = new List<RouterMenuModel>(){
                    new RouterMenuModel() {Path = "/", Redirect = "/list/table-list"},
                };
                foreach (NavTree tree in list)
                {
                    if (tree.Children == null)
                    {
                        routers.Add(new RouterMenuModel() { Path = $"/ZzbTable/list/{tree.NavId}", Name = tree.Name });
                    }
                    else
                    {
                        List<RouterMenuModel> children = new List<RouterMenuModel>();
                        foreach (NavTree treeChild in tree.Children)
                        {
                            children.Add(new RouterMenuModel() { Path = $"/ZzbTable/{SecurityHelper.MD5Encrypt(tree.Name)}/{treeChild.NavId}", Name = treeChild.Name });
                        }
                        routers.Add(new RouterMenuModel() { Path = $"/ZzbTable/{SecurityHelper.MD5Encrypt(tree.Name)}", Name = tree.Name, Routes = children.ToArray() });
                    }
                }

                var menus = GetCustomMenu();
                if (menus != null)
                {
                    foreach (RouterMenuModel tree in menus)
                    {
                        if (tree.Routes == null || tree.Routes.Length == 0)
                        {
                            routers.Add(tree);
                        }
                        else
                        {
                            var exist = (from r in routers where r.Name == tree.Name select r).FirstOrDefault();
                            if (exist == null)
                            {
                                routers.Add(tree);
                            }
                            else
                            {
                                var temp = new List<RouterMenuModel>();
                                temp.AddRange(exist.Routes);
                                temp.AddRange(tree.Routes);
                                exist.Routes = temp.ToArray();
                            }
                        }
                    }

                }
                routers.Add(new RouterMenuModel() { Component = "404" });
                return new ServiceResult<object>(ServiceResultCode.Success, "OK", new RouterMenuModel()
                {
                    Path = "/",
                    Component = "../layouts/BasicLayout",
                    //Routes = new[] { "src/pages/Authorized" },
                    Authority = new[] { "admin", "user" },
                    Routes = routers.ToArray()
                });
            }, "获取菜单失败");
        }

        public abstract ServiceResult GetUserInfo();


        private List<RouterMenuModel> GetCustomMenu()
        {
            var pList = new List<RouterMenuModel>();
            var list = CreateMenu();
            if (list != null)
            {
                foreach (RouterMenuModel model in list)
                {
                    if (model.Routes != null && model.Routes.Length > 0)
                    {
                        var routes = model.Routes;
                        model.Routes = null;
                        List<RouterMenuModel> temp = new List<RouterMenuModel>();
                        foreach (RouterMenuModel subRouter in routes)
                        {
                            if (NavTrees.IsExistPermission(UserId, subRouter.Path))
                            {
                                temp.Add(subRouter);
                            }
                        }
                        if (temp.Any())
                        {
                            model.Routes = temp.ToArray();
                            pList.Add(model);
                        }
                    }
                    else
                    {
                        if (NavTrees.IsExistPermission(UserId, model.Path))
                        {
                            pList.Add(model);
                        }
                    }
                }

                return pList;
            }
            return null;
        }

        public virtual List<RouterMenuModel> CreateMenu()
        {
            return null;
        }

        [HttpPost("SignOut")]
        public ServiceResult SignOut([FromBody] ZzbHomeGetTableInfomationViewModel model)
        {
            return Json(() =>
            {
                SignOut();
                return ObjectResult(true);
            }, "退出成功");
        }



        [HttpPost("GetTableInfomation")]
        public ServiceResult GetTableInfomation([FromBody] ZzbHomeGetTableInfomationViewModel model)
        {
            return Json(() =>
            {
                if (!NavTrees.IsExistPermission(UserId, model.NavId))
                {
                    HttpContext.Response.StatusCode = 403;
                    return null;
                }
                return ObjectResult(TableInfomation.GetTableInfomation(model.NavId));
            }, "获取表单信息失败");
        }

        [HttpPost("GetRowsData")]
        public ServiceResult GetRowsData(ZzbHomeGetRowsDataViewModel model)
        {
            return Json(() =>
            {
                var res = NavRow.GetRowsData(model.NavId, model.Size, (model.Index - 1) * model.Size, model.Query);
                var jsonRes = ObjectResult(res);
                return jsonRes;
            }, "获取表格数据失败");
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public ServiceResult Login(ZzbHomeLoginViewModel model)
        {
            return Json(() =>
            {
                var id = Login(model.Name, model.Password);

                _UserContext.SetUserContext(Guid.NewGuid(), id.ToString(), model.Name);
                var token = UserContext.Current.Token;
                return ObjectResult(token);
            }, "登录失败");
        }


        [HttpPost("GetModalInfo")]
        public ServiceResult GetModalInfo(ZzbHomeGetViewsModalInfoViewModel model)
        {
            return Json(() => ObjectResult(TableInfomation.GetButtonModalInfo(model.ModalId, model.Data)),
                "获取按钮信息失败");
        }

     
        [HttpPost("HandleAction")]
        public ServiceResult HandleAction(ZzbHomeHandleViewActionViewModel model)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var k in model.Data.Keys)
            {
                if (model.Data[k] != null)
                {
                    JsonElement jsonEle = (JsonElement)model.Data[k];

                    if (jsonEle.ValueKind == JsonValueKind.Array)
                    {
                        var list = jsonEle.EnumerateArray().ToList();
                        var str = "";
                        foreach (var item in list)
                        {
                            str += item.ToString() + ",";
                        }
                        if (str.Length > 0)
                        {
                            str = str.Substring(0, str.Length - 1);
                        }

                        dic.Add(k, str);
                    }
                    else
                    {
                        dic.Add(k, model.Data[k]?.ToString());
                    }
                }
                else
                {
                    dic.Add(k, model.Data[k]?.ToString());
                }


            }
            return Json(
                () => TableInfomation.HandleModalAction(model.ModalId, model.ButtonId,
                    dic), "执行action方法失败");
        }

        [HttpPost("HandleRowAction")]
        public ServiceResult HandleRowAction(ZzbHomeHandleRowActionViewModel model)
        {
            return Json(() => TableInfomation.HandleNavAction(model.NavId, model.ButtonId, model.Data), "执行Row按钮失败");
        }

        protected abstract int Login(string name, string password);

        [HttpPost("ChangePassowrd")]
        public ServiceResult ChangePassword(ZzbHomeChangePassowrd model)
        {
            return Json(() =>
            {
                ChangePassword(model.Password, model.NewPassword);
                SignOut();
                return new ServiceResult(ServiceResultCode.Success);
            }, "修改密码失败");
        }

        protected abstract void ChangePassword(string password, string newPassword);
    }

    public class ZzbHomeChangePassowrd
    {
        public string Password { get; set; }

        public string NewPassword { get; set; }
    }

    public class ZzbHomeHandleRowActionViewModel
    {
        public string NavId { get; set; }

        public Dictionary<string, string> Data { get; set; }

        public string ButtonId { get; set; }
    }

    public class RouterMenuModel
    {
        public string Path { get; set; }

        public string Component { get; set; }

        public RouterMenuModel[] Routes { get; set; }

        public string Icon { get; set; }

        public string Redirect { get; set; }

        public string[] Authority { get; set; }

        public string Name { get; set; }
    }

    public class ZzbHomeGetViewsModalInfoViewModel
    {
        public string ModalId { get; set; }

        public Dictionary<string, string> Data { get; set; }
    }

    public class ZzbHomeGetTableInfomationViewModel
    {
        public string NavId { get; set; }
    }

    public class ZzbHomeGetRowsDataViewModel : ZzbHomeGetTableInfomationViewModel
    {
        public int Size { get; set; }

        public int Index { get; set; }

        public Dictionary<string, string> Query { get; set; }
    }

    public class ZzbHomeLoginViewModel
    {
        public string Name { get; set; }

        public string Password { get; set; }
    }

    public class ZzbHomeHandleViewActionViewModel 
    {
        public string ButtonId { get; set; }

        public  Dictionary<string, object> Data { get; set; }

        public string ModalId { get; set; }
    }
}