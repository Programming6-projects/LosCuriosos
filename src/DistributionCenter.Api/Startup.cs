namespace DistributionCenter.Api;

using DistributionCenter.Api.Configurations;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.ConfigureApi();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        _ = app.ConfigureApi(env);
    }
}
