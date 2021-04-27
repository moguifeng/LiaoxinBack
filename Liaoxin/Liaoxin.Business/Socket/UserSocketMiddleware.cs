using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Zzb.Mvc;

namespace Liaoxin.Business.Socket
{
    public class UserSocketMiddleware : BaseServiceSocketMiddleware
    {
        public override string Path => "User";

        public override string CookiesName => ".AspNetCore.Cookies";

        public override string SchemeName => CookieAuthenticationDefaults.AuthenticationScheme;

        public UserSocketMiddleware(RequestDelegate next) : base(next)
        {
        }
    }
}