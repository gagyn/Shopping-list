using Microsoft.AspNetCore.Identity;
using ShoppingList.Infrastructure.Authentication;

namespace ShoppingList.Web.Components.Account;
internal sealed class IdentityUserAccessor(UserManager<ApplicationUserEntity> userManager, IdentityRedirectManager redirectManager)
{
    public async Task<ApplicationUserEntity> GetRequiredUserAsync(HttpContext context)
    {
        var user = await userManager.GetUserAsync(context.User);

        if (user is null)
        {
            redirectManager.RedirectToWithStatus("Account/InvalidUser", $"Error: Unable to load user with ID '{userManager.GetUserId(context.User)}'.", context);
        }

        return user;
    }
}
