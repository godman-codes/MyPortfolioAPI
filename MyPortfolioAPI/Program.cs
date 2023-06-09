using MyPortfolioAPI.Extensions;
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

// configure identity user
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();

// assembly reference to point to where the presentation layer with the controllers is at 
builder.Services.AddControllers()
    .AddApplicationPart(typeof(MyPortfolioAPI.Presentation.AssemblyReference).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
