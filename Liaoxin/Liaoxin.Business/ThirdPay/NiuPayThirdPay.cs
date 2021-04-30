using Liaoxin.Model;
using Liaoxin.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Web;
using Zzb;
using Zzb.Common;
using Zzb.ZzbLog;

namespace Liaoxin.Business.ThirdPay
{
    public class NiuPayThirdPay : BaseThirdPay
    {
        protected override ThirdPayModel ThirdPayUrl(Recharge recharge, decimal money, string url, MerchantsBank bank)
        {
            var merchant_id = bank.MerchantsNumber;
            var orderid = recharge.OrderNo;
            var paytype = bank.Aisle;
            var notifyurl = HttpContextAccessor.HttpContext.Request.GetDefaultUrl() + "api/ThirdPay/NiuPayCallBack";
            var callbackurl = HttpUtility.UrlDecode(url);
            var sign = SecurityHelper.MD5Encrypt(merchant_id + orderid + paytype + notifyurl + callbackurl + money +
                                                 bank.MerchantsKey);
            string jsonText = HttpHelper.HttpPost("http://api.niupay.top/", $"merchant_id={merchant_id}&orderid={orderid}&paytype={paytype}&notifyurl={notifyurl}&callbackurl={callbackurl}&sign={sign}&money={money}");
            JObject jo = (JObject)JsonConvert.DeserializeObject(jsonText);

            string qrcode1 = jo["data"]["url"].ToString();
            if (string.IsNullOrEmpty(qrcode1))
            {
                throw new ZzbException(jsonText);
            }
            return new ThirdPayModel(qrcode1);
        }

        protected override bool CreateRecharge(out Recharge thrid)
        {
            thrid = null;
            var orderid = HttpContextAccessor.HttpContext.Request.Query["orderid"];
            var merchant_id = HttpContextAccessor.HttpContext.Request.Query["merchant_id"];
            var money = HttpContextAccessor.HttpContext.Request.Query["money"];
            var status = HttpContextAccessor.HttpContext.Request.Query["status"];
            var sign = HttpContextAccessor.HttpContext.Request.Query["sign"];
            if (status != "1")
            {
                LogHelper.Error($"NiuPay返回错误状态[{status}]");
                return false;
            }

            if (string.IsNullOrEmpty(orderid))
            {
                LogHelper.Error($"NiuPay返回错误订单号[{orderid}]");
                return false;
            }

            thrid = (from r in Context.Recharges where r.OrderNo == orderid select r).FirstOrDefault();
            if (thrid == null)
            {
                LogHelper.Error($"NiuPay返回错误订单号[{orderid}]");
                return false;
            }

            //string parastring = merchant_id + orderid + money + thrid.MerchantsBank.MerchantsKey;
            //if (SecurityHelper.MD5Encrypt(parastring) != sign)
            //{
            //    LogHelper.Error($"NiuPay返回错误签名[{sign}]，我方签名[{SecurityHelper.MD5Encrypt(parastring)}]，签名字符串[{parastring}]");
            //    return false;
            //}

            return true;
        }
    }
}