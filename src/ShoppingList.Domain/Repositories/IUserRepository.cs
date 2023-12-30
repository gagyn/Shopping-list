using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Domain.Repositories;

public interface IUserRepository : IRepository
{
    Task<ApplicationUserEntity> FindOrThrow(Guid id, CancellationToken cancellationToken = default);
}