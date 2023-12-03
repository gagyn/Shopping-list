using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingList.Domain.ShoppingList;

namespace ShoppingList.Infrastructure.Database.Configurations;
public class ShoppingListEntityConfiguration : BaseEntityConfiguration<ShoppingListEntity>
{
    public override void Configure(EntityTypeBuilder<ShoppingListEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);
        builder.OwnsMany(x => x.Products)
            .WithOwner()
            .HasForeignKey(x => x.ShoppingListId);
    }
}
