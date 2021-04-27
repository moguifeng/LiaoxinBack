
using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Liaoxin.Business
{
    public class CoinLogService : BaseService, ICoinLogService
    {
        public CoinLog[] GetCoinLogs(int playerId, int index, int size, out int total, DateTime? begin, DateTime? end, CoinLogTypeEnum[] types)
        {
            var sql = from c in Context.CoinLogs where c.IsEnable && c.PlayerId == playerId select c;

            if (begin != null)
            {
                sql = sql.Where(t => t.CreateTime >= begin.Value);
            }

            if (end != null)
            {
                sql = sql.Where(t => t.CreateTime < end.Value);
            }

            if (types != null && types.Any())
            {
                sql = sql.Where(t => types.Contains(t.Type));
            }
            total = sql.Count();
            return sql.OrderByDescending(t => t.CreateTime).Skip(size * (index - 1)).Take(size).ToArray();
        }

        public CoinLog[] GetTeamCoinLogs(int playerId, int index, int size, out int total, DateTime? begin, DateTime? end, CoinLogTypeEnum[] types, string name)
        {
            var list = new List<int>();
            list.Add(playerId);
            var sql = from c in Context.CoinLogs where c.IsEnable && list.Contains(c.PlayerId) select c;

            if (begin != null)
            {
                sql = sql.Where(t => t.CreateTime >= begin.Value);
            }

            if (end != null)
            {
                var dt = end.Value.AddDays(1);
                sql = sql.Where(t => t.CreateTime < dt);
            }

            if (types != null && types.Any())
            {
                sql = sql.Where(t => types.Contains(t.Type));
            }

            if (!string.IsNullOrEmpty(name))
            {
                sql = sql.Where(t => t.Player.Name.Contains(name));
            }
            total = sql.Count();
            return sql.OrderByDescending(t => t.CreateTime).Skip(size * (index - 1)).Take(size).ToArray();
        }
    }
}