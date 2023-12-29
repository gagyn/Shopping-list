using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShoppingList.Web.Client;
using ShoppingList.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(new Uri(builder.HostEnvironment.BaseAddress), "api/")
});
builder.Services.AddScoped<IRecipesClient, RecipesClient>();

await builder.Build().RunAsync();
