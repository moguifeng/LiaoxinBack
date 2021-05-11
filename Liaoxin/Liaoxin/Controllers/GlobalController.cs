using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Zzb.Common;

namespace Liaoxin.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class GlobalController : ControllerBase
    //{
    //    [HttpGet]
    //    public ActionResult<object> Get(decimal money)
    //    {
    //        var url = "http://baidu.com";
    //        var _notifyUrl = "http://baidu.com";
    //        var order = RandomHelper.GetRandom("T");
    //        Dictionary<string, string> dic = new Dictionary<string, string>();
    //        dic.Add("pickupUrl", HttpUtility.UrlDecode(url));
    //        dic.Add("receiveUrl", HttpUtility.UrlDecode(_notifyUrl));
    //        dic.Add("signType", "MD5");
    //        dic.Add("orderNo", order);
    //        dic.Add("orderAmount", money.ToString());
    //        dic.Add("orderCurrency", "CNY");
    //        dic.Add("customerId", "15687957317");
    //        StringBuilder sb = new StringBuilder();
    //        sb.Append(HttpUtility.UrlDecode(url));
    //        sb.Append(HttpUtility.UrlDecode(_notifyUrl));
    //        sb.Append("MD5");
    //        sb.Append(order);
    //        sb.Append(money.ToString());
    //        sb.Append("CNY");
    //        sb.Append("15687957317");
    //        sb.Append("3uyrGiJ2BGcn8vQf");
    //        dic.Add("sign", SecurityHelper.MD5Encrypt(sb.ToString()));
    //        dic.Add("crmNo", "sf8DwJDu");
    //        return dic;
    //    }
    //}
}