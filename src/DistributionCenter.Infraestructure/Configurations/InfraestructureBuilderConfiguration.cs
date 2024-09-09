namespace DistributionCenter.Infraestructure.Configurations;

using DistributionCenter.Infraestructure.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class InfraestructureBuilderConfiguration
{
    public static IServiceCollection ConfigureInfraestructure(this IServiceCollection services)
    {
        return services.ConfigureMediatR().ConfigureValidators().ConfigureMappers();
    }

    private static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(static options =>
        {
            _ = options.RegisterServicesFromAssembly(typeof(InfraestructureBuilderConfiguration).Assembly);
            _ = options.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
    }

    private static IServiceCollection ConfigureValidators(this IServiceCollection services)
    {
        return services.AddValidatorsFromAssemblyContaining(typeof(InfraestructureBuilderConfiguration));
    }

    private static IServiceCollection ConfigureMappers(this IServiceCollection services)
    {
        return services.AddAutoMapper(typeof(InfraestructureBuilderConfiguration));
    }
}
