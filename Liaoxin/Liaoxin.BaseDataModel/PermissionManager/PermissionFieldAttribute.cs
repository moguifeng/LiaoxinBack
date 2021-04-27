using Castle.Components.DictionaryAdapter;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model;
using Zzb.EF;

namespace Liaoxin.BaseDataModel.PermissionManager
{
    public class PermissionFieldAttribute : TreeServiceFieldAttribute
    {
        public override List<TreesModel> Source
        {
            get
            {
                List<TreesModel> list = new EditableList<TreesModel>();
                List<TreesModel> listNav = new List<TreesModel>();
                List<TreesModel> listModal = new List<TreesModel>();
                foreach (Permission permission in Context.Permissions.Where(p => p.IsEnable))
                {
                    var model = BaseData.GetNavModel(permission.NavId);
                    if (model != null)
                    {
                        var ins = model.Type.Assembly.CreateInstance(model.Type.FullName);
                        if (ins is BaseNav nav)
                        {
                            listNav.Add(new TreesModel() { Title = nav.NavName, Value = permission.NavId });
                        }
                        else if (ins is BaseModal modal)
                        {
                            listModal.Add(new TreesModel() { Title = modal.ModalName, Value = permission.NavId });
                        }
                    }
                }

                if (listNav.Any())
                {
                    list.Add(new TreesModel() { Title = "基础数据", TreesModels = listNav, Value = "JiChu" });
                }
                if (listModal.Any())
                {
                    list.Add(new TreesModel() { Title = "模态数据", TreesModels = listModal, Value = "Motai" });
                }
                if (BaseData.GetCustomNavs().Any())
                {
                    list.Add(new TreesModel() { Title = "定制页面", TreesModels = (from g in BaseData.GetCustomNavs() select new TreesModel() { Title = g.Value, Value = g.Key }).ToList() });
                }

                return list;
            }
        }
    }
}