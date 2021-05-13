using Liaoxin.Cache;
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
    public class RateOfGroupEditModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }


        public RateOfGroupCacheManager _rateCache { get; set; }
        public RateOfGroupEditModal()
        {
        }

        public RateOfGroupEditModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "编辑群组概率";


        [HiddenTextField]
        public Guid RateOfGroupId { get; set; }


        [TextField("群号", IsReadOnly =true)]

        public string UniqueId { get; set; }
        

        [NumberField("百分比", IsRequired = true, Min = 1, Max = 100)]
        public int Rate { get; set; }

        [NumberField("优先级", IsRequired = true)]
        public int Priority { get; set; }


        [DropListField("是否禁用", IsRequired = true)]
        public bool IsStop { get; set; }


        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            if (this.Rate > 100 || this.Rate < 0)
            {
                return new ServiceResult(ServiceResultCode.Error, "请输入0~100的概率");
            }
            var entity = Context.RateOfGroups.Where(c => c.RateOfGroupId == this.RateOfGroupId).FirstOrDefault();
          
            entity.IsStop = this.IsStop;
            entity.Priority = this.Priority;
            entity.Rate = this.Rate;
            entity.UpdateTime = DateTime.Now;
            Context.RateOfGroups.Update(entity);
            UserOperateLogService.Log($"编辑[{entity.RateOfGroupId}]群组概率", Context);
            var res = Context.SaveChanges() > 0;
            if (res)
            {
                _rateCache.Set(entity.RateOfGroupId, new CacheRateOfGroup()
                {
                    Id = entity.RateOfGroupId.ToString(),
                    RateOfGroupId = entity.RateOfGroupId,
                    GroupId = entity.GroupId,
                    IsEnable = entity.IsEnable,
                    IsStop = entity.IsStop,
                    Priority = entity.Priority,
                    Rate = entity.Rate
                });
            }
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}