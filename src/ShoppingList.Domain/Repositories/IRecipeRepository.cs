using ShoppingList.Domain.Recipe;

namespace ShoppingList.Domain.Repositories;

public interface IRecipeRepository : IRepository
{
    void Add(RecipeEntity recipeEntity);
    Task<RecipeEntity> FindOrThrow(int id, CancellationToken cancellationToken = default);
    Task Remove(int id, CancellationToken cancellationToken = default);
}