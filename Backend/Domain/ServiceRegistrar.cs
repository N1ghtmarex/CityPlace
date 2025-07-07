using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    public static class ServiceRegistrar
    {
        public static IServiceCollection RegisterDataAccessService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
            });

            return services;
        }

        public static void SetConnectionStringEnvironment(this IApplicationBuilder app, string? value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Строка подключения не задана!");
            }

            Environment.SetEnvironmentVariable("ConnectionString", value);
        }

        public static void MigrateDb(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context?.Database.Migrate();
        }
    }
}
