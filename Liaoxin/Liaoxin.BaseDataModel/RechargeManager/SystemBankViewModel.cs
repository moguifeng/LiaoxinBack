using Liaoxin.Model;
using System.Linq;
using Zzb.BaseData.Attribute;
using Zzb.BaseData.Model.Button;

namespace Liaoxin.BaseDataModel.RechargeManager
{
    public class SystemBankViewModel : BaseServiceNav
    {
        public override string NavName => "系统银行管理";

        public override string FolderName => "充值管理";

        [NavField("主键", IsKey = true, IsDisplay = false)]
        public int SystemBankId { get; set; }

        [NavField("银行名称")]
        public string Name { get; set; }

        [NavField("排序")]
        public int SortIndex { get; set; }

        [NavField("图片", IsDisplay = false)]
        public int AffixId { get; set; }

        protected override object[] DoGetNavDatas()
        {
            return CreateEfDatas<SystemBank, SystemBankViewModel>(from b in Context.SystemBanks
                                                                  where b.IsEnable
                                                                  orderby b.SortIndex
                                                                  select b);
        }

        public override BaseButton[] CreateRowButtons()
        {
            return new BaseButton[] { new ConfirmActionButton("Delete", "删除", "是否确定删除"), new SystemBankEditModal("Edit", "编辑"), };
        }

        public override BaseButton[] CreateViewButtons()
        {
            return new[] { new SystemBankAddModal("Add", "新增"), };
        }

        public void Delete()
        {
            var exist = (from b in Context.SystemBanks where b.SystemBankId == SystemBankId select b).First();
            exist.IsEnable = false;
            Context.SaveChanges();
        }
    }
}