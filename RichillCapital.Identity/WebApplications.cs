using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace RichillCapital.Identity;

internal static class IdentityClients
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
            "https://localhost:10998/signin-oidc",
        },
        PostLogoutRedirectUris =
        {
            "http://localhost:9998/signout-callback-oidc",
            "https://localhost:10998/signout-callback-oidc",
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