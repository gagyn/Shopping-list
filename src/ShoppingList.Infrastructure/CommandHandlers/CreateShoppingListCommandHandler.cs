using MediatR;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database.Configurations;

namespace ShoppingList.Infrastructure.CommandHandlers;
public class CreateShoppingListCommandHandler(
    ShoppingListContext dbContext,
    IUserAccessor userAccessor) : IRequestHandler<CreateShoppingListCommand, int>
{
    public async Task<int> Handle(CreateShoppingListCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = ShoppingListEntity.Create(request.Name, userAccessor.Id, userAccessor.UserName);

        dbContext.ShoppingLists.Add(shoppingList);

        await dbContext.SaveChangesAsync(cancellationToken);

        return shoppingList.Id;
    }
}
