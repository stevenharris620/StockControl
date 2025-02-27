using Microsoft.EntityFrameworkCore;
using StockControl.API.Extensions;
using StockControl.API.Models;

var builder = WebApplication.CreateBuilder(args);

//var builder = WebApplication.CreateBuilder(new WebApplicationOptions
//{
//    EnvironmentName = Environments.Development
//});

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(opt =>
{
    opt.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureCors();
builder.Services.AddSwagger();
builder.Services.AddBusinessServices();
builder.Services.AddUnitOfWork();
builder.Services.AddMappers();
builder.Services.ConfigureIdentityOptions(); // user details - injectable

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddIdentityAuthentication(builder.Configuration);
builder.Services.AddApplicationDatabaseContext(builder.Configuration);



var app = builder.Build();

// Ensure the database is created
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate(); // This will apply any pending migrations and create the database if it doesn't exist
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureCustomExceptionMiddleware(); // handle exceptions in one place

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
