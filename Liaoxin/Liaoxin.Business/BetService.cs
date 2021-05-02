
using Liaoxin.IBusiness;
using Liaoxin.Model;
using Liaoxin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.Common;

//作废啦
namespace Liaoxin.Business
{
    //public class BetService : BaseService, IBetService
    //{
    //    public ILotteryOpenTimeServer LotteryOpenTimeServer { get; set; }

    //    public List<BetMode> GetBetModes()
    //    {
    //        return Context.BetModes.Where(t => t.IsEnable && !t.IsCredit).ToList();
    //    }add

    //    public BetMode GetCreditBetMode()
    //    {
    //        return Context.BetModes.FirstOrDefault(t => t.IsCredit);
    //    }

    //    public void AddBettings(AddBettingViewModel[] models, int playerId)
    //    {
    //        using (var scope = Context.Database.BeginTransaction())
    //        {
    //            foreach (AddBettingViewModel model in models)
    //            {
    //                AddBetting(model, playerId);
    //            }
    //            scope.Commit();
    //        }
    //    }

    //    public void CancleBet(int betId, int playerId)
    //    {
    //        var bet = (from b in Context.Bets where b.BetId == betId select b).FirstOrDefault();
    //        if (bet == null)
    //        {
    //            throw new ZzbException("无法找到投注订单");
    //        }

    //        if (bet.Status != BetStatusEnum.Wait)
    //        {
    //            throw new ZzbException("该投注订单无法撤单");
    //        }

    //        if (bet.PlayerId != playerId)
    //        {
    //            throw new ZzbException("该投注订单不是当前登录用户发起，无法撤单");
    //        }

    //        var lotteryData = (from d in Context.LotteryDatas
    //                           where d.LotteryTypeId == bet.LotteryPlayDetail.LotteryPlayType.LotteryTypeId && d.IsEnable && d.Number == bet.LotteryIssuseNo
    //                           select d).FirstOrDefault();
    //        if (lotteryData != null)
    //        {
    //            throw new ZzbException($"[{bet.LotteryIssuseNo}]期已经开奖，无法撤单");
    //        }

    //        var betTime =
    //            LotteryOpenTimeServer.GetBetTime(bet.LotteryPlayDetail.LotteryPlayType.LotteryTypeId, bet.LotteryIssuseNo);

    //        if (betTime == null)
    //        {
    //            throw new ZzbException("无法读取期号信息，无法撤单");
    //        }

    //        if (DateTime.Now > betTime.OpenRiskTime)
    //        {
    //            throw new ZzbException($"彩种[{bet.LotteryPlayDetail.LotteryPlayType.Name}]的[{bet.LotteryIssuseNo}]期期已超过投注时间，无法撤单。");
    //        }

    //        bet.Status = BetStatusEnum.Revoke;
    //        bet.Update();
    //        bet.Player.AddMoney(bet.BetMoney, CoinLogTypeEnum.Revoke, bet.BetId, out var log, $"撤单投注单号[{bet.Order}]");
    //        bet.Player.Update();
    //        Context.CoinLogs.Add(log);

    //        var detail = (from d in Context.ChasingOrderDetails where d.BetId == betId select d).FirstOrDefault();
    //        if (detail != null && (detail.ChasingOrder.Status == ChasingStatus.Wait || detail.ChasingOrder.Status == ChasingStatus.Doing))
    //        {
    //            detail.ChasingOrder.Status = ChasingStatus.Stop;
    //            detail.ChasingOrder.Update();

    //            var sql = from c in Context.ChasingOrderDetails
    //                      where c.ChasingOrderId == detail.ChasingOrderId && c.IsEnable &&
    //                            c.BetId == null
    //                      select c.BetMoney;
    //            if (sql.Any())
    //            {
    //                var betMoney = sql.Sum();
    //                bet.Player.Coin += betMoney;
    //                bet.Player.FCoin -= betMoney;
    //                Context.CoinLogs.Add(new CoinLog(bet.PlayerId, betMoney, bet.Player.Coin, -betMoney, bet.Player.FCoin, CoinLogTypeEnum.ChasingBetCancle, detail.ChasingOrderId, $"追号[{detail.ChasingOrder.Order}]完成"));
    //            }
    //        }

    //        Context.SaveChanges();
    //    }

    //    private void AddBetting(AddBettingViewModel model, int playerId)
    //    {
    //        if (model.Times <= 0)
    //        {
    //            throw new ZzbException("倍数必须大于0");
    //        }

    //        var betMode = (from b in Context.BetModes where b.BetModeId == model.BetModeId select b).FirstOrDefault();
    //        if (betMode == null)
    //        {
    //            throw new ZzbException($"投注模式[{model.BetModeId}]不存在，请检查参数");
    //        }
    //        var detail = (from d in Context.LotteryPlayDetails where d.LotteryPlayDetailId == model.LotteryPlayDetailId select d).FirstOrDefault();
    //        if (detail == null)
    //        {
    //            throw new ZzbException($"玩法[{model.LotteryPlayDetailId}]不存在，请检查参数");
    //        }

    //        if (!detail.IsEnable || !detail.LotteryPlayType.IsEnable)
    //        {
    //            throw new ZzbException($"玩法[{detail.Name}]维护中，无法投注");
    //        }

    //        if (!detail.LotteryPlayType.LotteryType.IsEnable)
    //        {
    //            throw new ZzbException($"彩种[{detail.LotteryPlayType.LotteryType.Name}]不存在，无法投注");
    //        }
    //        if (detail.LotteryPlayType.LotteryType.IsStop)
    //        {
    //            throw new ZzbException($"彩种[{detail.LotteryPlayType.LotteryType.Name}]正在维护，无法投注");
    //        }

