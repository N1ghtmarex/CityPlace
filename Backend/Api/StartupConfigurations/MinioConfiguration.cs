using Minio;

namespace Api.StartupConfigurations;

public static class MinioConfiguration
{
    public static IServiceCollection AddMinioConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddSingleton<IMinioClient>(provider =>
        {
            return new MinioClient()
                .WithEndpoint(configuration["MinIO:Endpoint"])
                .WithCredentials(configuration["MinIO:AccessKey"], configuration["MinIO:SecretKey"])
                .WithSSL(bool.Parse(configuration["MinIO:WithSSL"] ?? "false"))
                .Build();
        });
    }
}
