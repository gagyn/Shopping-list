using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DTO.Commands;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database;
using ShoppingList.Infrastructure.Extensions;

namespace ShoppingList.Infrastructure.CommandHandlers
{
    public class UpdateProductCommandHandler(
        ShoppingListContext dbContext,
        IUserAccessor userAccessor) : IRequestHandler<UpdateProductCommand>
    {
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var shoppingList = await dbContext.ShoppingLists
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == request.ShoppingListId && x.OwnedByUserId == userAccessor.Id, cancellationToken)
                ?? throw new Exception("Shopping list not found.");

            var product = shoppingList.Products.FirstOrDefault(x => x.Id == request.ProductId)
                ?? throw new Exception("Product not found.");
            product.Update(request.Name, request.Description, request.Amount, request.IsCompleted);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

