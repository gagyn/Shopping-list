using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;

public class UpdateShoppingListCommandHandler(
    IShoppingListRepository shoppingListRepository,
    IUserAccessor userAccessor) : IRequestHandler<UpdateShoppingListCommand>
{
    public async Task Handle(UpdateShoppingListCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = await shoppingListRepository.FindOrThrow(request.Id, userAccessor.Id, cancellationToken);

        shoppingList.UpdateName(request.Name, userAccessor.UserName);

        await shoppingListRepository.SaveChanges(cancellationToken);
    }
}
