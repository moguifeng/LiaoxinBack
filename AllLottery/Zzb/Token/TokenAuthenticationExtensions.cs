using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Zzb.Context;

namespace Zzb.Token
{
    public static class TokenAuthenticationExtensions
    {
        public static AuthenticationBuilder AddToken(this AuthenticationBuilder builder)
        {
            return builder.AddToken(TokenDefaults.AuthenticationScheme);
        }

        public static AuthenticationBuilder AddToken(this AuthenticationBuilder builder, string authenticationScheme)
        {
            return builder.AddToken(authenticationScheme, null);
        }

        public static AuthenticationBuilder AddToken(this AuthenticationBuilder builder, string authenticationScheme, Action<TokenAuthenticationOptions> configureOptions)
        {
            return builder.AddToken(authenticationScheme, null, configureOptions);
        }

        public static AuthenticationBuilder AddToken(this AuthenticationBuilder builder, string authenticationScheme,
            string displayName, Action<TokenAuthenticationOptions> options)
        {
            builder.Services.AddSingleton<UserContextManager>();
            return builder.AddScheme<TokenAuthenticationOptions, TokenAuthenticationHandler>(authenticationScheme,
                displayName, options);
        }
    }
}
