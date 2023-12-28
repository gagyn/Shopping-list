using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Recipe;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.Database;
public class ShoppingListContext(DbContextOptions options) : IdentityDbContext<ApplicationUserEntity>(options)
{
    public const string SchemaName = "shoppingList";
    public DbSet<ShoppingListEntity> ShoppingLists { get; set; }
    public DbSet<RecipeEntity> Recipes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(SchemaName);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoppingListContext).Assembly);
    }
}
