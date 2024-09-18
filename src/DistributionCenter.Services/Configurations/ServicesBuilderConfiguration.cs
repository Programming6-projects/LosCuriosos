namespace DistributionCenter.Services.Configurations;

using DistributionCenter.Services.Localization.Concretes;
using DistributionCenter.Services.Localization.Interfaces;
using DistributionCenter.Services.Notification.Concretes;
using DistributionCenter.Services.Notification.Interfaces;

public static class ServicesBuilderConfiguration
{
    public static IServiceCollection ConfigureOwnServices(this IServiceCollection services)
    {
        return services.ConfigureDependencies();
    }

    private static IServiceCollection ConfigureDependencies(this IServiceCollection services)
    {
        _ = services.AddScoped<IEmailService>(static _ => new GmailService(
            Environment.GetEnvironmentVariable("GMAIL_EMAIL")!,
            Environment.GetEnvironmentVariable("GMAIL_APP_PASSWORD")!
        ));

        _ = services.AddScoped(static _ => new HttpClient());

        _ = services.AddScoped<IDistanceCalculator>(static _ => new DistanceCalculator(
            _.GetRequiredService<HttpClient>(),
            Environment.GetEnvironmentVariable("MAPBOX_TOKEN")!
        ));
        _ = services.AddScoped<ILocationValidator>(static _ => new LocationValidator(
            _.GetRequiredService<HttpClient>(),
            Environment.GetEnvironmentVariable("MAPBOX_TOKEN")!
        ));

        _ = services.AddScoped<ILocationService>(static _ => new LocationService(
            _.GetRequiredService<ILocationValidator>(),
            _.GetRequiredService<IDistanceCalculator>()
        ));

        return services;
    }
}
