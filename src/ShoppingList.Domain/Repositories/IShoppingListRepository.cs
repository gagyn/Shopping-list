using ShoppingList.Domain.ShoppingList;

namespace ShoppingList.Domain.Repositories;
public interface IShoppingListRepository : IRepository
{
    void Add(ShoppingListEntity entity);
    Task<ShoppingListEntity> FindOrThrow(int id, Guid userId, CancellationToken cancellationToken = default);
    Task Remove(int id, Guid userId, CancellationToken cancellationToken = default);
}