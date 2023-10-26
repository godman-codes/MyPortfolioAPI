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
using Microsoft.OpenApi.Models;
using Contracts;
using Entities.SystemModels;
using Service.BackgroundServices;
using EntrustContracts;
using EntrustServices;

namespace MyPortfolioAPI.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Configures Cross-Origin Resource Sharing (CORS) policy.
        /// </summary>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
        }

        /// <summary>
        /// Configures integration with Internet Information Services (IIS).
        /// </summary>
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {
                // Configure IIS options here, if needed
            });
        }

        /// <summary>
        /// Registers the database context for Entity Framework Core.
        /// </summary>
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MyProjectDbContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }

        /// <summary>
        /// Registers the logger service for logging application events.
        /// </summary>
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        /// <summary>
        /// Registers the repository manager for managing data repositories.
        /// </summary>
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        /// <summary>
        /// Registers the service manager for managing business logic services.
        /// </summary>
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void ConfigureEntrustManager(this IServiceCollection services)
        {
            services.AddScoped<IEntrustManager, EntrustManager>();
        }

        /// <summary>
        /// Configures the identity system for user authentication and authorization.
        /// </summary>
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<UserModel, IdentityRole>(o =>
            {
                // Configure password requirements
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength = 5;
                o.User.RequireUniqueEmail = true;
                o.SignIn.RequireConfirmedEmail = true;
                o.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddEntityFrameworkStores<MyProjectDbContext>()
            .AddDefaultTokenProviders();
        }

        /// <summary>
        /// Configures JWT authentication for securing API endpoints.
        /// </summary>
        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtConfiguration = new JwtConfiguration();
            configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

            var secretKey = Environment.GetEnvironmentVariable("SECRET");

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Validate the issuer of the token
                    ValidateIssuer = true,
                    // Validate the audience of the token
                    ValidateAudience = true,
                    // Validate the expiration time of the token
                    ValidateLifetime = true,
                    // Validate the signing key of the token
                    ValidateIssuerSigningKey = true,
                    // Set the valid issuer and audience
                    ValidIssuer = jwtConfiguration.ValidIssuer,
                    ValidAudience = jwtConfiguration.ValidAudience,
                    // Set the signing key
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
        }
        public static void ConfigureHosting(this IServiceCollection services)
        {
            services.AddHostedService<PortfolioBackgroundService>();
        }

        public static void AddSMTPConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SMTPSettings>(configuration.GetSection("SMTPSettings"));

            services.AddOptions<SMTPSettings>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("SMTPSettings").Bind(settings);
                    settings.FromEmail = Environment.GetEnvironmentVariable("FromEmail");
                    settings.FromEmailPassword = Environment.GetEnvironmentVariable("FromEmailPassword");
                });
        }

        /// <summary>
        /// Configures JWT settings from app configuration.
        /// </summary>
        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));
        }

        /// <summary>
        /// Configures Swagger for API documentation and authentication.
        /// </summary>
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "MyPortfolio API",
                        Version = "v1",
                        Description = "Official API for MyPortfolio by Godman-codes",
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Faseun Tobi",
                            Email = "Faseunoluwatobilobasimon@gmail.com",
                            Url = new Uri("https://twitter.com/godman_codes"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MyPortfolio API LICX",
                            Url = new Uri("https://example.com/license"),
                        }
                    });
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Place to add JWT with Bearer",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Name = "Bearer"
                        },
                        new List<string>()
                    },
                });
            });
        }

        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
            builder.AddMvcOptions(config => config.OutputFormatters.Add(
                new CsvOutputFormatter()
                ));
    }
}
