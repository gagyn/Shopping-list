using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;

public class UpdateRecipeCommandHandler(
    IRecipeRepository recipeRepository,
    IUserAccessor userAccessor) : IRequestHandler<UpdateRecipeCommand>
{
    public async Task Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = await recipeRepository.FindOrThrow(request.Id, cancellationToken);

        recipe.Update(request.Name, request.Description, userAccessor.UserName);

        await recipeRepository.SaveChanges(cancellationToken);
    }
}