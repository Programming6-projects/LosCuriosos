namespace DistributionCenter.Application.Configurations;

using DistributionCenter.Application.Connections.Concretes;
using DistributionCenter.Application.Connections.Interfaces;
using DistributionCenter.Application.Constants;
using DistributionCenter.Application.Contexts.Concretes;
using DistributionCenter.Application.Contexts.Interfaces;
using DistributionCenter.Application.Repositories.Concretes;
using DistributionCenter.Application.Repositories.Interfaces;
using DistributionCenter.Application.Tables.Concretes;
using DistributionCenter.Domain.Entities.Concretes;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderConfiguration
{
    public static IServiceCollection ConfigureApplication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        return services.ConfigurePersistence(configuration).ConfigureRepositories();
    }

    private static IServiceCollection ConfigurePersistence(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        _ = services.AddScoped<IDbConnectionFactory>(_ => new NpgqlConnectionFactory(
            configuration[DbConstants.DefaultConnectionStringPath]!
        ));
        _ = services.AddScoped<IContext>(_ => new Context(
            new Dictionary<Type, object>()
            {
                { typeof(Client), new ClientTable(_.GetRequiredService<IDbConnectionFactory>()) },
            }
        ));

        return services;
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        _ = services.AddScoped<IRepository<Client>, ClientRepository>();

        return services;
    }
}
