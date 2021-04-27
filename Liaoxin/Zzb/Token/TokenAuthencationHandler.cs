using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Zzb.Context;

namespace Zzb.Token
{
    public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationOptions>
    {
        public TokenAuthenticationHandler(IOptionsMonitor<TokenAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

 
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (UserContext.Current.IsAuthenticated)
            {                
                var identity = new ClaimsIdentity(TokenDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Sid, UserContext.Current.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, UserContext.Current.Name));
                var principal = new ClaimsPrincipal(identity);
                return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal,
                    new AuthenticationProperties(), TokenDefaults.AuthenticationScheme)));
            }

            return Task.FromResult(AuthenticateResult.Fail("Not Authenticated"));
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
           
            await Task.Run(() =>
            {
                Response.StatusCode = 401;
            });
            
        }
    }
}
