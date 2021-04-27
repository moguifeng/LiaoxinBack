using Liaoxin.IBusiness;
using Liaoxin.Model;
using Castle.Components.DictionaryAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace Liaoxin.BaseDataModel.WithdrawManager
{
    public class PlayerBankViewModel : BaseServiceNav
    {
        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string NavName => "玩家银行管理";

        public override string FolderName => "提款管理";

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public int PlayerBankId { get; set; }

        [NavField("玩家")]
        public string PlayerName { get; set; }

        [NavField("真实姓名")]
        public string PayeeName { get; set; }

        [NavField("银行名称")]
        public string BankName { get; set; }

        [NavField("卡号")]
        public string CardNumber { get; set; }

        [NavField("创建时间", 150)]
        public DateTime CreateTime { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<PlayerBank, PlayerBankViewModel>(
                from b in Context.PlayerBanks where b.IsEnable  orderby b.CreateTime descending select b,
                (k, t) =>
                {
                    t.PlayerName = k.Player.Name;
                    t.BankName = k.SystemBank.Name;
                },
                (k, w) => w.Where(t => t.Player.Name.Contains(k)),
                (k, w) => w.Where(t => t.SystemBank.Name.Contains(k)),
                (k, w) => w.Where(t => t.CardNumber.Contains(k)));
        }

        public override BaseFieldAttribute[] GetQueryConditionses()
        {
            return new BaseFieldAttribute[] { new TextFieldAttribute("Player", "玩家"), new TextFieldAttribute("Bank", "银行"), new TextFieldAttribute("Card", "卡号"), };
        }

        public override BaseButton[] CreateRowButtons()
        {
            List<BaseButton> list = new EditableList<BaseButton>();
            if (!(from b in Context.Withdraws where b.PlayerBankId == PlayerBankId select b).Any())
            {
                list.Add(new PlayerBankEditModal("Test", "修改"));
            }
            list.Add(new ConfirmActionButton("Delete", "删除", "是否确定删除"));
            return list.ToArray();
        }

        public void Delete()
        {
            var exist = (from b in Context.PlayerBanks where b.PlayerBankId == PlayerBankId select b).First();
            exist.IsEnable = false;
            UserOperateLogService.Log($"删除[{exist.Player.Name}]银行卡，收款人[{exist.PayeeName}],卡号[{exist.CardNumber}],主键[{exist.PlayerBankId}]", Context);
            Context.SaveChanges();
        }
    }
}