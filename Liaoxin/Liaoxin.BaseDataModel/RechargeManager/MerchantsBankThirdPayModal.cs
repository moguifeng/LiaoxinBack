using Liaoxin.Model;
using System;
using Zzb.BaseData.Attribute.Field;

namespace Liaoxin.BaseDataModel.RechargeManager
{
    public class MerchantsBankThirdPayModal : BaseMerchantsBankAddModal
    {
        public MerchantsBankThirdPayModal()
        {
        }

        public MerchantsBankThirdPayModal(string id, string name) : base(id, name)
        {
        }

        public override string Name { get; set; }

        public override Guid? BannerAffixId { get; set; }

        [DropListField("第三方商家")]
        public ThirdPayMerchantsType ThirdPayMerchantsType { get; set; }

        [TextField("商户编号")]
        public string MerchantsNumber { get; set; }

        [TextField("商户密钥")]
        public string MerchantsKey { get; set; }

        [TextField("通道")]
        public string Aisle { get; set; }

        protected override MerchantsBankTypeEnum BankType => MerchantsBankTypeEnum.ThirdPay;



        public override void SaveDoSomething(MerchantsBank bank)
        {
            bank.MerchantsNumber = MerchantsNumber;
            bank.MerchantsKey = MerchantsKey;
            bank.Aisle = Aisle;
            bank.ThirdPayMerchantsType = ThirdPayMerchantsType;
        }

        protected override void InitDoSomething(MerchantsBank bank)
        {
            MerchantsNumber = bank.MerchantsNumber;
            MerchantsKey = bank.MerchantsKey;
            Aisle = bank.Aisle;
            ThirdPayMerchantsType = bank.ThirdPayMerchantsType;
        }
    }
}