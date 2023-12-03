using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database;

namespace ShoppingList.Infrastructure.CommandHandlers;

public class UpdateShoppingListCommandHandler(
    ShoppingListContext dbContext,
    IUserAccessor userAccessor) : IRequestHandler<UpdateShoppingListCommand>
{
    public async Task Handle(UpdateShoppingListCommand request, CancellationToken cancellationToken)
    {
        var shoppingList = await dbContext.ShoppingLists.FirstOrDefaultAsync(x => x.Id == request.Id && x.OwnedByUserId == userAccessor.Id)
            ?? throw new Exception("Shopping list not found.");

        shoppingList.UpdateName(request.Name, userAccessor.UserName);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
