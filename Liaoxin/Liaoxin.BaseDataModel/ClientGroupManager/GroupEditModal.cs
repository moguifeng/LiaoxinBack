using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace Liaoxin.BaseDataModel.ClientManger
{
    public class GroupEditModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public GroupEditModal()
        {
        }

        public GroupEditModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "编辑群组";


        [HiddenTextField]

        public Guid GroupId { get; set; }

        [TextField("群号", IsReadOnly =true)]

        public string UniqueId { get; set; }

        [TextField("群名称", Placeholder = "空代表不修改")]
        public string Name { get; set; }


        [TextField("绑定环信号", Placeholder = "空代表不修改")]
        public string HuanxinGroupId { get; set; }

        [DropListField("全体禁言")]
        public bool AllBlock { get; set; }


        [DropListField("确认群聊邀请")]
        public bool SureConfirmInvite { get; set; }        

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("SaveGroup", "保存"), };
        }

        public ServiceResult SaveGroup()
        {
            var entity = Context.Groups.Where(c => c.GroupId == this.GroupId).FirstOrDefault();
            if (!string.IsNullOrEmpty(this.Name))
            {
                entity.Name = this.Name;
            }
            
            if (!string.IsNullOrEmpty(this.HuanxinGroupId))
            {
                entity.HuanxinGroupId = this.HuanxinGroupId;
            }
            entity.AllBlock = this.AllBlock;
            entity.SureConfirmInvite = this.SureConfirmInvite;
            entity.UpdateTime = DateTime.Now;
            Context.Groups.Update(entity);
            UserOperateLogService.Log($"编辑[{entity.HuanxinGroupId}]群组", Context);
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}