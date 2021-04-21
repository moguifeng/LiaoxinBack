using AllLottery.IBusiness;
using AllLottery.Model;
using System;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.ContractManager
{
    public class DividendDateViewModel : BaseServiceNav
    {
        public override string NavName => "分红发放日期";

        public override string FolderName => "工资分红管理";

        public IUserOperateLogService UserOperateLogService { get; set; }

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public int DividendDateId { get; set; }

        [NavField("结算日期", IsDisplay = false)]
        public DateTime SettleTime { get; set; }

        [NavField("结算日期")]
        public string Settle { get; set; }

        [NavField("是否结算")]
        public bool IsCal { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<DividendDate, DividendDateViewModel>(from d in Context.DividendDates
                                                                      where d.IsEnable
                                                                      orderby d.SettleTime descending
                                                                      select d, (k, t) =>
                {
                    t.Settle = k.SettleTime.ToDateString();
                });
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new[] { new DividendDateAddModal("Add", "新增"), };
        }

        public override BaseButton[] CreateRowButtons()
        {
            if (IsCal)
            {
                return base.CreateRowButtons();
            }
            return new BaseButton[] { new ConfirmActionButton("Delete", "删除", "是否确定删除"), new DividendDateEditModal("Edit", "修改"), };
        }

        public ServiceResult Delete()
        {
            var exist = (from d in Context.DividendDates where d.DividendDateId == DividendDateId select d).First();
            if (exist.IsCal)
            {
                return new ServiceResult(ServiceResultCode.Error, "该期已计算，无法删除");
            }

            UserOperateLogService.Log($"删除分红日期[{exist.SettleTime.ToDateString()}]", Context);
            Context.DividendDates.Remove(exist);
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success, "删除成功");
        }
    }
}