using Castle.Components.DictionaryAdapter;
using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class GroupViewModel : BaseServiceNav
    {
        public override string NavName => "群管理";

        public override string FolderName => "群组管理";

        public override int Sort => 2;


        [NavField("群编号", IsKey = true,IsDisplay =false)]
        public Guid GroupId { get; set; }



        [NavField("群编号", 200)]
        public string UniqueId { get; set; }



        [NavField("群名称")]
        public string Name { get; set; }


        [NavField("环信群编号")]
        public string HuanxinGroupId { get; set; }

        [NavField("群主", 200)]
        public string Master { get; set; }

        [NavField("群管理员")]
        public string Managers { get; set; }

        [NavField("全员禁言")]
        public bool AllBlock { get; set; }

        [NavField("确认群聊邀请")]
        public bool SureConfirmInvite { get; set; }

        [NavField("创建时间", 170)]
        public DateTime CreateTime { get; set; }

        [NavField("群公告", 450)]
        public string Notice { get; set; }

   

        protected override object[] DoGetNavDatas()
        {
            List<GroupViewModel> lis = new List<GroupViewModel>();
            var sources = CreateEfDatasedHandle(from c in Context.Groups
                                                where c.IsEnable
                                                orderby c.CreateTime descending
                                                select c,
                                                      (k, w) => w.Where(t => t.UnqiueId==(k)),
                     (k, w) => w.Where(t => t.Name.Contains(k)),
                      (k, w) => w.Where(t => t.Client.LiaoxinNumber.Contains(k)));
                   



            foreach (var item in sources)
            {
                GroupViewModel model = new GroupViewModel();
                model.GroupId = item.GroupId;
                model.UniqueId = item.UnqiueId;
                model.Name = item.Name;
                model.HuanxinGroupId = item.HuanxinGroupId;
                model.Master = item.Client.LiaoxinNumber;
                model.AllBlock = item.AllBlock;
                model.SureConfirmInvite = item.SureConfirmInvite;
                model.CreateTime = item.CreateTime;
                model.Notice = item.Notice;
                string ms = string.Join(',',item.GroupMangers.Select(g => g.Client.LiaoxinNumber).ToArray());
                model.Managers = ms;
                lis.Add(model);
            }
            return lis.ToArray();
        }

        public override BaseButton[] CreateRowButtons()
        {
            List<BaseButton> list = new EditableList<BaseButton>();            
            list.Add(new GroupEditModal("Save", "编辑群组"));
            return list.ToArray();

        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[] {
                    new TextFieldAttribute("UniqueId", "群号"),
                new TextFieldAttribute("Name", "群名称"),
                new TextFieldAttribute("Master", "群主(聊信号)"),               
                 };
        }
 
    }
}
