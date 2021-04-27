using Liaoxin.Model;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace Liaoxin.BaseDataModel.RechargeManager
{
    public class SystemBankAddModal : BaseServiceModal
    {
        public SystemBankAddModal()
        {
        }

        public SystemBankAddModal(string id, string name) : base(id, name)
        {
        }

        public override string ModalName => "新增";

        [TextField("银行名称", IsRequired = true)]
        public string Name { get; set; }

        [ImageField("银行图片")]
        public int? AffixId { get; set; }

        [NumberField("排序")]
        public int SortIndex { get; set; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public virtual ServiceResult Save()
        {
            if (AffixId == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "请上传银行图片");
            }
            var exist = (from b in Context.SystemBanks where b.Name == Name select b).FirstOrDefault();
            if (exist != null)
            {
                return new ServiceResult(ServiceResultCode.Error, "已经有重复的系统银行");
            }

            Context.SystemBanks.Add(new SystemBank() { Name = Name, AffixId = AffixId.Value, SortIndex = SortIndex });
            Context.SaveChanges();
            return new ServiceResult();
        }
    }
}