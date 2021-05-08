using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Zzb.EF;

namespace Liaoxin.Model
{
    public class MerchantsBank : BaseModel
    {
        public int MerchantsBankId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 银行图片附件
        /// </summary>
        public Guid BannerAffixId { get; set; }

        [ForeignKey("BannerAffixId")]
        public virtual Affix BannerAffix { get; set; }

        #region 转账专用

        /// <summary>
        /// 银行收款人
        /// </summary>
        public string BankUserName { get; set; }

        /// <summary>
        /// 收款银行账号
        /// </summary>
        public string Account { get; set; }

        #endregion

        #region 扫码专用

        /// <summary>
        /// 扫码图片
        /// </summary>
        public Guid? ScanAffixId { get; set; }

        [ForeignKey("ScanAffixId")]
        public virtual Affix ScanAffix { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        #endregion

        /// <summary>
        /// 排序
        /// </summary>
        public int IndexSort { get; set; }

        public MerchantsBankTypeEnum Type { get; set; } = MerchantsBankTypeEnum.Scan;

        /// <summary>
        /// 第三方支付厂商
        /// </summary>
        public ThirdPayMerchantsType ThirdPayMerchantsType { get; set; } = ThirdPayMerchantsType.NiuPay;

        /// <summary>
        /// 商户编号
        /// </summary>
        public string MerchantsNumber { get; set; }

        /// <summary>
        /// 商户密钥
        /// </summary>
        public string MerchantsKey { get; set; }

        /// <summary>
        /// 通道
        /// </summary>
        public string Aisle { get; set; }

        public decimal Min { get; set; } = 0;

        public decimal Max { get; set; } = 100000;

        /// <summary>
        /// 是否启动
        /// </summary>
        public bool IsUseful { get; set; } = true;

        /// <summary>
        /// 显示
        /// </summary>
        public MerchantsBankShowType MerchantsBankShowType { get; set; } = MerchantsBankShowType.All;
    }

    public enum MerchantsBankShowType
    {
        [Description("全部")]
        All = 0,
        [Description("仅PC")]
        Pc = 1,
        [Description("仅APP")]
        App = 2
    }

    public enum MerchantsBankTypeEnum
    {
        [Description("扫码支付")]
        Scan = 0,
        [Description("第三方支付")]
        ThirdPay = 1,
        [Description("转账")]
        NetCollection = 2
    }

    public enum ThirdPayMerchantsType
    {
        [Description("NiuPay")]
        NiuPay = 0,
        [Description("易闪付")]
        YiShanFu = 1,
        [Description("全球付")]
        GlobalPay = 2,
        [Description("一点付")]
        YiDianFuThirdPay = 3,
        [Description("迅聚合")]
        XunJuHe = 4,
        [Description("条码支付")]
        TiaoMa = 5,
        [Description("星火支付")]
        XingHuo = 6
    }
}