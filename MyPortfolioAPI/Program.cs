using Contracts;
using Microsoft.AspNetCore.Mvc;
using MyPortfolioAPI.Extensions;
using MyPortfolioAPI.Presentation.ActionFilters;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// passing the enlog config file nto the logmanager load configuration method
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "./nlog.config"));

// Configure Cross-Origin Resource Sharing (CORS).
builder.Services.ConfigureCors();

// Configure IIS Integration.
builder.Services.ConfigureIISIntegration();

// add the ilogger service extension method 
builder.Services.ConfigureLoggerService();

// Configure SQL Context and pass it the IConfiguration class to get the connection string.
builder.Services.ConfigureSqlContext(builder.Configuration);

// configureRepository manager 
builder.Services.ConfigureRepositoryManager();

// configure service manager
builder.Services.ConfigureServiceManager();

// adding automapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ValidationFilterAttribute>();

// configure identity user
builder.Services.AddAuthentication();
// identity
builder.Services.ConfigureIdentity();
//jwt
builder.Services.ConfigureJWT(builder.Configuration);

// IOptions
builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

// assembly reference to point to where the presentation layer with the controllers is at 
builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    // the ABL tells the api not to accept content type it dosent recognise
})
    .AddXmlDataContractSerializerFormatters()
    .AddCustomCSVFormatter()
    .AddApplicationPart(typeof(MyPortfolioAPI.Presentation.AssemblyReference).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();

var app = builder.Build();

// This must be done before the builder.Build method.
// Reference UACWA pg 72.
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
