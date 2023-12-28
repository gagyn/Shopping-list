using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;

public class RemoveIngredientCommandHandler(
    IRecipeRepository recipeRepository,
    IUserAccessor userAccessor) : IRequestHandler<RemoveIngredientCommand>
{
    public async Task Handle(RemoveIngredientCommand request, CancellationToken cancellationToken)
    {
        var recipe = await recipeRepository.FindOrThrow(request.RecipeId, cancellationToken);

        recipe.RemoveIngredient(request.IngredientId, userAccessor.UserName);

        await recipeRepository.SaveChanges(cancellationToken);
    }
}