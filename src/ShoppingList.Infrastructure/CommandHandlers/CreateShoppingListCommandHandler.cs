using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using ShoppingList.Domain.Repositories;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Infrastructure.CommandHandlers;
public class CreateShoppingListCommandHandler(
    IShoppingListRepository shoppingListRepository,
    IUserAccessor userAccessor,
    AuthenticationStateProvider authProvider) : IRequestHandler<CreateShoppingListCommand, int>
{
    public async Task<int> Handle(CreateShoppingListCommand request, CancellationToken cancellationToken)
    {
        var state = await authProvider.GetAuthenticationStateAsync();
        var shoppingList = ShoppingListEntity.Create(request.Name, state.GetUserId(), state.GetUserName());

        shoppingListRepository.Add(shoppingList);

        await shoppingListRepository.SaveChanges(cancellationToken);

        return shoppingList.Id;
    }
}