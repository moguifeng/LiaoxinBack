using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.ContractManager
{
    public class DividendDateAddModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public DividendDateAddModal()
        {
        }

        public DividendDateAddModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "添加日期";

        [DateTimeField("计算日期")]
        public DateTime SettleTime { get; set; } = DateTime.Now;

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
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

            UserOperateLogService.Log($"新增分红日期[{SettleTime.ToDateString()}]", Context);

            Context.DividendDates.Add(new DividendDate(SettleTime));
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success, "保存成功");
        }
    }
}