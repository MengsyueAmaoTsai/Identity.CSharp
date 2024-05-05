using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Identity;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityForIdentityWeb(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddTraderStudioWebIdentity(this IServiceCollection services)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.Authority = "http://127.0.0.1:9999";
                options.ClientId = "RichillCapital.TraderStudio.Web";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("RichillCapital.Api");
                options.Scope.Add("offline_access");
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.TokenValidationParameters.NameClaimType = "name";
                options.RequireHttpsMetadata = false;
            });

        return services;
    }
}