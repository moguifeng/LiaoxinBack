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
    public class RateOfGroupAddModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public RateOfGroupAddModal()
        {
        }

        public RateOfGroupAddModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "新增群组概率";


        [HiddenTextField]
        public Guid RateOfGroupId { get; set; }


        [TextField("群号", IsRequired = true)]

        public string UniqueId { get; set; }
        

        [NumberField("百分比", IsRequired = true,Min =1,Max =100)]
        public int Rate { get; set; } = -1;

        [NumberField("优先级", IsRequired = true)] 
        public int Priority { get; set; } = -1;


        [DropListField("是否禁用", IsRequired = true)]
        public bool IsStop { get; set; }


        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            RateOfGroup entity = new RateOfGroup();
            var groupEntity = Context.Groups.Where(c => c.UnqiueId == this.UniqueId).Select(c => new { c.UnqiueId, c.GroupId }).FirstOrDefault();
            if (groupEntity == null)
            {
                return new ServiceResult(ServiceResultCode.Error,"不存在此群号,请重新输入");

            }
            if (this.Rate > 100 || this.Rate < 0)
            {
                return new ServiceResult(ServiceResultCode.Error, "请输入0~100的概率");
            }
            entity.GroupId = groupEntity.GroupId;
            entity.IsStop = this.IsStop;
            entity.Priority = this.Priority;
            entity.Rate = this.Rate;            
            Context.RateOfGroups.Add(entity);
            UserOperateLogService.Log($"新增[{entity.RateOfGroupId}]群组概率", Context);
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}