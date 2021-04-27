using Liaoxin.IBusiness;
using Liaoxin.Model;
using System.Linq;


//作废啦
namespace Liaoxin.Business
{
    //public class LotteryService : BaseService, ILotteryService
    //{
    //    public LotteryType[] GetAllLotteryTypes()
    //    {
    //        return (from p in Context.LotteryTypes where p.IsEnable && !p.IsStop orderby p.SortIndex select p).ToArray();
    //    }

    //    public LotteryType GetLotteryType(int id)
    //    {
    //        return (from p in Context.LotteryTypes where p.LotteryTypeId == id select p)
    //            .FirstOrDefault();
    //    }

    //    public LotteryData[] GetNewDatas(int id, int size)
    //    {
    //        return (from d in Context.LotteryDatas
    //            where d.LotteryTypeId == id && d.IsEnable
    //            orderby d.Time descending
    //            select d).Take(size).ToArray();
    //    }
    //}
}