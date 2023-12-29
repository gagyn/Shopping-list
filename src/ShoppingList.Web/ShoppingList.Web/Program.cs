using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Repositories;
using ShoppingList.Domain.User;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database;
using ShoppingList.Infrastructure.QueryHandlers;
using ShoppingList.Infrastructure.Repositories;
using ShoppingList.Web.Authorization;
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
    .AddRoles<IdentityRole>()
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

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

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
        .AddScoped<IIngredientRepository, IngredientRepository>();
}

void AddServices(IServiceCollection services)
{
    services
        .AddSingleton<IEmailSender<ApplicationUserEntity>, IdentityNoOpEmailSender>()
        .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<FindShoppingListQueryHandler>());
}

async Task InitializeDatabase(IServiceProvider serviceProvider)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ShoppingListContext>();

    dbContext.Database.Migrate();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { UserRoleEntity.BasicUser, UserRoleEntity.Administrator };

    if (await dbContext.Roles.AnyAsync())
    {
        return;
    }

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role.ToString()))
        {
            await roleManager.CreateAsync(new IdentityRole(role.ToString()));
        }
    }

    if (await dbContext.Users.AnyAsync())
    {
        var basicUserRole = await dbContext.Roles.FirstAsync(x => x.Name == UserRoleEntity.BasicUser.ToString());
        await dbContext.Users.ForEachAsync(x => dbContext.UserRoles.Add(new IdentityUserRole<string>
        {
            UserId = x.Id,
            RoleId = basicUserRole.Id
        }));
    }
    await dbContext.SaveChangesAsync();
}