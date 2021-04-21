using AllLottery.Model;
using AllLottery.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;
using Zzb.Common;
using Zzb.ZzbLog;

namespace AllLottery.Business.ThirdPay
{
    public class GlobalPayThirdPay : BaseThirdPay
    {
        protected string Url => "http://otc.globalokpaytech.com/pay/toConfirmIn";

        private string _notifyUrl => HttpContextAccessor.HttpContext.Request.GetDefaultUrl() + "api/ThirdPay/GlobalPayCallBack";

        protected override ThirdPayModel ThirdPayUrl(Recharge recharge, decimal money, string url, MerchantsBank bank)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("pickupUrl", HttpUtility.UrlDecode(url));
            dic.Add("receiveUrl", HttpUtility.UrlDecode(_notifyUrl));
            dic.Add("signType", "MD5");
            dic.Add("orderNo", recharge.OrderNo);
            dic.Add("orderAmount", money.ToString());
            dic.Add("orderCurrency", "CNY");
            dic.Add("customerId", "15687957317");
            StringBuilder sb = new StringBuilder();
            sb.Append(HttpUtility.UrlDecode(url));
            sb.Append(HttpUtility.UrlDecode(_notifyUrl));
            sb.Append("MD5");
            sb.Append(recharge.OrderNo);
            sb.Append(money.ToString());
            sb.Append("CNY");
            sb.Append("15687957317");
            sb.Append(bank.MerchantsKey);
            dic.Add("sign", SecurityHelper.MD5Encrypt(sb.ToString()));
            dic.Add("crmNo", bank.MerchantsNumber);
            return new ThirdPayModel(Url, HttpType.Post, dic);
        }

        protected override bool CreateRecharge(out Recharge thrid)
        {
            thrid = null;

            string signType = HttpContextAccessor.HttpContext.Request.Form["signType"];
            string orderNo = HttpContextAccessor.HttpContext.Request.Form["orderNo"];
            string orderAmount = HttpContextAccessor.HttpContext.Request.Form["orderAmount"];
            string orderCurrency = HttpContextAccessor.HttpContext.Request.Form["orderCurrency"];
            string transactionId = HttpContextAccessor.HttpContext.Request.Form["transactionId"];
            string status = HttpContextAccessor.HttpContext.Request.Form["status"];

            LogHelper.Information("signType=" + signType + "-orderNo=" + orderNo + "-orderAmount=" + orderAmount + "-orderCurrency=" + orderCurrency + "-transactionId=" + transactionId + "-status=" + status);
            if (status != "success")
            {
                LogHelper.Error("全球支付status不是success");
                return false;
            }

            if (string.IsNullOrEmpty(orderNo))
            {
                LogHelper.Error("全球支付没有订单号主键");
                return false;
            }

            var orderid = orderNo;
            thrid = (from r in Context.Recharges where r.OrderNo == orderid select r)
                .FirstOrDefault();
            if (thrid == null)
            {
                LogHelper.Error($"全球支付找不到该订单[{orderNo}]");
                return false;
            }

            string sb = signType + orderNo + orderAmount + orderCurrency + transactionId + status + thrid.MerchantsBank.MerchantsKey;
            string sign = SecurityHelper.MD5Encrypt(sb);
            if (sign != HttpContextAccessor.HttpContext.Request.Form["sign"])
            {
                LogHelper.Error($"全球支付签名不符合，我方签名[{sign}]，对方签名[{ HttpContextAccessor.HttpContext.Request.Form["sign"]}],签名字符串{sb}");
                return false;
            }

            return true;
        }
    }
}