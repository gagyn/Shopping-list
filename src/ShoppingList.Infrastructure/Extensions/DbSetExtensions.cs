using Microsoft.EntityFrameworkCore;
using ShoppingList.Domain.Base;
using ShoppingList.DTO.Exceptions;

namespace ShoppingList.Infrastructure.Extensions;
public static class DbSetExtensions
{
    public static async Task<T> FindOrThrowAsync<T>(this DbSet<T> dbSet, object id, CancellationToken cancellationToken) where T : BaseEntity
        => await dbSet.FindAsync([id], cancellationToken: cancellationToken) ?? throw new EntityNotFoundException(typeof(T).Name);
}
