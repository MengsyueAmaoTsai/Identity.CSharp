using Duende.IdentityServer;
using Duende.IdentityServer.Models;
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

internal static class InMemoryApiResources
{
    internal static IEnumerable<ApiResource> CreateApiResources()
    {
        yield return new ApiResource("RichillCapital.Api", "Richill Capital API")
        {
            Scopes = { "RichillCapital.Api" },
        };
    }
}

internal static class InMemoryIdentityResources
{
    internal static IEnumerable<IdentityResource> CreateIdentityResources()
    {
        yield return new IdentityResources.OpenId();
        yield return new IdentityResources.Profile();
    }
}

internal static class InMemoryApiScopes
{
    internal static IEnumerable<ApiScope> CreateApiScopes()
    {
        yield return new ApiScope("RichillCapital.Api", "Richill Capital API");
    }
}

internal static class InMemoryClients
{
    internal static IEnumerable<Client> CreateClients()
    {
        yield return WebApplications.TraderStudio;
    }
}

internal static class WebApplications
{
    internal static readonly Client TraderStudio = new()
    {
        ClientId = "RichillCapital.TraderStudio.Web",
        ClientName = "Trader Studio Web Client",
        ClientSecrets =
        {
            new Secret("secret".Sha256()),
        },
        AllowedGrantTypes = GrantTypes.Code,
        RequirePkce = true,
        RedirectUris =
        {
            "http://localhost:9998/signin-oidc",
        },
        PostLogoutRedirectUris =
        {
            "http://localhost:9998/signout-callback-oidc",
        },
        AllowedScopes =
        {
            IdentityServerConstants.StandardScopes.OpenId,
            IdentityServerConstants.StandardScopes.Profile,
            "RichillCapital.Api",
        },
        AllowOfflineAccess = true,
        RequireConsent = true,
    };
}