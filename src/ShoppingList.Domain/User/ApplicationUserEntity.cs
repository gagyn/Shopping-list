using Microsoft.AspNetCore.Identity;
using ShoppingList.Domain.Exceptions;
using ShoppingList.Domain.FavoriteRecipe;

namespace ShoppingList.Infrastructure.Authentication;
public class ApplicationUserEntity : IdentityUser<Guid>
{
    public ICollection<UserFavoriteRecipeEntity> FavoriteRecipes { get; private set; } = [];

    public void AddFavoriteRecipe(int recipeId, string modifiedBy)
    {
        FavoriteRecipes.Add(UserFavoriteRecipeEntity.Create(Id, recipeId, modifiedBy));
    }

    public void RemoveFavoriteRecipe(int recipeId)
    {
        var recipe = FavoriteRecipes.FirstOrDefault(x => x.RecipeId == recipeId)
            ?? throw new EntityNotFoundException<UserFavoriteRecipeEntity>();

        FavoriteRecipes.Remove(recipe);
    }
}