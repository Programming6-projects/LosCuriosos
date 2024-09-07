namespace DistributionCenter.Api.Configurations;

using Microsoft.OpenApi.Models;

public static class ApiBuilderConfiguration
{
    public static IServiceCollection ConfigureApi(this IServiceCollection services)
    {
        return services.ConfigureControllers().AddEndpointsApiExplorer().ConfigureSwagger().AddProblemDetails();
    }

    private static IServiceCollection ConfigureControllers(this IServiceCollection services)
    {
        _ = services.AddControllers();

        return services;
    }

    private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        return services.AddSwaggerGen(static c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Distribution Center API", Version = "v1" });
        });
    }
}