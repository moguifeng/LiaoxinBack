using Liaoxin.Model;
using Liaoxin.ViewModel;
using Zzb.Common;

namespace Liaoxin.Business.ThirdPay
{
    public class XunJuHeThirdPay : BaseThirdPay
    {
        private string _notifyUrl => HttpContextAccessor.HttpContext.Request.GetDefaultUrl() + "api/ThirdPay/XunJuHeCallBack";

        protected override ThirdPayModel ThirdPayUrl(Recharge recharge, decimal money, string url, MerchantsBank bank)
        {
            string paramer =
                $"parter={bank.MerchantsNumber}&type={bank.Aisle}&value={money}&orderid={recharge.OrderNo}&callbackurl={_notifyUrl}";
            paramer += $"&sign={SecurityHelper.MD5Encrypt(paramer + bank.MerchantsKey)}";
            return new ThirdPayModel("https://openapi.xunjuhe.com/index.aspx?" + paramer);
        }

        protected override bool CreateRecharge(out Recharge thrid)
        {
            throw new System.NotImplementedException();
        }
    }
}