using Blazored.LocalStorage;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using StockControl;
using StockControl.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// nuget Microsoft.Extensions.Http
builder.Services.AddHttpClient("StockAPI", client =>
{
    client.BaseAddress = new Uri("http://localhost:5199");
}).AddHttpMessageHandler<AuthorizationMessageHandler>();

builder.Services.AddTransient<AuthorizationMessageHandler>();
builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("StockAPI")); // httpclient DI

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopRight;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 2000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredModal();
builder.Services.AddAuthorizationCore(); // auth attribute
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();

builder.Services.AddScoped<ISupplierService, SuppliersService>(); // TODO move to ext class
builder.Services.AddScoped<IPartService, PartService>(); // TODO move to ext class

await builder.Build().RunAsync();


