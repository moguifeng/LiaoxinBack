using AllLottery.Business.Cathectic.LiuHeCai;
using AllLottery.Business.Config;
using AllLottery.Model;
using System.Linq;
using Zzb;

namespace AllLottery.Business.Cathectic
{
    public abstract class BaseCredit : BaseCathecticLiuHeCai
    {
        public abstract string[] Values { get; }

        public abstract string Key { get; }

        public override bool Valid(string data)
        {
            string[] datas = data.Split(':');
            if (datas.Length != 2)
            {
                return false;
            }

            if (datas[0] != Key)
            {
                return false;
            }

            if (!Values.Contains(datas[1]))
            {
                return false;
            }

            return true;
        }

        public override string[] SplitDatas(string datas)
        {
            return new string[] { datas };
        }

        public override void CheckBetMoney(int playerId, int detailId, string issuseNo, decimal betMoney, string value, int betCount)
        {
            if (MaxBetMoney(value.Split(":")[1]) != decimal.MaxValue && BaseConfig.HasValue(SystemConfigEnum.IsBoYue))
            {
                ExCheckBetMoney(playerId, detailId, issuseNo, betMoney, value.Split(":")[1]);
            }
            else
            {
                base.CheckBetMoney(playerId, detailId, issuseNo, betMoney, value, betCount);
            }
        }

        public virtual decimal MaxBetMoney(string value)
        {
            return decimal.MaxValue;
        }

        public virtual void ExCheckBetMoney(int playerId, int detailId, string issuseNo, decimal betMoney, string value)
        {
            using (var context = LotteryContext.CreateContext())
            {
                var detail = (from d in context.LotteryPlayDetails where d.LotteryPlayDetailId == detailId select d).FirstOrDefault();

                if (detail == null)
                {
                    throw new ZzbException($"玩法[{detailId }]不存在，请检查参数");
                }
                decimal existBetMoney = 0;
                var sql = from b in context.Bets
                          where b.PlayerId == playerId && b.LotteryPlayDetailId == detailId &&
                                b.LotteryIssuseNo == issuseNo && b.Status == BetStatusEnum.Wait && b.BetNo == value
                          select b;
                if (sql.Any())
                {
                    existBetMoney = sql.Sum(t => t.BetMoney);
                }
                if (existBetMoney + betMoney > MaxBetMoney(value))
                {
                    throw new ZzbException($"[{detail.LotteryPlayType.LotteryType.Name}]的[{detail.LotteryPlayType.Name}-{detail.Name}][{value}]不能超过每期最大投注额[{MaxBetMoney(value)}]");
                }
            }
        }
    }
}