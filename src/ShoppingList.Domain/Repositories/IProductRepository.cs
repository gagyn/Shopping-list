using ShoppingList.Domain.ShoppingList;

namespace ShoppingList.Domain.Repositories;
public interface IProductRepository : IRepository
{
    Task<ProductEntity> FindOrThrow(int shoppingListId, int productId, Guid userId, CancellationToken cancellationToken = default);
    Task Remove(int shoppingListId, int productId, Guid userId, CancellationToken cancellationToken = default);
}
