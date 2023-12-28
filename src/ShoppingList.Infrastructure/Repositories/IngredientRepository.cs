using ShoppingList.Domain.Exceptions;
using ShoppingList.Domain.Recipe;
using ShoppingList.Domain.Repositories;
using ShoppingList.Infrastructure.Database;

namespace ShoppingList.Infrastructure.Repositories;
public class IngredientRepository(IRecipeRepository recipeRepository, ShoppingListContext dbContext) : RepositoryBase(dbContext), IIngredientRepository
{
    public async Task<IngredientEntity> FindOrThrow(int recipeId, int ingredientId, CancellationToken cancellationToken = default)
    {
        var recipe = await recipeRepository.FindOrThrow(recipeId, cancellationToken);
        return recipe.Ingredients.FirstOrDefault(x => x.Id == ingredientId)
            ?? throw new EntityNotFoundException<IngredientEntity>();
    }
}
