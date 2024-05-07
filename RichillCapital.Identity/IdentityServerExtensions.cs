using Duende.IdentityServer.Models;

namespace RichillCapital.Identity;

public static class IdentityServerExtensions
{
    public static bool IsNativeClient(this AuthorizationRequest request) =>
        !request.RedirectUri.StartsWith("https", StringComparison.Ordinal) &&
        !request.RedirectUri.StartsWith("http", StringComparison.Ordinal);
}