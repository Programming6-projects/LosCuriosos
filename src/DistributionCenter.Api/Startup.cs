namespace DistributionCenter.Api;

using DistributionCenter.Api.Configurations;

public class Startup(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.ConfigureApi(_configuration);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        _ = app.ConfigureApi(env);
    }
}
