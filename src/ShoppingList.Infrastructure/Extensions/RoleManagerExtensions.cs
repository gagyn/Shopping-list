using Microsoft.AspNetCore.Identity;
using ShoppingList.Domain.Exceptions;
using ShoppingList.Domain.User;

namespace ShoppingList.Infrastructure.Extensions;
public static class RoleManagerExtensions
{
    public static async Task<IdentityRole<Guid>> FindByNameAsync(this RoleManager<IdentityRole<Guid>> roleManager, UserRoleEntity userRole)
    {
        return await roleManager.FindByNameAsync(userRole.ToString())
            ?? throw new EntityNotFoundException<IdentityRole<Guid>>();
    }
}
public static class UserManagerExtensions
{

}
