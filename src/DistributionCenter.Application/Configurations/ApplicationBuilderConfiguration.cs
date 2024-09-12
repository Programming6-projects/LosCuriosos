namespace DistributionCenter.Application.Configurations;

using System.Data;
using Constants;
using Contexts.Concretes;
using Contexts.Interfaces;
using Domain.Entities.Concretes;
using Repositories.Concretes;
using Repositories.Interfaces;
using Tables.Connections.Dapper.Concretes;
using Tables.Connections.Dapper.Interfaces;
using Tables.Connections.File.Concretes;
using Tables.Connections.File.Interfaces;
using Tables.Core.Concretes;

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
        _ = services.AddScoped<IDbConnectionFactory<IDbConnection> >(_ => new NpgqlConnectionFactory(
            configuration[DbConstants.DefaultConnectionStringPath]!
        ));
        _ = services.AddScoped<IFileConnectionFactory<Transport> >(_ => new JsonConnectionFactory<Transport>(
            DbConstants.TransportSchema));

        _ = services.AddScoped<IContext>(_ => new Context(
            new Dictionary<Type, object>
            {
                { typeof(Client), new ClientTable(_.GetRequiredService<IDbConnectionFactory<
                    IDbConnection>>()) },
                {typeof(Transport), new TransportTable(_.GetRequiredService<IFileConnectionFactory<
                    Transport>>())},
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
