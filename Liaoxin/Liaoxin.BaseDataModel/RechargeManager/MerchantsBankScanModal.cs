using Liaoxin.Model;
using Zzb;
using Zzb.BaseData.Attribute.Field;

namespace Liaoxin.BaseDataModel.RechargeManager
{
    public class MerchantsBankScanModal : BaseMerchantsBankAddModal
    {
        public MerchantsBankScanModal()
        {
        }

        public MerchantsBankScanModal(string id, string name) : base(id, name)
        {
        }

        public override string Name { get; set; }

        public override int? BannerAffixId { get; set; }

        [ImageField("收款二维码")]
        public int? ScanAffixId { get; set; }

        protected override MerchantsBankTypeEnum BankType => MerchantsBankTypeEnum.Scan;

        public override void SaveDoSomething(MerchantsBank bank)
        {
            if (ScanAffixId == null)
            {
                throw new ZzbException("请上传收款二维码");
            }
            bank.ScanAffixId = ScanAffixId.Value;

        }

        protected override void InitDoSomething(MerchantsBank bank)
        {
            ScanAffixId = bank.ScanAffixId;
        }
    }
}