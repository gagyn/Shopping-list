using MediatR;
using Microsoft.AspNetCore.Identity;
using ShoppingList.DTO.Models.Users;
using ShoppingList.DTO.Queries.Users;
using ShoppingList.Infrastructure.Authentication;
using ShoppingList.Infrastructure.Database;
using ShoppingList.Infrastructure.Extensions;

namespace ShoppingList.Infrastructure.QueryHandlers.Users;

public class FindUserQueryHandler(
    ShoppingListContext dbContext,
    UserManager<ApplicationUserEntity> userManager) : IRequestHandler<FindUserQuery, UserDetails>
{
    public async Task<UserDetails> Handle(FindUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.Id.ToString());
        var role = (await userManager.GetRolesAsync(user)).First();
        return new(user.Id, user.Email, role.Parse<UserRole>(), user.LockoutEnabled);
    }
}
