using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model;
using Zzb.BaseData.Model.Button;
using Zzb.BaseData.StringHandle;

namespace Zzb.BaseData
{
    public class TableInfomation
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public TableInfomationModel GetTableInfomation(string navId)
        {
            var nav = BaseData.GetNavModel(navId);
            if (nav == null)
            {
                throw new ZzbException("navId不正确，无法找到对应菜单");
            }
            TableInfomationModel model = new TableInfomationModel();
            model.NavColumns = CreateNavFields(nav.Type);
            var ins = GetNav(navId);

            if (ins == null)
            {
                throw new ZzbException("反射失败，该类不是BaseNav");
            }

            model.TableName = ins.NavName;

            #region 获取查询字段

            var fields = ins.GetQueryConditionses();
            if (fields != null)
            {
                model.QueryFields = CreateFields(fields);
            }

            #endregion

            #region 获取页面按钮

            var buttons = ins.CreateViewButtons();
            if (buttons != null)
            {
                model.ViewButtons = from b in buttons select new { b.Id, Name = b.ButtonName, b.Type, b.Icon, (b as BaseModal)?.ModalId, (b as JsActionButton)?.ActionType };
            }

            model.ShowOperaColumn = ins.ShowOperaColumn;
            model.OperaColumnWidth = ins.OperaColumnWidth;
            #endregion

            return model;
        }

        private List<NavFieldsViewModels> CreateNavFields(Type type)
        {
            List<NavFieldsViewModels> list = new List<NavFieldsViewModels>();
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                var navField = propertyInfo.GetCustomAttribute<NavFieldAttribute>();
                if (navField != null)
                {
                    if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType.BaseType == typeof(Enum))
                    {
                        //添加列
                        list.Add(new NavFieldsViewModels()
                        {
                            Name = propertyInfo.Name,
                            Title = navField.Name ?? propertyInfo.Name,
                            IsHidden = true,
                            Width = navField.Width,
                            ActionType = navField.ActionType
                        });
                        //添加列
                        list.Add(new NavFieldsViewModels()
                        {
                            Name = propertyInfo.Name + "_Value",
                            Title = navField.Name ?? propertyInfo.Name,
                            IsHidden = !navField.IsDisplay,
                            Width = navField.Width,
                            ActionType = navField.ActionType
                        });
                    }
                    else
                    {
                        //添加列
                        list.Add(new NavFieldsViewModels()
                        {
                            Name = propertyInfo.Name,
                            Title = navField.Name ?? propertyInfo.Name,
                            IsHidden = !navField.IsDisplay,
                            Width = navField.Width,
                            ActionType = navField.ActionType
                        });
                    }

                }
            }
            return list;
        }

        private object CreateFields(BaseFieldAttribute[] fields)
        {
            return from f in fields select f.GetField();
        }

        public object GetButtonModalInfo(string modalId, Dictionary<string, string> data)
        {
            var button = GetModal(modalId);

            if (data != null)
            {
                foreach (PropertyInfo propertyInfo in button.GetType().GetProperties())
                {
                    if (data.ContainsKey(propertyInfo.Name))
                    {
                        propertyInfo.SetValue(button, BaseStringHandle.GetValue(propertyInfo.PropertyType, data[propertyInfo.Name]));
                    }
                }
            }

            button.Init();

            List<BaseFieldAttribute> list = new List<BaseFieldAttribute>();
            foreach (PropertyInfo propertyInfo in button.GetType().GetProperties())
            {
                var field = propertyInfo.GetCustomAttribute<BaseFieldAttribute>();
                if (field != null)
                {
                    field.PropertyType = propertyInfo.PropertyType;
                    field.HttpContextAccessor = HttpContextAccessor;
                    field.Id = Guid.NewGuid().ToString("N") + propertyInfo.Name;
                    field.Value = BaseStringHandle.Handle(propertyInfo.PropertyType, propertyInfo.GetValue(button));
                    list.Add(field);
                }
            }

            return new { Fields = CreateFields(list.ToArray()), Buttons = from b in button.Buttons() select new { b.Id, b.Type, b.Icon, Name = b.ButtonName }, Title = button.ModalName };
        }

        private BaseModal GetModal(string modalId)
        {
            var nav = BaseData.GetNavModel(modalId);
            if (nav == null)
            {
                throw new ZzbException("modalId不正确，无法找到对应菜单");
            }
            if (!(HttpContextAccessor.HttpContext.RequestServices.GetService(nav.Type) is BaseModal ins))
            {
                throw new ZzbException("modalId不正确或类没有无参构造函数,反射BaseModal失败");
            }

            return ins;
        }

        private BaseNav GetNav(string navId)
        {
            var nav = BaseData.GetNavModel(navId);
            if (nav == null)
            {
                throw new ZzbException("navId不正确，无法找到对应菜单");
            }

            if (!(HttpContextAccessor.HttpContext.RequestServices.GetService(nav.Type) is BaseNav ins))
            {
                throw new ZzbException("navId不正确或类没有无参构造函数,反射BaseNav失败");
            }

            return ins;
        }

        public ServiceResult HandleModalAction(string modalId, string buttonId, Dictionary<string, string> data)
        {
            var button = GetModal(modalId);

            if (button.Buttons().FirstOrDefault(t => t.Id == buttonId) == null)
            {
                throw new ZzbException("当前buttonId没有按钮，出现不可能错误");
            }

            var method = button.GetType().GetMethod(buttonId);

            if (method == null)
            {
                throw new ZzbException("没有找到反射方法");
            }

            var ins = HttpContextAccessor.HttpContext.RequestServices.GetService(button.GetType());//button.GetType().Assembly.CreateInstance(button.GetType().FullName);
            foreach (PropertyInfo propertyInfo in button.GetType().GetProperties())
            {
                if (data.ContainsKey(propertyInfo.Name))
                {
                    propertyInfo.SetValue(ins, BaseStringHandle.GetValue(propertyInfo.PropertyType, data[propertyInfo.Name]));
                }
            }

            var s = method.Invoke(ins, null);
            if (s is ServiceResult)
            {
                return s as ServiceResult;
            }
            else
            {
                return new ServiceResult(ServiceResultCode.Success);
            }
        }

        public ServiceResult HandleNavAction(string navId, string buttonId, Dictionary<string, string> data)
        {
            var nav = GetNav(navId);

            if (nav.CreateRowButtons().FirstOrDefault(t => t.Id == buttonId) == null)
            {
                throw new ZzbException("当前buttonId没有按钮，出现不可能错误");
            }

            var method = nav.GetType().GetMethod(buttonId);

            if (method == null)
            {
                throw new ZzbException("没有找到反射方法");
            }

            foreach (PropertyInfo propertyInfo in nav.GetType().GetProperties())
            {
                if (data.ContainsKey(propertyInfo.Name))
                {
                    propertyInfo.SetValue(nav, BaseStringHandle.GetValue(propertyInfo.PropertyType, data[propertyInfo.Name]));
                }
            }

            var s = method.Invoke(nav, null);
            if (s is ServiceResult)
            {
                return s as ServiceResult;
            }
            else
            {
                return new ServiceResult(ServiceResultCode.Success);
            }
        }
    }
}