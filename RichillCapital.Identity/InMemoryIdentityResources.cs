using Duende.IdentityServer.Models;

namespace RichillCapital.Identity;

internal static class InMemoryIdentityResources
{
    internal static IEnumerable<IdentityResource> CreateIdentityResources()
    {
        yield return new IdentityResources.OpenId();
        yield return new IdentityResources.Profile();
    }
}
