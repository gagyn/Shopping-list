using ShoppingList.Domain.Exceptions;
using ShoppingList.Domain.Recipe;
using ShoppingList.Domain.Repositories;
using ShoppingList.Infrastructure.Database;

namespace ShoppingList.Infrastructure.Repositories;

public class RecipeRepository(ShoppingListContext dbContext) : RepositoryBase(dbContext), IRecipeRepository
{
    public void Add(RecipeEntity recipeEntity)
    {
        dbContext.Recipes.Add(recipeEntity);
    }

    public async Task<RecipeEntity> FindOrThrow(int id, CancellationToken cancellationToken = default)
    {
        var recipe = await dbContext.Recipes.FindAsync(id);
        return recipe ?? throw new EntityNotFoundException<RecipeEntity>();
    }

    public async Task Remove(int id, CancellationToken cancellationToken = default)
    {
        var recipe = await FindOrThrow(id, cancellationToken);
        dbContext.Recipes.Remove(recipe);
    }
}