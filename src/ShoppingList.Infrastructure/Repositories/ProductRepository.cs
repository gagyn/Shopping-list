using ShoppingList.Domain.Exceptions;
using ShoppingList.Domain.Repositories;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.Infrastructure.Database;

namespace ShoppingList.Infrastructure.Repositories;
public class ProductRepository(IShoppingListRepository shoppingListRepository, ShoppingListContext dbContext) : RepositoryBase(dbContext), IProductRepository
{
    public async Task<ProductEntity> FindOrThrow(int shoppingListId, int productId, Guid userId, CancellationToken cancellationToken)
    {
        var shoppingList = await shoppingListRepository.FindOrThrow(shoppingListId, userId, cancellationToken);
        return shoppingList.Products.FirstOrDefault(x => x.Id == productId)
            ?? throw new EntityNotFoundException<ProductEntity>();
    }
}
