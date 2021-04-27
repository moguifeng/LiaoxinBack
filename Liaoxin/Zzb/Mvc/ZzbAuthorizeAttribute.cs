

using Microsoft.AspNetCore.Authorization;

namespace Zzb.Mvc
{
    public class ZzbAuthorizeAttribute : AuthorizeAttribute
    {
        public const string ZzbAuthenticationScheme = "Zzb";

        public ZzbAuthorizeAttribute()
        {
            this.AuthenticationSchemes = ZzbAuthenticationScheme;
        }

       
    }
}