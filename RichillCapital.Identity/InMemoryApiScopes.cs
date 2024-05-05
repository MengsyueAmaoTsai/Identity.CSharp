using Duende.IdentityServer.Models;

namespace RichillCapital.Identity;

internal static class InMemoryApiScopes
{
    internal static IEnumerable<ApiScope> CreateApiScopes()
    {
        yield return new ApiScope("RichillCapital.Api", "Richill Capital API");
    }
}
