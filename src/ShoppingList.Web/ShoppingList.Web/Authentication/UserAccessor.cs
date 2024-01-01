using Microsoft.AspNetCore.Components.Authorization;
using ShoppingList.Domain.User;
using ShoppingList.DTO.Exceptions;
using ShoppingList.Infrastructure.Authentication;
using System.Security.Claims;

namespace ShoppingList.Web.Authentication;

public class UserAccessor(AuthenticationStateProvider authProvider) : IUserAccessor
{
    private readonly AuthenticationStateProvider authProvider = authProvider ?? throw new ArgumentNullException(nameof(authProvider));

    public ClaimsPrincipal User => authProvider.GetAuthenticationStateAsync().GetAwaiter().GetResult().User ?? throw new UnauthorizeException();

    public string UserName => User.FindFirst(ClaimTypes.Email)?.Value ?? throw new UnauthorizeException();
    public Guid Id => Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizeException());
    public bool IsAuthenticated => User.Identity?.IsAuthenticated ?? false;
    public bool IsAdministrator => User.IsInRole(UserRoleEntity.Administrator.ToString());
}