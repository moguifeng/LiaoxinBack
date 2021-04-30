using Liaoxin.Model;
using Liaoxin.ViewModel;
using Microsoft.AspNetCore.Http.Internal;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Zzb.Common;
using Zzb.ZzbLog;

namespace Liaoxin.Business.ThirdPay
{
    public class YiDianFuThirdPay : BaseThirdPay
    {
        private string _notifyUrl => HttpContextAccessor.HttpContext.Request.GetDefaultUrl() + "api/ThirdPay/YiDianFuCallBack";

        protected override ThirdPayModel ThirdPayUrl(Recharge recharge, decimal money, string url, MerchantsBank bank)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("notify_url", _notifyUrl);
            dic.Add("return_url", HttpUtility.UrlDecode(url));
            dic.Add("method", bank.Aisle);
            dic.Add("app_id", bank.MerchantsNumber);
            dic.Add("out_trade_no", recharge.OrderNo);
            dic.Add("total_amount", (money * 100).ToString());
            dic.Add("subject", "充值");
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
            return new ThirdPayModel("http://www.1hhm.com/unifiedorder", HttpType.Post, dic);

        }

        protected override bool CreateRecharge(out Recharge thrid)
        {
            thrid = null;
            HttpContextAccessor.HttpContext.Request.EnableRewind();
            HttpContextAccessor.HttpContext.Request.Body.Position = 0;
            var buffer = new MemoryStream();
            HttpContextAccessor.HttpContext.Request.Body.CopyTo(buffer);
            buffer.Position = 0;
            StreamReader reader = new StreamReader(buffer);
            var s = reader.ReadToEnd();
            Dictionary<string, string> dic = JsonHelper.Json<Dictionary<string, string>>(s);
            if (dic["status"] != "1")
            {
                LogHelper.Error($"一点付status状态码不为1，是{dic["status"]}");
                return false;
            }

            thrid = (from r in Context.Recharges where r.OrderNo == dic["out_trade_no"] select r).FirstOrDefault();
            if (thrid == null)
            {
                LogHelper.Error($"一点付找不到订单[{dic["out_trade_no"]}]");
                return false;
            }

            StringBuilder sb = new StringBuilder();
            string[] no = new[] { "sign", "bankcode", "client_ip" };
            foreach (string k in dic.Keys.OrderBy(t => t))
            {
                if (!no.Contains(k))
                {
                    sb.Append($"{k}={dic[k]}&");
                }
            }

            //sb.Append("key=" + thrid.MerchantsBank.MerchantsKey);

            //if (dic["sign"] != SecurityHelper.MD5Encrypt(sb.ToString().Trim()).ToUpper())
            //{
            //    LogHelper.Error($"{sb.ToString().Trim()}");
            //    LogHelper.Error($"签名失败，我方签名[{SecurityHelper.MD5Encrypt(sb.ToString().Trim()).ToUpper()}]，对方签名[{ dic["sign"]}]");
            //    return false;
            //}

            return true;
        }
    }
}