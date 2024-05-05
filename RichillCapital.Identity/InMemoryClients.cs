using Duende.IdentityServer.Models;

namespace RichillCapital.Identity;

internal static class InMemoryClients
{
    internal static IEnumerable<Client> CreateClients()
    {
        yield return WebApplications.TraderStudio;
    }
}
