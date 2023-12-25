using MediatR;
using ShoppingList.Domain.Repositories;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;
public class CreateShoppingListCommandHandler(
    IShoppingListRepository shoppingListRepository,
    IUserAccessor userAccessor) : IRequestHandler<CreateShoppingListCommand, int>
{
    public async Task<int> Handle(CreateShoppingListCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = ShoppingListEntity.Create(request.Name, userAccessor.Id, userAccessor.UserName);

        shoppingListRepository.Add(shoppingList);

        await shoppingListRepository.SaveChanges(cancellationToken);

        return shoppingList.Id;
    }
}