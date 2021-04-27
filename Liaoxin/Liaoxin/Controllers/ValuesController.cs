using Liaoxin.Business.Socket;
using Liaoxin.IBusiness;
using Liaoxin.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Zzb.Mvc;

namespace Liaoxin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : BaseApiController
    {
        public IMessageService MessageService { get; set; }

       

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //MessageService.AddAllUserMessage("测试登录", MessageTypeEnum.Message);
            //  int a =  UserId;
            //BaseServiceSocketMiddleware.SendAllMessage<PlayerSocketMiddleware>("Hello World!", MessageTypeEnum.Message);
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpGet("Get123")]
        public ActionResult<IEnumerable<string>> Get123()
        {
            return new string[] { "value1", "value2" };
        }


        [HttpGet("Login")]
        public ActionResult<string> Login()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "123")
            };
            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties());
            return "OK";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
