namespace DistributionCenter.Services.Configurations;

using Distribution.Concretes;
using Distribution.Concretes.Components.OrdersParser.Concretes;
using Distribution.Concretes.Components.OrdersParser.Interfaces;
using Distribution.Concretes.Components.TransportsParser.Concretes;
using Distribution.Concretes.Components.TransportsParser.Interfaces;
using Distribution.Interfaces;
using Localization.Concretes;
using Localization.Interfaces;
using Notification.Concretes;
using Notification.Interfaces;

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

        _ = services.AddScoped<IOrderParser>(static _ => new OrderParser());
        _ = services.AddScoped<ITransportParser>(static _ => new TransportParser());

        _ = services.AddScoped<IDistributionStrategy>(static _ => new GreedyDistribution(
            _.GetRequiredService<IOrderParser>(),
            _.GetRequiredService<ITransportParser>()
        ));

        return services;
    }
}
