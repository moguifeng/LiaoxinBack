using System;
using System.Linq;
using AllLottery.IBusiness;
using Zzb;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.PlayerManager
{
    //public class SoftwareExpiredEditViewModel : BaseServiceModal
    //{
    //    public IUserOperateLogService UserOperateLogService { get; set; }

    //    public SoftwareExpiredEditViewModel()
    //    {
    //    }

    //    public SoftwareExpiredEditViewModel(string id, string name) : base(id, name)
    //    {
    //    }

    //    [HiddenTextField]
    //    public int SoftwareExpiredId { get; set; }

    //    [TextField("玩家", IsReadOnly = true)]
    //    public string Name { get; set; }

    //    [DateTimeField("过期时间")]
    //    public DateTime Expired { get; set; }


    //    public override string ModalName => "编辑期限";

    //    public override BaseButton[] Buttons()
    //    {
    //        return new[] { new ActionButton("Save", "保存"), };
    //    }

    //    public ServiceResult Save()
    //    {
    //        var expire = (from e in Context.SoftwareExpireds where e.SoftwareExpiredId == SoftwareExpiredId select e)
    //            .FirstOrDefault();
    //        if (expire == null)
    //        {
    //            return new ServiceResult(ServiceResultCode.Error, "未找到该期限");
    //        }

    //        expire.Expired = Expired;
    //        expire.Update();
    //        UserOperateLogService.Log($"修改期限:{expire.Player.Name}，{Expired}", Context);
    //        Context.SaveChanges();
    //        return new ServiceResult(ServiceResultCode.Success);
    //    }
    //}
}