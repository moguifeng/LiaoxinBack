using AllLottery.IBusiness;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Zzb;
using Zzb.Common;
using Zzb.Mvc;

namespace AllLottery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ZzbAuthorize]
    public class BiController : BaseApiController
    {
        public IBiService BiService { get; set; }

        [HttpGet("RedirectBi")]
        public ServiceResult RedirectBi(int type)
        {
            return Json(() =>
            {
                var url = BiService.CreatePlatformUrl(UserId, type);
                return ObjectResult(url);
            }, "跳转BI失败");
        }

        [HttpPost("Transfer")]
        public ServiceResult Transfer(BiTransfer model)
        {
            return Json(() =>
            {
                BiService.Transfer(UserId, model.Type, model.Money);
                return new ServiceResult();
            }, "转账失败");
        }

        [HttpPost("CheckMoney")]
        public ServiceResult CheckMoney(BiCheckMoney model)
        {
            return Json(() => ObjectResult(BiService.CheckMoney(UserId, model.Type)), "查询失败");
        }

        [HttpPost("GetAllBi")]
        public ServiceResult GetAllBi()
        {
            return JsonObjectResult(from p in BiService.GetAllPlatforms() select new { Type = p.Type.ToDescriptionString(), Value = p.PlatformId, p.Name, p.Description, p.AffixId }, "获取所有BI游戏失败");
        }
    }

    public class BiTransfer
    {
        public decimal Money { get; set; }

        public int Type { get; set; }
    }

    public class BiCheckMoney
    {
        public int Type { get; set; }
    }
}