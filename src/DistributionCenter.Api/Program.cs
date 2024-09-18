namespace DistributionCenter.Api;

using DotNetEnv;

public class Program
{
    public static void Main(string[] args)
    {
        _ = Env.TraversePath().Load();

        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(static webBuilder =>
            {
                _ = webBuilder.UseStartup<Startup>();
            });
    }
}
