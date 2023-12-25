namespace ShoppingList.Domain.Repositories;
public interface IRepository
{
    Task SaveChanges(CancellationToken cancellationToken = default);
}
