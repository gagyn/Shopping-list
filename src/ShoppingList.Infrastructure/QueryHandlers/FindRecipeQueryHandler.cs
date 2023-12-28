using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.DTO.Models;
using ShoppingList.DTO.Queries;
using ShoppingList.Infrastructure.Extensions;
using Unit = ShoppingList.DTO.Models.Unit;

namespace ShoppingList.Infrastructure.QueryHandlers;

public class FindRecipeQueryHandler(
    IRecipeRepository recipeRepository) : IRequestHandler<FindRecipeQuery, RecipeDetails>
{
    public async Task<RecipeDetails> Handle(FindRecipeQuery request, CancellationToken cancellationToken)
    {
        var recipe = await recipeRepository.FindOrThrow(request.Id, cancellationToken);
        var ingredients = recipe.Ingredients.Select(x =>
            new IngredientDetails(x.Id, x.Name, x.Description, x.Amount, x.Unit.Parse<Unit>()));

        return new(recipe.Id, recipe.Name, recipe.Description, ingredients.ToList());
    }
}