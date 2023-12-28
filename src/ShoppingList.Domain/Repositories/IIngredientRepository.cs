using ShoppingList.Domain.Recipe;

namespace ShoppingList.Domain.Repositories;

public interface IIngredientRepository : IRepository
{
    Task<IngredientEntity> FindOrThrow(int recipeId, int ingredientId, CancellationToken cancellationToken = default);
}