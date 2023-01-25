namespace Webion.Licensing.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddLicensingHandlers(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceCollectionExtensions));
    }
}