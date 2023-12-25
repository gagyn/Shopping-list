using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;
public class AddProductToShoppingListCommandHandler(
    IShoppingListRepository shoppingListRepository,
    IUserAccessor userAccessor) : IRequestHandler<AddProductToShoppingListCommand, int>
{
    public async Task<int> Handle(AddProductToShoppingListCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = await shoppingListRepository.FindOrThrow(request.ShoppingListId, userAccessor.Id, cancellationToken);

        var product = shoppingList.AddProduct(request.Name, request.Description, request.Amount, userAccessor.UserName);

        await shoppingListRepository.SaveChanges(cancellationToken);

        return product.Id;
    }
}
