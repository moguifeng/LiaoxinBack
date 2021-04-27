using Liaoxin.IBusiness;
using Liaoxin.Model;
using System.Linq;
using Zzb;

//作废啦
namespace Liaoxin.Business
{
    //public class PlayerLotteryTypeService : BaseService, IPlayerLotteryTypeService
    //{
    //    public void AddPlayerLotteryType(int playerId, int lotteryTypeId)
    //    {
    //        var sql = from p in Context.PlayerLotteryTypes
    //                  where p.PlayerId == playerId && p.LotteryTypeId == lotteryTypeId
    //                  select p;
    //        if (sql.Any())
    //        {
    //            throw new ZzbException("该彩种已经添加");
    //        }
    //        Context.PlayerLotteryTypes.Add(new PlayerLotteryType()
    //        {
    //            PlayerId = playerId,
    //            LotteryTypeId = lotteryTypeId
    //        });
    //        Context.SaveChanges();
    //    }

    //    public void DeletePlayerLotteryType(int playerId, int lotteryTypeId)
    //    {
    //        Context.PlayerLotteryTypes.RemoveRange(from p in Context.PlayerLotteryTypes
    //                                               where p.PlayerId == playerId && p.LotteryTypeId == lotteryTypeId
    //                                               select p);
    //        Context.SaveChanges();
    //    }

    //    public PlayerLotteryType[] GetPlayerLotteryTypes(int playerId)
    //    {
    //        return (from p in Context.PlayerLotteryTypes where p.PlayerId == playerId orderby p.CreateTime select p)
    //            .ToArray();
    //    }
    //}
}