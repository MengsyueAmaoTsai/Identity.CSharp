using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Identity;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityForIdentityWeb(this IServiceCollection services)
    {
        services
            .AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl = "/users/login";
            })
            .AddInMemoryClients(InMemoryClients.CreateClients())
            .AddInMemoryIdentityResources(InMemoryIdentityResources.CreateIdentityResources())
            .AddInMemoryApiScopes(InMemoryApiScopes.CreateApiScopes())
            .AddInMemoryApiResources(InMemoryApiResources.CreateApiResources());

        return services;
    }

    public static IServiceCollection AddIdentityForTraderStudioWeb(this IServiceCollection services)
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
                options.Authority = "http://localhost:9999";
                options.ClientId = "RichillCapital.TraderStudio.Web";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("RichillCapital.Api");
                options.Scope.Add("offline_access");
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role",
                };
                options.RequireHttpsMetadata = false;
            });

        return services;
    }
}
