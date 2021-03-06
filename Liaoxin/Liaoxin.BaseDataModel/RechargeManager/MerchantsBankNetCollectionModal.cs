using Liaoxin.Model;
using System;
using Zzb.BaseData.Attribute.Field;

namespace Liaoxin.BaseDataModel.RechargeManager
{
    public class MerchantsBankNetCollectionModal : BaseMerchantsBankAddModal
    {
        public MerchantsBankNetCollectionModal()
        {
        }

        public MerchantsBankNetCollectionModal(string id, string name) : base(id, name)
        {
        }


        public override string Name { get; set; }

        public override Guid? BannerAffixId { get; set; }

        [TextField("收款人")]
        public string BankUserName { get; set; }

        [TextField("收款账号")]
        public string Account { get; set; }

        protected override MerchantsBankTypeEnum BankType => MerchantsBankTypeEnum.NetCollection;

        public override void SaveDoSomething(MerchantsBank bank)
        {
            bank.BankUserName = BankUserName;
            bank.Account = Account;
        }

        protected override void InitDoSomething(MerchantsBank bank)
        {
            BankUserName = bank.BankUserName;
            Account = bank.Account;
        }
    }
}