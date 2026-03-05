using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CryptoApp;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<CryptoApp.Services.CryptoService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<CryptoApp.Services.FakeAuthStateProvider>();
builder.Services.AddScoped<CryptoApp.Services.FavoriteService>();
builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetRequiredService<CryptoApp.Services.FakeAuthStateProvider>());

await builder.Build().RunAsync();
