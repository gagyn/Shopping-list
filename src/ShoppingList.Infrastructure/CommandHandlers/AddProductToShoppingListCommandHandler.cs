using MediatR;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database.Configurations;
using ShoppingList.Infrastructure.Extensions;

namespace ShoppingList.Infrastructure.CommandHandlers;
public class AddProductToShoppingListCommandHandler(
    ShoppingListContext dbContext,
    IUserAccessor userAccessor) : IRequestHandler<AddProductToShoppingListCommand>
{
    public async Task Handle(AddProductToShoppingListCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = await dbContext.ShoppingLists.FindOrThrowAsync(request.ShoppingListId, cancellationToken);

        shoppingList.AddProduct(request.Name, request.Description, request.Amount, userAccessor.UserName);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
