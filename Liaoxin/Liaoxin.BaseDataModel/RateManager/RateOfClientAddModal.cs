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
    public class RateOfClientAddModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public RateOfClientCacheManager _rateCache { get; set; }

        public RateOfClientAddModal()
        {
        }

        public RateOfClientAddModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "编辑客户概率";


        [HiddenTextField]
        public Guid RateOfClientId { get; set; }


        [TextField("客户聊信号", IsRequired = true)]

        public string LiaoxinNumber { get; set; }


        [NumberField("百分比", IsRequired = true, Min = 1, Max = 100)]
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
            RateOfClient entity = new RateOfClient();
            var clientEntity = Context.Clients.Where(c => c.LiaoxinNumber == this.LiaoxinNumber).Select(c => new { c.LiaoxinNumber, c.ClientId }).FirstOrDefault();
            if (clientEntity == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "不存在此聊信号,请重新输入");

            }
            if (this.Rate > 100 || this.Rate < 0)
            {
                return new ServiceResult(ServiceResultCode.Error, "请输入0~100的概率");
            }
            entity.ClientId = clientEntity.ClientId;
            entity.IsStop = this.IsStop;
            entity.Priority = this.Priority;
            entity.Rate = this.Rate;
            Context.RateOfClients.Add(entity);
            UserOperateLogService.Log($"新增[{entity.RateOfClientId}]客户概率", Context);
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