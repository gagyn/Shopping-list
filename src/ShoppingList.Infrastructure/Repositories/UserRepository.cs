using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Exceptions;
using ShoppingList.Domain.Repositories;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database;

namespace ShoppingList.Infrastructure.Repositories;

public class UserRepository(ShoppingListContext dbContext) : RepositoryBase(dbContext), IUserRepository
{
    public async Task<ApplicationUserEntity> FindOrThrow(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await dbContext.Users.Include(x => x.FavoriteRecipes).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return user ?? throw new EntityNotFoundException<ApplicationUserEntity>();
    }
}