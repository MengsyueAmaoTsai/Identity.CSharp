using Duende.IdentityServer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace RichillCapital.Identity;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityForIdentityWeb(this IServiceCollection services)
    {
        services
            .AddIdentityServer(options =>
            {
                options.UserInteraction.LoginUrl = "/users/login";

                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                // see https://docs.duendesoftware.com/identityserver/v5/basics/resources
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryClients(InMemoryClients.CreateClients())
            .AddInMemoryIdentityResources(InMemoryIdentityResources.CreateIdentityResources())
            .AddInMemoryApiScopes(InMemoryApiScopes.CreateApiScopes())
            .AddInMemoryApiResources(InMemoryApiResources.CreateApiResources());

        var authenticationBuilder = services.AddAuthentication();

        authenticationBuilder
            .AddMicrosoftAccount(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.ClientId = "ef2018e1-03c1-44c3-9825-0ef20a56cb96";
                options.ClientSecret = "6zg8Q~lUijS6jWCvWNNYFkGOL5uC~rMzSLtRabT3";
            })
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.ClientId = "115664116245-siag9hsoj44aag0mad1bcuqk993g5iru.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });

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
                options.Authority = "https://localhost:10999";
                options.ClientId = "RichillCapital.TraderStudio.Web";
                options.ClientSecret = "secret";
                options.ResponseType = "code";
                options.Scope.Add("openid");
                options.Scope.Add("profile");
                options.Scope.Add("RichillCapital.Api");
                options.Scope.Add("offline_access");
                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name",
                    RoleClaimType = "role",
                };
                options.RequireHttpsMetadata = false;
            });

        return services;
    }
}
