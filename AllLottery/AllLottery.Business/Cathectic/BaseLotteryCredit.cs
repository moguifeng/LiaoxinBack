using AllLottery.Model;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;

namespace AllLottery.Business.Cathectic
{
    public abstract class BaseLotteryCredit<T> : BaseCathectic where T : BaseCredit
    {
        private static Dictionary<string, Dictionary<string, BaseCathectic>> _cache = new Dictionary<string, Dictionary<string, BaseCathectic>>();

        public override bool Valid(string datas)
        {
            return CreateShuangMianPan(datas).Valid(datas);
        }

        public override string[] SplitDatas(string datas)
        {
            return CreateShuangMianPan(datas).SplitDatas(datas);
        }

        public override long IsWinEach(string data, int[] openDatas)
        {
            return CreateShuangMianPan(data).IsWinEach(data.Split(':')[1], openDatas);
        }

        public override void CheckBetMoney(int playerId, int detailId, string issuseNo, decimal betMoney, string value, int betCount)
        {
            CreateShuangMianPan(value).CheckBetMoney(playerId, detailId, issuseNo, betMoney, value, betCount);
        }

        private BaseCathectic CreateShuangMianPan(string data)
        {
            string key = data.Split(":")[0];
            lock (_cache)
            {
                string fullName = typeof(T).FullName;
                if (!_cache.ContainsKey(fullName))
                {
                    _cache.Add(fullName, new Dictionary<string, BaseCathectic>());
                }
                if (!_cache[fullName].Any())
                {
                    foreach (Type type in typeof(BaseCathectic).Assembly.GetTypes())
                    {
                        if ((type.IsSubclassOf(typeof(T)) || type.FullName == typeof(T).FullName) && !type.IsAbstract)
                        {
                            var zhenghe = typeof(BaseCathectic).Assembly.CreateInstance(type.FullName) as T;
                            // ReSharper disable once PossibleNullReferenceException
                            _cache[fullName].Add(zhenghe.Key, zhenghe);
                        }
                    }
                }

                if (_cache[fullName].ContainsKey(key) && _cache[fullName][key] is T)
                {
                    return _cache[fullName][key];
                }

                return null;
            }
        }

        public override decimal CalculateWinMoney(Bet bet, decimal maxRate, int[] openDatas)
        {
            return CreateShuangMianPan(bet.BetNo).CalculateWinMoney(bet, maxRate, openDatas);
        }
    }
}