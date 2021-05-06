using Liaoxin.Business.Config;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Zzb;
using Zzb.Common;

namespace Liaoxin.Business
{
    //public class WithdrawService : BaseService, IWithdrawService
    //{
    //    public IMessageService MessageService { get; set; }

    //    public void AddWithdraw(decimal money, int playerBankId, int playerId, string password)
    //    {
    //        if (money <= 0)
    //        {
    //            throw new ZzbException("提现金额必须大于0");
    //        }

    //        if (money % 100 > 0)
    //        {
    //            throw new ZzbException("提现金额必须是整百");
    //        }

    //        var playerBank = (from b in Context.PlayerBanks where b.PlayerBankId == playerBankId select b).FirstOrDefault();

    //        if (playerBank == null)
    //        {
    //            throw new ZzbException("不存在该银行卡");
    //        }

    //        if (playerBank.PlayerId != playerId)
    //        {
    //            throw new ZzbException("该银行卡不属于当前用户");
    //        }

    //        if (playerBank.Player.IsFreeze)
    //        {
    //            throw new ZzbException("该用户已被冻结");
    //        }
     
    //        if (!playerBank.Player.CanWithdraw)
    //        {
    //            throw new ZzbException("申请提现失败:银行系统维护中，给您带来不便敬请谅解！");
    //        }

    //        if (DateTime.Now > DateTime.Today.AddHours(int.Parse(BaseConfig.CreateInstance(SystemConfigEnum.WithdrawEnd).Value)) || DateTime.Now < DateTime.Today.AddHours(int.Parse(BaseConfig.CreateInstance(SystemConfigEnum.WithdrawBegin).Value)))
    //        {
    //            throw new ZzbException($"请于每天的{BaseConfig.CreateInstance(SystemConfigEnum.WithdrawBegin).Value}:00-{BaseConfig.CreateInstance(SystemConfigEnum.WithdrawEnd).Value}:00申请提现，多谢您的谅解");
    //        }

    //        var exist = from b in Context.Withdraws
    //                    where b.Status == WithdrawStatusEnum.Wait && b.PlayerBank.PlayerId == playerBank.PlayerId && b.IsEnable
    //                    select b;
    //        if (exist.Any())
    //        {
    //            throw new ZzbException("用户存在未处理的提现订单，请稍后再试");
    //        }

    //        if (playerBank.Player.CoinPassword != SecurityHelper.Encrypt(password))
    //        {
    //            throw new ZzbException("提款密码错误");
    //        }

    //        if (money > playerBank.Player.Coin)
    //        {
    //            throw new ZzbException("当前用户余额不足");
    //        }

    //        if (money > BaseConfig.CreateInstance(SystemConfigEnum.MaxWithdraw).DecimalValue)
    //        {
    //            throw new ZzbException($"提现金额不能超过{BaseConfig.CreateInstance(SystemConfigEnum.MaxWithdraw).Value}");
    //        }

    //        if (money < BaseConfig.CreateInstance(SystemConfigEnum.MinWithdraw).DecimalValue)
    //        {
    //            throw new ZzbException($"提现金额不能低于{BaseConfig.CreateInstance(SystemConfigEnum.MinWithdraw).Value}");
    //        }

 

    //        CheckPlayerWithdrawCount(playerId);

    //        var withdraw = new Withdraw(playerBankId, money);
    //        playerBank.Player.Coin -= money;
    //        playerBank.Player.FCoin += money;
    //        playerBank.Player.UpdateTime = DateTime.Now;
    //        //var coinLog = new CoinLog(playerBank.Player.PlayerId, -money, playerBank.Player.Coin, money, playerBank.Player.FCoin, CoinLogTypeEnum.ApplyWithdraw, withdraw.WithdrawId, $"申请提现[{money}]，订单号为[{withdraw.OrderNo}]");
    //        Context.Withdraws.Add(withdraw);
    //        //Context.CoinLogs.Add(coinLog);
    //        Context.SaveChanges();
    //        MessageService.AddAllUserMessage($"玩家[{playerBank.Player.Name}]申请提现[{money}]元", MessageTypeEnum.LongMessage);
    //    }

    //    /// <summary>
    //    /// 检查用户的提现次数
    //    /// </summary>
    //    private void CheckPlayerWithdrawCount(int id)
    //    {
    //        //系统配置的最低消费比例
    //        var count = BaseConfig.CreateInstance(SystemConfigEnum.WithdrawCount).DecimalValue;
    //        if (count < 0)
    //        {
    //            return;
    //        }

    //        var playeCount = (from w in Context.Withdraws
    //                          where w.Status == WithdrawStatusEnum.Ok && w.PlayerBank.PlayerId == id && w.CreateTime > DateTime.Today
    //                          select w).Count();
    //        if (playeCount >= count)
    //        {
    //            throw new ZzbException("已超出当天提现次数.每天只能提现" + count + "次,请明天再提现.");
    //        }
    //    }

    //    public Withdraw[] GetWithdraws(int id, int index, int size, out int total)
    //    {
    //        var sql = from w in Context.Withdraws
    //                  where w.IsEnable && w.PlayerBank.PlayerId == id
    //                  orderby w.CreateTime descending
    //                  select w;
    //        total = sql.Count();
    //        return sql.Skip((index - 1) * size).Take(size).ToArray();
    //    }

    //    public Withdraw[] GetTeamWithdraws(int id, string name, DateTime? begin, DateTime? end, int index, int size, out int total)
    //    {
    //        var list = new List<int>();
    //        list.Add(id);
    //        var sql = from w in Context.Withdraws
    //                  where w.IsEnable && list.Contains(w.PlayerBank.PlayerId)
    //                  select w;
    //        if (!string.IsNullOrEmpty(name))
    //        {
    //            sql = sql.Where(t => t.PlayerBank.Player.Name.Contains(name));
    //        }
    //        if (begin != null)
    //        {
    //            sql = sql.Where(t => t.CreateTime >= begin.Value);
    //        }
    //        if (end != null)
    //        {
    //            var dt = end.Value.AddDays(1);
    //            sql = sql.Where(t => t.CreateTime < end.Value);
    //        }
    //        total = sql.Count();
    //        return sql.OrderByDescending(t => t.CreateTime).Skip((index - 1) * size).Take(size).ToArray();
    //    }
    //}
}