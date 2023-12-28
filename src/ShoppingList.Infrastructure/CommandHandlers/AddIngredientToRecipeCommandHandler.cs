using MediatR;
using ShoppingList.Domain.Recipe;
using ShoppingList.Domain.Repositories;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Extensions;

namespace ShoppingList.Infrastructure.CommandHandlers;

public class AddIngredientToRecipeCommandHandler(
    IRecipeRepository recipeRepository,
    IUserAccessor userAccessor) : IRequestHandler<AddIngredientToRecipeCommand, int>
{
    public async Task<int> Handle(AddIngredientToRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await recipeRepository.FindOrThrow(request.RecipeId, cancellationToken);

        var ingredient = IngredientEntity.Create(
            request.Name,
            request.Description,
            request.Amount,
            request.Unit.Parse<UnitEntity>(),
            request.RecipeId);

        recipe.AddIngredient(ingredient, userAccessor.UserName);
        
        await recipeRepository.SaveChanges(cancellationToken);
        return ingredient.Id;
    }
}