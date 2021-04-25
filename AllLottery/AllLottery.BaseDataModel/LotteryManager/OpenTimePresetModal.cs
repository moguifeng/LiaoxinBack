using AllLottery.Business.Generate;
using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.LotteryManager
{
    //public class OpenTimePresetModal : BaseServiceModal
    //{
    //    public OpenTimePresetModal()
    //    {
    //    }

    //    public OpenTimePresetModal(string id, string name) : base(id, name)
    //    {
    //    }

    //    public ILotteryOpenTimeServer LotteryOpenTimeServer { get; set; }

    //    public IUserOperateLogService UserOperateLogService { get; set; }

    //    public override string ModalName => "预设一天";

    //    [LotteryTypeDropListField("彩种", IsInit = true, IsSystem = true)]
    //    public int LotteryTypeId { get; set; }

    //    public override BaseButton[] Buttons()
    //    {
    //        return new[] { new ActionButton("Save", "预设一天"), };
    //    }

    //    public ServiceResult Save()
    //    {
    //        var lottery = (from l in Context.LotteryTypes where l.LotteryTypeId == LotteryTypeId select l).First();
    //        var openDic = LotteryOpenTimeServer.GetBetCacheDictionary(lottery.LotteryTypeId);
    //        List<LotteryData> list = new List<LotteryData>();
    //        foreach (var kv in openDic[DateTime.Now.ToDateString()])
    //        {
    //            var exist = from d in Context.LotteryDatas
    //                        where d.LotteryTypeId == lottery.LotteryTypeId && d.Number == kv.Key
    //                        select d;
    //            if (!exist.Any())
    //            {
    //                list.Add(new LotteryData()
    //                {
    //                    Data = BaseGenerate.CreateGenerate(lottery.LotteryClassify.Type).RandomGenerate(),
    //                    IsEnable = false,
    //                    LotteryTypeId = lottery.LotteryTypeId,
    //                    Number = kv.Key,
    //                    Time = kv.Value.OpenTime
    //                });
    //            }
    //        }
    //        UserOperateLogService.Log($"预设[{lottery.Name}]一天的开奖号码", Context);
    //        Context.LotteryDatas.AddRange(list);
    //        Context.SaveChanges();
    //        return new ServiceResult(ServiceResultCode.Success, "预设成功");
    //    }
    //}
}