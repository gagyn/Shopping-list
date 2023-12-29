using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;
public class CreateShoppingListFromRecipeCommandHandler(
    IRecipeRepository recipeRepository,
    IShoppingListRepository shoppingListRepository,
    IUserAccessor userAccessor) : IRequestHandler<CreateShoppingListFromRecipeCommand, int>
{
    public async Task<int> Handle(CreateShoppingListFromRecipeCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = ShoppingListEntity.Create(request.Name, userAccessor.Id, userAccessor.UserName);
        shoppingListRepository.Add(shoppingList);
        await shoppingListRepository.SaveChanges(cancellationToken);

        var recipe = await recipeRepository.FindOrThrow(request.RecipeId, cancellationToken);

        foreach (var ingredient in recipe.Ingredients)
        {
            shoppingList.AddProduct(
                ingredient.Name,
                ingredient.Description,
                ingredient.Amount,
                userAccessor.UserName,
                ingredient.Unit);
        }

        await shoppingListRepository.SaveChanges(cancellationToken);
        return shoppingList.Id;
    }
}
