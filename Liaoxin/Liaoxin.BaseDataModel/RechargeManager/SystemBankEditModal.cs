using System;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;

namespace Liaoxin.BaseDataModel.RechargeManager
{
    public class SystemBankEditModal : SystemBankAddModal
    {
        public SystemBankEditModal()
        {
        }

        public SystemBankEditModal(string id, string name) : base(id, name)
        {
        }

        [HiddenTextField]
        public Guid SystemBankId { get; set; }

        public override ServiceResult Save()
        {
            if (AffixId == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "请上传银行图片");
            }
            var exist = (from b in Context.SystemBanks where b.SystemBankId == SystemBankId select b).First();
            exist.Name = Name;
            exist.AffixId = AffixId.Value;
            exist.SortIndex = SortIndex;
            exist.Update();
            Context.SaveChanges();
            return new ServiceResult();
        }
    }
}