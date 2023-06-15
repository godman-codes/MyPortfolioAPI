using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;
using System.Text;

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

        // repository manager config
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        // service manager config
        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        // Identity configurations
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<UserModel, IdentityRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength = 5;
                o.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<MyProjectDbContext>()
                .AddDefaultTokenProviders();
        }
        // configuring jwt
        public static void ConfigureJWT(this IServiceCollection service, IConfiguration configuration)
        {
            var jwtConfiguration = new JwtConfiguration();

            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            service.AddAuthentication(
                opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // The issuer is the actual server that created the token (ValidateIssuer=true)
                        ValidateIssuer = true,
                        //The receiver of the token is a valid recipient(ValidateAudience = true)
                        ValidateAudience = true,
                        //The token has not expired(ValidateLifetime = true)
                        ValidateLifetime = true,
                        //The signing key is valid and is trusted by the server(ValidateIssuerSigningKey = true)
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtConfiguration.ValidIssuer,
                        ValidAudience = jwtConfiguration.ValidAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                    };
                });
        }
        public static void AddJwtConfiguration(this IServiceCollection services,
           IConfiguration configuration) =>
           services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));
    }
}
