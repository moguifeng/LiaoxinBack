using AllLottery.Business.Generate;
using AllLottery.IBusiness;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.LotteryManager
{
    //public class LotteryDataEditModal : BaseServiceModal
    //{
    //    public LotteryDataEditModal()
    //    {

    //    }

    //    public LotteryDataEditModal(string id, string name) : base(id, name)
    //    {
    //    }

    //    public override string ModalName => "编辑开奖信息";

    //    public IUserOperateLogService UserOperateLogService { get; set; }

    //    [HiddenTextField]
    //    public int LotteryDataId { get; set; }

    //    [TextField("期号", IsReadOnly = true)]
    //    public string Number { get; set; }

    //    [TextField("开奖号码")]
    //    public string Data { get; set; }

    //    public override BaseButton[] Buttons()
    //    {
    //        return new[] { new ActionButton("Save", "保存"), };
    //    }

    //    public ServiceResult Save()
    //    {
    //        var data = (from d in Context.LotteryDatas where d.LotteryDataId == LotteryDataId select d)
    //            .First();
    //        if (data.IsEnable)
    //        {
    //            return new ServiceResult(ServiceResultCode.Error, "该彩种已开奖");
    //        }

    //        if (!BaseGenerate.CheckData(data.LotteryTypeId, Data))
    //        {
    //            return new ServiceResult(ServiceResultCode.Error, "开奖号码格式错误，请重新输入");
    //        }

    //        data.Data = Data;
    //        data.Update();
    //        UserOperateLogService.Log($"编辑开奖信息：[{data.LotteryType.Name}]彩种的[{data.Number}]期号设置为[{Data}]", Context);
    //        Context.SaveChanges();
    //        return new ServiceResult();
    //    }
    //}
}