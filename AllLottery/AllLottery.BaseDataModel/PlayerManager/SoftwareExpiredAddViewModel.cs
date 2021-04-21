using System;
using System.Linq;
using AllLottery.IBusiness;
using AllLottery.Model;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.PlayerManager
{
    public class SoftwareExpiredAddViewModel : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public SoftwareExpiredAddViewModel()
        {
        }

        public SoftwareExpiredAddViewModel(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "新增期限";

        [TextField("玩家", IsRequired = true)]
        public string Name { get; set; }

        [DateTimeField("期限")]
        public DateTime Expired { get; set; } = DateTime.Today;

        public override BaseButton[] Buttons()
        {
            return new BaseButton[] { new ActionButton("Save", "保存") };
        }

        public ServiceResult Save()
        {
            //Name = Name.Trim();
            var player = (from p in Context.Players where p.Name == Name select p).FirstOrDefault();
            if (player == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "未找到该玩家");
            }
            UserOperateLogService.Log($"新增软件权限{player.Name}:{Expired}:", Context);
            Context.SoftwareExpireds.Add(new SoftwareExpired() { PlayerId = player.PlayerId, Expired = Expired });
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}