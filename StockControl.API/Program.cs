using StockControl.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureCors();
builder.Services.AddSwagger();
builder.Services.AddBusinessServices();
builder.Services.AddUnitOfWork();
builder.Services.AddMappers();
builder.Services.ConfigureIdentityOptions(); // user details - injectable

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationDatabaseContext(builder.Configuration);
builder.Services.AddIdentityAuthentication(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
