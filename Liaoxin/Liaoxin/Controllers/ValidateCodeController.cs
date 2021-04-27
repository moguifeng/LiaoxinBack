using Liaoxin.IBusiness;
using Microsoft.AspNetCore.Mvc;
using Zzb.Mvc;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateCodeController : BaseApiController
    {
        public IValidateCodeService ValidateCodeService { get; set; }

        [HttpGet("GetCode")]
        public IActionResult GetCode()
        {
            return new FileContentResult(ValidateCodeService.CreateCode(), "image/jpeg");
        }
    }
}