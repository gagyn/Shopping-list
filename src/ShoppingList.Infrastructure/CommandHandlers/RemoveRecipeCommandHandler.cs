using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.DTO.Commands;

namespace ShoppingList.Infrastructure.CommandHandlers;

public class RemoveRecipeCommandHandler(
    IRecipeRepository recipeRepository) : IRequestHandler<RemoveRecipeCommand>
{
    public async Task Handle(RemoveRecipeCommand request, CancellationToken cancellationToken)
    {
        await recipeRepository.Remove(request.RecipeId, cancellationToken);
        await recipeRepository.SaveChanges(cancellationToken);
    }
}
