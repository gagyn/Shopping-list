using ShoppingList.Domain.Base;

namespace ShoppingList.Domain.FavoriteRecipe;
public class UserFavoriteRecipeEntity : BaseEntity
{
    public Guid UserId { get; private set; }
    public int RecipeId { get; private set; }

    private UserFavoriteRecipeEntity()
    {
    }

    private UserFavoriteRecipeEntity(Guid userId, int recipeId, string createdBy) : base(createdBy)
    {
        UserId = userId;
        RecipeId = recipeId;
    }

    public static UserFavoriteRecipeEntity Create(Guid userId, int recipeId, string createdBy)
        => new(userId, recipeId, createdBy);
}
