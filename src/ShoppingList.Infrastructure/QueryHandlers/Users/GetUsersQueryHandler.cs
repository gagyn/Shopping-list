using MediatR;
using Microsoft.EntityFrameworkCore;
using ShoppingList.DTO.Extensions;
using ShoppingList.DTO.Models.Users;
using ShoppingList.DTO.Queries.Users;
using ShoppingList.Infrastructure.Database;

namespace ShoppingList.Infrastructure.QueryHandlers.Users;
public class GetUsersQueryHandler(
    ShoppingListContext dbContext) : IRequestHandler<GetUsersQuery, IReadOnlyCollection<UserDetails>>
{
    public async Task<IReadOnlyCollection<UserDetails>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = from user in dbContext.Users
                    join userRole in dbContext.UserRoles on user.Id equals userRole.UserId
                    join role in dbContext.Roles on userRole.RoleId equals role.Id
                    select new UserDetails(user.Id, user.Email, role.Name!.Parse<UserRole>(), user.LockoutEnabled);

        return await query.ToListAsync(cancellationToken);
    }
}
