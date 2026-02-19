using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using CryptoApp;
using Blazored.LocalStorage; // Utilisation de LocalStorage pour le stockage */
using CryptoApp.Services; /* Utilisation de CryptoApp.serices pour l'API */

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage(); /* Utilisation de LocalStorage pour le stockage */

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<CryptoService>();

builder.Services.AddScoped<AuthService>();

await builder.Build().RunAsync();
