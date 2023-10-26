using Contracts;
using Microsoft.AspNetCore.Mvc;
using MyPortfolioAPI.Extensions;
using MyPortfolioAPI.Presentation.ActionFilters;
using NLog;
using System.Reflection;
using Utilities.Constants;

var builder = WebApplication.CreateBuilder(args);

//// Subscribe to the AssemblyResolve event for custom assembly loading.
//AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
//{
//    var location = AppDomain.CurrentDomain.BaseDirectory;
//    // Specify the directory where external assemblies are located.
//    string assemblyDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EntrustAssemblies");

//    // Attempt to load the assembly from the specified directory.
//    string assemblyPath = Path.Combine(assemblyDir, new AssemblyName(e.Name).Name + ".dll");
//    if (File.Exists(assemblyPath))
//    {
//        return Assembly.LoadFile(assemblyPath);
//    }

//    // If the assembly is not found in the specified directory, return null to allow the default resolution process.
//    return null;
//};

//// Load multiple assemblies from a folder.
//string externalAssembliesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EntrustAssemblies");
//if (Directory.Exists(externalAssembliesDir))
//{
//    foreach (var assemblyFile in Directory.GetFiles(externalAssembliesDir, "*.dll"))
//    {
//        try
//        {
//            Assembly.LoadFile(assemblyFile);
//        }
//        catch (Exception ex)
//        {
//            Console.WriteLine(ex);
//        }
//    }
//}

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
builder.Services.ConfigureEntrustManager();
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
builder.Services.AddSMTPConfigurations(builder.Configuration);
builder.Services.ConfigureHosting();
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


AppDomain.CurrentDomain.SetData(Constants.WebRootPath, app.Environment.WebRootPath);


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
