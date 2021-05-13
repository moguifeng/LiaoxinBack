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
    public class RateOfClientEditModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public RateOfClientCacheManager _rateCache { get; set; }
        public RateOfClientEditModal()
        {
        }

        public RateOfClientEditModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "编辑客户概率";


        [HiddenTextField]
        public Guid RateOfClientId { get; set; }


        [TextField("客户聊信号", IsReadOnly =true)]

        public string LiaoxinNumber { get; set; }
        

        [NumberField("百分比", IsRequired = true, Min = 1, Max = 100)]
        public int Rate { get; set; }

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
            if (this.Rate > 100 || this.Rate < 0)
            {
                return new ServiceResult(ServiceResultCode.Error, "请输入0~100的概率");
            }
            var entity = Context.RateOfClients.Where(c => c.RateOfClientId == this.RateOfClientId).FirstOrDefault();
          
            entity.IsStop = this.IsStop;
            entity.Priority = this.Priority;
            entity.Rate = this.Rate;
            entity.UpdateTime = DateTime.Now;
            Context.RateOfClients.Update(entity);
            UserOperateLogService.Log($"编辑[{entity.RateOfClientId}]客户概率", Context);
            var res = Context.SaveChanges() > 0;
            if (res)
            {
                _rateCache.Set(entity.RateOfClientId, new CacheRateOfClient()
                {
                    Id = entity.RateOfClientId.ToString(),
                    RateOfClientId = entity.RateOfClientId,
                    ClientId = entity.ClientId,
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