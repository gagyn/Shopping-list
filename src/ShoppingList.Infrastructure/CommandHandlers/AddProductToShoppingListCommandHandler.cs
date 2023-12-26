using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Extensions;

namespace ShoppingList.Infrastructure.CommandHandlers;
public class AddProductToShoppingListCommandHandler(
    IShoppingListRepository shoppingListRepository,
    IUserAccessor userAccessor) : IRequestHandler<AddProductToShoppingListCommand, int>
{
    public async Task<int> Handle(AddProductToShoppingListCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = await shoppingListRepository.FindOrThrow(request.ShoppingListId, userAccessor.Id, cancellationToken);

        var product = shoppingList.AddProduct(
            request.Name,
            request.Description,
            request.Amount,
            userAccessor.UserName,
            request.Unit.Parse<UnitEntity>());

        await shoppingListRepository.SaveChanges(cancellationToken);

        return product.Id;
    }
}
