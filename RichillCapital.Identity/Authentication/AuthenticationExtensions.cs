using Microsoft.Extensions.DependencyInjection;

namespace RichillCapital.Identity.Authentication;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddApiAuthentication(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddTraderStudioAuthentication(this IServiceCollection services)
    {
        return services;
    }
}