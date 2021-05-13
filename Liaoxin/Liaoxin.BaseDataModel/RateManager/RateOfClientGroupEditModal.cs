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
    public class RateOfClientGroupEditModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public RateOfClientGroupCacheManager _rateCache { get; set; }
        public RateOfClientGroupEditModal()
        {
        }

        public RateOfClientGroupEditModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "编辑客户群组概率";


        [HiddenTextField]
        public Guid RateOfGroupClientId { get; set; }


        [TextField("群号", IsReadOnly =true)]

        public string UniqueId { get; set; }


        [TextField("客户聊信号", IsReadOnly = true)]

        public string LiaoxinNumber { get; set; }


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
            var entity = Context.RateOfGroupClients.Where(c => c.RateOfGroupClientId == this.RateOfGroupClientId).FirstOrDefault();
          
            entity.IsStop = this.IsStop;
            entity.Priority = this.Priority;
            entity.Rate = this.Rate;
            entity.UpdateTime = DateTime.Now;
            Context.RateOfGroupClients.Update(entity);
            UserOperateLogService.Log($"编辑[{entity.RateOfGroupClientId}]群组概率", Context);
            var res = Context.SaveChanges() > 0;
            if (res)
            {
                _rateCache.Set(entity.RateOfGroupClientId, new CacheRateOfClientGroup()
                {
                    Id = entity.RateOfGroupClientId.ToString(),
                    RateOfGroupClientId = entity.RateOfGroupClientId,
                    ClientId = entity.ClientId,
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