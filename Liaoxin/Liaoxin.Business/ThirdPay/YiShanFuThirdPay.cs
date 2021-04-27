using Liaoxin.Model;
using Liaoxin.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Http;
using Zzb.Common;
using Zzb.ZzbLog;

namespace Liaoxin.Business.ThirdPay
{
    public class YiShanFuThirdPay : BaseThirdPay
    {
        private string _notifyUrl => HttpContextAccessor.HttpContext.Request.GetDefaultUrl() + "api/ThirdPay/YiShanFuCallBack";

        protected override ThirdPayModel ThirdPayUrl(Recharge recharge, decimal money, string url, MerchantsBank bank)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();


            dic.Add("amount", money.ToString("#0.00").Trim());
            dic.Add("appid", bank.MerchantsNumber.Trim());
            dic.Add("callback_url", HttpUtility.UrlDecode(_notifyUrl)?.Trim());
            dic.Add("out_trade_no", recharge.OrderNo.Trim());
            dic.Add("pay_type", bank.Aisle.Trim());
            dic.Add("version", "v1.0");

            var sign = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (string k in dic.Keys.OrderBy(t => t))
            {
                if (!string.IsNullOrEmpty(dic[k]))
                {
                    sb.Append($"{k}={dic[k]}&");
                }
            }
            sb.Append("key=" + bank.MerchantsKey);
            dic.Add("sign", SecurityHelper.MD5Encrypt(sb.ToString()).ToUpper());
            return new ThirdPayModel("http://api.xaapay.com/index/unifiedorder", HttpType.Post, dic);
        }

        protected override bool CreateRecharge(out Recharge thrid)
        {
            thrid = null;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string[] keys = new[] { "callbacks", "appid", "pay_type", "success_ulr", "error_ulr", "out_trade_no", "amount" };
            foreach (string key in keys)
            {
                dic.Add(key, HttpContextAccessor.HttpContext.Request.Form[key]);
            }

            StringBuilder sb1 = new StringBuilder();
            foreach (string key in keys)
            {
                sb1.Append($"[{key}]=[{dic[key]}];");
            }

            if (!dic.ContainsKey("callbacks") || dic["callbacks"] != "CODE_SUCCESS")
            {
                LogHelper.Error(sb1.ToString());
                LogHelper.Error("易闪付code状态码不为CODE_SUCCESS");
                return false;
            }

            if (!dic.ContainsKey("out_trade_no") || string.IsNullOrEmpty(dic["out_trade_no"]))
            {
                LogHelper.Error(sb1.ToString());
                LogHelper.Error("易闪付无法找到主键");
                return false;
            }
            string out_trade_no = dic["out_trade_no"];
            thrid = (from t in Context.Recharges where t.OrderNo == out_trade_no select t)
                .FirstOrDefault();
            if (thrid?.MerchantsBankId == null)
            {
                LogHelper.Error(sb1.ToString());
                LogHelper.Error($"易闪付无法找到订单号");
                return false;
            }

            StringBuilder sb = new StringBuilder();
            foreach (string k in dic.Keys.OrderBy(t => t))
            {
                if (!string.IsNullOrEmpty(dic[k]))
                {
                    sb.Append($"{k}={dic[k]}&");
                }
            }

            sb.Append("key=" + thrid.MerchantsBank.MerchantsKey);

            if (HttpContextAccessor.HttpContext.Request.Form["sign"] != SecurityHelper.MD5Encrypt(sb.ToString().Trim()).ToUpper())
            {
                LogHelper.Error($"{sb.ToString().Trim()}");
                LogHelper.Error($"签名失败，我方签名[{SecurityHelper.MD5Encrypt(sb.ToString().Trim()).ToUpper()}]，对方签名[{ HttpContextAccessor.HttpContext.Request.Form["sign"]}]");
                return false;
            }

            return true;
        }
    }
}