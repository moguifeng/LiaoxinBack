using AllLottery.Business.Config;
using AllLottery.IBusiness;
using AllLottery.Model;
using AllLottery.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Web;
using Zzb;
using Zzb.Common;
using Zzb.ZzbLog;

namespace AllLottery.Business.ThirdPay
{
    public abstract class BaseThirdPay
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }

        public LotteryContext Context { get; set; }

        public IRechargeService RechargeService { get; set; }

        public static ThirdPayModel CreateThirdPayUrl(int playerId, decimal money, int bankId, string url, IHttpContextAccessor httpContextAccessor)
        {
            using (var context = LotteryContext.CreateContext())
            {
                var bank = (from b in context.MerchantsBanks where b.MerchantsBankId == bankId select b).FirstOrDefault();
                if (bank == null || bank.IsEnable == false)
                {
                    throw new ZzbException("该收款通道不存在");
                }
                if (bank.Type != MerchantsBankTypeEnum.ThirdPay)
                {
                    throw new ZzbException("该收款通道不是第三方支付");
                }
                if (string.IsNullOrEmpty(bank.MerchantsNumber))
                {
                    throw new ZzbException("未设置商户编号，请联系网站管理员");
                }
                if (!string.IsNullOrEmpty(url))
                {
                    url = HttpUtility.UrlEncode(url, System.Text.Encoding.UTF8);
                }
                BaseThirdPay thirdPay = null;
                switch (bank.ThirdPayMerchantsType)
                {
                    case ThirdPayMerchantsType.NiuPay:
                        thirdPay = httpContextAccessor.HttpContext.RequestServices.GetService(typeof(NiuPayThirdPay)) as NiuPayThirdPay;
                        break;
                    case ThirdPayMerchantsType.YiShanFu:
                        thirdPay = httpContextAccessor.HttpContext.RequestServices.GetService(typeof(YiShanFuThirdPay)) as YiShanFuThirdPay;
                        break;
                    case ThirdPayMerchantsType.GlobalPay:
                        thirdPay = httpContextAccessor.HttpContext.RequestServices.GetService(typeof(GlobalPayThirdPay)) as GlobalPayThirdPay;
                        break;
                    case ThirdPayMerchantsType.YiDianFuThirdPay:
                        thirdPay = httpContextAccessor.HttpContext.RequestServices.GetService(typeof(YiDianFuThirdPay)) as YiDianFuThirdPay;
                        break;
                    case ThirdPayMerchantsType.XunJuHe:
                        thirdPay = httpContextAccessor.HttpContext.RequestServices.GetService(typeof(XunJuHeThirdPay)) as XunJuHeThirdPay;
                        break;
                    case ThirdPayMerchantsType.TiaoMa:
                        thirdPay = httpContextAccessor.HttpContext.RequestServices.GetService(typeof(TiaoMaThirdPay)) as TiaoMaThirdPay;
                        break;
                    case ThirdPayMerchantsType.XingHuo:
                        thirdPay = httpContextAccessor.HttpContext.RequestServices.GetService(typeof(XingHuoThirdPay)) as XingHuoThirdPay;
                        break;
                    default:
                        break;
                }
                var recharge = thirdPay?.AddThirdPay(RandomHelper.GetRandom("R"), playerId, money, bankId, context);
                return thirdPay?.ThirdPayUrl(recharge, money, url, bank);
            }

        }

        protected virtual Recharge AddThirdPay(string order, int playerId, decimal money, int bankId, LotteryContext context)
        {
            Recharge thirdPay = new Recharge() { PlayerId = playerId, Money = money, MerchantsBankId = bankId, State = RechargeStateEnum.Wait, IsEnable = false, OrderNo = order };
            context.Recharges.Add(thirdPay);
            context.SaveChanges();
            return thirdPay;
        }

        protected abstract ThirdPayModel ThirdPayUrl(Recharge recharge, decimal money, string url, MerchantsBank bank);

        public void CallBack()
        {
            Recharge thrid = null;
            if (!CreateRecharge(out thrid)) throw new ZzbException("验证失败");

            if (thrid.State != RechargeStateEnum.Ok)
            {
                thrid.State = RechargeStateEnum.Ok;
                thrid.IsEnable = true;
              //  thrid.Player.AddMoney(thrid.Money, CoinLogTypeEnum.Recharge, thrid.RechargeId, out var log, $"第三方充值自动到账，订单号为[{thrid.OrderNo}]");
              //  Context.CoinLogs.Add(log);
                //AcvitityServer.ActivityHandle(thrid);
            //    RechargeService.ReceiveGift(thrid, Context);
                var rate = BaseConfig.CreateInstance(SystemConfigEnum.ConsumerWithdrawRate).DecimalValue;
                if (rate > 0)
                {
                    rate = rate / 100;
                 //   thrid.Player.UpdateLastBetMoney(thrid.Money * rate);
                }
                Context.SaveChanges();
                LogHelper.Debug($"自动到账成功[{thrid.RechargeId}]");
            }
        }

        protected abstract bool CreateRecharge(out Recharge thrid);
    }


}