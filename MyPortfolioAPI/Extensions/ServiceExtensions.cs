using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace MyPortfolioAPI.Extensions
{
    public static class ServiceExtensions
    {
        // Cors
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                    );
            });

        // IIS Configuration
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });

        // registering the new db context
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) =>
            services.AddDbContext<MyProjectDbContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        // logging IOC
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();
    }
}
