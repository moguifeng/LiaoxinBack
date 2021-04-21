using AllLottery.IBusiness;
using AllLottery.Model;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.RechargeManager
{
    public class GiftReceiveAddModal : BaseServiceModal
    {
        public GiftReceiveAddModal()
        {
        }

        public GiftReceiveAddModal(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "新增";

        [TextField("玩家", IsRequired = true)]
        public string Name { get; set; }

        [NumberField("赠送金额", IsRequired = true)]
        public decimal GiftMoney { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            var player = (from p in Context.Players where p.Name == Name && p.IsEnable select p).FirstOrDefault();
            if (player == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "未找到该玩家");
            }

            player.AddMoney(GiftMoney, Model.CoinLogTypeEnum.GiftMoney, 0, out var log, "手工新增活动金额");

            UserOperateLogService.Log($"手工新增[{player.Name}]的活动金额[{GiftMoney.ToDecimalString()}]", Context);

            Context.GiftReceives.Add(new GiftReceive(player.PlayerId, GiftMoney));

            Context.CoinLogs.Add(log);

            player.UpdateReportDate();

            player.GiftMoney += GiftMoney;

            Context.SaveChanges();

            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}