using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;
public class SwitchRecipeMarkedAsFavoriteCommandHandler(
    IUserRepository userRepository,
    IUserAccessor userAccessor) : IRequestHandler<SwitchRecipeMarkedAsFavoriteCommand>
{
    public async Task Handle(SwitchRecipeMarkedAsFavoriteCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindOrThrow(userAccessor.Id, cancellationToken);

        var isAlreadyFavorite = user.FavoriteRecipes.Any(x => x.RecipeId == request.RecipeId);
        if (isAlreadyFavorite)
        {
            user.RemoveFavoriteRecipe(request.RecipeId);
        }
        else
        {
            user.AddFavoriteRecipe(request.RecipeId, userAccessor.UserName);
        }
        await userRepository.SaveChanges(cancellationToken);
    }
}