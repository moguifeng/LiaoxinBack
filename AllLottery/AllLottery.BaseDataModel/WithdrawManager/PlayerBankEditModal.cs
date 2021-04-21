using AllLottery.IBusiness;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;
using Zzb.Common;

namespace AllLottery.BaseDataModel.WithdrawManager
{
    public class PlayerBankEditModal : BaseServiceModal
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public PlayerBankEditModal()
        {
        }

        public PlayerBankEditModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "修改";

        [HiddenTextField]
        public int PlayerBankId { get; set; }

        [TextField("玩家", IsReadOnly = true)]
        public string PlayerName { get; set; }

        [TextField("银行名称", IsReadOnly = true)]
        public string BankName { get; set; }

        [TextField("真实姓名", IsRequired = true)]
        public string PayeeName { get; set; }

        [TextField("卡号", IsRequired = true)]
        public string CardNumber { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            if ((from b in Context.Withdraws where b.PlayerBankId == PlayerBankId select b).Any())
            {
                return new ServiceResult(ServiceResultCode.Error, "该银行已经发起了提款，无法修改");
            }

            var exist = (from b in Context.PlayerBanks where b.PlayerBankId == PlayerBankId select b).First();
            exist.PayeeName = PayeeName;
            exist.CardNumber = CardNumber;
            exist.Update();

            UserOperateLogService.Log($"修改[{exist.Player.Name}]的银行卡{MvcHelper.LogDifferent(new LogDifferentViewModel(exist.PayeeName, PayeeName, "收款人"), new LogDifferentViewModel(exist.CardNumber, CardNumber, "银行卡"))},主键为[{PlayerBankId}]", Context);
            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }
    }
}