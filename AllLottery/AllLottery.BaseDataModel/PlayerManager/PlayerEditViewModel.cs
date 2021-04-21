using AllLottery.Business.Config;
using AllLottery.IBusiness;
using AllLottery.Model;
using System.Linq;
using System.Text;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.PlayerManager
{
    public class PlayerEditViewModel : BaseServiceModal
    {
        public PlayerEditViewModel()
        {
        }

        public PlayerEditViewModel(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        [TextField("玩家", IsReadOnly = true)]
        public string Name { get; set; }

        [PasswordField("登录密码", Placeholder = "空代表不改变")]
        public string Password { get; set; }

        [PasswordField("资金密码", Placeholder = "空代表不改变")]
        public string CoidPassword { get; set; }

        [NumberField("返点")]
        public decimal Rebate { get; set; }

        [NumberField("标准日工资", Min = 0)]
        public decimal? DailyWageRate { get; set; }

        [NumberField("分红比例", Min = 0)]
        public decimal? DividendRate { get; set; }

        [DropListField("是否冻结")]
        public bool IsFreeze { get; set; }

        [DropListField("是否可以提款")]
        public bool CanWithdraw { get; set; }

        public override BaseButton[] Buttons()
        {
            return new BaseButton[] { new ActionButton("Save", "保存") };
        }

        public override string ModalName => "编辑玩家";

        public ServiceResult Save()
        {
            var player = (from p in Context.Players where p.Name == Name select p).First();
            Rebate /= 100;

            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(Password))
            {
                sb.Append(",修改登录密码");
                player.Password = SecurityHelper.Encrypt(Password);
            }
            if (!string.IsNullOrEmpty(CoidPassword))
            {
                sb.Append(",修改资金密码");
                player.CoinPassword = SecurityHelper.Encrypt(CoidPassword);
            }



            UserOperateLogService.Log($"修改玩家[{player.Name}]资料{sb}{MvcHelper.LogDifferent(new LogDifferentViewModel((player.Rebate * 100).ToDecimalString(), (Rebate * 100).ToDecimalString(), "返点"), new LogDifferentViewModel(player.DailyWageRate == null ? null : (player.DailyWageRate.Value * 100).ToDecimalString(), DailyWageRate?.ToDecimalString(), "标准日工资"), new LogDifferentViewModel(player.DividendRate == null ? null : (player.DividendRate.Value * 100).ToDecimalString(), DividendRate?.ToDecimalString(), "分红比例"), new LogDifferentViewModel(player.IsFreeze.ToString(), IsFreeze.ToString(), "是否冻结"), new LogDifferentViewModel(player.CanWithdraw.ToString(), CanWithdraw.ToString(), "是否可以提现"))}", Context);

            if (player.ParentPlayer != null)
            {
                if (player.ParentPlayer.Rebate < Rebate)
                {
                    return new ServiceResult(ServiceResultCode.Error, $"返点不能超过上级[{player.ParentPlayer.Rebate.ToPercenString()}]");
                }

                if (DailyWageRate != null)
                {
                    if (player.ParentPlayer.DailyWageRate == null)
                    {
                        return new ServiceResult(ServiceResultCode.Error, $"上级没有设置标准日工资");
                    }
                    if (player.ParentPlayer.DailyWageRate * 100 < DailyWageRate)
                    {
                        return new ServiceResult(ServiceResultCode.Error, $"标准日工资不能超过上级[{player.ParentPlayer.DailyWageRate?.ToPercenString()}]");
                    }
                }

                if (DividendRate != null)
                {
                    if (player.ParentPlayer.DividendRate == null)
                    {
                        return new ServiceResult(ServiceResultCode.Error, $"上级没有设置比例分红");
                    }

                    if (player.ParentPlayer.DividendRate * 100 < DividendRate)
                    {
                        return new ServiceResult(ServiceResultCode.Error, $"比例分红不能超过上级[{player.ParentPlayer.DividendRate?.ToPercenString()}]");
                    }
                }
            }
            else
            {
                if (BaseConfig.CreateInstance(SystemConfigEnum.MaxRebate).DecimalValue < Rebate)
                {
                    return new ServiceResult(ServiceResultCode.Error, $"返点不能超过最高返点[{(BaseConfig.CreateInstance(SystemConfigEnum.MaxRebate).DecimalValue * 100).ToDecimalString() + "%"}]");
                }
            }

            if (player.Players.Any())
            {
                var min = (from p in player.Players orderby p.Rebate descending select p.Rebate).First();
                if (min > Rebate)
                {
                    return new ServiceResult(ServiceResultCode.Error, $"返点不能低于下级[{min.ToPercenString()}]");
                }
            }

            if (player.Players.Any(t => t.DailyWageRate != null))
            {
                if (DailyWageRate == null)
                {
                    return new ServiceResult(ServiceResultCode.Error, $"标准日工资不能为空，因为已有下级设置了标准日工资");
                }
                var min = (from p in player.Players where p.DailyWageRate != null orderby p.DailyWageRate descending select p.DailyWageRate).First();
                if (min > DailyWageRate)
                {
                    return new ServiceResult(ServiceResultCode.Error, $"标准日工资不能低于下级[{min?.ToPercenString()}]");
                }
            }

            if (player.Players.Any(t => t.DividendRate != null))
            {
                if (DividendRate == null)
                {
                    return new ServiceResult(ServiceResultCode.Error, $"比例分红不能为空，因为已有下级设置了标准日工资");
                }
                var min = (from p in player.Players where p.DividendRate != null orderby p.DividendRate descending select p.DividendRate).First();
                if (min > DividendRate)
                {
                    return new ServiceResult(ServiceResultCode.Error, $"比例分红不能低于下级[{min?.ToPercenString()}]");
                }
            }

            player.Rebate = Rebate;
            if (DailyWageRate != null)
            {
                player.DailyWageRate = DailyWageRate.Value / 100;
            }
            else
            {
                player.DailyWageRate = null;
            }

            if (DividendRate != null)
            {
                player.DividendRate = DividendRate.Value / 100;
            }
            else
            {
                player.DividendRate = null;
            }

            player.IsFreeze = IsFreeze;
            player.CanWithdraw = CanWithdraw;

            player.Update();
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}