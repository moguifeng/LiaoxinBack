using Microsoft.AspNetCore.Http;
using Zzb.Mvc;

namespace Liaoxin.Business.Socket
{
    public class PlayerSocketMiddleware : BaseServiceSocketMiddleware
    {
        public PlayerSocketMiddleware(RequestDelegate next) : base(next)
        {
        }

        public override string Path => "Player";

        public override string CookiesName => ".AspNetCore.Zzb";

        public override string SchemeName => "Zzb";
    }
}