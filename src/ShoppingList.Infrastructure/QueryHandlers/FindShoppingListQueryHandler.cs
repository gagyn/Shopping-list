using MediatR;
using ShoppingList.Domain.Exceptions;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.DTO.Models;
using ShoppingList.DTO.Queries;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database;
using ShoppingList.Infrastructure.Extensions;

namespace ShoppingList.Infrastructure.QueryHandlers;

public class FindShoppingListQueryHandler(
    ShoppingListContext dbContext,
    IUserAccessor userAccessor) : IRequestHandler<FindShoppingListQuery, ShoppingListDetails>
{
    public async Task<ShoppingListDetails> Handle(FindShoppingListQuery request, CancellationToken cancellationToken)
    {
        var shoppingList = await dbContext.ShoppingLists.FindOrThrowAsync(request.Id, cancellationToken);
        if (shoppingList.OwnedByUserId != userAccessor.Id)
        {
            throw new EntityNotFoundException<ShoppingListEntity>();
        }

        var products = shoppingList.Products?.Select(x => new ProductDetails(x.Id, x.ProductName, x.ProductDescription, x.Amount, x.Completed));
        return new(shoppingList.Id, shoppingList.Name, products?.ToList() ?? []);
    }
}
