using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingList.Domain.FavoriteRecipe;
using ShoppingList.Domain.Recipe;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.Database.Configurations;

public class UserFavoriteRecipeEntityConfiguration : BaseEntityConfiguration<UserFavoriteRecipeEntity>
{
    public override void Configure(EntityTypeBuilder<UserFavoriteRecipeEntity> builder)
    {
        base.Configure(builder);
        builder.HasKey(x => new { x.UserId, x.RecipeId });

        builder.HasOne<ApplicationUserEntity>()
            .WithMany(x => x.FavoriteRecipes)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<RecipeEntity>()
            .WithMany()
            .HasForeignKey(x => x.RecipeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}