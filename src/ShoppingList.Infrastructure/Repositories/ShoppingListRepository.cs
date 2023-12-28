using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Exceptions;
using ShoppingList.Domain.Repositories;
using ShoppingList.Domain.ShoppingList;
using ShoppingList.Infrastructure.Database;

namespace ShoppingList.Infrastructure.Repositories;
public class ShoppingListRepository(ShoppingListContext dbContext) : RepositoryBase(dbContext), IShoppingListRepository
{
    public void Add(ShoppingListEntity entity)
    {
        dbContext.ShoppingLists.Add(entity);
    }

    public async Task<ShoppingListEntity> FindOrThrow(int id, Guid userId, CancellationToken cancellationToken)
    {
        var shoppingList = await dbContext.ShoppingLists.FirstOrDefaultAsync(x => x.Id == id && x.OwnedByUserId == userId, cancellationToken);
        return shoppingList ?? throw new EntityNotFoundException<ShoppingListEntity>();
    }

    public async Task Remove(int id, Guid userId, CancellationToken cancellationToken)
    {
        var shoppingList = await FindOrThrow(id, userId, cancellationToken);
        dbContext.ShoppingLists.Remove(shoppingList);
    }
}
