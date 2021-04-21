using AllLottery.Model;
using System;
using System.Collections.Generic;

namespace AllLottery.Business.Cathectic.LiuHeCai.Credit
{
    public class LiuHeCaiCredit : BaseCathectic
    {
        private Dictionary<string, BaseLiuHeCaiCredit> _cache = new Dictionary<string, BaseLiuHeCaiCredit>();

        public override bool Valid(string data)
        {
            var datas = data.Split(":");
            if (datas.Length != 2)
            {
                return false;
            }
            var lhc = CreateLiuHeCaiCredit(datas[0]);
            if (lhc == null)
            {
                return false;
            }
            return lhc.Valid(data);
        }

        public override string[] SplitDatas(string datas)
        {
            return new string[] { datas };
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            return CreateLiuHeCaiCredit(data.Split(":")[0]).IsWinEach(data.Split(":")[1], openDatas);
        }

        public override void CheckBetMoney(int playerId, int detailId, string issuseNo, decimal betMoney, string value, int betCount)
        {
            CreateLiuHeCaiCredit(value.Split(":")[0]).CheckBetMoney(playerId, detailId, issuseNo, betMoney, value, betCount);
        }

        public override decimal CalculateWinMoney(Bet bet, decimal maxRate, int[] openDatas)
        {
            return CreateLiuHeCaiCredit(bet.BetNo.Split(":")[0]).CalculateWinMoney(bet, maxRate, openDatas);
        }

        private BaseLiuHeCaiCredit CreateLiuHeCaiCredit(string key)
        {
            lock (_cache)
            {
                if (_cache.ContainsKey(key))
                {
                    return _cache[key];
                }

                foreach (Type type in typeof(BaseLiuHeCaiCredit).Assembly.GetTypes())
                {
                    if (type.IsSubclassOf(typeof(BaseLiuHeCaiCredit)) && !type.IsAbstract)
                    {
                        var lhc = Activator.CreateInstance(type, key.Split("-")[0]) as BaseLiuHeCaiCredit;
                        if (!_cache.ContainsKey(lhc.Key))
                        {
                            _cache.Add(lhc.Key, lhc);
                        }
                    }
                }

                if (!_cache.ContainsKey(key))
                {
                    return null;
                }

                return _cache[key];
            }

        }
    }
}