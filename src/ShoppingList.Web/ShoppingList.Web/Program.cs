using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database;
using ShoppingList.Infrastructure.QueryHandlers;
using ShoppingList.Web.Client.Pages;
using ShoppingList.Web.Components;
using ShoppingList.Web.Components.Account;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState()
    .AddScoped<IdentityUserAccessor>()
    .AddScoped<IdentityRedirectManager>()
    .AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>()
    .AddTransient<IUserAccessor, UserAccessor>()
    .AddHttpContextAccessor();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services
    .AddDbContext<ShoppingListContext>(options => options.UseSqlServer(connectionString))
    .AddDatabaseDeveloperPageExceptionFilter();

builder.Services
    .AddIdentityCore<ApplicationUserEntity>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
    })
    .AddEntityFrameworkStores<ShoppingListContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services
    .AddSingleton<IEmailSender<ApplicationUserEntity>, IdentityNoOpEmailSender>()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<FindShoppingListQueryHandler>());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<ShoppingListContext>();

    dbContext.Database.Migrate();
}

app.Run();
