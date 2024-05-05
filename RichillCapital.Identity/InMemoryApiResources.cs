using Duende.IdentityServer.Models;

namespace RichillCapital.Identity;

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
