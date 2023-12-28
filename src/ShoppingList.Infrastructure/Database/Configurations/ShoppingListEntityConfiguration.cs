using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingList.Domain.ShoppingList;

namespace ShoppingList.Infrastructure.Database.Configurations;
public class ShoppingListEntityConfiguration : BaseEntityConfiguration<ShoppingListEntity>
{
    public override void Configure(EntityTypeBuilder<ShoppingListEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.OwnsMany(x => x.Products, b =>
        {
            b.ToTable(GetTableName<ProductEntity>());
            b.WithOwner().HasForeignKey(x => x.ShoppingListId);
            b.Property(x => x.Unit).HasConversion<string>();
            b.Property(x => x.ProductName).IsRequired();
            b.Property(x => x.Amount).IsRequired().HasPrecision(10, 2);
        });
    }
}
