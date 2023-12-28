using MediatR;
using ShoppingList.Domain.Recipe;
using ShoppingList.Domain.Repositories;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;
public class CreateRecipeCommandHandler(
    IRecipeRepository recipeRepository,
    IUserAccessor userAccessor) : IRequestHandler<CreateRecipeCommand, int>
{
    public async Task<int> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
    {
        var recipe = RecipeEntity.Create(request.Name, userAccessor.UserName);
        recipeRepository.Add(recipe);

        await recipeRepository.SaveChanges(cancellationToken);
        return recipe.Id;
    }
}