    //        var lotteryData = (from d in Context.LotteryDatas
    //                           where d.LotteryTypeId == detail.LotteryPlayType.LotteryTypeId && d.IsEnable && d.Number == model.LotteryIssuseNo
    //                           select d).FirstOrDefault();
    //        if (lotteryData != null)
    //        {
    //            throw new ZzbException($"[{model.LotteryIssuseNo}]期已经开奖，无法投注");
    //        }

    //        var betTime =
    //            LotteryOpenTimeServer.GetBetTime(detail.LotteryPlayType.LotteryTypeId, model.LotteryIssuseNo);

    //        if (betTime == null)
    //        {
    //            throw new ZzbException("期号不正确，请勿非法操作");
    //        }

    //        if (DateTime.Now > betTime.OpenRiskTime)
    //        {
    //            throw new ZzbException($"彩种[{detail.LotteryPlayType.Name}]的[{model.LotteryIssuseNo}]期期已超过投注时间。");
    //        }

    //        var player = (from p in Context.Players where p.PlayerId == playerId select p).FirstOrDefault();
    //        if (player == null || !player.IsEnable || player.IsFreeze)
    //        {
    //            throw new ZzbException("当前用户不存在");
    //        }

    //        var cathectic = BaseCathectic.CreateCathectic(detail.ReflectClass);

    //        var betCount = cathectic.CalculateBetCount(model.BetNo);

    //        if (betCount <= 0)
    //        {
    //            throw new ZzbException("注数不能为0");
    //        }

    //        var betMoney = betCount * model.Times * betMode.Money;

    //        cathectic.CheckBetMoney(playerId, detail.LotteryPlayDetailId, model.LotteryIssuseNo, betMoney, model.BetNo, betCount);

    //        if (betMoney > player.Coin)
    //        {
    //            throw new ZzbException("当前账户余额不足，无法投注");
    //        }

    //        player.Coin -= betMoney;

    //        var bet = new Bet(RandomHelper.GetRandom("B"), playerId, detail.LotteryPlayDetailId, model.LotteryIssuseNo, model.BetNo, betMoney, betCount, model.BetModeId, model.Times) { CreditRemark = model.CreditRemark };

    //        var betId = Context.Bets.Add(bet).Entity.BetId;

    //        var coinLog = new CoinLog(playerId, -betMoney, player.Coin, CoinLogTypeEnum.Bet, betId, $"投注彩种[{detail.LotteryPlayType.LotteryType.Name}][{model.LotteryIssuseNo}]期的[{detail.LotteryPlayType.Name}-{detail.Name}]玩法[{betMoney}]元");

    //        Context.CoinLogs.Add(coinLog);

    //        Context.SaveChanges();
    //    }

    //    public Bet[] GetBets(int playerId, int index, int size, out int total, bool? isCredit = null, int? lotteryTypeId = null, List<BetStatusEnum> status = null, string order = null)
    //    {
    //        var sql = from b in Context.Bets
    //                  where b.PlayerId == playerId && b.IsEnable
    //                  select b;
    //        if (isCredit != null)
    //        {
    //            if (isCredit.Value)
    //            {
    //                sql = sql.Where(t => !string.IsNullOrEmpty(t.CreditRemark));
    //            }
    //            else
    //            {
    //                sql = sql.Where(t => string.IsNullOrEmpty(t.CreditRemark));
    //            }
    //        }

    //        if (lotteryTypeId != null)
    //        {
    //            sql = sql.Where(t => t.LotteryPlayDetail.LotteryPlayType.LotteryTypeId == lotteryTypeId.Value);
    //        }

    //        if (status != null && status.Any())
    //        {
    //            sql = sql.Where(t => status.Contains(t.Status));
    //        }

    //        if (!string.IsNullOrEmpty(order))
    //        {
    //            sql = sql.Where(t => t.Order.Contains(order));
    //        }
    //        total = sql.Count();
    //        return sql.OrderByDescending(t => t.CreateTime).Skip((index - 1) * size).Take(size).ToArray();
    //    }

    //    public Bet GetBet(int betId, int playerId)
    //    {
    //        var bet =
    //            (from b in Context.Bets where b.BetId == betId && b.PlayerId == playerId select b).FirstOrDefault();
    //        if (bet == null)
    //        {
    //            throw new ZzbException("未找到该投注订单");
    //        }

    //        return bet;
    //    }

    //    public Bet[] GetTeamBets(int playerId, string name, DateTime? begin, DateTime? end, int index, int size, out int total)
    //    {
    //        var list = BaseReport.GetTeamPlayerIdsWhitoutSelf(playerId);
    //        list.Add(playerId);
    //        var sql = from b in Context.Bets
    //                  where list.Contains(b.PlayerId) && b.IsEnable
    //                  select b;
    //        if (!string.IsNullOrEmpty(name))
    //        {
    //            sql = sql.Where(t => t.Player.Name.Contains(name));
    //        }

    //        if (begin != null)
    //        {
    //            sql = sql.Where(t => t.CreateTime >= begin.Value);
    //        }
    //        if (end != null)
    //        {
    //            var dt = end.Value.AddDays(1);
    //            sql = sql.Where(t => t.CreateTime < dt);
    //        }
    //        total = sql.Count();
    //        return sql.OrderByDescending(t => t.CreateTime).Skip((index - 1) * size).Take(size).ToArray();
    //    }
    //}
}