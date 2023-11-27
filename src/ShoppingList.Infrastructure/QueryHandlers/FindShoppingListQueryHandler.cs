using MediatR;
using ShoppingList.DTO.Models;
using ShoppingList.DTO.Queries;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database.Configurations;
using ShoppingList.Infrastructure.Extensions;

namespace ShoppingList.Infrastructure.QueryHandlers;

public class FindShoppingListQueryHandler(
    ShoppingListContext dbContext,
    IUserAccessor userAccessor) : IRequestHandler<FindShoppingListQuery, ShoppingListDetails>
{
    public async Task<ShoppingListDetails> Handle(FindShoppingListQuery request, CancellationToken cancellationToken)
    {
        var shoppingList = await dbContext.ShoppingLists.FindOrThrowAsync(request.Id, cancellationToken);
        if (shoppingList == null || shoppingList.OwnedByUserId != userAccessor.Id)
        {
            throw new UnauthorizedAccessException();
        }

        var products = shoppingList.Products.Select(x => new Product(x.Id, x.ProductName, x.ProductDescription, x.Amount, x.Completed));
        return new(shoppingList.Id, shoppingList.Name, products.ToList());
    }
}
