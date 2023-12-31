using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Repositories;
using ShoppingList.Domain.User;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database;
using ShoppingList.Infrastructure.Extensions;
using ShoppingList.Infrastructure.QueryHandlers;
using ShoppingList.Infrastructure.Repositories;
using ShoppingList.Web.Authorization;
using ShoppingList.Web.Client.Pages;
using ShoppingList.Web.Client.Services;
using ShoppingList.Web.Components;
using ShoppingList.Web.Components.Account;
using ShoppingList.Web.Controllers;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();
var appInsightsConnectionString = builder.Configuration.GetValue<string>("ApplicationInsights:ConnectionString");
if (!string.IsNullOrEmpty(appInsightsConnectionString))
{
    builder.Services.AddApplicationInsightsTelemetry();
}

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();

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
}).AddIdentityCookies();

builder.Services.AddAuthorization(options => options.AddPolicies());

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
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
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ShoppingListContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

AddRepositories(builder.Services);
AddServices(builder.Services);

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

app.MapControllers();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Auth).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

await InitializeDatabase(app.Services);

app.Run();

void AddRepositories(IServiceCollection services)
{
    services
        .AddScoped<IShoppingListRepository, ShoppingListRepository>()
        .AddScoped<IProductRepository, ProductRepository>()
        .AddScoped<IRecipeRepository, RecipeRepository>()
        .AddScoped<IIngredientRepository, IngredientRepository>()
        .AddScoped<IUserRepository, UserRepository>();
}

void AddServices(IServiceCollection services)
{
    services
        .AddSingleton<IEmailSender<ApplicationUserEntity>, IdentityNoOpEmailSender>()
        .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<FindShoppingListQueryHandler>())
        .AddScoped<IRecipesClient, RecipesService>();
}

async Task InitializeDatabase(IServiceProvider serviceProvider)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ShoppingListContext>();

    dbContext.Database.Migrate();

    var alreadyHasData = await dbContext.Roles.AnyAsync();
    if (alreadyHasData)
    {
        return;
    }

    var roles = new[] { UserRoleEntity.BasicUser, UserRoleEntity.Administrator };
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUserEntity>>();

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role.ToString()))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(role.ToString()));
        }
    }

    var isAnyUser = await dbContext.Users.AnyAsync();
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    if (isAnyUser)
    {
        var basicUserRole = await roleManager.FindByNameAsync(UserRoleEntity.BasicUser);
        await dbContext.Users.ForEachAsync(x => dbContext.UserRoles.Add(new IdentityUserRole<Guid>
        {
            UserId = x.Id,
            RoleId = basicUserRole.Id
        }));
    }
    else if (configuration.GetValue<bool>("InitAdminUser"))
    {
        var adminRole = await roleManager.FindByNameAsync(UserRoleEntity.Administrator);
        const string password = "123456";
        const string username = "admin@admin.com";
        var adminUser = new ApplicationUserEntity()
        {
            Email = username,
            UserName = username
        };
        await userManager.CreateAsync(adminUser, password);
        await userManager.AddToRoleAsync(adminUser, adminRole.Name!);
        await userManager.SetLockoutEnabledAsync(adminUser, enabled: false);
    }
    await dbContext.SaveChangesAsync();
}