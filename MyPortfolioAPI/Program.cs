using MyPortfolioAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Configure Cross-Origin Resource Sharing (CORS).
builder.Services.ConfigureCors();

// Configure IIS Integration.
builder.Services.ConfigureIISIntegration();

// Configure SQL Context and pass it the IConfiguration class to get the connection string.
builder.Services.ConfigureSqlContext(builder.Configuration);


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
