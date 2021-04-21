using AllLottery.Model;
using AllLottery.ViewModel;
using System.Linq;
using System.Web;
using Zzb.Common;
using Zzb.ZzbLog;

namespace AllLottery.Business.ThirdPay
{
    public class XingHuoThirdPay : BaseThirdPay
    {
        private string _notifyUrl => HttpContextAccessor.HttpContext.Request.GetDefaultUrl() + "api/ThirdPay/XingHuoCallBack";

        protected override ThirdPayModel ThirdPayUrl(Recharge recharge, decimal money, string url, MerchantsBank bank)
        {
            string customerAmount = "";
            string pickupUrl;
            string receiveUrl = _notifyUrl;
            string signType = "MD5";
            string outOrderId = recharge.OrderNo;
            string customerAmountCny = money.ToString();



            string baseUrl = "https://s.starfireotc.com/payLink/web.html";
            if (string.IsNullOrEmpty(url))
            {
                pickupUrl = HttpContextAccessor.HttpContext.Request.GetDefaultUrl();
                baseUrl = "https://s.starfireotc.com/payLink/mobile.html";
            }
            else
            {
                pickupUrl = HttpUtility.UrlDecode(url);
            }
            string md5Str = outOrderId + customerAmount + pickupUrl + receiveUrl + customerAmountCny + signType + bank.MerchantsKey;
            string sign = SecurityHelper.MD5Encrypt(md5Str);

            LogHelper.Debug($"签名字符串:[{md5Str}],签名完毕:[{sign}],访问网址:[" + baseUrl + $"?outOrderId={outOrderId}&customerAmount={customerAmount}&customerAmountCny={customerAmountCny}&APPKey={bank.MerchantsNumber}&pickupUrl={pickupUrl}&receiveUrl={receiveUrl}&signType={signType}&sign={sign}" + "]");

            return new ThirdPayModel(baseUrl + $"?outOrderId={outOrderId}&customerAmount={customerAmount}&customerAmountCny={customerAmountCny}&APPKey={bank.MerchantsNumber}&pickupUrl={pickupUrl}&receiveUrl={receiveUrl}&signType={signType}&sign={sign}");
        }

        protected override bool CreateRecharge(out Recharge thrid)
        {
            thrid = null;

            string signType = HttpContextAccessor.HttpContext.Request.Form["signType"];
            string outOrderId = HttpContextAccessor.HttpContext.Request.Form["outOrderId"];
            string customerAmountCny = HttpContextAccessor.HttpContext.Request.Form["customerAmountCny"];
            string customerAmount = HttpContextAccessor.HttpContext.Request.Form["customerAmount"];
            string orderId = HttpContextAccessor.HttpContext.Request.Form["orderId"];
            string status = HttpContextAccessor.HttpContext.Request.Form["status"];

            LogHelper.Debug("signType=" + signType + "-outOrderId=" + outOrderId + "-customerAmount=" + customerAmount + "-customerAmountCny=" + customerAmountCny + "-orderId=" + orderId + "-status=" + status);
            if (status != "success")
            {
                LogHelper.Information("星火支付status不是success");
                return false;
            }

            if (string.IsNullOrEmpty(outOrderId))
            {
                LogHelper.Error("星火支付没有订单号主键");
                return false;
            }

            var orderid = outOrderId;
            thrid = (from r in Context.Recharges where r.OrderNo == orderid select r)
                .FirstOrDefault();
            if (thrid == null)
            {
                LogHelper.Error($"星火支付找不到该订单[{outOrderId}]");
                return false;
            }

            string sb = customerAmount + customerAmountCny + outOrderId + orderId + signType + status + thrid.MerchantsBank.MerchantsKey;
            string sign = SecurityHelper.MD5Encrypt(sb);
            if (sign != HttpContextAccessor.HttpContext.Request.Form["sign"])
            {
                LogHelper.Error($"星火支付签名不符合，我方签名[{sign}]，对方签名[{ HttpContextAccessor.HttpContext.Request.Form["sign"]}],签名字符串{sb}");
                return false;
            }

            return true;
        }

    }
}
