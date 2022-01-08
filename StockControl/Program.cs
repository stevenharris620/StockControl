using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using StockControl;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// nuget Microsoft.Extensions.Http
builder.Services.AddHttpClient("StockAPI", client =>
    client.BaseAddress = new Uri("http://localhost:5199")).AddHttpMessageHandler<AuthorizationMessageHandler>();

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();


