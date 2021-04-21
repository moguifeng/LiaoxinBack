﻿using AllLottery.IBusiness;
using AllLottery.Model;
using System.Linq;
using Zzb;

namespace AllLottery.Business
{
    public class BankService : BaseService, IBankService
    {
        public SystemBank[] GetSystemBanks()
        {
            return (from b in Context.SystemBanks where b.IsEnable orderby b.SortIndex select b).ToArray();
        }

        public PlayerBank[] GetPlayerBanks(int playerId)
        {
            return (from b in Context.PlayerBanks where b.IsEnable && b.PlayerId == playerId select b).ToArray();
        }

        public void AddPlayerBank(int playerId, int systemBankId, string name, string cardNumber)
        {
            if (name.Length <= 1)
            {
                throw new ZzbException("请输入正确的收款人");
            }

            if (cardNumber.Length <= 4)
            {
                throw new ZzbException("请输入正确的银行卡号");
            }

            var banks = GetPlayerBanks(playerId);
            if (banks != null && banks.Length >= 5)
            {
                throw new ZzbException("当前用户设置收款银行不能超过5张");
            }

            var exist = from b in Context.SystemBanks where b.IsEnable && b.SystemBankId == systemBankId select b;
            if (!exist.Any())
            {
                throw new ZzbException("参数错误，未找到系统银行");
            }

            var existBank = from b in Context.PlayerBanks where b.CardNumber == cardNumber && b.PlayerId == playerId select b;
            if (existBank.Any())
            {
                throw new ZzbException("已经存在相同卡号，无法添加");
            }

            Context.PlayerBanks.Add(new PlayerBank(playerId, systemBankId, cardNumber, name));

            Context.SaveChanges();
        }

        public MerchantsBank[] GetMerchantsBanks(bool isApp)
        {
            if (isApp)
            {
                return (from b in Context.MerchantsBanks where b.IsEnable && b.IsUseful && b.MerchantsBankShowType != MerchantsBankShowType.Pc orderby b.IndexSort select b)
                    .ToArray();
            }
            else
            {
                return (from b in Context.MerchantsBanks where b.IsEnable && b.IsUseful && b.MerchantsBankShowType != MerchantsBankShowType.App orderby b.IndexSort select b)
                    .ToArray();
            }

        }

        public MerchantsBank GetMerchantsBank(int id)
        {
            return (from b in Context.MerchantsBanks where b.MerchantsBankId == id select b)
                .FirstOrDefault();
        }
    }
}