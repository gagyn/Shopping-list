using ShoppingList.Domain.Repositories;
using ShoppingList.Infrastructure.Database;

namespace ShoppingList.Infrastructure.Repositories;
public abstract class RepositoryBase : IRepository
{
    protected readonly ShoppingListContext dbContext;

    protected RepositoryBase(ShoppingListContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public Task SaveChanges(CancellationToken cancellationToken)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
