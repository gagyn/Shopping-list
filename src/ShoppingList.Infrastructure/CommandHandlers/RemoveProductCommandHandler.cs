using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;
public class RemoveProductCommandHandler(
    IShoppingListRepository shoppingListRepository,
    IUserAccessor userAccessor) : IRequestHandler<RemoveProductCommand>
{
    public async Task Handle(RemoveProductCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = await shoppingListRepository.FindOrThrow(request.ShoppingListId, userAccessor.Id, cancellationToken);

        shoppingList.RemoveProduct(request.ProductId, userAccessor.UserName);

        await shoppingListRepository.SaveChanges(cancellationToken);
    }
}