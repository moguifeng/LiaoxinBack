using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using System.Linq;
using Zzb;
using Zzb.BaseData.Attribute.Field;
using Zzb.BaseData.Model.Button;

namespace Liaoxin.BaseDataModel.RechargeManager
{
    public abstract class BaseMerchantsBankAddModal : BaseServiceModal
    {
        protected BaseMerchantsBankAddModal()
        {
        }

        protected BaseMerchantsBankAddModal(string id, string name) : base(id, name)
        {
        }

        public IUserOperateLogService UserOperateLogService { get; set; }

        public override string ModalName => "新增收款银行";

        [HiddenTextField]
        public int MerchantsBankId { get; set; } = -1;

        [TextField("标题")]
        public virtual string Name { get; set; }

        [ImageField("银行图片")]
        public virtual Guid? BannerAffixId { get; set; }


        [NumberField("最小充值金额", 0, 1000000)]
        public decimal Min { get; set; } = 100;

        [NumberField("最大充值金额", 0, 1000000)]
        public decimal Max { get; set; } = 100000;

        [NumberField("排序")]
        public int IndexSort { get; set; }

        [DropListField("是否启动")]
        public bool IsUseful { get; set; }

        protected abstract MerchantsBankTypeEnum BankType { get; }

        public override BaseButton[] Buttons()
        {
            return new[] { new ActionButton("Save", "保存"), };
        }

        public ServiceResult Save()
        {
            if (BannerAffixId == null)
            {
                return new ServiceResult(ServiceResultCode.Error, "请上传银行图片");
            }

            MerchantsBank bank = new MerchantsBank();
            if (MerchantsBankId >= 0)
            {
                bank = (from m in Context.MerchantsBanks where m.MerchantsBankId == MerchantsBankId select m).First();
            }
            bank.BannerAffixId = BannerAffixId.Value;

            try
            {
                SaveDoSomething(bank);
            }
            catch (ZzbException e)
            {
                return new ServiceResult(ServiceResultCode.Error, e.Message);
            }

            bank.Type = BankType;
            bank.Name = Name;
            bank.Min = Min;
            bank.Max = Max;
            bank.IndexSort = IndexSort;
            bank.IsUseful = IsUseful;
            if (MerchantsBankId >= 0)
            {
                UserOperateLogService.Log($"编辑收款银行[{Name}]", Context);
            }
            else
            {
                Context.MerchantsBanks.Add(bank);
                UserOperateLogService.Log($"添加收款银行[{Name}]", Context);
            }

            Context.SaveChanges();
            return new ServiceResult(ServiceResultCode.Success);
        }

        public override void Init()
        {
            if (MerchantsBankId > 0)
            {
                var exist = (from m in Context.MerchantsBanks where m.MerchantsBankId == MerchantsBankId select m)
                    .FirstOrDefault();
                if (exist != null)
                {
                    BannerAffixId = exist.BannerAffixId;
                    InitDoSomething(exist);
                }
            }
        }

        protected virtual void InitDoSomething(MerchantsBank bank)
        {

        }

        public abstract void SaveDoSomething(MerchantsBank bank);
    }
}