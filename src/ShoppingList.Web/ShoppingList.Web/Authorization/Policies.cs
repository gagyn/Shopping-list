using Microsoft.AspNetCore.Authorization;
using ShoppingList.Domain.User;

namespace ShoppingList.Web.Authorization;

public static class Policies
{
    public const string BasicUser = nameof(BasicUser);
    public const string Administrator = nameof(Administrator);

    public static AuthorizationOptions AddPolicies(this AuthorizationOptions options) => options
        .AddRoles(BasicUser, UserRoleEntity.BasicUser, UserRoleEntity.Administrator)
        .AddRoles(Administrator, UserRoleEntity.Administrator);

    private static AuthorizationOptions AddRoles(this AuthorizationOptions options, string policyName, params UserRoleEntity[] userRoles)
    {
        var roleNames = userRoles.Select(x => x.ToString()).ToArray();
        options.AddPolicy(policyName, x => x.RequireRole(roleNames));
        return options;
    }
}
