using Castle.Components.DictionaryAdapter;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class GroupMemberViewModel : BaseServiceNav
    {


        public IUserOperateLogService UserOperateLogService { get; set; }
        public override string NavName => "群成员管理";

        public override string FolderName => "群组管理";

        public override int Sort => 3;


        [NavField("主键", IsKey = true, IsDisplay = false)]
        public Guid GroupMemberId { get; set; }


        [NavField("群编号")]
        public string UniqueId { get; set; }

        [NavField("群名称")]
        public string Name { get; set; }

        [NavField("客户群昵称")]
        public string MyNickName { get; set; }

        [NavField("客户聊信号")]
        public string LiaoxinNumber { get; set; }

        [NavField("群禁言")]
        public bool IsBlock { get; set; }


        [NavField("加入群时间", width: 170)]
        public DateTime CreateTime { get; set; }


        protected override object[] DoGetNavDatas()
        {
            List<GroupMemberViewModel> lis = new List<GroupMemberViewModel>();

            var sources = CreateEfDatasedHandle(from c in Context.GroupClients
                                                where c.IsEnable
                                                orderby c.CreateTime descending
                                                select c,
                     (k, w) => w.Where(t =>

                           t.Group.UnqiueId.Contains(UniqueId)
                     ),

                      (k, w) => w.Where(t => t.Client.LiaoxinNumber.Contains(k))); ;




            foreach (var item in sources)
            {
                GroupMemberViewModel model = new GroupMemberViewModel();
                model.GroupMemberId = item.GroupClientId;
                model.UniqueId = item.Group.UnqiueId;
                model.Name = item.Group.Name;
                model.MyNickName = item.MyNickName;
                model.LiaoxinNumber = item.Client.LiaoxinNumber;
                model.IsBlock = item.IsBlock;
                model.CreateTime = item.CreateTime;

                lis.Add(model);
            }
            return lis.ToArray();
        }

        public override BaseButton[] CreateRowButtons()
        {
            List<BaseButton> list = new EditableList<BaseButton>();
            string title = this.IsBlock ? "解禁言" : "禁言";
            list.Add(new ConfirmActionButton("EnableOperation", title, "是否确认操作?"));

            return list.ToArray();

        }



        public ServiceResult EnableOperation()
        {
            var entity = Context.GroupClients.Where(c => c.GroupClientId == this.GroupMemberId).FirstOrDefault();
            if (entity == null)
            {
                return new ServiceResult(ServiceResultCode.QueryNull, "找不到群成员");
            }

            entity.IsBlock = !this.IsBlock;
            Context.GroupClients.Update(entity);
            string title = this.IsBlock ? "解禁言" : "禁言";
            UserOperateLogService.Log($"[群编号:{entity.Group.GroupId} {title}了+{entity.Client.LiaoxinNumber}]聊信客户", Context);
            int res = Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[] {
                     new TextFieldAttribute("UniqueId", "聊信群编号"),
                new TextFieldAttribute("LiaoxinNumber", "客户聊信号")};


        }

    }
}
