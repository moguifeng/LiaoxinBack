using AllLottery.BaseDataModel.PlayTypeManager;
using AllLottery.Business.Generate;
using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace AllLottery.BaseDataModel.LotteryManager
{
    public class LotteryDataAddModal : BaseServiceModal
    {
        public LotteryDataAddModal()
        {
        }

        public LotteryDataAddModal(string id, string name) : base(id, name)
        {
        }

        public ILotteryOpenTimeServer LotteryOpenTimeServer { get; set; }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "预设彩种";

        [LotteryTypeDropListField("彩种", IsInit = true)]
        public int LotteryTypeId { get; set; }

        [TextField("期号")]
        public string Number { get; set; }

        [TextField("开奖号码")]
        public string Data { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            var lottery = (from l in Context.LotteryTypes where l.LotteryTypeId == LotteryTypeId select l).First();

            var exist = from d in Context.LotteryDatas
                        where d.LotteryTypeId == LotteryTypeId && d.Number == Number
                        select d;
            if (exist.Any())
            {
                return new ServiceResult(ServiceResultCode.Error, "该期号已经开奖或已经预设，保存失败");
            }

            var openTime = LotteryOpenTimeServer.GetBetTime(lottery.LotteryTypeId, Number);
            if (openTime == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "无法找到开奖信息");
            }

            if (!BaseGenerate.CheckData(lottery.LotteryTypeId, Data))
            {
                return new ServiceResult(ServiceResultCode.Error, "开奖号码格式错误，请重新输入");
            }

            var data = new LotteryData()
            {
                LotteryTypeId = LotteryTypeId,
                Data = Data,
                Number = Number,
                Time = openTime.OpenTime
            };
            if (openTime.OpenTime > DateTime.Now)
            {
                data.IsEnable = false;
            }
            UserOperateLogService.Log($"预设[{lottery.Name}]的[{data.Number}]的开奖号码[{data.Data}]", Context);
            Context.LotteryDatas.Add(data);
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success, "保存成功");
        }
    }
}