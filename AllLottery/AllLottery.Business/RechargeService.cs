
using AllLottery.Business.ThirdPay;
using AllLottery.IBusiness;
using AllLottery.Model;
using AllLottery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;

namespace AllLottery.Business
{
    public class RechargeService : BaseService, IRechargeService
    {
        public IPlayerService PlayerService { get; set; }

        public IMessageService MessageService { get; set; }

        public void AddRecharge(int bankId, int playerId, decimal money)
        {
            CheckRechager(bankId, playerId, money, out var player, out var bank);


            Context.Recharges.Add(new Recharge(playerId, money, bankId));
            Context.SaveChanges();
            MessageService.AddAllUserMessage($"用户[{player.Name}]申请充值[{money}]元", MessageTypeEnum.Message);
        }

        public ThirdPayModel AddThirdPayRecharge(int bankId, int playerId, decimal money, string url)
        {
            CheckRechager(bankId, playerId, money, out var player, out var bank);
            if (bank.Type != MerchantsBankTypeEnum.ThirdPay)
            {
                throw new ZzbException("该收款银行不是第三方商家");
            }
            return BaseThirdPay.CreateThirdPayUrl(playerId, money, bankId, url, HttpContextAccessor);
        }

        private void CheckRechager(int bankId, int playerId, decimal money, out Player player, out MerchantsBank bank)
        {
            player = (from p in Context.Players where p.PlayerId == playerId select p)
                .FirstOrDefault();
            if (player == null)
            {
                throw new ZzbException("不存在该用户");
            }

            //if (player.Type == PlayerTypeEnum.TestPlay)
            //{
            //    throw new ZzbException("试用玩家不能充值");
            //}

            if (IsExistRecharges(playerId))
            {
                throw new ZzbException("当前用户还存在未处理的充值订单，请稍后再试");
            }

            bank = (from b in Context.MerchantsBanks
                    where b.IsEnable && b.MerchantsBankId == bankId
                    select b).FirstOrDefault();
            if (bank == null || !bank.IsEnable)
            {
                throw new ZzbException("无法找到该收款银行");
            }

            if (!bank.IsUseful)
            {
                throw new ZzbException("该收款银行目前正在维护中，如有不便敬请原谅");
            }

            if (money < bank.Min)
            {
                throw new ZzbException($"亲，最低充值金额为{bank.Min}元哦");
            }

            if (money > bank.Max)
            {
                throw new ZzbException($"亲，最高充值金额为{bank.Max}元哦");
            }
        }

        public bool IsExistRecharges(int playerid)
        {
            var exist = from r in Context.Recharges
                        where r.IsEnable && r.PlayerId == playerid && r.State == RechargeStateEnum.Wait && r.MerchantsBank.Type != MerchantsBankTypeEnum.ThirdPay
                        select r;
            return exist.Any();
        }

        public Recharge[] GetRecharges(int id, int size, int index, out int total)
        {
            var recharges = from r in Context.Recharges where r.IsEnable && r.PlayerId == id orderby r.CreateTime descending select r;
            total = recharges.Count();
            return recharges.Skip((index - 1) * size).Take(size).ToArray();
        }

        public Recharge[] GetTeamRecharges(int id, string name, DateTime? begin, DateTime? end, int size, int index, out int total)
        {
            // var list = BaseReport.GetTeamPlayerIdsWhitoutSelf(id);
            var list = new List<int>();
            list.Add(id);
            var recharges = from r in Context.Recharges where r.IsEnable && list.Contains(r.PlayerId) select r;
            if (!string.IsNullOrEmpty(name))
            {
                recharges = recharges.Where(t => t.Player.Name.Contains(name));
            }
            if (begin != null)
            {
                recharges = recharges.Where(t => t.CreateTime >= begin.Value);
            }
            if (end != null)
            {
                var dt = end.Value.AddDays(1);
                recharges = recharges.Where(t => t.CreateTime < dt);
            }
            total = recharges.Count();
            return recharges.OrderByDescending(t => t.CreateTime).Skip((index - 1) * size).Take(size).ToArray();
        }

    }
}