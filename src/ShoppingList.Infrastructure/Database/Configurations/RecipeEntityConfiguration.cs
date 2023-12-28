using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingList.Domain.Recipe;

namespace ShoppingList.Infrastructure.Database.Configurations;
public class RecipeEntityConfiguration : BaseEntityConfiguration<RecipeEntity>
{
    public override void Configure(EntityTypeBuilder<RecipeEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.OwnsMany(x => x.Ingredients, b =>
        {
            b.ToTable(GetTableName<IngredientEntity>());
            b.WithOwner().HasForeignKey(x => x.RecipeId);
            b.Property(x => x.Unit).HasConversion<string>();
            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.Amount).IsRequired().HasPrecision(10, 2);
        });
    }
}
