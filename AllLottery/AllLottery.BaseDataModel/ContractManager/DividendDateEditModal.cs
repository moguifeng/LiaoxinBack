using AllLottery.IBusiness;
using System;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.ContractManager
{
    public class DividendDateEditModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public DividendDateEditModal()
        {
        }

        public DividendDateEditModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "修改日期";

        [HiddenTextField]
        public int DividendDateId { get; set; }

        [DateTimeField("结算日期")]
        public DateTime SettleTime { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            var div = (from d in Context.DividendDates where d.DividendDateId == DividendDateId select d).First();
            if (div.IsCal)
            {
                return new ServiceResult(ServiceResultCode.Error, "该期已经计算，无法修改");
            }

            var exist = from d in Context.DividendDates where d.SettleTime == SettleTime select d;
            if (exist.Any())
            {
                return new ServiceResult(ServiceResultCode.Error, "该计算日期已存在，添加失败");
            }

            var first = (from d in Context.DividendDates where d.IsCal orderby d.SettleTime descending select d).FirstOrDefault();
            if (first != null)
            {
                first.Update();
                if (first.SettleTime > SettleTime)
                {
                    return new ServiceResult(ServiceResultCode.Error, $"该日期已经结算，请填写大于[{first.SettleTime.ToDateString()}]的日期");
                }
            }

            UserOperateLogService.Log($"修改分红日期从[{div.SettleTime.ToDateString()}]改成[{SettleTime.ToDateString()}]", Context);

            div.SettleTime = SettleTime;
            div.Update();
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success, "保存成功");
        }
    }
}