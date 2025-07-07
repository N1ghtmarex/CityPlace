using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
    /// <summary>
    /// Регистрация сервисов
    /// </summary>
    public static class ServiceRegistrar
    {
        /// <summary>
        /// Регистрация доступа к БД
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterDataAccessService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DbConnection"));
            });

            return services;
        }

        /// <summary>
        /// Задать значение ConnectionString для подключения к БД
        /// </summary>
        /// <param name="app"></param>
        /// <param name="value"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetConnectionStringEnvironment(this IApplicationBuilder app, string? value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Строка подключения не задана!");
            }

            Environment.SetEnvironmentVariable("ConnectionString", value);
        }

        /// <summary>
        /// Миграция
        /// </summary>
        /// <param name="app"></param>
        public static void MigrateDb(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context?.Database.Migrate();
        }
    }
}
