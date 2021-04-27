using Liaoxin.Business.ThirdPay;
using Microsoft.AspNetCore.Mvc;
using Zzb.Mvc;
using Zzb.ZzbLog;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThirdPayController : BaseApiController
    {
        public NiuPayThirdPay NiuPayThirdPay { get; set; }

        public YiShanFuThirdPay YiShanFuThirdPay { get; set; }

        public GlobalPayThirdPay GlobalPayThirdPay { get; set; }

        public YiDianFuThirdPay YiDianFuThirdPay { get; set; }

        public XingHuoThirdPay XingHuoThirdPay { get; set; }

        [HttpGet("NiuPayCallBack")]
        public string NiuPayCallBack()
        {
            NiuPayThirdPay.CallBack();
            return "SUCCESS";
        }

        [HttpPost("YiShanFuCallBack")]
        public string YiShanFuCallBack()
        {
            YiShanFuThirdPay.CallBack();
            return "SUCCESS";
        }

        [HttpPost("GlobalPayCallBack")]
        public string GlobalPayCallBack()
        {
            //GlobalPayThirdPay.CallBack();
            return "SUCCESS";
        }

        [HttpPost("YiDianFuCallBack")]
        public string YiDianFuCallBack()
        {
            try
            {
                YiDianFuThirdPay.CallBack();
                return "SUCCESS";
            }
            catch (System.Exception e)
            {
                LogHelper.Error("错误", e);
                return "error";
            }

        }

        [HttpPost("XingHuoCallBack")]
        public string XingHuoCallBack()
        {
            try
            {
                XingHuoThirdPay.CallBack();
                return "SUCCESS";
            }
            catch (System.Exception e)
            {
                LogHelper.Error("错误", e);
                return "error";
            }
        }
    }
}