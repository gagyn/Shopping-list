using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;

public class RemoveShoppingListHandler(
    IShoppingListRepository shoppingListRepository,
    IUserAccessor userAccessor) : IRequestHandler<RemoveShoppingList>
{
    public async Task Handle(RemoveShoppingList request, CancellationToken cancellationToken)
    {
        await shoppingListRepository.Remove(request.ShoppingListId, userAccessor.Id, cancellationToken);
        await shoppingListRepository.SaveChanges(cancellationToken);
    }
}