using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.Database.Configurations;
public class ShoppingListContext(DbContextOptions options) : IdentityDbContext<ApplicationUserEntity>(options)
{
    public const string SchemaName = "shoppingList";
    public DbSet<ShoppingListEntity> ShoppingLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShoppingListContext).Assembly);
    }
}
